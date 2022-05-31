﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hi3Helper.Preset
{
    public static class GameConfigurationTemplate
    {
        public static List<PresetConfigClasses> GameConfigTemplate = new List<PresetConfigClasses>
        {
            new PresetConfigClasses
            {
                ProfileName = "Hi3SEA",
                ZoneName = "Southeast Asia",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Honkai Impact 3",
                ConfigRegistryLocation = "Software\\miHoYo\\Honkai Impact 3",
                DefaultGameLocation = "C:\\Program Files\\Honkai Impact 3 sea",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                UpdateDictionaryAddress = "update_sea/",
                BlockDictionaryAddress = "com.miHoYo.bh3oversea/",
                LanguageAvailable = new List<string>{ "en", "cn", "vn", "th", "id" },
                FallbackLanguage = "en",
                GameDirectoryName = "Games",
                GameExecutableName = "BH3.exe",
                IsConvertible = true,
                ConvertibleTo = new List<string>{ "Hi3Global" },
                ConvertibleCookbookURL = "https://prophost.ironmaid.xyz/_shared/_diffrepo/{0}",
                BetterHi3LauncherVerInfoReg = "VersionInfoSEA",
                ZipFileURL = "https://prophost.ironmaid.xyz/_shared/_zipfiles/sea/{0}/",
                CachesListAPIURL = "https://prophost.ironmaid.xyz/api/updatepackage?platform=0&datatype={0}&gamever={1}&usenewformat=true",
                CachesEndpointURL = "http://data.in.seanew.ironmaid.xyz:14000/update_sea/{0}/editor_compressed/",
                CachesListGameVerID = 0,
                LauncherSpriteURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/content?filter_adv=true&language=en-us&launcher_id=9",
                LauncherResourceURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/resource?channel_id=1&key=tEGNtVhN&launcher_id=9&sub_channel_id=1",
                LauncherInfoURL = "https://honkaiimpact3.mihoyo.com/launcher/9/en-us?api_url=https%3A%2F%2Fapi-os-takumi.mihoyo.com%2Fbh3_global&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "Hi3Global",
                ZoneName = "Global",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Honkai Impact 3rd",
                SteamInstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Steam App 1671200",
                SteamGameID = 1671200,
                ConfigRegistryLocation = "Software\\miHoYo\\Honkai Impact 3rd",
                DefaultGameLocation = "C:\\Program Files\\Honkai Impact 3rd glb",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                UpdateDictionaryAddress = "update_global/",
                BlockDictionaryAddress = "tmp/com.miHoYo.bh3global/",
                LanguageAvailable = new List<string>{ "en", "cn", "fr", "de" },
                FallbackLanguage = "en",
                GameDirectoryName = "Games",
                GameExecutableName = "BH3.exe",
                IsConvertible = true,
                ConvertibleTo = new List<string>{ "Hi3SEA" },
                ConvertibleCookbookURL = "https://prophost.ironmaid.xyz/_shared/_diffrepo/{0}",
                BetterHi3LauncherVerInfoReg = "VersionInfoGlobal",
                ZipFileURL = "https://prophost.ironmaid.xyz/_shared/_zipfiles/global/{0}/",
                CachesListAPIURL = "https://prophost.ironmaid.xyz/api/updatepackage?platform=0&datatype={0}&gamever={1}&usenewformat=true",
                CachesEndpointURL = "http://data.in.seanew.ironmaid.xyz:14000/update_global/{0}/editor_compressed/",
                CachesListGameVerID = 1,
                LauncherSpriteURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/content?filter_adv=true&language=en-us&launcher_id=10",
                LauncherResourceURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/resource?channel_id=1&key=dpz65xJ3&launcher_id=10&sub_channel_id=1",
                LauncherInfoURL = "https://honkaiimpact3.mihoyo.com/launcher/10/en-us?api_url=https%3A%2F%2Fapi-os-takumi.mihoyo.com%2Fbh3_global&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "Hi3CN",
                ZoneName = "Mainland China",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\崩坏3",
                ConfigRegistryLocation = "Software\\miHoYo\\崩坏3",
                DefaultGameLocation = "C:\\Program Files\\Honkai Impact 3",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                UpdateDictionaryAddress = "update_cn/",
                BlockDictionaryAddress = "tmp/Original/",
                LanguageAvailable = new List<string>{ "cn" },
                FallbackLanguage = "cn",
                GameDirectoryName = "Games",
                GameExecutableName = "BH3.exe",
                BetterHi3LauncherVerInfoReg = "VersionInfoCN",
                ZipFileURL = "https://prophost.ironmaid.xyz/_shared/_zipfiles/{0}/",
                CachesListAPIURL = "https://prophost.ironmaid.xyz/api/updatepackage?platform=0&datatype={0}&gamever={1}&usenewformat=true",
                CachesEndpointURL = "http://data.in.seanew.ironmaid.xyz:14000/update_cn/{0}/editor_compressed/",
                CachesListGameVerID = 2,
                UseRightSideProgress = true,
                LauncherSpriteURL = "https://sdk-static.mihoyo.com/bh3_cn/mdk/launcher/api/content?filter_adv=true&language=zh-cn&launcher_id=4",
                LauncherResourceURL = "https://sdk-static.mihoyo.com/bh3_cn/mdk/launcher/api/resource?channel_id=1&key=SyvuPnqL&launcher_id=4&sub_channel_id=1",
                LauncherInfoURL = "https://www.bh3.com/launcher/4/zh-cn?api_url=https%3A%2F%2Fapi-sdk.mihoyo.com%2Fbh3_cn&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "Hi3TW",
                ZoneName = "TW/HK/MO",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\崩壞3rd",
                ConfigRegistryLocation = "Software\\miHoYo\\崩壞3rd",
                DefaultGameLocation = "C:\\Program Files\\Honkai Impact 3rd tw",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                UpdateDictionaryAddress = "update_cn/",
                BlockDictionaryAddress = "tmp/Original/",
                LanguageAvailable = new List<string>{ "cn" },
                FallbackLanguage = "cn",
                GameDirectoryName = "Games",
                GameExecutableName = "BH3.exe",
                BetterHi3LauncherVerInfoReg = "VersionInfoTW",
                ZipFileURL = "https://prophost.ironmaid.xyz/_shared/_zipfiles/{0}/",
                LauncherSpriteURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/content?filter_adv=true&language=zh-tw&launcher_id=8",
                LauncherResourceURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/resource?channel_id=1&key=demhUTcW&launcher_id=8&sub_channel_id=1",
                LauncherInfoURL = "https://honkaiimpact3.mihoyo.com/launcher/8/zh-tw?api_url=https%3A%2F%2Fapi-os-takumi.mihoyo.com%2Fbh3_global&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "Hi3KR",
                ZoneName = "Korean",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\붕괴3rd",
                ConfigRegistryLocation = "Software\\miHoYo\\붕괴3rd",
                DefaultGameLocation = "C:\\Program Files\\Honkai Impact 3rd kr",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                UpdateDictionaryAddress = "update_cn/",
                BlockDictionaryAddress = "tmp/Original/",
                LanguageAvailable = new List<string>{ "cn" },
                FallbackLanguage = "cn",
                GameDirectoryName = "Games",
                GameExecutableName = "BH3.exe",
                BetterHi3LauncherVerInfoReg = "VersionInfoKR",
                ZipFileURL = "https://prophost.ironmaid.xyz/_shared/_zipfiles/{0}/",
                LauncherSpriteURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/content?filter_adv=true&language=ko-kr&launcher_id=11",
                LauncherResourceURL = "https://sdk-os-static.mihoyo.com/bh3_global/mdk/launcher/api/resource?channel_id=1&key=PRg571Xh&launcher_id=11&sub_channel_id=1",
                LauncherInfoURL = "https://honkaiimpact3.mihoyo.com/launcher/11/ko-kr?api_url=https%3A%2F%2Fapi-os-takumi.mihoyo.com%2Fbh3_global&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "GIGlb",
                ZoneName = "Genshin Impact",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\Genshin Impact",
                ConfigRegistryLocation = "Software\\miHoYo\\Genshin Impact",
                DefaultGameLocation = "C:\\Program Files\\Genshin Impact",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                LanguageAvailable = new List<string>{ "en" },
                FallbackLanguage = "en",
                IsGenshin = true,
                UseRightSideProgress = true,
                GameDirectoryName = "Genshin Impact game",
                GameExecutableName = "GenshinImpact.exe",
                ProtoDispatchKey = "10616738251919d2",
                LauncherSpriteURL = "https://sdk-os-static.mihoyo.com/hk4e_global/mdk/launcher/api/content?filter_adv=true&key=gcStgarh&language=en-us&launcher_id=10",
                LauncherResourceURL = "https://sdk-os-static.mihoyo.com/hk4e_global/mdk/launcher/api/resource?channel_id=1&key=gcStgarh&launcher_id=10&sub_channel_id=0",
                LauncherInfoURL = "https://genshin.mihoyo.com/launcher/10/en-us?api_url=https%3A%2F%2Fapi-os-takumi.mihoyo.com%2Fhk4e_global&key=gcStgarh&prev=false",
            },
            new PresetConfigClasses
            {
                ProfileName = "GICN",
                ZoneName = "原神",
                InstallRegistryLocation = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\原神",
                ConfigRegistryLocation = "Software\\miHoYo\\原神",
                DefaultGameLocation = "C:\\Program Files\\Genshin Impact",
                DictionaryHost = "http://data.in.seanew.ironmaid.xyz:14000/",
                LanguageAvailable = new List<string>{ "en" },
                FallbackLanguage = "en",
                IsGenshin = true,
                UseRightSideProgress = true,
                GameDirectoryName = "Genshin Impact game",
                GameExecutableName = "YuanShen.exe",
                LauncherSpriteURL = "https://sdk-static.mihoyo.com/hk4e_cn/mdk/launcher/api/content?filter_adv=true&key=eYd89JmJ&language=zh-cn&launcher_id=18",
                LauncherResourceURL = "https://sdk-static.mihoyo.com/hk4e_cn/mdk/launcher/api/resource?channel_id=1&key=eYd89JmJ&launcher_id=18&sub_channel_id=1",
                LauncherInfoURL = "https://ys.mihoyo.com/launcher/18/zh-cn?api_url=https%3A%2F%2Fapi-sdk.mihoyo.com%2Fhk4e_cn&key=eYd89JmJ&prev=false",
            }
        };
    }
}
