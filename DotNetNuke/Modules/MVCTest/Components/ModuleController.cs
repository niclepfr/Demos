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

namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    [DnnHandleError]
    public class ModuleController : DnnController
    {
        //internal string HttpPostAction
        //{
        //    get
        //    {
        //        var str = "";
        //        if (Request.Form["form_post_action"] != null)
        //            str = Request.Form["form_post_action"].ToString();

        //        return str;
        //    }
        //}
        //internal string HttpPostArgument
        //{
        //    get
        //    {
        //        var str = "";
        //        if (Request.Form["form_post_arg"] != null)
        //            str = Request.Form["form_post_arg"].ToString();

        //        return str;
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            SetLocalResourceFile();
                
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetLocalResourceFile()
        {
            if ((!string.IsNullOrWhiteSpace(LocalResourceFile)) && (!PortalSettings.DefaultLanguage.Equals("en-us", StringComparison.InvariantCultureIgnoreCase)))
            {
                var fullfilename = LocalResourceFile.Replace(".resx", "." + ModuleContext.PortalSettings.DefaultLanguage + ".resx");
                if (System.IO.File.Exists(fullfilename))
                    LocalResourceFile = fullfilename;
            }
        }

        
    }
}