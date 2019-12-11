using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DotNetNuke.Web.Mvc.Helpers;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Modules;

namespace NLDotNet.DNN.Modules.MVCTest.Models
{
    public class ViewModelEditItem : ViewModelBase
    {
        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public Item ItemModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public ItemContent ItemContentModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TextContentToDisplay
        {
            get
            {
                var str = "";
                if((ItemContentModel!=null) && (!string.IsNullOrWhiteSpace(ItemContentModel.nText)))
                {
                    if ((RenderModeSelectedValue.Equals("BASIC", StringComparison.InvariantCultureIgnoreCase)) && (TextRenderModeCheckedValue.Equals("H", StringComparison.InvariantCultureIgnoreCase)))
                        str = (new MvcHtmlString(ItemContentModel.nText)).ToString().Trim();
                    else
                        str = ItemContentModel.nText.Trim();
                }
                return str;
            }
        }
        public override string ModelResourceFile
        {
            get
            {
                return string.IsNullOrWhiteSpace(base.ModelResourceFile)? "/DesktopModules/MVC/MVCTest/App_LocalResources/Item.resx" : base.ModelResourceFile;
            }

            set
            {
                base.ModelResourceFile = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string RenderModeSelectedValue
        {
            get
            {
                var _value = "RICH";
                if (HttpContext.Current.Request.Form["RenderModeList"] != null)
                    _value = (string.IsNullOrWhiteSpace(HttpContext.Current.Request.Form["RenderModeList"].ToString())) ? "" : HttpContext.Current.Request.Form["RenderModeList"].ToString();
                return _value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TextRenderModeCheckedValue
        {
            get
            {
                var _value = "T";
                if (HttpContext.Current.Request.Form["TextRenderModeRBList"] != null)
                    _value = (string.IsNullOrWhiteSpace(HttpContext.Current.Request.Form["TextRenderModeRBList"].ToString())) ? "" : HttpContext.Current.Request.Form["TextRenderModeRBList"].ToString();
                return _value;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GetCurrentVersionTemplateFile()
        {
            if (ItemModel.ItemId > 0)
                return @"_itemCurrentVersion";
            else
                return @"_blank";
            
        }
        /// <summary>
        /// 
        /// </summary>
        public string GetEditorTemplateFile()
        {
            if (RenderModeSelectedValue.Equals("RICH",StringComparison.InvariantCultureIgnoreCase))
                return @"_ckEditor";
            else
                return @"_basicEditor";
            
        }
        /// <summary>
        /// 
        /// </summary>
        public List<SelectListItem> GetRenderModeList()
        {
            
            var RenderModeList = new List<SelectListItem>();
            RenderModeList.Add(new SelectListItem { Text = Localization.GetString("RICH", ModelResourceFile), Value = "RICH", Selected = (RenderModeSelectedValue.Equals("RICH", StringComparison.InvariantCultureIgnoreCase)) });
            RenderModeList.Add(new SelectListItem { Text = Localization.GetString("BASIC", ModelResourceFile), Value = "BASIC", Selected = (RenderModeSelectedValue.Equals("BASIC", StringComparison.InvariantCultureIgnoreCase)) });

            return RenderModeList;
            
        }
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> GetTextRenderModeRBList()
        {
            var TextRenderModeRBList = new Dictionary<string, string>()
            {
                { "T", Localization.GetString("T",ModelResourceFile) },
                { "H", Localization.GetString("H",ModelResourceFile) },
                { "R", Localization.GetString("R",ModelResourceFile) }
            };

            return TextRenderModeRBList;            
        }
        #endregion

        //#region Constructor

        ///// <summary>
        ///// 
        ///// </summary>
        //public ViewModelEditItem()
        //{
        //    ModelResourceFile = "~/Item.resx";
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dnnPortalId"></param>
        //public ViewModelEditItem(string modelResourceFile)
        //{
        //    ModelResourceFile = modelResourceFile;
        //}

        //#endregion        
    }
}