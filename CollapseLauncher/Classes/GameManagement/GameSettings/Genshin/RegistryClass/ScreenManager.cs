﻿using CollapseLauncher.GameSettings.Base;
using CollapseLauncher.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Hi3Helper;
using Hi3Helper.Screen;
using Microsoft.Win32;
using SharpCompress.Common;
using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static CollapseLauncher.GameSettings.Statics;
using static Hi3Helper.Logger;

namespace CollapseLauncher.GameSettings.Genshin
{
    internal class ScreenManager : BaseScreenSettingData, IGameSettingsValue<ScreenManager>
    {
        #region Fields
        private const string _ValueNameScreenManagerWidth = "Screenmanager Resolution Width_h182942802";
        private const string _ValueNameScreenManagerHeight = "Screenmanager Resolution Height_h2627697771";
        private const string _ValueNameScreenManagerFullscreen = "Screenmanager Is Fullscreen mode_h3981298716";
        private static Size currentRes = ScreenProp.currentResolution;
        #endregion

        #region Properties
        /// <summary>
        /// This value references values from inner width and height.<br/><br/>
        /// Range: 64 x 64 - int.MaxValue x int.MaxValue<br/>
        /// Default: Your screen resolution
        /// </summary>
        public override Size sizeRes
        {
            get => new Size(width, height);
            set
            {
                width = value.Width < 64 ? currentRes.Width : value.Width;
                height = value.Height < 64 ? currentRes.Height : value.Height;
            }
        }

        /// <summary>
        /// This value references values from inner width and height as string.<br/><br/>
        /// Range: 64 x 64 - int.MaxValue x int.MaxValue<br/>
        /// Default: Your screen resolution
        /// </summary>
        public override string sizeResString
        {
            get => $"{width}x{height}";
            set
            {
                string[] size = value.Split('x');
                int w;
                int h;
                if (!int.TryParse(size[0], out w) || !int.TryParse(size[1], out h))
                {
                    width = currentRes.Width;
                    height = currentRes.Height;
                }
                else
                {
                    width = w;
                    height = h;
                }
            }
        }

        /// <summary>
        /// This defines "<c>Game Resolution</c>'s Width" combobox In-game settings -> Video.<br/><br/>
        /// Range: 0 - int.MaxValue<br/>
        /// Default: Your screen width
        /// </summary>
        public override int width { get; set; } = currentRes.Width;

        /// <summary>
        /// This defines "<c>Game Resolution</c>'s Height" combobox In-game settings -> Video.<br/><br/>
        /// Range: 0 - int.MaxValue<br/>
        /// Default: Your screen Height
        /// </summary>
        public override int height { get; set; } = currentRes.Height;

        /// <summary>
        /// This defines "<c>Fullscreen</c>" checkbox In-game settings -> Video.<br/><br/>
        /// Range: 0 - 1
        /// Default: 1
        /// </summary>
        public int fullscreen { get; set; } = 1;
        #endregion
#nullable enable
        #region Methods
        public static ScreenManager Load()
        {
            try
            {
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot load {_ValueNameScreenManagerWidth} RegistryKey is unexpectedly not initialized!");
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot load {_ValueNameScreenManagerHeight} RegistryKey is unexpectedly not initialized!");
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot load {_ValueNameScreenManagerFullscreen} RegistryKey is unexpectedly not initialized!");

                object? valueWidth = RegistryRoot.GetValue(_ValueNameScreenManagerWidth, null);
                object? valueHeight = RegistryRoot.GetValue(_ValueNameScreenManagerHeight, null);
                object? valueFullscreen = RegistryRoot.GetValue(_ValueNameScreenManagerFullscreen, null);
                if (valueWidth != null && valueHeight != null && valueFullscreen != null)
                {
                    int width = (int)valueWidth;
                    int height = (int)valueHeight;
                    int fullscreen = (int)valueFullscreen;
                    LogWriteLine($"Loaded Genshin Settings: {_ValueNameScreenManagerWidth} : {width}", LogType.Default, true);
                    LogWriteLine($"Loaded Genshin Settings: {_ValueNameScreenManagerHeight} : {height}", LogType.Default, true);
                    LogWriteLine($"Loaded Genshin Settings: {_ValueNameScreenManagerFullscreen} : {fullscreen}", LogType.Default, true);
                    return new ScreenManager();
                }
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed while reading Genshin Impact ScreenManager Values\r\n{ex}", LogType.Error, true);
            }

            return new ScreenManager();
        }

        public override void Save()
        {
            try
            {
                RegistryRoot?.SetValue(_ValueNameScreenManagerFullscreen, fullscreen, RegistryValueKind.DWord);
                LogWriteLine($"Saved Genshin Settings: {_ValueNameScreenManagerFullscreen} : {RegistryRoot.GetValue(_ValueNameScreenManagerFullscreen, null)}", LogType.Default, true);

                RegistryRoot?.SetValue(_ValueNameScreenManagerWidth, width, RegistryValueKind.DWord);
                LogWriteLine($"Saved Genshin Settings: {_ValueNameScreenManagerWidth} : {RegistryRoot.GetValue(_ValueNameScreenManagerWidth, null)}", LogType.Default, true);

                RegistryRoot?.SetValue(_ValueNameScreenManagerHeight, height, RegistryValueKind.DWord);
                LogWriteLine($"Saved Genshin Settings: {_ValueNameScreenManagerHeight} : {RegistryRoot.GetValue(_ValueNameScreenManagerHeight, null)}", LogType.Default, true);
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed to save Genshin Impact ScreenManager Values!\r\n{ex}", LogType.Error, true);
            }
        }

        public bool Equals(ScreenManager? comparedTo)
        {
            if (ReferenceEquals(this, comparedTo)) return true;
            if (comparedTo == null) return false;

            return comparedTo.sizeRes == this.sizeRes &&
                comparedTo.height == this.height &&
                comparedTo.width == this.width &&
                comparedTo.fullscreen == this.fullscreen;
        }
        #endregion
#nullable disable

    }
}