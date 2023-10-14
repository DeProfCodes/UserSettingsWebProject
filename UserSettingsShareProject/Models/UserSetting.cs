using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserSettingsSharedProject.Models
{
    /// <summary>
    /// User Settings Model class
    /// </summary>
    public class UserSetting
    {
        /// <summary>
        /// ID - Identity column
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Setting Description
        /// </summary>
        public string SettingDescription { get; set; }

        /// <summary>
        /// Screen SaverTimeout In Seconds
        /// </summary>
        public long ScreenSaverTimeout { get; set; }

        /// <summary>
        /// Popup Timeout In Seconds
        /// </summary>
        public long PopupTimeout { get; set; }

        /// <summary>
        /// Desktop Timeout In Seconds
        /// </summary>
        public long DesktopTimeout { get; set; }

        /// <summary>
        /// Ticker Timeout In Seconds
        /// </summary>
        public long TickerTimeout { get; set; }

        /// <summary>
        /// Sync Timeout In Seconds
        /// </summary>
        public long SyncTimeout { get; set; }
    }
}