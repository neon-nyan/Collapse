﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;

using Hi3Helper.Data;

using static Hi3Helper.Logger;
using static CollapseLauncher.LauncherConfig;
using static CollapseLauncher.Region.InstallationManagement;

namespace CollapseLauncher.Dialogs
{
    public static class SimpleDialogs
    {
        public static async Task<ContentDialogResult> Dialog_ExistingInstallation(UIElement Content) =>
            await SpawnDialog(
                    "Existing Installation is Detected!",
                    string.Format(
                        "Game is already been installed on this location:\r\n\r\n{0}\r\n\r\n"
                        + "It's recommended to migrate the game to CollapseLauncher.\r\nHowever, you can still use Official Launcher to start the game."
                        + "\r\n\r\nDo you want to continue?", CurrentRegion.ActualGameDataLocation),
                    Content,
                    "Cancel",
                    "Yes, Migrate it",
                    "No, Keep Install it"
            );

        public static async Task<ContentDialogResult> Dialog_GameInstallationFileCorrupt(UIElement Content, string sourceHash, string downloadedHash) =>
            await SpawnDialog(
                    "Oops! Game Installation is Corrupted",
                    string.Format(
                        "Sorry but seems the downloaded Game Installation file."
                       + "\r\n\r\nServer Hash: {0}\r\nDownloaded Hash: {1}"
                       + "\r\n\r\nDo you want to rerdownload the file?", sourceHash, downloadedHash),
                    Content,
                    "No, Cancel",
                    "Yes, Redownload",
                    null
            );

        public static async Task<ContentDialogResult> Dialog_ExistingDownload(UIElement Content, long partialLength, long contentLength) =>
            await SpawnDialog(
                    "Resume Download?",
                    string.Format("You have downloaded {0}/{1} of the game previously.\r\n\r\nDo you want to continue?",
                                  ConverterTool.SummarizeSizeSimple(partialLength),
                                  ConverterTool.SummarizeSizeSimple(contentLength)),
                    Content,
                    null,
                    "Yes, Resume",
                    "No, Start from Beginning"
                );

        public static async Task<ContentDialogResult> SpawnDialog(
            string title, string content, UIElement Content,
            string closeText = null, string primaryText = null,
            string secondaryText = null) =>
            await new ContentDialog
            {
                Title = title,
                Content = content,
                CloseButtonText = closeText,
                PrimaryButtonText = primaryText,
                SecondaryButtonText = secondaryText,
                DefaultButton = ContentDialogButton.Primary,
                Background = (Brush)Application.Current.Resources["DialogAcrylicBrush"],
                XamlRoot = Content.XamlRoot
            }.ShowAsync();
    }
}
