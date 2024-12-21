﻿using CollapseLauncher.CustomControls;
using CollapseLauncher.Dialogs;
using CollapseLauncher.Extension;
using CollapseLauncher.Helper;
using CollapseLauncher.Helper.Loading;
using CollapseLauncher.InstallManager.Base;
using CommunityToolkit.WinUI;
using Hi3Helper;
using Hi3Helper.Data;
using Hi3Helper.Win32.Native.ManagedTools;
using Microsoft.UI.Text;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

// ReSharper disable CheckNamespace
// ReSharper disable IdentifierTypo

#nullable enable
namespace CollapseLauncher.Pages
{
    public sealed partial class FileCleanupPage
    {
        internal static FileCleanupPage?                    Current { get; set; }
        internal        ObservableCollection<LocalFileInfo> FileInfoSource;

    #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public FileCleanupPage()
        {
            FileInfoSource = new ObservableCollection<LocalFileInfo>();

            InitializeComponent();
            Current = this;
            Loaded += (_, _) =>
                      {
                          FileInfoSource.CollectionChanged += UpdateUIOnCollectionChange;
                      };
        }
    #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

        private int    _selectedAssetsCount;
        private long   _assetTotalSize;
        private string _assetTotalSizeString = string.Empty;
        private long   _assetSelectedSize;

        public async Task InjectFileInfoSource(IEnumerable<LocalFileInfo> fileInfoList)
        {
            var s = new Stopwatch();
            s.Start();
            
            FileInfoSource.Clear();
            List<LocalFileInfo> backedFileInfoSourceList = ObservableCollectionExtension<LocalFileInfo>
                .GetBackedCollectionList(FileInfoSource) as List<LocalFileInfo> ?? throw new InvalidCastException();

            _assetTotalSize = 0;

            List<LocalFileInfo> localFileCollection = [..fileInfoList];
            int batchSize = Math.Clamp(localFileCollection.Count / Environment.ProcessorCount, 50, 5000);
            
            IEnumerable<List<LocalFileInfo>> batches = localFileCollection
                                                      .Select((file, index) => new { File = file, Index = index })
                                                      .GroupBy(x => x.Index / batchSize)
                                                      .Select(group => group.Select(x => x.File).ToList());
            
            List<Task> tasks = [];
            Logger.LogWriteLine($"[FileCleanupPage::InjectFileInfoSource] " +
                                $"Starting to inject file info source with {localFileCollection.Count} items",
                                LogType.Scheme);

            int b = 0;
            await Task.Run(() =>
                           {
                               tasks.AddRange(batches.Select(batch => EnqueueOnDispatcherQueueAsync(() =>
                                                                                                    {
                                                                                                        var sI = new Stopwatch();
                                                                                                        s.Start();
                                                                                                        foreach (var fileInfoInner in batch)
                                                                                                        {
                                                                                                            FileInfoSource.Add(fileInfoInner);
                                                                                                            localFileCollection.Remove(fileInfoInner);
                                                                                                        }

                                                                                                        sI.Stop();
                                                                                                        Logger.LogWriteLine($"[FileCleanupPage::InjectFileInfoSource] " + $"Finished batch #{b} with {batch.Count} items after {sI.ElapsedMilliseconds} ms", LogType.Scheme);
                                                                                                        Interlocked.Increment(ref b);
                                                                                                    })));
                           });
            await Task.WhenAll(tasks);

            if (localFileCollection.Count > 0)
            {
                await EnqueueOnDispatcherQueueAsync(() =>
                {
                    var i  = 0;
                    var sI = new Stopwatch();
                    sI.Start();
                    while (localFileCollection.Count > 0)
                    {
                        backedFileInfoSourceList.Add(localFileCollection[0]);
                        localFileCollection.RemoveAt(0);
                        i++;
                    }

                    sI.Stop();
                    Logger
                    .LogWriteLine($"[FileCleanupPage::InjectFileInfoSource] " +
                                  $"Finished last batch at #{b} after {i} items in {sI.ElapsedMilliseconds} ms",
                                    LogType.Scheme);
                });
            }

            while (localFileCollection.Count != 0)
            {
                backedFileInfoSourceList.Add(localFileCollection[0]);
                localFileCollection.RemoveAt(0);
            }

            ObservableCollectionExtension<LocalFileInfo>.RefreshAllEvents(FileInfoSource);

            await Task.Run(() => _assetTotalSizeString = ConverterTool.SummarizeSizeSimple(_assetTotalSize));
            await DispatcherQueue.EnqueueAsync(() => UpdateUIOnCollectionChange(FileInfoSource, null));
            s.Stop();
            Logger.LogWriteLine($"InjectFileInfoSource done after {s.ElapsedMilliseconds} ms", LogType.Scheme);
            await CheckAll();
        }

        private void UpdateUIOnCollectionChange(object? sender, NotifyCollectionChangedEventArgs? args)
        {
            ObservableCollection<LocalFileInfo>? obj        = (ObservableCollection<LocalFileInfo>?)sender;
            int                                  count      = obj?.Count ?? 0;
            bool                                 isHasValue = count > 0;
            ListViewTable.Opacity   = isHasValue ? 1 : 0;
            NoFilesTextGrid.Opacity = isHasValue ? 0 : 1;

            ToggleCheckAllCheckBox.IsEnabled = isHasValue;
            DeleteAllFiles.IsEnabled         = isHasValue;
            DeleteSelectedFiles.IsEnabled    = isHasValue && _selectedAssetsCount > 0;

            ToggleCheckAllCheckBox.Visibility = isHasValue ? Visibility.Visible : Visibility.Collapsed;
            DeleteSelectedFiles.Visibility    = isHasValue ? Visibility.Visible : Visibility.Collapsed;
            DeleteAllFiles.Visibility         = isHasValue ? Visibility.Visible : Visibility.Collapsed;
        }


        private async void ToggleCheckAll(object sender, RoutedEventArgs e)
        {
            var s = new Stopwatch();
            if (ListViewTable.Items.Count > 1000)
            {
                LoadingMessageHelper.Initialize();
                LoadingMessageHelper.ShowLoadingFrame();
                LoadingMessageHelper.SetMessage(Locale.Lang._FileCleanupPage.LoadingTitle,
                                                Locale.Lang._FileCleanupPage.LoadingSubtitle3);
            }
            
            await Task.Delay(100);
            s.Start();
            bool toCheckCopy = false;
            if (sender is CheckBox checkBox)
            {
                bool toCheck = checkBox.IsChecked ?? false;
                await ToggleCheckAllInnerAsync(toCheck);
                toCheckCopy = toCheck;
            } 
            s.Stop();
            LoadingMessageHelper.HideLoadingFrame();
            Logger.LogWriteLine($"[FileCleanupPage::ToggleCheckAll({toCheckCopy})] Elapsed time: {s.ElapsedMilliseconds} ms", LogType.Scheme);
        }

        private async Task CheckAll()
        {
            await ToggleCheckAllInnerAsync(true);
        }

        private async Task ToggleCheckAllInnerAsync(bool selectAll)
        {
            if (selectAll)
            {
                await EnqueueOnDispatcherQueueAsync(ListViewTable.SelectAllSafe);
            }
            else
            {
                await EnqueueOnDispatcherQueueAsync(ListViewTable.DeselectAll);
            }
        }

        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var removedItems = e.RemovedItems.OfType<LocalFileInfo>().ToList();
            var addedItems   = e.AddedItems.OfType<LocalFileInfo>().ToList();
            
            var removedSizeTask = Task.Run(() => removedItems.Count == 0 ? 0 : removedItems.Count < 512
                                               ? removedItems.Sum(x => x.FileSize)
                                               : removedItems.Select(x => x.FileSize).ToArray().Sum());

            var addedSizeTask = Task.Run(() => addedItems.Count == 0 ? 0 : addedItems.Count < 512
                                             ? addedItems.Sum(x => x.FileSize)
                                             : addedItems.Select(x => x.FileSize).ToArray().Sum());

            var results     = await Task.WhenAll(removedSizeTask, addedSizeTask);
            var removedSize = results[0];
            var addedSize   = results[1];

            _selectedAssetsCount += addedItems.Count - removedItems.Count;
            _assetSelectedSize   += addedSize - removedSize;

            await EnqueueOnDispatcherQueueAsync(() =>
            {
                if (_selectedAssetsCount > 0)
                {
                    ToggleCheckAllCheckBox.Content = string.Format(
                                                                   Locale.Lang._FileCleanupPage.BottomCheckboxFilesSelected,
                                                                    _selectedAssetsCount,
                                                                    ConverterTool.SummarizeSizeSimple(_assetSelectedSize),
                                                                    _assetTotalSizeString);
                }
                else
                {
                    ToggleCheckAllCheckBox.Content = Locale.Lang._FileCleanupPage.BottomCheckboxNoFileSelected;
                }

                DeleteSelectedFilesText.Text =
                    string.Format(Locale.Lang._FileCleanupPage.BottomButtonDeleteSelectedFiles, _selectedAssetsCount);
                DeleteSelectedFiles.IsEnabled = _selectedAssetsCount > 0;

                ToggleCheckAllCheckBox.IsChecked = _selectedAssetsCount == 0
                    ? false
                    : _selectedAssetsCount == FileInfoSource.Count ? true : null;
            });
        }

        private Task EnqueueOnDispatcherQueueAsync(Action action)
        {
            var tcs = new TaskCompletionSource();
            DispatcherQueue.TryEnqueue(() =>
                                       {
                                           try
                                           {
                                               action();
                                               tcs.SetResult();
                                           }
                                           catch (Exception ex)
                                           {
                                               tcs.SetException(ex);
                                           }
                                       });
            return tcs.Task;
        }

        private async void DeleteAllFiles_Click(object sender, RoutedEventArgs e)
        {
            List<LocalFileInfo> fileInfoList = FileInfoSource
               .ToList();
            long size = fileInfoList.Sum(x => x.FileSize);
            await PerformRemoval(fileInfoList, size);
        }

        private async void DeleteSelectedFiles_Click(object sender, RoutedEventArgs e)
        {
            List<LocalFileInfo> fileInfoList = ListViewTable.SelectedItems
                                                             .OfType<LocalFileInfo>()
                                                             .ToList();
            long size = fileInfoList.Sum(x => x.FileSize);
            await PerformRemoval(fileInfoList, size);
        }

        private async Task PerformRemoval(List<LocalFileInfo>? deletionSource, long totalSize)
        {
            if (deletionSource == null)
            {
                return;
            }

            TextBlock textBlockMsg = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    TextWrapping  = TextWrapping.WrapWholeWords
                }.AddTextBlockLine(Locale.Lang._FileCleanupPage.DialogDeletingFileSubtitle1, true)
                 .AddTextBlockLine(string.Format(Locale.Lang._FileCleanupPage.DialogDeletingFileSubtitle2, deletionSource.Count),
                                   true, FontWeights.Medium)
                 .AddTextBlockLine(Locale.Lang._FileCleanupPage.DialogDeletingFileSubtitle3, true)
                 .AddTextBlockLine(string.Format(Locale.Lang._FileCleanupPage.DialogDeletingFileSubtitle4, ConverterTool.SummarizeSizeSimple(totalSize)),
                                   FontWeights.Medium)
                 .AddTextBlockNewLine()
                 .AddTextBlockLine(Locale.Lang._FileCleanupPage.DialogDeletingFileSubtitle5);

            ContentDialogResult dialogResult = await SimpleDialogs.SpawnDialog(
                                                                               Locale.Lang._FileCleanupPage
                                                                                  .DialogDeletingFileTitle,
                                                                               textBlockMsg,
                                                                               this,
                                                                               Locale.Lang._Misc.NoCancel,
                                                                               Locale.Lang._Misc.YesContinue,
                                                                               Locale.Lang._FileCleanupPage
                                                                                  .DialogMoveToRecycleBin,
                                                                               ContentDialogButton.Close,
                                                                               ContentDialogTheme.Warning);

            int deleteSuccess = 0;
            int deleteFailed  = 0;

            bool isToRecycleBin = dialogResult == ContentDialogResult.Secondary;
            if (dialogResult == ContentDialogResult.None)
            {
                return;
            }

            LoadingMessageHelper.SetMessage(Locale.Lang._FileCleanupPage.LoadingTitle,
                                            Locale.Lang._FileCleanupPage.DeleteSubtitle);
            LoadingMessageHelper.ShowLoadingFrame();

            List<LocalFileInfo> deletedItems = [];
            if (isToRecycleBin)
            {
                // Get the list of the file to be deleted and add it to the deletedItems List if it exists
                List<string> toBeDeleted = await Task.Run(() => deletionSource
                                          .Select(x =>
                                                  {
                                                      var localFileInfo = x.ToFileInfo().EnsureNoReadOnly(out bool isFileExist);
                                                      if (!isFileExist)
                                                      {
                                                          return string.Empty;
                                                      }

                                                      deletedItems.Add(x);
                                                      return localFileInfo.FullName;
                                                  })
                                          .Where(x => !string.IsNullOrEmpty(x))
                                          .ToList()).ConfigureAwait(false);

                // Execute the deletion process
                await Task.Run(() => RecycleBin.MoveFileToRecycleBin(toBeDeleted)).ConfigureAwait(false);
                deleteSuccess = toBeDeleted.Count;
            }
            else
            {
                ConcurrentDictionary<LocalFileInfo, byte> processedFiles = new();

                var options = new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount };
                await Task.Run(() => Parallel.ForEach(deletionSource, options, (fileInfo, _) =>
                {
                    if (!processedFiles.TryAdd(fileInfo, 0))
                        return;

                    try
                    {
                        FileInfo fileInfoN = fileInfo.ToFileInfo().EnsureNoReadOnly(out bool isFileExist);
                        if (isFileExist)
                        {
                            deletedItems.Add(fileInfo);
                            fileInfoN.Delete();
                        }

                        Interlocked.Increment(ref deleteSuccess);
                    }
                    catch (Exception ex)
                    {
                        Interlocked.Increment(ref deleteFailed);
                        Logger.LogWriteLine($"Failed while deleting this file: {fileInfo.FullPath}\r\n{ex}",
                                            LogType.Error, true);
                    }
                })).ConfigureAwait(false);
            }

            // Execute the deleted items removal from the source collection with our own method (which is ridiculously faster).
            // For god sake, MSFT. We hope a better to delete all these items in one go.
            // The current implementation is reaaaaallllyyyy slooowwwwwww.
            await EnqueueOnDispatcherQueueAsync(() =>
                ObservableCollectionExtension<LocalFileInfo>
                    .RemoveItemsFast(deletedItems, FileInfoSource));

            string diagTitle = dialogResult == ContentDialogResult.Primary
                ? Locale.Lang._FileCleanupPage.DialogDeleteSuccessTitle
                : Locale.Lang._FileCleanupPage.DialogTitleMovedToRecycleBin;

            await SimpleDialogs.SpawnDialog(diagTitle,
                                            string.Format(Locale.Lang._FileCleanupPage.DialogDeleteSuccessSubtitle1,
                                                          deleteSuccess)
                                            + (deleteFailed == 0
                                                ? string.Empty
                                                : ' ' +
                                                  string
                                                     .Format(Locale.Lang._FileCleanupPage.DialogDeleteSuccessSubtitle2,
                                                             deleteFailed)),
                                            this,
                                            Locale.Lang._Misc.OkayHappy,
                                            null,
                                            null,
                                            ContentDialogButton.Close,
                                            ContentDialogTheme.Success);
        }
    }
}