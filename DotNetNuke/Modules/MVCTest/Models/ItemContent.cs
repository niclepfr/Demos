using System;
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
namespace NLDotNet.DNN.Modules.MVCTest.Models
{
    [TableName("MVCTest_Items_Content")]
    //setup the primary key for table
    [PrimaryKey("Id", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("ItemContent", CacheItemPriority.Default, 20)]
    //scope the objects to the ItemID 
    [Scope("ItemId")]
    public class ItemContent
    {
        ///<summary>
        /// ID de la ligne
        ///</summary>
        public int Id { get; set; } = -1;
        ///<summary>
        /// ID of de l'item associé au contenu text
        ///</summary>
        public int ItemId { get; set; } = -1;
        ///<summary>
        /// ID du module associé à l'item
        ///</summary>
        public int ModuleID { get; set; } = -1;
        ///<summary>
        /// HTMLText de l'item
        ///</summary>
        public string nText { get; set; }
        ///<summary>
        /// Numéro de version du contenu HTMLText (5 dernières versions sont conservées)
        ///</summary>
        public int VersionNumber { get; set; } = 0;
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
        public int CreaUserID { get; set; } = -1;
        ///<summary>
        /// Date de modif du contenu
        ///</summary>
        public DateTime ModifDate { get; set; }
        ///<summary>
        /// ID de l'utilisateur
        ///</summary>
        public int ModifUserID { get; set; } = -1;
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
        /// <summary>
        /// 
        /// </summary>
        [ReadOnlyColumn]
        public string TextInHtmlFormat { get; set; }
    }

    [TableName("MVCTest_Items_HTMLContent")]
    //scope the objects to the ItemID
    [Scope("iItemID")]
    public class ItemContentNumMaxVersion
    {
        ///<summary>
        /// Num max de version 
        ///</summary>
        [ReadOnlyColumn]
        public int iNumMaxVersion { get; set; }
    }

    [TableName("CKE_Settings")]
    //scope the objects to the ItemID
    [Scope("iItemID")]
    public class CKEditorSetting
    {
        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        [ReadOnlyColumn]
        public string SettingKey { get; set; }

        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        [ReadOnlyColumn]
        public string SettingName { get; set; }

        ///<summary>
        /// A string with the description of the object
        ///</summary>
        [ReadOnlyColumn]
        public string SettingValue { get; set; }
    }
}