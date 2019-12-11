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
    interface IItemContentManager
    {
        void CreateItemContent(ItemContent t, int iMaxHistorique);
        void DeleteItemContent(ItemContent t);
        void DeleteItemContent(int itemId);
        void DeleteItemContent(int itemId, int iContentID);        
        ItemContent GetItemContent(int itemId, int iContentID = -1);
        IEnumerable<ItemContent> GetItemsContent(int itemId);
        IEnumerable<ItemContent> GetItemsContent(string sqlRequest, params object[] sqlParams);
        void UpdateItemContent(ItemContent t, int iMaxHistorique);
        int GetMaximumVersionHistory(int PortalID);
        IEnumerable<ItemContent> GetItemsToView(int ModuleId, string sOrder = "ASC");
        string GetItemContentSQLRequest(string sqlRequestName);
    }

    class ItemContentManager : ServiceLocator<IItemContentManager, ItemContentManager>, IItemContentManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override System.Func<IItemContentManager> GetFactory()
        {
            return () => new ItemContentManager();
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   CreateItemContent ajoute une ligne à la table MVCTest_Items_Content
        /// </summary>
        /// <remarks>
        /// Supprime si besoin les versions anciennes
        /// </remarks>
        /// <param name = "t">la ligne à insérer</param>
        /// <param name = "iMaxHistorique">le nombre max de version conservée</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void CreateItemContent(ItemContent t, int iMaxHistorique)
        {
            int iVersion = 0;

            using (IDataContext ctx = DataContext.Instance())
            {
                iVersion = MVCModuleBase.StringToInt(ctx.ExecuteScalar<Int32>(CommandType.Text, "select ISNULL(max(VersionNumber),0) AS VersionNumber from MVCTest_Items_Content where ItemID=@0", t.ItemId).ToString(), 0);

                iVersion++;
                t.VersionNumber = iVersion;
                t.CreaDate = DateTime.UtcNow.ToLocalTime();
                t.ModifDate = DateTime.UtcNow.ToLocalTime();

                var rep = ctx.GetRepository<ItemContent>();
                rep.Insert(t);

                if (iMaxHistorique == 0) { iMaxHistorique = 5; }
                ctx.Execute(CommandType.Text, "delete from MVCTest_Items_Content where ItemID=@0 and VersionNumber <= (@1 - @2)", t.ItemId, iVersion, iMaxHistorique);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   DeleteItemContent supprime une ligne à la table MVCTest_Items_Content
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "itemId">la scope value</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteItemContent(int itemId)
        {
            IEnumerable<ItemContent> ts;
            try
            {
                if (itemId > 0)
                {
                    ts = GetItemsContent(itemId);
                    if ((ts != null) && (ts.Count() > 0))
                    {
                        foreach (var t in ts)
                        {
                            DeleteItemContent(t);
                        }
                    }
                }
            }
            catch { }                
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   DeleteItemContent supprime une ligne à la table MVCTest_Items_Content
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "t">la ligne à supprimer</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteItemContent(ItemContent t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemContent>();
                rep.Delete(t);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///   DeleteItemContent supprime une ligne à la table MVCTest_Items_Content
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "iContentID">l'ID de la ligne à supprimer</param>
        /// <param name = "itemId">la scope value</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void DeleteItemContent(int itemId, int iContentID)
        {
            ItemContent t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemContent>();
                t = rep.GetById(iContentID, itemId);
                rep.Delete(t);
            }
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        /// GetItemContent retourne :
        /// la ligne associée à son id (iContentID) si celui-ci est spécifié
        /// la ligne la plus récemment modifiée associée à l'Item (itemId)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="itemId">l'ID de l'Item (FilInfo)</param>
        /// <param name="iContentID"></param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public ItemContent GetItemContent(int itemId, int iContentID = -1)
        {
            ItemContent t = null;
            try
            {
                if (MVCModuleBase.StringToInt(itemId.ToString()) > 0)
                {
                    if (MVCModuleBase.StringToInt(iContentID.ToString()) > 0)
                    {
                        using (IDataContext ctx = DataContext.Instance())
                        {
                            var rep = ctx.GetRepository<ItemContent>();
                            t = rep.GetById(iContentID, itemId);                       
                        }
                    }
                    else
                        t = GetItemsContent(itemId).OrderByDescending(x => x.ModifDate).First();
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
        ///  GetItemsContent retourne les lignes de la table MVCTest_Items_Content associées à l'Item (FilInfo)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "itemId">l'ID de l'Item (FilInfo)</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public IEnumerable<ItemContent> GetItemsContent(int itemId)
        {
            IEnumerable<ItemContent> ts;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<ItemContent>();
                ts = rep.Get(itemId);
            }
            return ts;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///  GetItemsContent retourne les lignes de la table MVCTest_Items_Content associées à l'Item (FilInfo)
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "iItemID">l'ID de l'Item (FilInfo)</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public IEnumerable<ItemContent> GetItemsContent(string sqlRequest, params object[] sqlParams)
        {
            IEnumerable<ItemContent> ts;
            using (IDataContext ctx = DataContext.Instance())
            {
                ts = ctx.ExecuteQuery<ItemContent>(CommandType.Text, sqlRequest, sqlParams);
            }
            return ts;
        }

        /// -----------------------------------------------------------------------------
        /// <summary>
        ///  En raison de la gestion de version UpdateItemContent ajoute une ligne dans la table ItemContent à partir de l'élément existant
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name = "t">la ligne à mettre à jour</param>
        /// <param name = "iMaxHistorique">le nombre max de version conservée</param>
        /// <history>
        /// </history>
        /// -----------------------------------------------------------------------------
        public void UpdateItemContent(ItemContent t, int iMaxHistorique)
        {
            CreateItemContent(t, iMaxHistorique);
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
        public IEnumerable<ItemContent> GetItemsToView(int ModuleId, string sOrder = "ASC")
        {
            IEnumerable<ItemContent> ts;
            List<ItemContent> tl = null;
            ItemContent t = null;
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
                    t = ctx.ExecuteSingleOrDefault<ItemContent>(CommandType.Text, GetItemContentSQLRequest("GetItemsToView"), it.ItemId);
                    if (tl == null) { tl = new List<ItemContent>(); }
                    tl.Add(t);
                }
                ts = (IEnumerable<ItemContent>)tl;
            }

            return ts ?? Enumerable.Empty<ItemContent>();
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
        public string GetItemContentSQLRequest(string sqlRequestName)
        {
            var sb = new StringBuilder();

            switch (sqlRequestName)
            {
                case "GetItemContentVersions":
                    sb.Append("SELECT tb1.ID, tb1.ItemID, tb1.ModuleID, tb1.VersionNumber, tb1.CreaDate, tb1.CreaUserID, tb1.ModifDate, tb1.ModifUserID, (tb2.FirstName + ' ' + tb2.LastName) AS UserFullName")
                        .Append(" FROM MVCTest_Items_Content AS tb1 INNER JOIN Users AS tb2 ON tb1.ModifUserID=tb2.UserID")
                        .Append(" WHERE tb1.ItemID=@0")
                        .Append(" ORDER BY tb1.VersionNumber DESC");
                    break;
                case "GetItemsToView":
                    sb.Append("SELECT TOP 1 tb1.*, tb2.ItemPubDateStart AS ItemPubDateStart")
                        .Append(" FROM SDFilInfo_Items_Content AS tb1")
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