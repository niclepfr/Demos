using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
namespace NLDotNet.DNN.Modules.MVCTest.Models
{
    [TableName("MVCTest_Items_HTMLText")]
    //setup the primary key for table
    [PrimaryKey("Id", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("ItemHTMLText", CacheItemPriority.Default, 20)]
    //scope the objects to the ItemID 
    [Scope("ItemId")]
    public class ItemHTMLText
    {
        ///<summary>
        /// ID de la ligne
        ///</summary>
        public int Id { get; set; }
        ///<summary>
        /// ID of de l'item associé au contenu text
        ///</summary>
        public int ItemId { get; set; }
        ///<summary>
        /// ID du module associé à l'item
        ///</summary>
        public int ModuleID { get; set; }
        ///<summary>
        /// HTMLText de l'item
        ///</summary>
        public string nText { get; set; }
        ///<summary>
        /// Numéro de version du contenu HTMLText (5 dernières versions sont conservées)
        ///</summary>
        public int VersionNumber { get; set; }
        ///<summary>
        /// HTMLText de l'item
        ///</summary>
        public string nResume { get; set; }
        ///<summary>
        /// Date de creation du contenu
        ///</summary>
        public DateTime CreaDate { get; set; }
        ///<summary>
        /// ID de l'utilisateur
        ///</summary>
        public int CreaUserID { get; set; }
        ///<summary>
        /// Date de modif du contenu
        ///</summary>
        public DateTime ModifDate { get; set; }
        ///<summary>
        /// ID de l'utilisateur
        ///</summary>
        public int ModifUserID { get; set; }
        ///<summary>
        /// nom complet de l'utilisateur
        ///</summary>
        [ReadOnlyColumn]
        public string UserFullName{ get; set; }
        ///<summary>
        /// nom complet de l'utilisateur
        ///</summary>
        [ReadOnlyColumn]
        public DateTime ItemPubDateStart { get; set; }
    }

    [TableName("MVCTest_Items_HTMLText")]
    //scope the objects to the ItemID
    [Scope("iItemID")]
    public class ItemHTMLTextNumMaxVersion
    {
        ///<summary>
        /// Num max de version 
        ///</summary>
        [ReadOnlyColumn]
        public int iNumMaxVersion { get; set; }
    }
}