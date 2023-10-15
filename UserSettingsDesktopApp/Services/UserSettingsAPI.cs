using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserSettingsShareProject.Models;
using UserSettingsShareProject.Helpers.Constants;
using UserSettingsShareProject.ViewModels;

namespace UserSettingsDesktopApp.Services
{
    public class UserSettingsAPI
    {
        private HttpClient client;

        /// <summary>
        /// Client API to query web api
        /// </summary>
        public UserSettingsAPI()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(UserSettingAPIRoutes.API_BASE_ADDRESS);
        }

        /// <summary>
        /// Get All User Settings 
        /// </summary>
        /// <returns></returns>
        public List<UserSetting> GetAllUserSettings()
        {
            var responseTask = client.GetAsync(UserSettingAPIRoutes.GET_ALL_USER_SETTINGS);
            responseTask.Wait();

            var result = responseTask.Result;

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

        /// <summary>
        /// Get User Setting
        /// </summary>
        /// <param name="userSettingId"></param>
        /// <returns></returns>
        public UserSetting GetUserSetting(int userSettingId)
        {
            var url = $"{UserSettingAPIRoutes.GET_USER_SETTING_BY_ID}?id={userSettingId}";

            var responseTask = client.GetAsync(url);
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var userSettingTask = result.Content.ReadAsAsync<UserSetting>();
                userSettingTask.Wait();

                return userSettingTask.Result;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Create new user from json input from user
        /// </summary>
        /// <param name="userSetting">UserSetting Object</param>
        /// <returns>Error or success message</returns>
        public string CreateUserSetting(UserSetting userSetting)
        {
            var payloadJson = JsonSerializer.Serialize(userSetting); 

            var httpContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            var responseTask = client.PostAsync(UserSettingAPIRoutes.CREATE_USER_SETTING, httpContent);
            responseTask.Wait();

            var result = responseTask.Result;

            var messageTask = result.Content.ReadAsAsync<string>();
            messageTask.Wait();

            var message = messageTask.Result;
            return message;
        }

        /// <summary>
        /// Update User Setting endpoint
        /// </summary>
        /// <param name="userSetting"></param>
        /// <returns></returns>
        public string UpdateUserSetting(UserSetting userSetting)
        {
            var payloadJson = JsonSerializer.Serialize(userSetting); ;

            var httpContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            var responseTask = client.PutAsync(UserSettingAPIRoutes.UPDATE_USER_SETTING, httpContent);
            responseTask.Wait();

            var result = responseTask.Result;

            var messageTask = result.Content.ReadAsAsync<string>();
            messageTask.Wait();

            var message = messageTask.Result;

            return message;
        }

        /// <summary>
        /// Delete user setting
        /// </summary>
        /// <param name="userSettingId"></param>
        /// <returns></returns>
        public string DeleteUserSetting(int userSettingId)
        {
            var url = $"{UserSettingAPIRoutes.DELETE_USER_SETTING}?id={userSettingId}";

            var responseTask = client.DeleteAsync(url);
            responseTask.Wait();

            var result = responseTask.Result;
            var messageResponse = result.Content.ReadAsAsync<string>();
            messageResponse.Wait();

            var message = messageResponse.Result;

            return message;
        }
    }
}
