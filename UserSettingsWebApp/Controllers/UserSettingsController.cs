using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using UserSettingsSharedProject.Models;
using UserSettingsSharedProject.ViewModels;
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
            var userSettingViewModel = new UserSetting();
            try
            {
                userSettingViewModel = await _userSettingsApi.GetUserSetting(id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occured while retrieving all user settings, error = {ex.Message}");
            }
            return PartialView(userSettingViewModel);
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
                _logger.LogError($"An error occured while retrieving all user settings, error = {ex.Message}");
                
                return BadRequest();
            }
        }
    }
}
