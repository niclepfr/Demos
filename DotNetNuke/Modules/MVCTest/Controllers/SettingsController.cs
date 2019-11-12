/*
' Copyright (c) 2019 niclep.fr
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Collections;
using System.Web.Mvc;
using DotNetNuke.Security;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLDotNet.DNN.Modules.MVCTest.Components;
using NLDotNet.DNN.Modules.MVCTest.Models;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Services.Localization;
using System.Web.Routing;

namespace NLDotNet.DNN.Modules.MVCTest.Controllers
{
    [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.Edit)]
    [DnnHandleError]
    public class SettingsController : DnnController
    {

        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> SettingsByDefaut
        {
            get
            {
                return new Dictionary<string, string>() { { "ItemDisplayOrder", "desc" }, { "Animation", "none" } };
            }
        }

        #endregion


        #region EventHandlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            LocalResourceFile = LocalResourceFile.Replace(".resx",".fr-FR.resx");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Settings()
        {
            //var settings = new Models.Settings();
            //settings.Setting1 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("MVCTest_Setting1", false);
            //settings.Setting2 = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("MVCTest_Setting2", System.DateTime.Now);

            var rblItemDisplayOrder = new Dictionary<string, string>()
            {
                { "asc", Localization.GetString("ASC",LocalResourceFile) },
                { "desc", Localization.GetString("DESC",LocalResourceFile) }
            };

            var rblItemAnimation = new Dictionary<string, string>()
            {
                { "none", Localization.GetString("None",LocalResourceFile) },
                { "slide", Localization.GetString("Slide",LocalResourceFile) },
                { "scroll", Localization.GetString("Scroll",LocalResourceFile) }
            };

            var settings = SettingManager.Instance.GetSettings(ModuleContext.ModuleId).ToList();
            foreach (var _key in SettingsByDefaut.Keys)
            {
                if (!(settings.Select(stg => stg.SettingName).Contains(_key)))
                    settings.Add(new Settings()
                    {
                        CreatedByUserId = User.UserID,
                        CreatedOnDate = DateTime.UtcNow.ToLocalTime(),
                        LastModifiedByUserId = User.UserID,
                        LastModifiedOnDate = DateTime.UtcNow.ToLocalTime(),
                        ModuleId = ModuleContext.ModuleId,
                        SettingId = -1,
                        SettingName = _key,
                        SettingValue = SettingsByDefaut[_key].ToString()
                    });
            }
            ViewBag.rblItemDisplayOrder = rblItemDisplayOrder;
            ViewBag.rblItemAnimation = rblItemAnimation;

            return View(settings);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="supportsTokens"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateInput(false)]
        //[DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        //public ActionResult Settings(Models.Settings settings)
        //{
        //    ModuleContext.Configuration.ModuleSettings["MVCTest_Setting1"] = settings.Setting1.ToString();
        //    ModuleContext.Configuration.ModuleSettings["MVCTest_Setting2"] = settings.Setting2.ToUniversalTime().ToString("u");

        //    return RedirectToDefaultRoute();
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supportsTokens"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Settings(IList<Settings> settings)
        {
            foreach (var setting in settings)
            {
                var setng = SettingManager.Instance.GetSettings(GetSettingsBySettingNameSQL(), ModuleContext.ModuleId, setting.SettingName).FirstOrDefault();
                
                if ((setng != null) && (setng.SettingId > 0))
                {
                    setng.SettingValue = setting.SettingValue;
                    setng.LastModifiedByUserId = User.UserID;
                    setng.LastModifiedOnDate = DateTime.UtcNow.ToLocalTime();
                    SettingManager.Instance.UpdateSetting(setng);
                }                    
                else
                {
                    setng = new Models.Settings() {
                        CreatedByUserId = User.UserID,
                        CreatedOnDate = DateTime.UtcNow.ToLocalTime(),
                        LastModifiedByUserId = User.UserID,
                        LastModifiedOnDate = DateTime.UtcNow.ToLocalTime(),
                        ModuleId = ModuleContext.ModuleId,
                        SettingId = -1,
                        SettingName = setting.SettingName,
                        SettingValue = setting.SettingValue
                    };
                    SettingManager.Instance.CreateSetting(setng);
                }                    
            }            
            return RedirectToDefaultRoute();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetSettingsBySettingNameSQL()
        {
            var sb = new StringBuilder()
                .Append("select * from MVCTest_Settings where ModuleId=@0")
                .Append(" and SettingName=@1");
            return sb.ToString();
        }


        #endregion
    }
}