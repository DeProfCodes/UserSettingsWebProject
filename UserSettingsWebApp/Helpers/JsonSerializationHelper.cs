using System;
using System.Text.Json;
using UserSettingsShareProject.Models;
using UserSettingsShareProject.ViewModels;

namespace UserSettingsWebApp.Helpers
{
    public class JsonSerializationHelper
    {
        public static string GetJsonPayloadForApiCall(string jsonFromClient)
        {
            var userSettingVm = JsonSerializer.Deserialize<UserSettingViewModel>(jsonFromClient);

            var userSetting = new UserSetting
            {
                Id = Convert.ToInt32(userSettingVm.Id),
                SettingDescription = userSettingVm.SettingDescription,
                DesktopTimeout = Convert.ToInt64(userSettingVm.DesktopTimeout),
                TickerTimeout = Convert.ToInt64(userSettingVm.TickerTimeout),
                SyncTimeout = Convert.ToInt64(userSettingVm.SyncTimeout),
                ScreenSaverTimeout = Convert.ToInt64(userSettingVm.ScreenSaverTimeout),
                PopupTimeout = Convert.ToInt64(userSettingVm.PopupTimeout),
            };

            var cleanJson = JsonSerializer.Serialize(userSetting);

            return cleanJson;   
        }
    }
}
