using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserSettingsWebApi.Services;
using Swashbuckle.Swagger.Annotations;
using UserSettingsShareProject.Models;

namespace UserSettingsWebApi.Controllers
{
    public class UserSettingsController : ApiController
    {
        private DatabaseHelper dbHelper;
        public UserSettingsController()
        {
            dbHelper = new DatabaseHelper();
        }

        /// <summary>
        /// Retrieve all User Settings
        /// </summary>
        /// <returns>User Settings List</returns>
        [HttpGet]
        [Route("api/getAllUserSettings")]
        public IHttpActionResult GetAllUserSettings()
        {
            try
            {
                var allUserSettings = dbHelper.GetAllUserSettings();

                if (allUserSettings.Count > 0)
                {
                    return Ok(allUserSettings);
                }
                else
                {
                    return BadRequest("No users found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Retrieve single user setting 
        /// </summary>
        /// <param name="id">User Setting Id</param>
        /// <returns>User Setting object if found</returns>
        [HttpGet]
        [Route("api/getUserSettingById")]
        public IHttpActionResult GetUserSettingById(int id)
        {
            try
            {
                var userSetting = dbHelper.GetUserSettingById(id);

                if (userSetting != null)
                {
                    return Ok(userSetting);
                }
                else
                {
                    return BadRequest($"No user setting with Id={id} was found!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Insert new user setting 
        /// </summary>
        /// <param name="payload">JSON payload with all fields except Id field</param>
        /// <returns>Error or success message</returns>
        [HttpPost]
        [Route("api/createUserSetting")]
        public IHttpActionResult CreateUserSetting(UserSetting userSetting)
        {
            try
            {
                bool success = dbHelper.CreateUserSetting(userSetting);

                if (success)
                {
                    return Ok("User setting was created successfully");
                }
                else 
                {
                    return BadRequest("Something went wrong while attempting to create a new user setting");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update user settings 
        /// </summary>
        /// <param name="payload">JSON payload with all fields</param>
        /// <returns>Error or success message</returns>
        [HttpPut]
        [Route("api/updateUserSetting")]
        public IHttpActionResult UpdateUserSetting(UserSetting userSettings)
        {
            try
            {
                bool success = dbHelper.UpdateUserSetting(userSettings);

                if (success)
                {
                    return Ok("User setting was updated successfully");
                }
                else
                {
                    return BadRequest("Something went wrong while attempting to update user setting");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Insert new user settings 
        /// </summary>
        /// <param name="Id">User Setting Id</param>
        /// <returns>Error or success message</returns>
        [HttpDelete]
        [Route("api/deleteUserSetting")]
        public IHttpActionResult DeleteUserSetting(int id)
        {
            try
            {
                bool success = dbHelper.DeleteUserSetting(id);

                if (success)
                {
                    return Ok("User setting was updated successfully");
                }
                else
                {
                    return BadRequest("Something went wrong while attempting to update user settings");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
