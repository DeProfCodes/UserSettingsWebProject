using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserSettingsSharedProject.Models;
using UserSettingsShareProject.ViewModels;

namespace UserSettingsWebApp.Services
{
    public class UserSettingsAPI : IUserSettingsAPI
    {
        private HttpClient client;

        public UserSettingsAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44359/api/");
        }

        public async Task<List<UserSetting>> GetAllUserSettings()
        {
            var result = await client.GetAsync("getAllUserSettings");
            
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsAsync<List<UserSetting>>();
                readTask.Wait();

                var allUserSettings = readTask.Result;

                return allUserSettings;
            }
            else
            {
                return null;
            }
        }

        public async Task<UserSetting> GetUserSetting(int userSettingId)
        {
            var url = $"getUserSettingById?id={userSettingId}";

            var result = await client.GetAsync(url);
            
            if (result.IsSuccessStatusCode)
            {
                var userSetting = await result.Content.ReadAsAsync<UserSetting>();

                return userSetting;
            }
            else
            {
                return null;
            }
        }

        public async Task<string> CreateUserSetting(string payload)
        {
            var url = $"createUserSetting?payload={payload}";

            var result = await client.GetAsync(url);
            var message = await result.Content.ReadAsAsync<string>();
            
            return message;
        }

        public async Task<string> UpdateUserSetting(string payload)
        {
            var url = $"updateUserSetting?payload={payload}";

            var userSettingVm = JsonSerializer.Deserialize<UserSettingViewModel>(payload);
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

            var result = await client.GetAsync(url);
            result = await client.PutAsJsonAsync(url, userSetting);

            var message = await result.Content.ReadAsAsync<string>();

            return message;
        }

        public async Task<string> DeleteUserSetting(int userSettingId)
        {
            var url = $"deleteUserSettingBy?id={userSettingId}";

            var result = await client.GetAsync(url);
            var message = await result.Content.ReadAsAsync<string>();

            return message;
        }
    }
}
