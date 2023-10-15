using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserSettingsDesktopApp.Services;
using UserSettingsShareProject.Models;

namespace UserSettingsDesktopApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private UserSettingsAPI userSettingsApi;
        public List<UserSetting> AllUserSettings { get; set; }
        public UserSetting userSetting { get; set; }

        public MainWindow()
        {
            userSettingsApi = new();

            InitializeComponent();

            ReloadAllUserSettings();
        }

        /// <summary>
        /// Query API to load all user settings from database then serve them to DataGrid in main page
        /// </summary>
        private void ReloadAllUserSettings()
        {
            AllUserSettings = userSettingsApi.GetAllUserSettings();
            if (AllUserSettings != null && AllUserSettings.Count > 0)
            {
                UserSettingsGrid.ItemsSource = AllUserSettings;
            }
        }

        /// <summary>
        /// DoubleClick Row event handler which then loads up the user setting details of the clicked row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UserSettingDetailsHeading.Visibility = Visibility.Visible;
            UserSettingDetailsBd.Visibility = Visibility.Visible;

            var selectedUserSetting = (sender as FrameworkElement).DataContext;
            var userSetting = selectedUserSetting as UserSetting;

            if (userSetting != null)
            {
                this.userSetting = userSetting;
                settingDescriptionTxt.Text = userSetting.SettingDescription;
                screensaverTimeoutTxt.Text = Convert.ToString(userSetting.ScreenSaverTimeout);
                popupTimeoutTxt.Text = Convert.ToString(userSetting.PopupTimeout);  
                desktopTimeoutTxt.Text = Convert.ToString(userSetting.DesktopTimeout);
                syncTimeoutTxt.Text = Convert.ToString(userSetting.SyncTimeout);
                tickerTimeoutTxt.Text = Convert.ToString(userSetting.TickerTimeout);
            }
        }

        /// <summary>
        /// Clear textboxes values
        /// </summary>
        private void ClearFields()
        {
            settingDescriptionTxt.Text = "";
            screensaverTimeoutTxt.Text = "";
            popupTimeoutTxt.Text = "";
            desktopTimeoutTxt.Text = "";
            syncTimeoutTxt.Text = "";
            tickerTimeoutTxt.Text = "";
        }

        /// <summary>
        /// Event handler for Clear button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearUserSetting_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        /// <summary>
        /// Event handler for Delete User button, which then deletes user from the database using web api
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteUserSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var response = MessageBox.Show("Are you sure you want to delete?", "Deleting", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                {
                    if (userSetting != null && userSetting.Id > 0)
                    {
                        var status = userSettingsApi.DeleteUserSetting(userSetting.Id);
                        MessageBox.Show(status);

                        ReloadAllUserSettings();
                        HideUserDetailsView();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Event handler for creating or updating user setting details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveUserSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var titleMsg = (userSetting != null && userSetting.Id > 0) ? "Are you sure you want to update user setting?" : "Are you sure you want to create new user setting?";
                var title = (userSetting != null && userSetting.Id > 0) ? "Update User" : "Create User";
                var response = MessageBox.Show(titleMsg, title, MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (response == MessageBoxResult.Yes)
                {
                    if (AllFieldsAreValidated())
                    {
                        if (userSetting == null)
                            userSetting = new UserSetting { Id = 0 };

                        userSetting.SettingDescription = settingDescriptionTxt.Text;
                        userSetting.ScreenSaverTimeout = Convert.ToInt64(screensaverTimeoutTxt.Text);
                        userSetting.PopupTimeout = Convert.ToInt64(popupTimeoutTxt.Text);
                        userSetting.DesktopTimeout = Convert.ToInt64(desktopTimeoutTxt.Text);
                        userSetting.SyncTimeout = Convert.ToInt64(syncTimeoutTxt.Text);
                        userSetting.TickerTimeout = Convert.ToInt64(tickerTimeoutTxt.Text);

                        var status = "";
                        if (userSetting.Id > 0)
                        {
                            status = userSettingsApi.UpdateUserSetting(userSetting);
                        }
                        else
                        {
                            status = userSettingsApi.CreateUserSetting(userSetting);
                        }
                        MessageBox.Show(status);

                        ReloadAllUserSettings();
                        HideUserDetailsView();
                    }
                    else
                    {
                        MessageBox.Show("All Fields are required, make sure all are filled", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            catch 
            {
                MessageBox.Show("Something went wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Hide User Details Section of the page
        /// </summary>
        private void HideUserDetailsView()
        {
            UserSettingDetailsHeading.Visibility = Visibility.Hidden;
            UserSettingDetailsBd.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Display User Details Section of the page
        /// </summary>
        private void ShowUserDetailsView()
        {
            UserSettingDetailsHeading.Visibility = Visibility.Visible;
            UserSettingDetailsBd.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Event handler for when creating a new user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewUserSetting_Click(object sender, RoutedEventArgs e)
        {
            ShowUserDetailsView();
            ClearFields();
            userSetting = new();
        }

        /// <summary>
        /// Event handler to force the numerical TextBoxes to only accept numerical inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputFields_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null)
            {
                textBox.Text = Regex.Replace(textBox.Text, "[^0-9]+", "");
            }
        }

        /// <summary>
        /// Validation method which checks if all fields are not empty
        /// </summary>
        /// <returns></returns>
        private bool AllFieldsAreValidated()
        {
            if(settingDescriptionTxt.Text == "") return false;
            if(screensaverTimeoutTxt.Text == "") return false;
            if(popupTimeoutTxt.Text == "") return false; 
            if(desktopTimeoutTxt.Text == "") return false; 
            if(syncTimeoutTxt.Text == "") return false; 
            if(tickerTimeoutTxt.Text == "") return false;

            return true;
        }
    }
}
