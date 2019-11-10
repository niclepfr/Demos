/*
' Copyright (c) 2019  niclep.fr
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
using System.Data;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Services.Localization;
using DotNetNuke.Web.Client.ClientResourceManagement;
using SD.Services.Localisation;


namespace SDDotNet.DNN.Skins.NicLep
{
    public class SkinBase : DotNetNuke.UI.Skins.Skin
    {
        private UpdatePanel upConsentPolicy = null;
        protected HtmlControl divConsentPolicy;
        protected Panel divConsentPolicycontainer;
        protected HtmlAnchor cmdConsentPolicyValid;
        protected Label lblConsentPolicyText;
        protected HtmlAnchor cmdConsentPolicyInfo;
        private string ConsentPolicyCookieKey = "WS_ConsentPolicy_Agreement";
        private string ConsentPolicyTemplatePath = "/Portals/_default/Skins/niclep/Resources/includes/consentpolicy.inc";
        private string ConsentPolicyResourceFilePath = "/Portals/_default/skins/niclep/App_LocalResources/Home";
        

        #region Private Properties

        /// <summary>
        /// 
        /// </summary>
        private bool ConsentPolicyValidate
        {
            get
            {
                var _ConsentPolicyValidate = false;
                if (Request.Cookies[ConsentPolicyCookieKey] != null)
                {
                    var _cookieValue = Request.Cookies[ConsentPolicyCookieKey].Value;
                    DateTime _cookieDate;
                    if (DateTime.TryParse(_cookieValue, out _cookieDate))
                    {
                        if (_cookieDate.AddYears(1).CompareTo(DateTime.UtcNow) >= 0)
                        {
                            _ConsentPolicyValidate = true;
                        }
                    }
                }
                return _ConsentPolicyValidate;
            }
        }
        
        #endregion


        #region Event Handlers

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            InitPolicyConsent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            try
            {
                if (upConsentPolicy != null)
                {
                    if (cmdConsentPolicyValid != null)
                    {
                        cmdConsentPolicyValid.ServerClick += new EventHandler(cmdConsentPolicyValid_Click);
                        cmdConsentPolicyValid.PreRender += new EventHandler(ControlConsentPolicyValid_PreRender);
                    }
                    if (cmdConsentPolicyInfo != null)
                    {
                        cmdConsentPolicyInfo.PreRender += new EventHandler(ControlConsentPolicyValid_PreRender);
                    }
                    if (lblConsentPolicyText != null)
                    {
                        lblConsentPolicyText.PreRender += new EventHandler(ControlConsentPolicyValid_PreRender);
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            if ((ConsentPolicyValidate) && (upConsentPolicy != null))
            {
                divConsentPolicycontainer.CssClass = "hide";
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdConsentPolicyValid_Click(object sender, EventArgs e)
        {
            SetPolicyConsent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ControlConsentPolicyValid_PreRender(object sender, EventArgs e)
        {
            Localisation.LocaliseControl(sender, ConsentPolicyResourceFilePath);
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        private void InitPolicyConsent()
        {
            if ((!ConsentPolicyValidate) && (divConsentPolicy != null))
            {
                try
                {
                    if (File.Exists(Server.MapPath("~" + ConsentPolicyTemplatePath)))
                    {
                        divConsentPolicycontainer = new Panel();
                        divConsentPolicycontainer.CssClass = "ws-policy-container";
                        upConsentPolicy = new UpdatePanel();
                        upConsentPolicy.ID = "upConsentPolicy";
                        upConsentPolicy.UpdateMode = UpdatePanelUpdateMode.Always;
                        upConsentPolicy.ContentTemplate = new CompiledTemplateBuilder(new BuildTemplateMethod(BuildPolicyConsentTemplate));
                    }

                }
                catch (Exception ex)
                {
                    upConsentPolicy = null;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="container"></param>
        private void BuildPolicyConsentTemplate(Control container)
        {
            try
            {
                var _htmls = File.ReadAllLines(Server.MapPath("~" + ConsentPolicyTemplatePath));
                foreach (var _html in _htmls)
                {
                    if (_html.IndexOf("cmdConsentPolicyValid") >= 0)
                    {
                        cmdConsentPolicyValid = new HtmlAnchor();
                        cmdConsentPolicyValid.ID = "cmdConsentPolicyValid";
                        cmdConsentPolicyValid.Attributes.Add("class", "ws-policy-cmd valid");
                        divConsentPolicycontainer.Controls.Add(cmdConsentPolicyValid);
                    }
                    else if (_html.IndexOf("cmdConsentPolicyInfo") >= 0)
                    {
                        cmdConsentPolicyInfo = new HtmlAnchor();
                        cmdConsentPolicyInfo.ID = "cmdConsentPolicyInfo";
                        cmdConsentPolicyInfo.HRef = "/Privacy";
                        cmdConsentPolicyInfo.Attributes.Add("class", "ws-policy-cmd info");
                        divConsentPolicycontainer.Controls.Add(cmdConsentPolicyInfo);
                    }
                    else if (_html.IndexOf("lblConsentPolicyText") >= 0)
                    {
                        lblConsentPolicyText = new Label();
                        lblConsentPolicyText.ID = "lblConsentPolicyText";
                        divConsentPolicycontainer.Controls.Add(lblConsentPolicyText);
                    }
                    else
                    {
                        divConsentPolicycontainer.Controls.Add(new LiteralControl(@"" + _html));
                    }
                }
                container.Controls.Add(divConsentPolicycontainer);
                divConsentPolicy.Controls.Add(upConsentPolicy);
            }
            catch
            {
                upConsentPolicy = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetPolicyConsent()
        {
            HttpContext.Current.Response.Cookies.Set(new HttpCookie(ConsentPolicyCookieKey, DateTime.UtcNow.ToString()) { Expires = DateTime.UtcNow.AddYears(1) });
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPortalPrivacy()
        {
            var sPrivacy = Localization.GetString("MESSAGE_PORTAL_PRIVACY", Localization.GlobalResourceFile);
            return (string.IsNullOrWhiteSpace(sPrivacy)) ? "" : sPrivacy.Replace("[Portal:PortalName]", PortalSettings.Current.PortalName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetPortalTerms()
        {
            var sTerms = Localization.GetString("MESSAGE_PORTAL_TERMS", Localization.GlobalResourceFile);
            return (string.IsNullOrWhiteSpace(sTerms)) ? "" : sTerms.Replace("[Portal:PortalName]", PortalSettings.Current.PortalName);
        }

        #endregion

        private void RegisterJavaScript()
        {
            jQuery.RequestRegistration();
            //ClientResourceManager.RegisterScript(Page, "/portals/_default/skins/NicLep/js/jquery.blueimp-gallery.min.js", FileOrder.Js.jQuery, "DnnFormBottomProvider"); // default priority and provider
            //ClientResourceManager.RegisterScript(Page, "/portals/_default/skins/NicLep/js/bootstrap-image-gallery.min.js", FileOrder.Js.jQuery, "DnnFormBottomProvider"); // default priority and provider           

        }
    }
}