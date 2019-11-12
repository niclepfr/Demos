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

using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using NLDotNet.DNN.Modules.MVCTest.Components.Data.PetaPoco;
using NLDotNet.DNN.Modules.MVCTest.Models;

namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    interface IItemManager
    {
        void CreateItem(Item t);
        void DeleteItem(int itemId, int moduleId);
        void DeleteItem(Item t);
        IEnumerable<Item> GetItems(int moduleId);
        Item GetItem(int itemId, int moduleId);
        IEnumerable<Item> GetItems(string sqlRequest, params object[] sqlParams);        
        void UpdateItem(Item t);
        string GetItemsSQLRequest(string sqlRequestName);
    }

    class ItemManager : ServiceLocator<IItemManager, ItemManager>, IItemManager
    {
        protected override System.Func<IItemManager> GetFactory()
        {
            return () => new ItemManager();
        }

        public void CreateItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                t = (Item)PPUtils.NullableToDbNull(t);
                var rep = ctx.GetRepository<Item>();
                rep.Insert(t);
            }
        }

        public void DeleteItem(int itemId, int moduleId)
        {
            var t = GetItem(itemId, moduleId);
            DeleteItem(t);
        }

        public void DeleteItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                rep.Delete(t);
            }
        }

        public IEnumerable<Item> GetItems(int moduleId)
        {
            IEnumerable<Item> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlRequest"></param>
        /// <param name="sqlParams"></param>
        /// <returns></returns>
        public IEnumerable<Item> GetItems(string sqlRequest, params object[] sqlParams)
        {
            IEnumerable<Item> ts = null;
            if (!string.IsNullOrWhiteSpace(sqlRequest))
            {
                using (IDataContext ctx = DataContext.Instance())
                {
                    ts = ctx.ExecuteQuery<Item>(System.Data.CommandType.Text, sqlRequest, sqlParams);
                }
            }
            return ts ?? Enumerable.Empty<Item>();
        }

        public Item GetItem(int itemId, int moduleId)
        {
            Item t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateItem(Item t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<Item>();
                rep.Update(t);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlRequestName"></param>
        /// <returns></returns>
        public string GetItemsSQLRequest(string sqlRequestName)
        {
            var sb = new StringBuilder();

            switch (sqlRequestName)
            {
                case "GetItemsActive":
                    sb.Append("SELECT * FROM MVCTest_Items")
                        .Append(" WHERE ModuleID=@0")
                        .Append(" AND (DATEDIFF(n,getdate(),ItemPubDateStart)<=0)")
                        .Append(" AND ((DATEDIFF(n,getdate(),ItemPubDateEnd)>=0) OR (ItemPubDateEnd IS NULL))")
                        .Append(" AND (ItemIsPub = 1)")
                        .Append(" ORDER BY ItemName ASC"); ;
                    break;
                case "GetItemsActiveOrderByDateDePubASC":
                    sb.Append("SELECT * FROM MVCTest_Items")
                        .Append(" WHERE ModuleID=@0")
                        .Append(" AND (DATEDIFF(n,getdate(),ItemPubDateStart)<=0)")
                        .Append(" AND ((DATEDIFF(n,getdate(),ItemPubDateEnd)>=0) OR (ItemPubDateEnd IS NULL))")
                        .Append(" AND (ItemIsPub = 1)")
                        .Append(" ORDER BY ItemPubDateStart ASC");
                    break;
                case "GetItemsActiveOrderByDateDePubDESC":
                    sb.Append("SELECT * FROM MVCTest_Items")
                        .Append(" WHERE ModuleID=@0")
                        .Append(" AND (DATEDIFF(n,getdate(),ItemPubDateStart)<=0)")
                        .Append(" AND ((DATEDIFF(n,getdate(),ItemPubDateEnd)>=0) OR (ItemPubDateEnd IS NULL))")
                        .Append(" AND (ItemIsPub = 1)")
                        .Append(" ORDER BY ItemPubDateStart DESC");
                    break;
                case "GetItemsToActive":
                    sb.Append("SELECT * FROM MVCTest_Items")
                        .Append(" WHERE ModuleID=@0")
                        .Append(" AND (DATEDIFF(n,getdate(),ItemPubDateStart)<=0)")
                        .Append(" AND ((DATEDIFF(n,getdate(),ItemPubDateEnd)>=0) OR (ItemPubDateEnd IS NULL))")
                        .Append(" AND ((ItemIsPub IS NULL) OR (ItemIsPub = 0))")
                        .Append(" ORDER BY ItemCreaDate DESC"); 
                    break;
                case "GetItemsArchive":
                    sb.Append("SELECT * FROM MVCTest_Items")
                        .Append(" WHERE ModuleID=@0")
                        .Append(" AND ItemId NOT IN (")
                        .Append(" SELECT tb1.ItemId FROM MVCTest_Items AS tb1")
                        .Append(" WHERE tb1.ModuleID=@0")
                        .Append(" AND (DATEDIFF(n,getdate(),tb1.ItemPubDateStart)<=0)")
                        .Append(" AND ((DATEDIFF(n,getdate(),tb1.ItemPubDateEnd)>=0) OR (tb1.ItemPubDateEnd IS NULL))")
                        .Append(")")
                        .Append(" ORDER BY ItemCreaDate DESC");
                    break;                
            }

            return sb.ToString();
        }        
    }
}
