using Hardcodet.Wpf.TaskbarNotification;
using ModernFlyouts.Helpers;
using ModernFlyouts.Utilities;
using ModernWpf;
using ModernWpf.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ModernFlyouts.UI
{
    internal class TrayIconManager
    {
        #region Properties

        public static TaskbarIcon TaskbarIcon { get; private set; }

        public static ContextMenu TaskbarIconContextMenu { get; private set; }

        public static ToolTip TaskbarIconToolTip { get; private set; }

        private static ElementTheme _currentTheme = ElementTheme.Dark;
        private static bool _useColoredTrayIcon = true;

        #endregion

        public static void CreateTrayIcon()
        {
            if (TaskbarIcon != null)
            {
                RemoveTrayIcon();
            }
            
            var settingsItem = new MenuItem()
            {
                Header = Properties.Strings.SettingsItem,
                ToolTip = Properties.Strings.SettingsItemDescription,
                Icon = new FontIcon() { Glyph = CommonGlyphs.Settings },
                Command = CommonCommands.OpenSettingsWindowCommand
            };

            var exitItem = new MenuItem()
            {
                Header = Properties.Strings.ExitItem,
                ToolTip = Properties.Strings.ExitItemDescription,
                Icon = new FontIcon() { Glyph = CommonGlyphs.PowerButton },
                Command = CommonCommands.ExitAppCommand
            };

            TaskbarIconContextMenu = new ContextMenu()
            {
                Items = { settingsItem, exitItem }
            };

            TaskbarIconToolTip = new ToolTip() { Content = Program.AppName };

            TaskbarIcon = new TaskbarIcon()
            {
                TrayToolTip = TaskbarIconToolTip,
                ContextMenu = TaskbarIconContextMenu,
                DoubleClickCommand = CommonCommands.OpenSettingsWindowCommand
            };
            
            UpdateTrayIconTheme(_currentTheme, _useColoredTrayIcon);
        }

        public static void RemoveTrayIcon()
        {
            if (TaskbarIcon != null)
            {
                TaskbarIcon.Dispose();
                TaskbarIcon = null;
            }
        }

        public static void SetupTrayIcon()
        {
            CreateTrayIcon();
        }

        public static void UpdateTrayIconVisibility(bool isVisible)
        {
            if (isVisible && TaskbarIcon == null)
            {
                CreateTrayIcon();
            }
            else if (!isVisible && TaskbarIcon != null)
            {
                RemoveTrayIcon();
            }
        }

        public static void UpdateTrayIconInternal(ElementTheme currentTheme, bool useColoredTrayIcon)
        {
            UpdateTrayIconTheme(currentTheme, useColoredTrayIcon);
        }
        
        private static void UpdateTrayIconTheme(ElementTheme currentTheme, bool useColoredTrayIcon)
        {
            _currentTheme = currentTheme;
            _useColoredTrayIcon = useColoredTrayIcon;
            
            if (TaskbarIcon == null) return;
            
            ThemeManager.SetRequestedTheme(TaskbarIconContextMenu, currentTheme);
            ThemeManager.SetRequestedTheme(TaskbarIconToolTip, currentTheme);

            Uri iconUri;
            if (useColoredTrayIcon)
            {
                iconUri = PackUriHelper.GetAbsoluteUri(@"Assets\Logo.ico");
            }
            else
            {
                iconUri = PackUriHelper.GetAbsoluteUri(currentTheme == ElementTheme.Light ? @"Assets\Logo_Tray_Black.ico" : @"Assets\Logo_Tray_White.ico");
            }

            TaskbarIcon.IconSource = BitmapFrame.Create(iconUri);
        }
    }
}
