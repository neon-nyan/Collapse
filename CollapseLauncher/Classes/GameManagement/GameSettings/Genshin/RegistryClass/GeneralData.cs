﻿using CollapseLauncher.GameSettings.Genshin.Context;
using CollapseLauncher.Interfaces;
using Hi3Helper;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using static CollapseLauncher.GameSettings.Statics;
using static Hi3Helper.Logger;

namespace CollapseLauncher.GameSettings.Genshin
{
    internal class GeneralData
    {
        #region Fields
        private const string _ValueName = "GENERAL_DATA_h2389025596";
        #endregion

        #region Properties
        //in need of help,,
        //Using guide from https://github.com/Myp3a/GenshinConfigurator/wiki/Config-format
        //Thanks Myp3a!

        #region Root Level
        /// <summary>
        /// deviceUUID<br/>
        /// This is supposed to be empty
        /// </summary>
        public string deviceUUID { get; set; } = "";

        /// <summary>
        /// userLocalDataVersionId<br/>
        /// This should be static<br/>
        /// Value: 0.0.1
        /// </summary>
        public string userLocalDataVersionId { get; set; } = "0.0.1";

        /// <summary>
        /// This define "<c>Text Language</c>" in-game settings. <br/>
        /// Valid values: 1-13 
        /// Default: 1
        /// TODO: Index this language
        /// EN(1)
        /// </summary>
        public int deviceLanguageType { get; set; } = 1;

        /// <summary>
        /// This define "<c>Voice Language</c>" in-game settings. <br/>
        /// Valid values: 1-4
        /// Default: 1
        /// TODO: Index the language
        /// JP(2)
        /// </summary>
        public int deviceVoiceLanguageType { get; set; } = 1;

        /// <summary>
        /// This define "<c>Server Name</c>" last selected in loading menu. <br/>
        /// </summary>
        public string selectedServerName { get; set; } = "os_usa";

        /// <summary>
        /// I don't know what this do
        /// </summary>
        public int localLevelIndex { get; set; } = 0;
        public string deviceID { get; set; } = "";
        public string targetUID { get; set; } = "";
        public string currentAccountName { get; set; } = "";

        /// <summary>
        /// This define "<c>Resolution</c>" index selected in-game. <br/>
        /// Valid value: 0-?
        /// Default: 0
        /// </summary>
        public string uiSaveData { get; set; }

        /// <summary>
        /// This holds settings for input sens and stuff.<br/>
        /// Please look at https://github.com/Myp3a/GenshinConfigurator/wiki/Config-format#input-data-format
        /// </summary>
        public string inputData { get; set; }

        /// <summary>
        /// This holds settings that holds Graphics Settings settings. YEP!<br/>
        /// Needs to be serialized again with its keys and values in dictionary<br/>
        /// https://github.com/Myp3a/GenshinConfigurator/wiki/Config-format#graphics-data-format
        /// </summary>
        //[JsonPropertyName("graphicsData")]
        //public string _graphicsData { get; set; }

        //[JsonIgnore]
        //public GraphicsData graphicsData { get; set; }

        /// <summary>
        /// This is a dict that keeps track of graphics settings changes.<br/>
        /// Save to ignore (?)
        /// </summary>
        //[JsonPropertyName("globalPerfData")]
        //public string _globalPerfData { get; set; }

        //[JsonIgnore]
        //public globalPerfData globalPerfData { get; set; }

        /// <summary>
        /// Something about minimap config ?
        /// </summary>
        public int miniMapConfig { get; set; } = 1;

        /// <summary>
        /// This  defines "<c>Automatic View Height</c>" in-game settings.<br/>
        /// Default: true
        /// </summary>
        public bool enableCameraSlope { get; set; } = true;

        /// <summary>
        /// This defines "<c>Smart combat camera</c>" whatever that is.<br/>
        /// Default: true
        /// </summary>
        public bool enableCameraCombatLock { get; set; } = true;

        /// <summary>
        /// not sure either what these does.
        /// </summary>
        public bool completionPkg { get; set; } = false;
        public bool completionPkgGoPkg { get; set; } = false;

        /// <summary>
        /// Could be for PlayStation multiplayer stuff
        /// </summary>
        public bool onlyPlayWithPSPlayer { get; set; } = false;

        /// <summary>
        /// Mysterious~
        /// </summary>
        public bool needPlayGoFullPkgPatch { get; set; } = false;

        /// <summary>
        /// Mobile notification stuff
        /// </summary>
        public bool resinNotification { get; set; } = true;
        public bool exploreNotification { get; set; } = true;

        /// <summary>
        /// This define "<c>Global Volume</c>" slider in-game.<br/>
        /// Valid values: 0-10
        /// Default: 10
        /// </summary>
        public int volumeGlobal { get; set; } = 10;

        /// <summary>
        /// This define "<c>SFX Volume</c>" slider in-game.<br/>
        /// Valid values: 0-10
        /// Default: 10
        /// </summary>
        public int volumeSFX { get; set; } = 10;

        /// <summary>
        /// This define "<c>Music Volume</c>" slider in-game.<br/>
        /// Valid values: 0-10
        /// Default: 10
        /// </summary>
        public int volumeMusic { get; set; } = 10;

        /// <summary>
        /// This define "<c>Voice Volume</c>" slider in-game.<br/>
        /// Valid values: 0-10
        /// Default: 10
        /// </summary>
        public int volumeVoice { get; set; } = 10;

        /// <summary>
        /// Probably just leave it alone...
        /// </summary>
        public int audioAPI { get; set; } = -1;

        /// <summary>
        /// This defines "<c>Dynamic Range</c>" audio combo box in-game.
        /// Valid values: 0 (full), 1 (limited)
        /// Default: 0
        /// </summary>
        public int audioDynamicRange { get; set; } = 0;

        /// <summary>
        /// This defines "<c>Audio Output</c>" combo box in-game.
        /// Valid Values: 0 (stereo), 1 (surround)
        /// Default: 0
        /// </summary>
        public int audioOutput { get; set; } = 0;

        /// <summary>
        /// Audio related stuff...
        /// </summary>
        public bool _audioSuccessInit { get; set; } = true;
        public bool enableAudioChangeAndroidMinimumBufferCapacity { get; set; } = true;
        public int audioAndroidMinimumBufferCapacity { get; set; } = 2048;

        /// <summary>
        /// This define vibration level for certain controller probably ?
        /// Valid Values: 0 (Full), 1 (Partial), 2 (Off)
        /// Default: 0
        /// </summary>
        public int vibrationLevel { get; set; } = 0;

        /// <summary>
        /// This defines "<c>Vibration Intensity</c>" slider in-game. <br/>
        /// Valid Values: 1-5
        /// Default: 5
        /// </summary>
        public int vibrationIntensity { get; set; } = 5;

        /// <summary>
        /// Some vibration related stuff ?
        /// </summary>
        public bool usingNewVibrationSetting { get; set; } = true;

        /// <summary>
        /// This define "<c>Motion Blur</c>" settings in-game. <br/>
        /// Default: true
        /// </summary>
        public bool motionBlur { get; set; } = true;

        /// <summary>
        /// Gyro Aiming stuff, most likely not used in PC.
        /// </summary>
        public bool gyroAiming { get; set; } = false;

        //unsure what these does, probably HDR stuff? doesn't have HDR monitor to test...
        public bool firstHDRSetting { get; set; } = true;
        public float maxLuminosity { get; set; } = 0.0f;
        public float uiPaperWhite { get; set; } = 0.0f;
        public float scenePaperWhite { get; set; } = 0.0f;

        /// <summary>
        /// This defines "<c>Gamma</c>" slider in-game. <br/>
        /// </summary>
        public float gammaValue { get; set; } = 2.0f;
        
        /// <summary>
        /// This holds value for controllers input that has been customized.
        /// </summary>
        public List<string> _overrideControllerMapKeyList { get; set; }


        /// <summary>
        /// This holds said override for those controllers.
        /// </summary>
        //[JsonPropertyName("_overrideControllerMapValueList")]
        //public List<string> __overrideControllerMapValueList { get; set; }

        //[JsonIgnore]
        //public Controllers _overrideControllerMapValueList { get; set; }

        //misc values
        //just, idk, ignore?
        public bool rewiredDisableKeyboard { get; set; } = false;
        public bool rewiredEnableKeyboard { get; set; } = false;
        public bool rewiredEnableEDS { get; set; } = false;
        public bool disableRewiredDelayInit { get; set; } = false;
        public bool disableRewiredInitProtection { get; set; } = false;
        public int lastSeenPreDownloadTime { get; set; } = 0;
        public bool enableEffectAssembleInEditor { get; set; } = true;
        public bool forceDisableQuestResourceManagement { get; set; } = false;
        public bool needReportQuestResourceDeleteStatusFiles { get; set; } = false;
        public bool mtrCached { get; set; } = true;
        public bool mtrIsOpen { get; set; } = true;
        public int mtrMaxTTL { get; set; } = 32;
        public int mtrTimeOut { get; set; } = 5000;
        public int mtrTraceCount { get; set; } = 5;
        public int mtrAbortTimeOutCount { get; set; } = 3;
        public int mtrAutoTraceInterval { get; set; } = 3600;
        public int mtrTraceCDEachReason { get; set; } = 600;
        public int mtrTimeInterval { get; set; } = 1000;
        public List<object> mtrBanReason { get; set; }
        public List<string> _customDataKeyList { get; set; }
        public List<string> _customDataValueList { get; set; }
        public List<int> _serializedCodeSwitches { get; set; }
        public bool urlCheckCached { get; set; } = false;
        public bool urlCheckIsOpen { get; set; } = false;
        public bool urlCheckAllIP { get; set; } = false;
        public int urlCheckTimeOut { get; set; } = 5000;
        public int urlCheckSueecssTraceCount { get; set; } = 5;
        public int urlCheckErrorTraceCount { get; set; } = 30;
        public int urlCheckAbortTimeOutCount { get; set; } = 3;
        public int urlCheckTimeInterval { get; set; } = 1000;
        public int urlCheckCDEachReason { get; set; } = 600;
        public List<object> urlCheckBanReasons { get; set; }
        public bool mtrUseOldWinVersion { get; set; } = false;
        public string greyTestDeviceUniqueId { get; set; } = "";
        #endregion


        #endregion

        #region Methods
#nullable enable
        public static GeneralData Load()
        {
            try
            {
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot load {_ValueName} RegistryKey is unexpectedly not initialized!");
                object? value = RegistryRoot.GetValue(_ValueName, null);

                if (value != null)
                {
                    ReadOnlySpan<byte> byteStr = (byte[])value;
                    LogWriteLine($"Loaded Genshin Settings: {_ValueName}\r\n{Encoding.UTF8.GetString((byte[])value, 0, ((byte[])value).Length - 1)}", LogType.Default, true);
                    return (GeneralData?)JsonSerializer.Deserialize(byteStr.Slice(0, byteStr.Length - 1), typeof(GeneralData), GeneralDataContext.Default) ?? new GeneralData();
                }
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed while reading {_ValueName}\r\n{ex}", LogType.Error, true);
            }

            return new GeneralData();
        }

        public void Save()
        {
            try
            {
                if (RegistryRoot == null) throw new NullReferenceException($"Cannot save {_ValueName} since RegistryKey is unexpectedly not initialized!");

                string data = JsonSerializer.Serialize(this, typeof(GeneralData), GeneralDataContext.Default) + '\0';
                byte[] dataByte = Encoding.UTF8.GetBytes(data);
                LogWriteLine($"Saved Genshin Settings: {_ValueName}\r\n{data}", LogType.Default, true);
                RegistryRoot.SetValue(_ValueName, dataByte, RegistryValueKind.Binary);
            }
            catch (Exception ex)
            {
                LogWriteLine($"Failed to save {_ValueName}!\r\n{ex}", LogType.Error, true);
            }
        }

        public bool Equals(GeneralData? comparedTo)
        {
            if (ReferenceEquals(this, comparedTo)) return true;
            if (comparedTo == null) return false;
            //todo add properties

            return comparedTo.deviceUUID == deviceUUID &&
                comparedTo.userLocalDataVersionId == userLocalDataVersionId &&
                comparedTo.deviceLanguageType == deviceLanguageType &&
                comparedTo.deviceVoiceLanguageType == deviceVoiceLanguageType &&
                comparedTo.selectedServerName == selectedServerName &&
                comparedTo.localLevelIndex == localLevelIndex &&
                comparedTo.deviceID == deviceID &&
                comparedTo.targetUID == targetUID &&
                comparedTo.uiSaveData == uiSaveData &&
                comparedTo.inputData == inputData &&
                //comparedTo.graphicsData == graphicsData &&
                //comparedTo.globalPerfData == globalPerfData &&
                comparedTo.miniMapConfig == miniMapConfig &&
                comparedTo.enableCameraSlope == enableCameraSlope &&
                comparedTo.enableCameraCombatLock == enableCameraCombatLock &&
                comparedTo.completionPkg == completionPkg &&
                comparedTo.completionPkgGoPkg == completionPkgGoPkg &&
                comparedTo.onlyPlayWithPSPlayer == onlyPlayWithPSPlayer &&
                comparedTo.needPlayGoFullPkgPatch == needPlayGoFullPkgPatch &&
                comparedTo.resinNotification == resinNotification &&
                comparedTo.exploreNotification == exploreNotification &&
                comparedTo.volumeGlobal == volumeGlobal &&
                comparedTo.volumeSFX == volumeSFX &&
                comparedTo.volumeVoice == volumeVoice &&
                comparedTo.audioAPI == audioAPI &&
                comparedTo.audioDynamicRange == audioDynamicRange &&
                comparedTo.audioOutput == audioOutput &&
                comparedTo._audioSuccessInit == _audioSuccessInit &&
                comparedTo.enableAudioChangeAndroidMinimumBufferCapacity == enableAudioChangeAndroidMinimumBufferCapacity &&
                comparedTo.vibrationLevel == vibrationLevel &&
                comparedTo.vibrationIntensity == vibrationIntensity &&
                comparedTo.usingNewVibrationSetting == usingNewVibrationSetting &&
                comparedTo.motionBlur == motionBlur &&
                comparedTo.gyroAiming == gyroAiming &&
                comparedTo.firstHDRSetting == firstHDRSetting &&
                comparedTo.maxLuminosity == maxLuminosity &&
                comparedTo.uiPaperWhite == uiPaperWhite &&
                comparedTo.scenePaperWhite == scenePaperWhite &&
                comparedTo.gammaValue == gammaValue &&
                comparedTo._overrideControllerMapKeyList == _overrideControllerMapKeyList &&
                comparedTo.rewiredDisableKeyboard == rewiredDisableKeyboard &&
                comparedTo.rewiredEnableKeyboard == rewiredEnableKeyboard &&
                comparedTo.rewiredEnableEDS == rewiredEnableEDS &&
                comparedTo.disableRewiredDelayInit == disableRewiredDelayInit &&
                comparedTo.disableRewiredInitProtection == disableRewiredInitProtection &&
                comparedTo.lastSeenPreDownloadTime == lastSeenPreDownloadTime &&
                comparedTo.enableEffectAssembleInEditor == enableEffectAssembleInEditor &&
                comparedTo.forceDisableQuestResourceManagement == forceDisableQuestResourceManagement &&
                comparedTo.needReportQuestResourceDeleteStatusFiles == needReportQuestResourceDeleteStatusFiles &&
                comparedTo.mtrCached == mtrCached &&
                comparedTo.mtrIsOpen == mtrIsOpen &&
                comparedTo.mtrMaxTTL == mtrMaxTTL &&
                comparedTo.mtrTimeOut == mtrTimeOut &&
                comparedTo.mtrTraceCount == mtrTraceCount &&
                comparedTo.mtrAbortTimeOutCount == mtrAbortTimeOutCount &&
                comparedTo.mtrAutoTraceInterval == mtrAutoTraceInterval &&
                comparedTo.mtrTraceCDEachReason == mtrTraceCDEachReason &&
                comparedTo.mtrTimeInterval == mtrTimeInterval &&
                comparedTo.mtrBanReason == mtrBanReason &&
                comparedTo._customDataKeyList == _customDataKeyList &&
                comparedTo._customDataValueList == _customDataValueList &&
                comparedTo._serializedCodeSwitches == _serializedCodeSwitches &&
                comparedTo.urlCheckCached == urlCheckCached &&
                comparedTo.urlCheckIsOpen == urlCheckIsOpen &&
                comparedTo.urlCheckAllIP == urlCheckAllIP &&
                comparedTo.urlCheckTimeOut == urlCheckTimeOut &&
                comparedTo.urlCheckSueecssTraceCount == urlCheckSueecssTraceCount &&
                comparedTo.urlCheckErrorTraceCount == urlCheckErrorTraceCount &&
                comparedTo.urlCheckAbortTimeOutCount == urlCheckAbortTimeOutCount &&
                comparedTo.urlCheckTimeInterval == urlCheckTimeInterval &&
                comparedTo.urlCheckCDEachReason == urlCheckCDEachReason &&
                comparedTo.urlCheckBanReasons == urlCheckBanReasons &&
                comparedTo.mtrUseOldWinVersion == mtrUseOldWinVersion &&
                comparedTo.greyTestDeviceUniqueId == greyTestDeviceUniqueId;
        }
#nullable disable
        #endregion
    }
}
