using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserSettingsWebApi.Helpers.Enums
{
    public enum UserSettingsStoredProceduresEnums
    {
        [Display(Name = "stpInsertNewUserSetting")]
        InsertNewUserSettings = 1,

        [Display(Name = "stpUpdateUserSettingById")]
        UpdateUserSettingsById = 2,

        [Display(Name = "stpDeleteUserSettingById")]
        DeleteUserSettingsById = 3,

        [Display(Name = "stpGetAllUserSettings")]
        GetAllUserSettings = 4,

        [Display(Name = "stpGetUserSettingById")]
        GetUserSettingsById = 5
    }
}