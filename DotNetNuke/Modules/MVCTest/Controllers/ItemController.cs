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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.Mvc;
using NLDotNet.DNN.Modules.MVCTest.Components;
using NLDotNet.DNN.Modules.MVCTest.Models;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.Modules.Html;
using DotNetNuke.Modules.Html.Components;
using DotNetNuke.Services.Localization;
using System.Web.Routing;

namespace NLDotNet.DNN.Modules.MVCTest.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {
        /// <summary>
        /// 
        /// </summary>
        private string _itemDisplayOrder = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            LocalResourceFile = LocalResourceFile.Replace(".resx", ".fr-FR.resx");
            _itemDisplayOrder = SettingManager.Instance.GetSettingValue("ItemDisplayOrder", ModuleContext.ModuleId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ActionResult Delete(int itemId)
        {
            ItemManager.Instance.DeleteItem(itemId, ModuleContext.ModuleId);
            return RedirectToDefaultRoute();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var itemsActive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest((_itemDisplayOrder.Equals("ASC",StringComparison.InvariantCultureIgnoreCase))? "GetItemsActiveOrderByDateDePubASC" : "GetItemsActiveOrderByDateDePubDESC")
                ,ModuleContext.ModuleId
                );

            var itemsToActive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest("GetItemsToActive")
                , ModuleContext.ModuleId
                );

            var itemsArchive = ItemManager.Instance.GetItems(
                ItemManager.Instance.GetItemsSQLRequest("GetItemsArchive")
                , ModuleContext.ModuleId
                );

            ViewBag.itemsToActive = itemsArchive;/* itemsToActive;*/
            ViewBag.itemsArchive = itemsArchive;
            return View(itemsActive);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult Edit(Item item)
        {
            if (ModelState.IsValid)
            {
                if (item.ItemId == -1)
                {
                    item.ItemCreaUserID = User.UserID;
                    item.ItemCreaDate = DateTime.UtcNow;
                    item.ItemModifUserID = User.UserID;
                    item.ItemModifDate = DateTime.UtcNow;

                    ItemManager.Instance.CreateItem(item);
                }
                else
                {
                    var existingItem = ItemManager.Instance.GetItem(item.ItemId, item.ModuleId);
                    existingItem.ItemModifUserID = User.UserID;
                    existingItem.ItemModifDate = DateTime.UtcNow;
                    existingItem.ItemName = item.ItemName;
                    existingItem.ItemDescription = item.ItemDescription;


                    ItemManager.Instance.UpdateItem(existingItem);
                }
            }

            return RedirectToDefaultRoute();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ActionResult EditItem(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            var _item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);


            return View(_item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [DotNetNuke.Web.Mvc.Framework.ActionFilters.ValidateAntiForgeryToken]
        public ActionResult EditItem(Item item)
        {
            if (ModelState.IsValid)
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
                    var existingItem = ItemManager.Instance.GetItem(item.ItemId, item.ModuleId);
                    existingItem.ItemModifUserID = User.UserID;
                    existingItem.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                    existingItem.ItemName = item.ItemName;
                    existingItem.ItemDescription = item.ItemDescription;


                    ItemManager.Instance.UpdateItem(existingItem);
                }                                
            }
            ModelState.Clear();
            return View("EditItem",item);            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemId"></param>
        /// <returns></returns>
        public ActionResult ActiveItem(int itemId = -1)
        {
            DotNetNuke.Framework.JavaScriptLibraries.JavaScript.RequestRegistration(CommonJs.DnnPlugins);

            if(itemId>0)
            {
                var _item = ItemManager.Instance.GetItem(itemId, ModuleContext.ModuleId);
                if ((_item != null) && (_item.ItemId > 0))
                {
                    _item.ItemIsPub = !_item.ItemIsPub;
                    _item.ItemModifUserID = User.UserID;
                    _item.ItemModifDate = DateTime.UtcNow.ToLocalTime();
                    ItemManager.Instance.UpdateItem(_item);
                }
            }            
            return View(
                ItemHtmlTextManager.Instance.GetItemsToView(ModuleContext.ModuleId,
                (string.IsNullOrWhiteSpace(_itemDisplayOrder)) ? "ASC" : _itemDisplayOrder)
                );
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult Index()
        {
            //var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            //var items = ItemHtmlTextManager.Instance.GetItemsHTMLText(ModuleContext.ModuleId);

            var items = ItemHtmlTextManager.Instance.GetItemsToView(ModuleContext.ModuleId, (string.IsNullOrWhiteSpace(_itemDisplayOrder)) ? "ASC" : _itemDisplayOrder);
            
            return View(items);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_html"></param>
        public string FormatToHtmlText(string _nText)
        {
            return HtmlTextController.FormatHtmlText(ModuleContext.ModuleId ,_nText, new HtmlModuleSettings(), ModuleContext.PortalSettings,(Page)HttpContext.CurrentHandler);
        }

    }
}
