﻿// ---------------------------------------------------------------------------- 
// Copyright (c) Microsoft Corporation. All rights reserved.
// ---------------------------------------------------------------------------- 

namespace Microsoft.WindowsAzure.Setup
{
    using System;
    using System.IO;
    using Deployment.WindowsInstaller;

    public class CustomAction
    {
        private static uint[] powerShellDefaultColorTable = new uint[] 
            { 
                0x0, 0x800000, 0x8000, 0x808000, 0x80, 0x562401, 0xF0EDEE, 0xC0C0C0,
                0x808080, 0xFF0000, 0xFF00, 0xFFFF00, 0xFF, 0xFF00FF, 0xFFFF, 0xFFFFFF 
            };

        [CustomAction]
        public static ActionResult UpdatePSShortcut(Session session)
        {
            string powerShellShortcutPath = session.CustomActionData["ShortcutPath"];
            string powerShellDefaultShortcutPath = session.CustomActionData["DefaultShortcutPath"];

            if (!File.Exists(powerShellShortcutPath))
            {
                session.Log("UpdatePSShortcut: file {0} does not exist", powerShellShortcutPath);
                return ActionResult.Failure;
            }
            try
            {
                ShellLink powerShellShellLink = new ShellLink(powerShellShortcutPath);
                if (File.Exists(powerShellDefaultShortcutPath))
                {
                    session.Log("UpdatePSShortcut: found default Windows PowerShell shortcut at {0}", powerShellDefaultShortcutPath);
                    ShellLink powerShellDefaultShellLink = new ShellLink(powerShellDefaultShortcutPath);
                    powerShellShellLink.ConsoleProperties = powerShellDefaultShellLink.ConsoleProperties;
                }
                else
                {
                    session.Log("UpdatePSShortcut: default Windows PowerShell shortcut does not exist at {0}", powerShellDefaultShortcutPath);

                    for (int i = 0; i < powerShellShellLink.ConsoleProperties.ColorTable.Length; i++)
                    {
                        powerShellShellLink.ConsoleProperties.ColorTable[i] = powerShellDefaultColorTable[i];
                    }
                    powerShellShellLink.AutoPosition = true;
                    powerShellShellLink.CommandHistoryBufferSize = 50;
                    powerShellShellLink.CommandHistoryBufferCount = 4;

                    powerShellShellLink.InsertMode = true;

                    powerShellShellLink.PopUpBackgroundColor = 15;
                    powerShellShellLink.PopUpTextColor = 3;

                    powerShellShellLink.QuickEditMode = true;

                    powerShellShellLink.ScreenBackgroundColor = 5;
                    powerShellShellLink.ScreenTextColor = 6;

                    powerShellShellLink.SetScreenBufferSize(120, 3000);
                    powerShellShellLink.SetWindowSize(120, 50);
                }
                powerShellShellLink.Save();
                session.Log("UpdatePSShortcut: success");
            }
            catch (Exception ex)
            {
                session.Log("UpdatePSShortcut: failed with exception {0}", ex.Message);
            }

            return ActionResult.Success;
        }
    }
}