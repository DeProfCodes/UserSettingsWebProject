using System;
using System.Collections.Generic;
using System.Text;

namespace UserSettingsShareProject.ViewModels
{
    public class UserSettingViewModel
    {
        public string Id { get; set; }

        public string SettingDescription { get; set; }

        public string ScreenSaverTimeout { get; set; }

        public string PopupTimeout { get; set; }

        public string DesktopTimeout { get; set; }

        public string TickerTimeout { get; set; }

        public string SyncTimeout { get; set; }
    }
}
