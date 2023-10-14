using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserSettingsShareProject.Models;
using UserSettingsShareProject.Helpers.Constants;
using UserSettingsShareProject.ViewModels;
using UserSettingsWebApp.Helpers;

namespace UserSettingsWebApp.Services
{
    public class UserSettingsAPI : IUserSettingsAPI
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
        public async Task<List<UserSetting>> GetAllUserSettings()
        {
            var result = await client.GetAsync(UserSettingAPIRoutes.GET_ALL_USER_SETTINGS);

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
        public async Task<UserSetting> GetUserSetting(int userSettingId)
        {
            var url = $"{UserSettingAPIRoutes.GET_USER_SETTING_BY_ID}?id={userSettingId}";

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

        /// <summary>
        /// Create new user from json input from user
        /// </summary>
        /// <param name="payloadFromClient">Json Input</param>
        /// <returns>Error or success message</returns>
        public async Task<string> CreateUserSetting(string payloadFromClient)
        {
            var payloadJson = JsonSerializationHelper.GetJsonPayloadForApiCall(payloadFromClient);

            var httpContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            var result = await client.PostAsync(UserSettingAPIRoutes.CREATE_USER_SETTING, httpContent);

            var message = await result.Content.ReadAsAsync<string>();

            return message;
        }

        /// <summary>
        /// Update User Setting endpoint
        /// </summary>
        /// <param name="payloadFromClient"></param>
        /// <returns></returns>
        public async Task<string> UpdateUserSetting(string payloadFromClient)
        {
            var payloadJson = JsonSerializationHelper.GetJsonPayloadForApiCall(payloadFromClient);

            var httpContent = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            var result = await client.PutAsync(UserSettingAPIRoutes.UPDATE_USER_SETTING, httpContent);

            var message = await result.Content.ReadAsAsync<string>();

            return message;
        }

        /// <summary>
        /// Delete user setting
        /// </summary>
        /// <param name="userSettingId"></param>
        /// <returns></returns>
        public async Task<string> DeleteUserSetting(int userSettingId)
        {
            var url = $"{UserSettingAPIRoutes.DELETE_USER_SETTING}?id={userSettingId}";

            var result = await client.DeleteAsync(url);
            var message = await result.Content.ReadAsAsync<string>();

            return message;
        }
    }
}
