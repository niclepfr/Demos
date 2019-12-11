using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DotNetNuke.Entities.Portals;
using DotNetNuke.UI.Modules;

namespace NLDotNet.DNN.Modules.MVCTest.Models
{
    public class ViewModelBase
    {
        private string modelResourceFile = "";

        //internal enum  ModelViewList {  };
        //internal string ModelActionName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public virtual string ModelResourceFile
        {
            get
            {
                return modelResourceFile;
            }
            set
            {
                modelResourceFile = value;                
            }
        }
    }
}