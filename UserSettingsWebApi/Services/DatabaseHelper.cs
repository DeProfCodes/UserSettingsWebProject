using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UserSettingsShareProject.Models;
using UserSettingsWebApi.Helpers.Enums;

namespace UserSettingsWebApi.Services
{
    public class DatabaseHelper
    {
        string connectionString;
        public DatabaseHelper()
        {
            connectionString = "Server=NTHASSESSMENTS;Database=CLAAssessment;User Id=CLAAssessment_User;Password=fGTy6tn82yvRDbaW;Trusted_Connection=True;Encrypt=False;MultipleActiveResultSets=true";
            //connectionString = "Server=(localdb)\\mssqllocaldb;Database=CLAAssessment;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        public List<UserSetting> GetAllUserSettings()
        {
            var allUserSettings = new List<UserSetting>();
            
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = UserSettingsStoredProceduresEnums.GetAllUserSettings.GetDisplayName();

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        var userSettings = new UserSetting
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            SettingDescription = reader["SettingDescription"].ToString(),
                            ScreenSaverTimeout = Convert.ToInt64(reader["ScreenSaverTimeout"]),
                            PopupTimeout = Convert.ToInt64(reader["PopupTimeout"]),
                            DesktopTimeout = Convert.ToInt64(reader["DesktopTimeout"]),
                            TickerTimeout = Convert.ToInt64(reader["TickerTimeout"]),
                            SyncTimeout = Convert.ToInt64(reader["SyncTimeout"]),
                        };
                        allUserSettings.Add(userSettings);
                    }

                    conn.Close();
                }
            }

            return allUserSettings;
        }

        public UserSetting GetUserSettingById(int UserSettingId)
        {
            UserSetting userSettings = null;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = UserSettingsStoredProceduresEnums.GetUserSettingsById.GetDisplayName();

                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.Id.GetDisplayName(), SqlDbType.Int).Value = UserSettingId;

                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        userSettings = new UserSetting
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            SettingDescription = reader["SettingDescription"].ToString(),
                            ScreenSaverTimeout = Convert.ToInt64(reader["ScreenSaverTimeout"]),
                            PopupTimeout = Convert.ToInt64(reader["PopupTimeout"]),
                            DesktopTimeout = Convert.ToInt64(reader["DesktopTimeout"]),
                            TickerTimeout = Convert.ToInt64(reader["TickerTimeout"]),
                            SyncTimeout = Convert.ToInt64(reader["SyncTimeout"]),
                        };
                    }
                    conn.Close();
                }
            }

            return userSettings;
        }

        public bool CreateUserSetting(UserSetting userSetting)
        {
            var affectedRows = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = UserSettingsStoredProceduresEnums.InsertNewUserSettings.GetDisplayName();

                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.SettingDescription.GetDisplayName(), SqlDbType.NVarChar).Value = userSetting.SettingDescription;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.ScreenSaverTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.ScreenSaverTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.PopupTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.PopupTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.DesktopTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.DesktopTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.TickerTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.TickerTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.SyncTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.SyncTimeout;

                    conn.Open();

                    affectedRows = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

            bool isSuccess = affectedRows > 0;
            return isSuccess;
        }

        public bool UpdateUserSetting(UserSetting userSetting)
        {
            var affectedRows = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = UserSettingsStoredProceduresEnums.UpdateUserSettingsById.GetDisplayName();

                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.Id.GetDisplayName(), SqlDbType.Int).Value = userSetting.Id;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.SettingDescription.GetDisplayName(), SqlDbType.NVarChar).Value = userSetting.SettingDescription;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.ScreenSaverTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.ScreenSaverTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.PopupTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.PopupTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.DesktopTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.DesktopTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.TickerTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.TickerTimeout;
                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.SyncTimeout.GetDisplayName(), SqlDbType.BigInt).Value = userSetting.SyncTimeout;

                    conn.Open();

                    affectedRows = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

            bool isSuccess = affectedRows > 0;
            return isSuccess;
        }

        public bool DeleteUserSetting(int UserSettingId)
        {
            var affectedRows = 0;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = UserSettingsStoredProceduresEnums.DeleteUserSettingsById.GetDisplayName();

                    cmd.Parameters.AddWithValue(StoredProcInputNamesEnum.Id.GetDisplayName(), SqlDbType.Int).Value = UserSettingId;

                    conn.Open();

                    affectedRows = cmd.ExecuteNonQuery();

                    conn.Close();
                }
            }

            bool isSuccess = affectedRows > 0;
            return isSuccess;
        }
    }
}