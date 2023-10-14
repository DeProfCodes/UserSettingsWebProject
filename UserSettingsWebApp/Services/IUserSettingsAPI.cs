using System.Collections.Generic;
using System.Threading.Tasks;
using UserSettingsShareProject.Models;

namespace UserSettingsWebApp.Services
{
    public interface IUserSettingsAPI
    {
        public Task<List<UserSetting>> GetAllUserSettings();

        public Task<UserSetting> GetUserSetting(int userSettingId);

        public Task<string> CreateUserSetting(string payload);

        public Task<string> UpdateUserSetting(string payload);

        public Task<string> DeleteUserSetting(int userSettingId);
    }
}
