using System;
using System.Collections.Generic;
using System.Linq;
using PolyFilms.Common;
using PolyFilms.Data.Entity;
using PolyFilms.Data.Repositories;

namespace PolyFilms.Data.CustomModel
{
    public static class SettingConfig
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        public static string MailServerHost;
        /// <summary>
        /// 
        /// </summary>
        public static string MailServerPassword;
        /// <summary>
        /// 
        /// </summary>
        public static string MailServerFromMail;
        /// <summary>
        /// 
        /// </summary>
        public static int MailServerPort;
        /// <summary>
        /// 
        /// </summary>
        public static bool MailServerEnableSsl;
        /// <summary>
        /// 
        /// </summary>
        public static string MailServerEmail;
        /// <summary>
        /// 
        /// </summary>
        public static bool SendErrorEmail;
        /// <summary>
        /// 
        /// </summary>
        public static string ErrorEmail;

        /// <summary>
        /// 
        /// </summary>
        public static string ReportServerURL;

        /// <summary>
        /// 
        /// </summary>
        public static List<TblAuditSettings> AuditSettings { get; set; }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        public static void InitilizeSettings()
        {
            PolyFilmsContext context = BaseContext.GetDbContext();

            List<TblSetting> settings = context.TblSetting.ToList();

            foreach (TblSetting setting in settings)
            {
                switch (setting.KeyName)
                {
                    case TblSetting.MailServerHost:
                        MailServerHost = setting.Value;
                        break;
                    case TblSetting.MailServerPassword:
                        MailServerPassword = setting.Value;
                        break;
                    case TblSetting.MailServerFromMail:
                        MailServerFromMail = setting.Value;
                        break;
                    case TblSetting.MailServerPort:
                        Int32.TryParse(setting.Value, out MailServerPort);
                        break;
                    case TblSetting.MailServerEnableSSL:
                        MailServerEnableSsl = Convert.ToBoolean(setting.Value);
                        break;
                    case TblSetting.MailServerEmail:
                        MailServerEmail = setting.Value;
                        break;
                    case TblSetting.SendErrorEmail:
                        SendErrorEmail = Convert.ToBoolean(setting.Value);
                        break;
                    case TblSetting.ErrorEmail:
                        ErrorEmail = setting.Value;
                        break;
                    case TblSetting.ReportServerURL:
                        ReportServerURL = setting.Value;
                        break;
                }
            }

            AuditSettings = context.TblAuditSettings.ToList();

            EmailHelper.MailServerEmail = MailServerEmail;
            EmailHelper.MailServerHost = MailServerHost;
            EmailHelper.MailServerPort = MailServerPort;
            EmailHelper.MailServerPassword = MailServerPassword;
            EmailHelper.MailServerEnableSsl = MailServerEnableSsl;
            EmailHelper.MailServerFromMail = MailServerFromMail;
        }

        #endregion
    }
}
