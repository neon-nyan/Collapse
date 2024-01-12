﻿using System;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using Microsoft.Windows.AppLifecycle;
using Windows.ApplicationModel.Activation;
using static Hi3Helper.Logger;
using static CollapseLauncher.InnerLauncherConfig;
using static Hi3Helper.Shared.Region.LauncherConfig;

namespace CollapseLauncher
{
    public static class AppActivation
    {
        public static void Enable()
        {
            AppInstance.GetCurrent().Activated += App_Activated;

            // Name for the protocol 
            string name = "collapse";
            RegistryKey reg = Registry.ClassesRoot.OpenSubKey(name + "\\shell\\open\\command", true);

            if (reg != null)
            {
                if ((string)reg.GetValue("") == string.Format("\"{0}\" %1", AppExecutablePath))
                {
                    LogWriteLine("Already activated.");
                    return;
                }
            }

            Registry.ClassesRoot.DeleteSubKeyTree(name, false);

            RegistryKey protocol = Registry.ClassesRoot.CreateSubKey(name, true);

            protocol.SetValue("", "CollapseLauncher protocol");
            protocol.SetValue("URL Protocol", "");
            protocol.SetValue("Version", AppCurrentVersionString);

            RegistryKey command = protocol.CreateSubKey("shell\\open\\command", true);

            command.SetValue("", string.Format("\"{0}\" %1", AppExecutablePath));
        }

        private static void App_Activated(object sender, AppActivationArguments e)
        {
            if (IsMultipleInstanceEnabled)
                return;
            if (e.Kind != ExtendedActivationKind.Launch)
                return;
            if (e.Data == null)
                return;

            var args = e.Data as ILaunchActivatedEventArgs;
            ArgumentParser.ResetRootCommand();
            m_arguments = new Arguments();

            // Matches anything that is between two \" or " and anything that is not a space.
            var splitArgs = Regex.Matches(args.Arguments, @"[\""].+?[\""]|[^ ]+")
                                    .Cast<Match>()
                                    .Select(x => x.Value.Trim('"'));

            ArgumentParser.ParseArguments(splitArgs.Skip(1).ToArray());

            if (m_arguments.StartGame != null)
            {
                m_mainPage?.OpenAppActivation();
                return;
            }
        }

        public static bool DecideRedirection()
        {
            bool isRedirect = false;
            AppActivationArguments args = AppInstance.GetCurrent().GetActivatedEventArgs();
            AppInstance keyInstance = AppInstance.FindOrRegisterForKey(m_appMode.ToString());

            if (!keyInstance.IsCurrent && !IsMultipleInstanceEnabled)
            {
                isRedirect = true;
                keyInstance.RedirectActivationToAsync(args).GetAwaiter().GetResult();
            }
            return isRedirect;
        }

        public static void Disable()
        {
            AppInstance.GetCurrent().Activated -= App_Activated;
        }
    }
}
