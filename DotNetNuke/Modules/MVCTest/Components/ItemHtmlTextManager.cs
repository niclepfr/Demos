using DotNetNuke.Data;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Framework;
using DotNetNuke.Services.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using NLDotNet.DNN.Modules.MVCTest.Models;

namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    interface IItemHtmlTextManager
    {
        void CreateItemHTMLText(ItemHTMLText t, int iMaxHistorique);
        void DeleteItemHTMLText(ItemHTMLText t);
        void DeleteItemHTMLText(int iHtmlTextID, int itemId);
        ItemHTMLText GetItemHTMLText(int itemId, int iHtmlTextID = -1);
        IEnumerable<ItemHTMLText> GetItemsHTMLText(int itemId);
        IEnumerable<ItemHTMLText> GetItemsHTMLText(string sqlRequest, params object[] sqlParams);
        void UpdateItemHTMLText(ItemHTMLText t, int iMaxHistorique);
        int GetMaximumVersionHistory(int PortalID);
        IEnumerable<ItemHTMLText> GetItemsToView(int ModuleId, string sOrder = "ASC");
        string GetItemHTMLTextSQLRequest(string sqlRequestName);
    }

    class ItemHtmlTextManager : ServiceLocator<IItemHtmlTextManager, ItemHtmlTextManager>, IItemHtmlTextManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override System.Func<IItemHtmlTextManager> GetFactory()
        {
            return () => new ItemHtmlTextManager();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   CreateItemHTMLText ajoute une ligne à la table MVCTest_Items_HTMLText
        /// </summary>
        /// <remarks>
        /// Supprime si besoin les versions anciennes
        /// </remarks>
        /// <param name = "t">la ligne à insérer</param>
        /// <param name = "iMaxHistorique">le nombre max de version conservée</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void CreateItemHTMLText(ItemHTMLText t, int iMaxHistorique)
        {
            int iVersion = 0;

            using (IDataContext ctx = DataContext.Instance())
            {
                iVersion = MCVTestBase.StringToInt(ctx.ExecuteScalar<Int32>(CommandType.Text, "select ISNULL(max(VersionNumber),0) AS VersionNumber from MVCTest_Items_HTMLText where ItemID=@0", t.ItemId).ToString(), 0);

                iVersion++;
                t.VersionNumber = iVersion;
                t.CreaDate = DateTime.UtcNow.ToLocalTime();
                t.ModifDate = DateTime.UtcNow.ToLocalTime();

                var rep = ctx.GetRepository<ItemHTMLText>();
                rep.Insert(t);

                if (iMaxHistorique == 0) { iMaxHistorique = 5; }
                ctx.Execute(CommandType.Text, "delete from MVCTest_Items_HTMLText where ItemID=@0 and VersionNumber <= (@1 - @2)", t.ItemId, iVersion, iMaxHistorique);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   DeleteItemHTMLText supprime une ligne à la table MVCTest_Items_HTMLText
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "t">la ligne à supprimer</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteItemHTMLText(ItemHTMLText t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemHTMLText>();
                rep.Delete(t);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   DeleteItemHTMLText supprime une ligne à la table MVCTest_Items_HTMLText
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "iHtmlTextID">l'ID de la ligne à supprimer</param>
        /// <param name = "itemId">la scope value</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteItemHTMLText(int iHtmlTextID, int itemId)
        {
            ItemHTMLText t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemHTMLText>();
                t = rep.GetById(iHtmlTextID, itemId);
                rep.Delete(t);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetItemHTMLText retourne :
        /// la ligne associée à son id (iHtmlTextID) si celui-ci est spécifié
        /// la ligne la plus récemment modifiée associée à l'Item (itemId)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="itemId">l'ID de l'Item (FilInfo)</param>
        /// <param name="iHtmlTextID"></param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ItemHTMLText GetItemHTMLText(int itemId, int iHtmlTextID = -1)
        {
            ItemHTMLText t = null;
            try
            {
                if (MCVTestBase.StringToInt(itemId.ToString()) > 0)
                {
                    if (MCVTestBase.StringToInt(iHtmlTextID.ToString()) > 0)
                    {
                        using (IDataContext ctx = DataContext.Instance())
                        {
                            var rep = ctx.GetRepository<ItemHTMLText>();
                            t = rep.GetById(iHtmlTextID, itemId);                       
                        }
                    }
                    else
                        t = GetItemsHTMLText(itemId).OrderByDescending(x => x.ModifDate).First();
                }

            }
            catch(Exception ex)
            {
                Exceptions.LogException(ex);
            }
            return t;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///  GetItemsHTMLText retourne les lignes de la table MVCTest_Items_HTMLText associées à l'Item (FilInfo)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "itemId">l'ID de l'Item (FilInfo)</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public IEnumerable<ItemHTMLText> GetItemsHTMLText(int itemId)
        {
            IEnumerable<ItemHTMLText> ts;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemHTMLText>();
                ts = rep.Get(itemId);
            }
            return ts;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///  GetItemsHTMLText retourne les lignes de la table MVCTest_Items_HTMLText associées à l'Item (FilInfo)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "iItemID">l'ID de l'Item (FilInfo)</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public IEnumerable<ItemHTMLText> GetItemsHTMLText(string sqlRequest, params object[] sqlParams)
        {
            IEnumerable<ItemHTMLText> ts;
            using (IDataContext ctx = DataContext.Instance())
            {
                ts = ctx.ExecuteQuery<ItemHTMLText>(CommandType.Text, sqlRequest, sqlParams);
            }
            return ts;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   UpdateItemHTMLText redirige vers CreateItemHTMLText gestion de version oblige
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "t">la ligne à mettre à jour</param>
        /// <param name = "iMaxHistorique">le nombre max de version conservée</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void UpdateItemHTMLText(ItemHTMLText t, int iMaxHistorique)
        {
            CreateItemHTMLText(t, iMaxHistorique);
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   GetItemsToView charge les éléments du module à afficher filtrer sur la date de publication, la publication.
        ///   C'est le num de version le plus grand de l'élément qui est chargé.
        ///   Le tri s'effectue en fonction du paramètre SDFilInfo_Settings.sItemOrdre.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="ModuleId">ID de l'item </param>
        /// <param name="sOrder"></param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public IEnumerable<ItemHTMLText> GetItemsToView(int ModuleId, string sOrder = "ASC")
        {
            IEnumerable<ItemHTMLText> ts;
            List<ItemHTMLText> tl = null;
            ItemHTMLText t = null;
            StringBuilder sSQL = new StringBuilder();
            using (IDataContext ctx = DataContext.Instance())
            {
                IEnumerable<Item> its = ItemManager.Instance.GetItems(
                    ItemManager.Instance.GetItemsSQLRequest(
                        (sOrder.Equals("ASC", StringComparison.InvariantCultureIgnoreCase) ? "GetActiveItemsOrderByDateDePubASC" : "GetActiveItemsOrderByDateDePubDESC"))
                    , ModuleId
                    );
                foreach (var it in its ?? Enumerable.Empty<Item>())
                {
                    t = ctx.ExecuteSingleOrDefault<ItemHTMLText>(CommandType.Text, GetItemHTMLTextSQLRequest("GetItemsToView"), it.ItemId);
                    if (tl == null) { tl = new List<ItemHTMLText>(); }
                    tl.Add(t);
                }
                ts = (IEnumerable<ItemHTMLText>)tl;
            }

            return ts ?? Enumerable.Empty<ItemHTMLText>();
        }


        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   GetMaximumVersionHistory retrieves the maximum number of versions to store for a module.
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "PortalID">The ID of the Portal</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public int GetMaximumVersionHistory(int PortalID)
        {
            int intMaximumVersionHistory = -1;

            // get from portal settings
            intMaximumVersionHistory = int.Parse(PortalController.GetPortalSetting("MaximumVersionHistory", PortalID, "-1"));

            // if undefined at portal level, set portal default
            if (intMaximumVersionHistory == -1)
            {
                intMaximumVersionHistory = 5;
                // default
                PortalController.UpdatePortalSetting(PortalID, "MaximumVersionHistory", intMaximumVersionHistory.ToString());
            }
            return intMaximumVersionHistory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlRequestName"></param>
        /// <returns></returns>
        public string GetItemHTMLTextSQLRequest(string sqlRequestName)
        {
            var sb = new StringBuilder();

            switch (sqlRequestName)
            {
                case "GetItemHTMLTextVersions":
                    sb.Append("SELECT tb1.ID, tb1.ItemID, tb1.ModuleID, tb1.VersionNumber, tb1.CreaDate, tb1.CreaUserID, tb1.ModifDate, tb1.ModifUserID, (tb2.FirstName + ' ' + tb2.LastName) AS UserFullName")
                        .Append(" FROM MVCTest_Items_HTMLText AS tb1 INNER JOIN Users AS tb2 ON tb1.ModifUserID=tb2.UserID")
                        .Append(" WHERE tb1.ItemID=@0")
                        .Append(" ORDER BY tb1.VersionNumber DESC");
                    break;
                case "GetItemsToView":
                    sb.Append("SELECT TOP 1 tb1.*, tb2.ItemPubDateStart AS ItemPubDateStart")
                        .Append(" FROM SDFilInfo_Items_HTMLText AS tb1")
                        .Append(" INNER JOIN SDFilInfo_Items AS tb2")
                        .Append(" ON tb1.ItemID=tb2.Id")
                        .Append(" WHERE tb1.ItemID=@0")
                        .Append(" ORDER BY tb1.VersionNumber DESC");
                    break;
            }

            return sb.ToString();
        }

    }
}