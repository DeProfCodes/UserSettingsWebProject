using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserSettingsSharedProject.ViewModels;
using UserSettingsShareProject.Models;
using UserSettingsShareProject.ViewModels;
using UserSettingsWebApp.Models;
using UserSettingsWebApp.Services;

namespace UserSettingsWebApp.Controllers
{
    public class UserSettingsController : Controller
    {
        private readonly IUserSettingsAPI _userSettingsApi;
        private readonly ILogger<UserSettingsController> _logger;

        public UserSettingsController(IUserSettingsAPI userSettingsApi, ILogger<UserSettingsController> logger)
        {
            _userSettingsApi = userSettingsApi;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var userSettingsViewModel = new UserSettingsViewModel();
            try
            {
                userSettingsViewModel.AllUserSettings = await _userSettingsApi.GetAllUserSettings();
            }
            catch(Exception ex)
            {
                _logger.LogError($"An error occured while retrieving all user settings, error = {ex.Message}");
            }
            return View(userSettingsViewModel);
        }

        public async Task<IActionResult> UserSettingDetails(int id)
        {
            var userSetting = new UserSetting { Id = id };

            if (id != 0)
            {
                try
                {
                    userSetting = await _userSettingsApi.GetUserSetting(id);

                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occured while retrieving a user setting with id={id}, error = {ex.Message}");
                }
            }
            return PartialView(userSetting);
        }

        public async Task<IActionResult> UpdateUserSettingDetails(string payload)
        {
            var userSettingViewModel = new UserSetting();
            try
            {
                var userSetting = await _userSettingsApi.UpdateUserSetting(payload);    
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while updating user setting with payload={payload}, error = {ex.Message}");
                
                return BadRequest();
            }
        }

        public async Task<IActionResult> CreateUserSettingDetails(string payload)
        {
            var userSettingViewModel = new UserSetting();
            try
            {
                var userSetting = await _userSettingsApi.CreateUserSetting(payload);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while retrieving all user settings with payload={payload}, error = {ex.Message}");

                return BadRequest();
            }
        }

        public async Task<IActionResult> DeleteUserSetting(int id)
        {
            if (id != 0)
            {
                try
                {
                    await _userSettingsApi.DeleteUserSetting(id);

                    return Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"An error occured while deleting user setting with id={id}, error = {ex.Message}");
                }
            }
            return BadRequest();
        }
    }
}
