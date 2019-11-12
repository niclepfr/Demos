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
using System.Web.Caching;
using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Content;
using NLDotNet.DNN.Modules.MVCTest.Components.DataAnnotations;

namespace NLDotNet.DNN.Modules.MVCTest.Models
{
    [TableName("MVCTest_Items")]
    //setup the primary key for table
    [PrimaryKey("ItemId", AutoIncrement = true)]
    //configure caching using PetaPoco
    [Cacheable("Items", CacheItemPriority.Default, 20)]
    //scope the objects to the ModuleId of a module on a page (or copy of a module on a page)
    [Scope("ModuleId")]
    public class Item
    {
        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int ItemId { get; set; } = -1;

        ///<summary>
        /// The ID of your object with the name of the ItemName
        ///</summary>
        public int ModuleId { get; set; } = -1;

        ///<summary>
        /// A string with the name of the ItemName
        ///</summary>
        [DnnRequiredFieldValidator]
        public string ItemName { get; set; }

        ///<summary>
        /// A string with the description of the object
        ///</summary>
        public string ItemDescription { get; set; }

        ///<summary>
        /// A datetetime for the begining item publication
        ///</summary>
        public DateTime? ItemPubDateStart { get; set; } = Null.NullDate;

        ///<summary>
        /// A datetime for the ending item publication 
        ///</summary>
        public DateTime? ItemPubDateEnd { get; set; } = Null.NullDate;

        /// <summary>
        /// 
        /// </summary>
        public bool? ItemIsPub { get; set; } = false;

        ///<summary>
        /// An integer for the user id of the user who created the object
        ///</summary>
        public int ItemCreaUserID { get; set; } = -1;

        ///<summary>
        /// An integer for the user id of the user who last updated the object
        ///</summary>
        public int ItemModifUserID { get; set; } = -1;

        ///<summary>
        /// The date the object was created
        ///</summary>
        public DateTime ItemCreaDate { get; set; } = DateTime.UtcNow.ToLocalTime();

        ///<summary>
        /// The date the object was updated
        ///</summary>
        public DateTime ItemModifDate { get; set; } = DateTime.UtcNow.ToLocalTime();
    }
}
