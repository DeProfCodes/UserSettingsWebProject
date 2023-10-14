using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserSettingsWebApi.Helpers.Enums
{
    public enum StoredProcInputNamesEnum
    {
        [Display(Name = "@Id")]
        Id = 1,

        [Display(Name = "@SettingDescription")]
        SettingDescription = 2,

        [Display(Name = "@ScreenSaverTimeout")]
        ScreenSaverTimeout = 3,

        [Display(Name = "@PopupTimeout")]
        PopupTimeout = 4,

        [Display(Name = "@DesktopTimeout")]
        DesktopTimeout = 5,

        [Display(Name = "@TickerTimeout")]
        TickerTimeout = 6,

        [Display(Name = "@SyncTimeout")]
        SyncTimeout = 7
    }
}