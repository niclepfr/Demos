using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using DotNetNuke.Data.PetaPoco;

using NLDotNet.DNN.Modules.MVCTest.Models;

namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    interface ISettingsManager
    {
        void CreateSetting(Settings t);
        void DeleteSetting(int settingId, int moduleId);
        void DeleteSetting(Settings t);
        IEnumerable<Settings> GetSettings(int moduleId);
        IEnumerable<Settings> GetSettings(string sqlRequest, params object[] sqlParams);
        Settings GetSetting(int settingId, int moduleId);
        string GetSettingValue(int settingId, int moduleId);
        string GetSettingValue(string settingName, int moduleId);
        void UpdateSetting(Settings t);
    }

    class SettingManager : ServiceLocator<ISettingsManager, SettingManager>, ISettingsManager
    {
        public void CreateSetting(Settings t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Settings>();
                rep.Insert(t);
            }
        }

        public void DeleteSetting(int settingId, int moduleId)
        {
            var t = GetSetting(settingId, moduleId);
            DeleteSetting(t);
        }

        public void DeleteSetting(Settings t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Settings>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Settings> GetSettings(int moduleId)
        {
            IEnumerable<Settings> ts;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Settings>();
                ts = rep.Get(moduleId);
            }
            
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlRequest"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public IEnumerable<Settings> GetSettings(string sqlRequest,params object[] sqlParams)
        {
            IEnumerable<Settings> ts = null;
            if(!string.IsNullOrWhiteSpace(sqlRequest))
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                   ts = ctx.ExecuteQuery<Settings>(System.Data.CommandType.Text, sqlRequest,sqlParams);
                }
            }                
            return ts;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public Hashtable GetSettingsCollection(int moduleId)
        {
            var ts = GetSettings(moduleId);
            return new Hashtable((from t in ts select new { SettingName = t.SettingName, SettingValue = t.SettingValue }).ToDictionary(s => s.SettingName));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public Settings GetSetting(int settingId, int moduleId)
        {
            Settings t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Settings>();
                t = rep.GetById(settingId, moduleId);
            }
            return t;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingId"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public string GetSettingValue(int settingId, int moduleId)
        {
            var t = GetSetting(settingId, moduleId);
            return (t != null) ? t.SettingValue : "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingName"></param>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        public string GetSettingValue(string settingName, int moduleId)
        {
            Settings t;
            using (IDataContext ctx = DataContext.Instance())
            {
                t = ctx.ExecuteSingleOrDefault<Settings>(
                    System.Data.CommandType.Text,
                    "select * from MVCTest_Settings where ModuleId=@0 and SettingName=@1",
                    moduleId,
                    settingName
                    );
            }
            return (t != null) ? t.SettingValue : "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        public void UpdateSetting(Settings t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Settings>();
                rep.Update(t);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override System.Func<ISettingsManager> GetFactory()
        {
            return () => new SettingManager();
        }        
    }
}