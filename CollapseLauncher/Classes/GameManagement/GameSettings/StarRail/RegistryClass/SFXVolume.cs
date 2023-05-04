﻿using CollapseLauncher.GameSettings.Base;
using CollapseLauncher.GameSettings.StarRail;
using CollapseLauncher.GameSettings.StarRail.Context;
using CollapseLauncher.Interfaces;
using Google.Protobuf.WellKnownTypes;
using Hi3Helper;
using Hi3Helper.Screen;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static CollapseLauncher.GameSettings.Statics;
using static Hi3Helper.Logger;

namespace CollapseLauncher.GameSettings.StarRail
{
    internal class SFXVolume : IGameSettingsValue<SFXVolume>
    {
        #region Fields
        private const string _ValueName = "AudioSettings_SFXVolume_h2753520268";
        #endregion

        #region Properties
        /// <summary>
        /// This defines "<c>SFX Volume</c>" slider in-game setting
        /// Range: 0 - 10
        /// Default: 10
        /// </summary>
        public float SFXVol { get; set; } = 10;

        #endregion

        #region Methods
#nullable enable
        public static SFXVolume Load()
        {
            try
            {
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot load {_ValueName} RegistryKey is unexpectedly not initialized!");
                object? value = RegistryRoot.GetValue(_ValueName);
                LogWriteLine($"Loaded {_ValueName} with the value of {value}");
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed while reading {_ValueName}\r\n{ex}", LogType.Error, true);
            }
            return new SFXVolume();
        }

        public void Save()
        {
            try
            {
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot save {_ValueName} since RegistryKey is unexpectedly not initialized!");
                RegistryRoot?.SetValue(_ValueName, SFXVol, RegistryValueKind.DWord);
                LogWriteLine($"Saved {_ValueName} with {SFXVol}", LogType.Default, true);
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed to save {_ValueName}!\r\n{ex}", LogType.Error, true);
            }

        }

        public bool Equals(SFXVolume? comparedTo)
        {
            if (ReferenceEquals(this, comparedTo)) return true;
            if (comparedTo == null) return false;

            return comparedTo.SFXVol == this.SFXVol;
        }
#nullable disable
        #endregion
    }
}
