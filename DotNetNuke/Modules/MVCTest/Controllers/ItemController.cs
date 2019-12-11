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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.Mvc;
using NLDotNet.DNN.Modules.MVCTest.Components;
using NLDotNet.DNN.Modules.MVCTest.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Modules.Html;
using DotNetNuke.Modules.Html.Components;
using DotNetNuke.Services.Localization;
using System.Web.Routing;

namespace NLDotNet.DNN.Modules.MVCTest.Controllers
{
    [DnnHandleError]
    public class ItemController : ModuleController
    {
        /// <summary>
        /// 
        /// </summary>
        private string ItemDisplayOrder
        {
            get
            {
                return SettingManager.Instance.GetSettingValue("ItemDisplayOrder", ModuleContext.ModuleId);
            }
        }

        private string DnnModuleName
        {
            get
            {
                return "MVCTest";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string DnnBaseMvcModulesFolderPath
        {
            get
            {
                return "/DesktopModules/MVC/";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string DnnMvcModuleFolderPath
        {
            get
            {
                return string.Format("{0}{1}/", DnnBaseMvcModulesFolderPath, DnnModuleName);
            }
        }

        //private string RenderModeSelectedValue
        //{
        //    get
        //    {
        //        var _value = "RICH";
        //        if (Request.Form["RenderModeList"] != null)
        //            _value = (string.IsNullOrWhiteSpace(Request.Form["RenderModeList"].ToString())) ? "" : Request.Form["RenderModeList"].ToString();
        //        return _value;
        //    }
        //}

        //private string TextRenderModeCheckedValue
        //{
        //    get
        //    {
        //        var _value = "T";
        //        if (Request.Form["TextRenderModeRBList"] != null)
        //            _value = (string.IsNullOrWhiteSpace(Request.Form["TextRenderModeRBList"].ToString())) ? "" : Request.Form["TextRenderModeRBList"].ToString();
        //        return _value;
        //    }
        //}

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }        

        #region Action Edit

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return ViewEdit();
        }

        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(IEnumerable<Item> items)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);
            
            try
            {
                var formHttpRqs = HttpContext.Request.Form;
                if (formHttpRqs == null) { return ViewEdit(); }

                var formKeys = formHttpRqs.Keys;
                if ((formKeys == null) && (formKeys.Count == 0)) { return ViewEdit(); }

                //le nom des button submit est "submitItem_n" où n est la valeur de l'indice de l'item associé
                Regex rx = new Regex(@"^submitItem_[0-9]+$", RegexOptions.IgnoreCase);
                var formActionKeys = formKeys.Cast<string>().ToList().Where(_s => rx.IsMatch(_s));
                //Item peut être identifié uniquement si un seul button submit est présent dans le formulaire
                if (formActionKeys.Count() == 1)
                {
                    var formActionKeyName = formActionKeys.ElementAt(0);
                    var formActionKey = formActionKeyName.Split('_');
                    //var actionButtonName = formActionKey[0];
                    var actionButtonIndice = formActionKey[1];
                    var actionName = formHttpRqs[formActionKeyName].ToString();
                    var actionItemId = (formHttpRqs["itemId_" + actionButtonIndice] == null)
                        ? -1
                        : MVCModuleBase.StringToInt(formHttpRqs["itemId_" + actionButtonIndice].ToString(),-1);
                    if(actionItemId>0)
                    {
                        switch(actionName.ToLower())
                        {
                            case "deleteitem":
                                ItemManager.Instance.DeleteItem(actionItemId, ModuleContext.ModuleId);
                                break;
                            case "activeitem":
                                ActiveItem(actionItemId);
                                break;
                            default:
                                break;
                        }
                    }

                }                
            }
            catch(Exception Ex) {
                
            }
            return ViewEdit();
            //return RedirectToDefaultRoute();
        }

        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult DeleItem(IEnumerable<Item> items, int itemId = -1)
        {


            return ViewEdit();
        }

        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult ActiveItem(IEnumerable<Item> items, int itemId = -1)
        {
            try
            {
                if (itemId > 0)
                {
                    var item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);
                    if ((item != null) && (item.ItemId > 0))
                    {
                        item.ItemIsPub = !item.ItemIsPub;
                        item.ItemModifUserID = User.UserID;
                        item.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                        ItemManager.Instance.UpdateItem(item);
                    }
                }
            }
            catch { }

            return ViewEdit();
        }

        /// <summary>
        /// 
        /// </summary>
        private ActionResult ViewEdit()
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            ModelState.Clear();
            var itemsActive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest((ItemDisplayOrder.Equals("ASC", StringComparison.InvariantCultureIgnoreCase)) ? "GetItemsActiveOrderByDateDePubASC" : "GetItemsActiveOrderByDateDePubDESC")
                , ModuleContext.ModuleId
                );

            var itemsToActive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest("GetItemsToActive")
                , ModuleContext.ModuleId
                );

            var itemsArchive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest("GetItemsArchive")
                , ModuleContext.ModuleId
                );

            ViewBag.itemsToActive = itemsToActive;
            ViewBag.itemsArchive = itemsArchive.ToArray();

            return View("Edit", itemsActive);
        }

        #endregion

        #region Action EditItem

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ActionResult EditItem(int itemId = -1)
        {
            return ViewEditItem(itemId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult EditItem(ViewModelEditItem modelView)
        {
            try
            {
                var item = modelView.ItemModel;
                var itemContent = modelView.ItemContentModel;

                if ((ModelState.IsValid) && (item != null))
                {
                    if (item.ItemId == -1)
                    {
                        item.ModuleId = ModuleContext.ModuleId;
                        item.ItemIsPub = false;
                        item.ItemCreaUserID = User.UserID;
                        item.ItemCreaDate = DateTime.UtcNow.ToLocalTime();
                        item.ItemModifUserID = User.UserID;
                        item.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                        ItemManager.Instance.CreateItem(item);
                    }
                    else
                    {
                        var existingItem = ItemManager.Instance.GetItem(item.ItemId, ModuleContext.ModuleId);
                        existingItem.ItemModifUserID = User.UserID;
                        existingItem.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                        existingItem.ItemName = item.ItemName;
                        existingItem.ItemDescription = item.ItemDescription;
                        existingItem.ItemPubDateStart = item.ItemPubDateStart;
                        existingItem.ItemPubDateEnd = item.ItemPubDateEnd;
                        ItemManager.Instance.UpdateItem(existingItem);
                        item = existingItem;
                    }
                    //Sauvegarde le contenu html de l'item
                    if ((item.ItemId > 0) && (itemContent != null))
                    {
                        var aliases = from PortalAliasInfo pa in PortalAliasController.Instance.GetPortalAliasesByPortalId(PortalSettings.PortalId)
                                      select pa.HTTPAlias;

                        string content;
                        content = itemContent.nText;
                        if (Request.QueryString["nuru"] == null)
                        {
                            content = HtmlUtils.AbsoluteToRelativeUrls(content, aliases);
                        }

                        var htmlContent = ItemContentManager.Instance.GetItemContent(item.ItemId) ?? new ItemContent() { ItemId = item.ItemId, ModuleID = ModuleContext.ModuleId };
                        htmlContent.CreaUserID = User.UserID;
                        htmlContent.ModifUserID = User.UserID;
                        htmlContent.nText = content;
                        ItemContentManager.Instance.UpdateItemContent(htmlContent, ItemContentManager.Instance.GetMaximumVersionHistory(ModuleContext.PortalId));
                        modelView.ItemContentModel = htmlContent;
                    }
                    modelView.ItemModel = item;                    
                }
            }
            catch { }
            
            ModelState.Clear();
            //return View("EditItem", modelView);
            return ViewEditItem((modelView.ItemModel != null) ? modelView.ItemModel.ItemId : -1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult RenderModeListSelectedIndexChanged(ViewModelEditItem modelView)
        {
            LoadViewEditItemStuff();
            return View("EditItem", modelView);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult TextRenderModeRBListCheckedIndexChanged(ViewModelEditItem modelView)
        {
            LoadViewEditItemStuff();
            ModelState.Clear();
            return View("EditItem", modelView);
        }

        /// <summary>
        /// 
        /// </summary>
        private ActionResult ViewEditItem(int itemId = -1)
        {
            //le viewbag
            LoadViewEditItemStuff();

            //item
            var _item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);
            //son contenu
            var _itemContent = ItemContentManager.Instance.GetItemContent(_item.ItemId) ?? new ItemContent() { ItemId = _item.ItemId, ModuleID = ModuleContext.ModuleId };
            if (string.IsNullOrWhiteSpace(_itemContent.nText))
                _itemContent.nText = Localization.GetString("AddContent", LocalResourceFile);
            _itemContent.TextInHtmlFormat = FormatToHtmlText(_itemContent.nText);

            //le model
            var viewModel = new ViewModelEditItem();
            viewModel.ItemModel = _item;
            viewModel.ItemContentModel = _itemContent;
            
            return View("EditItem", viewModel);
        }        

        #endregion

        #region Action Index

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult Index()
        {
            var _itemDisplayOrder = ItemDisplayOrder;
            //var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            //var items = ItemHtmlTextManager.Instance.GetItemsHTMLText(ModuleContext.ModuleId);
            var items = ItemContentManager.Instance.GetItemsToView(ModuleContext.ModuleId, (string.IsNullOrWhiteSpace(_itemDisplayOrder)) ? "ASC" : _itemDisplayOrder);
            
            return View(items);
        }



        #endregion

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult RedirectToError()
        {
            return Redirect(Url.Action("Error", "Item"));
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <returns></returns>
        //public PartialViewResult ItemCurrentVersion(int itemId)
        //{
        //    var _item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);
        //    var _itemContent = ItemContentManager.Instance.GetItemContent(_item.ItemId);
        //    if (string.IsNullOrWhiteSpace(_itemContent.nText))
        //        _itemContent.nText = Localization.GetString("AddContent", LocalResourceFile); //System.Web.HttpUtility.JavaScriptStringEncode("<script>alert('lalala');</script>");
        //    _itemContent.TextInHtmlFormat = FormatToHtmlText(_itemContent.nText);

        //    var viewModel = new ViewModelEditItem();
        //    viewModel.ItemModel = _item;

        //    return PartialView("_itemCurrentVersion", viewModel);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        private void ActiveItem(int itemId)
        {
            try
            {
                if (itemId > 0)
                {
                    var item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);
                    if ((item != null) && (item.ItemId > 0))
                    {
                        item.ItemIsPub = !item.ItemIsPub;
                        item.ItemModifUserID = User.UserID;
                        item.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                        ItemManager.Instance.UpdateItem(item);
                    }
                }
            }
            catch { }            
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadViewEditItemStuff()
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            //editeur
            LoadCKEditor();

            ////liste du mode de rendu editeur
            //LoadRenderModeList();

            ////liste mode de rendu du texte
            //LoadTextRenderModeRBList();
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadCKEditor()
        {
            //editeur html + les fichiers css et js associés + la liste de sélection du mode de rendu de l'editeur + la liste de selection du rendu du texte de l'editeur
            var ckeSettings = new CKEditorManager(ModuleContext.PortalId, ModuleContext.ModuleId, ModuleContext.TabId, User, DnnMvcModuleFolderPath);

            ViewBag.CKEditorJSScript = ckeSettings.GetStartupJSScript();
            ViewBag.CKEditorControlName = ckeSettings.CKEditorControlName;
            ViewBag.JQDateTimePickerCSSFile = string.Format("~{0}Content/jquery.datetimepicker.css", DnnMvcModuleFolderPath);
            ViewBag.JQDateTimePickerJSFile = string.Format("~{0}Scripts/jquery.datetimepicker.js", DnnMvcModuleFolderPath);
            ViewBag.CKEditorToolBarsCSSFile = string.Format("~{0}css/CKEditorToolBars.css", ckeSettings.CKEditorFolderPathBase);
            ViewBag.CKEditorCSSFile = string.Format("~{0}css/CKEditorOverride.css", ckeSettings.CKEditorFolderPathBase);
            ViewBag.CKEditorJSFile = string.Format("~{0}js/ckeditor/4.5.3/ckeditor.js", ckeSettings.CKEditorFolderPathBase);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //private void LoadRenderModeList()
        //{
        //    var RenderModeList = new List<SelectListItem>();
        //    RenderModeList.Add(new SelectListItem { Text = Localization.GetString("RICH", LocalResourceFile), Value = "RICH", Selected = (RenderModeSelectedValue.Equals("RICH", StringComparison.InvariantCultureIgnoreCase)) });
        //    RenderModeList.Add(new SelectListItem { Text = Localization.GetString("BASIC", LocalResourceFile), Value = "BASIC", Selected = (RenderModeSelectedValue.Equals("BASIC", StringComparison.InvariantCultureIgnoreCase)) });

        //    ViewBag.RenderModeList = RenderModeList;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        //private void LoadTextRenderModeRBList()
        //{
        //    var TextRenderModeRBList = new Dictionary<string, string>()
        //    {
        //        { "T", Localization.GetString("T",LocalResourceFile) },
        //        { "H", Localization.GetString("H",LocalResourceFile) },
        //        { "R", Localization.GetString("R",LocalResourceFile) }
        //    };

        //    ViewBag.TextRenderModeRBList = TextRenderModeRBList;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_html"></param>
        private string FormatToHtmlText(string _nText)
        {
            var repo = new HtmlModuleSettingsRepository();
            var settings = repo.GetSettings(ModuleContext.Configuration);
            return HtmlTextController.FormatHtmlText(ModuleContext.ModuleId, _nText, settings, ModuleContext.PortalSettings, (Page)HttpContext.CurrentHandler);
        }        
    }
}
