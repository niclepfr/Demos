using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security.Roles;
using DotNetNuke.Data;
using DotNetNuke.Services.Exceptions;
using NLDotNet.DNN.Modules.MVCTest.Models;

namespace NLDotNet.DNN.Modules.MVCTest.Components
{
    public class CKEditorManager
    {
        #region Private Members

        private int _dnnPortalId = -1;
        private int _dnnTabId = -1;
        private int _dnnModuleId = -1;
        private UserInfo _dnnUser = null;
        private string _dnnMvcModuleFolderPath = "";
        private string _dnnEditorControlname = "";

        #endregion


        #region Private Properties

        /// <summary>
        /// 
        /// </summary>
        private string DnnBaseSkinsFolderPath
        {
            get
            {
                return "/portals/_default/";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string DnnPortalSkinFolderPath
        {
            get
            {
                return string.Format("{0}{1}", DnnBaseSkinsFolderPath, PortalSettings.Current.DefaultPortalSkin.Substring(3, (PortalSettings.Current.DefaultPortalSkin.LastIndexOf(@"/") - 2)));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string CKEditorFolderPathBrowserUrl
        {
            get
            {
                return (System.IO.File.Exists(Path.Combine(HttpContext.Current.Server.MapPath("~" + CKEditorFolderPathBase), "Browser", "Browser.aspx"))) ? string.Format("{0}Browser/Browser.aspx", CKEditorFolderPathBase) : "";                
                //return (File.Exists(Path.Combine(HttpContext.Current.Request.PhysicalApplicationPath, "Providers", "HtmlEditorProviders", "DNNConnect.CKE", "Browser", "Browser.aspx"))) ? string.Format("{0}{1}/{2}", CKEditorFolderPathBase, "Browser", "Browser.aspx") : "";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string[] CodeMirrorKeys
        {
            get
            {
                return new string[] { "theme", "lineNumbers", "lineWrapping", "matchBrackets", "autoCloseTags", "enableSearchTools", "enableCodeFolding", "enableCodeFormatting", "autoFormatOnStart", "autoFormatOnUncomment", "highlightActiveLine", "highlightMatches", "showTabs", "showFormatButton", "showCommentButton", "showUncommentButton" };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int dnnPortalId
        {
            get
            {
                return _dnnPortalId;
            }
            set
            {
                _dnnPortalId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int dnnTabId
        {
            get
            {
                return _dnnTabId;
            }
            set
            {
                _dnnTabId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private int dnnModuleId
        {
            get
            {
                return _dnnModuleId;
            }
            set
            {
                _dnnModuleId = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private UserInfo dnnUser
        {
            get
            {
                return _dnnUser;
            }
            set
            {
                _dnnUser = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private string dnnMvcModuleFolderPath
        {
            get
            {
                return _dnnMvcModuleFolderPath;
            }
            set
            {
                _dnnMvcModuleFolderPath = value;
            }
        }

        #endregion


        #region Public Properties

        /// <summary>
        /// 
        /// </summary>
        public string CKEditorFolderPathBase
        {
            get
            {
                return "/Providers/HtmlEditorProviders/DNNConnect.CKE/";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string CKEditorControlName
        {
            get
            {
                return "htmlContentEditor";
            }
        }

        #endregion


        #region Constructor

        public CKEditorManager(){}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <param name="tabId"></param>
        /// <param name="moduleId"></param>
        /// <param name="user"></param>
        /// <param name="baseSkinsFolderPath"></param>
        /// <param name="portalSkinFolderPath"></param>
        /// <param name="mvcModuleFolderPath"></param>
        public CKEditorManager(int portalId, int moduleId, int tabId, UserInfo user, string mvcModuleFolderPath)
        {
            dnnPortalId = portalId;
            dnnTabId = tabId;
            dnnModuleId = moduleId;
            dnnUser = user;
            dnnMvcModuleFolderPath = mvcModuleFolderPath;            
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetStartupJSScript()
        {
            if(!((dnnPortalId>0) && (dnnTabId>0) && (dnnModuleId>0) && (dnnUser != null))) { return ""; }

            //Paramètres de l'éditeur (CKEditor)
            var ckeSettings = new StringBuilder()
                .Append("var htmlContentEditorConfig = {").AppendLine()
                .Append(GetCKEditorSettingsToString()).AppendLine()
                .Append("};").AppendLine()
                .Append(string.Format("CKEDITOR.replace('{0}', htmlContentEditorConfig);", CKEditorControlName));

            return ckeSettings.ToString();
        }

        #endregion


        #region Private Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        private string GetCKEditorSettingsToString()
        {
            var sb = new StringBuilder();
            var sbCodeMirror = new StringBuilder();
            
            IEnumerable<CKEditorSetting> ckeSettings = null;
            try
            {
                ckeSettings = GetCKEditorSettings(dnnPortalId);
                if ((ckeSettings != null) && (ckeSettings.Count() > 0))
                {
                    sb.Clear();
                    foreach (var ckeSetting in ckeSettings)
                    {
                        var _key = ckeSetting.SettingKey;
                        var _value = ckeSetting.SettingValue;
                        if (!string.IsNullOrEmpty(_value))
                        {
                            switch (_key.ToLower().Trim())
                            {
                                case "templates_files":
                                    sb.Append(string.Format("{0}: ['{1}']", _key, _value.ToString()));
                                    break;
                                default:
                                    if (IsSettingValueBool(_value.ToString()))
                                    {
                                        if (CodeMirrorKeys.Contains(_key.Trim()))
                                            sbCodeMirror.Append(string.Format("{0}: {1}", _key, _value.ToString().ToLower()));
                                        else
                                            sb.Append(string.Format("{0}: {1}", _key, _value.ToString().ToLower()));
                                    }
                                    else if ((IsSettingValueNumeric(_value.ToString())) || (IsSettingValueArray(_value.ToString())))
                                    {
                                        if (CodeMirrorKeys.Contains(_key.Trim()))
                                            sbCodeMirror.Append(string.Format("{0}: {1}", _key, _value.ToString().ToLower()));
                                        else
                                            sb.Append(string.Format("{0}: {1}", _key, _value.ToString()));
                                    }
                                    else
                                    {
                                        if (CodeMirrorKeys.Contains(_key.Trim()))
                                            sbCodeMirror.Append(string.Format("{0}: '{1}'", _key, _value.ToString()));
                                        else
                                        {
                                            if (_key.Trim().Equals("toolbarLocation", StringComparison.InvariantCultureIgnoreCase))
                                                sb.Append(string.Format("{0}: '{1}'", _key, _value.ToLower().ToString()));
                                            else
                                                sb.Append(string.Format("{0}: '{1}'", _key, _value.ToString()));
                                        }
                                    }
                                    break;
                            }
                            if ((sb.Length > 0) && (!sb.ToString().EndsWith(", "))) { sb.Append(", "); }
                            if ((sbCodeMirror.Length > 0) && (!sbCodeMirror.ToString().EndsWith(","))) { sbCodeMirror.Append(","); }
                        }
                    }
                    //codemirror
                    if (sbCodeMirror.Length > 0)
                    {
                        if (sbCodeMirror.ToString().EndsWith(",")) { sbCodeMirror.Remove(sbCodeMirror.Length - 1, 1); }
                        sb.Append("codemirror:{").Append(sbCodeMirror.ToString()).Append("}, ");
                    }
                    //contentsCss
                    sb.Append(string.Format(@"contentsCss:[""{0}default.css"",""{1}skin.css"",""{2}module.css"",""/Portals/{3}/portal.css"",""/Providers/HtmlEditorProviders/DNNConnect.CKE/css/CkEditorContents.css""], ", DnnBaseSkinsFolderPath, DnnPortalSkinFolderPath, dnnMvcModuleFolderPath, dnnPortalId.ToString()));
                    //toolbar
                    sb.Append(string.Format("toolbar:[{0}], ", GetToolbarTemplateToString()));
                    //maxFileSize
                    sb.Append(string.Format("maxFileSize:{0}, ", Config.GetMaxUploadSize().ToString()));
                    //filebrowserurl
                    var _url = CKEditorFolderPathBrowserUrl;
                    if (!string.IsNullOrWhiteSpace(_url))
                    {
                        sb.Append(string.Format("filebrowserBrowseUrl: '{0}?Type=Link&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append(string.Format("filebrowserImageBrowseUrl: '{0}?Type=Image&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append(string.Format("filebrowserFlashBrowseUrl: '{0}?Type=Flash&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append(string.Format("filebrowserUploadUrl: '{0}?Type=FileUpload&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append(string.Format("filebrowserFlashUploadUrl: '{0}?Type=FlashUpload&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append(string.Format("filebrowserImageUploadUrl: '{0}?Type=ImageUpload&tabid={1}&PortalID={2}&mid={3}&ckid=htmlContentEditor&mode=Portal&lang=fr-FR', ", _url, dnnTabId, dnnPortalId, dnnModuleId))
                            .Append("filebrowserWindowWidth: 870,")
                            .Append("filebrowserWindowHeight: 800");
                    }
                }
            }
            catch
            {
                sb.Clear();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetToolbarTemplateToString()
        {
            var sb = new StringBuilder();

            try
            {
                var dnnRoles = RoleController.Instance.GetRoles(dnnPortalId);
                if ((dnnUser.Roles.Length > 0) && (dnnRoles != null) && (dnnRoles.Count > 0))
                {
                    //var dnnRolesId = dnnRoles.Where(r => User.IsInRole(r.RoleName)).Select(r => r.RoleID).ToArray();
                    var dnnRolesId = (from RoleInfo dnnRole in dnnRoles
                                      where dnnUser.IsInRole(dnnRole.RoleName)
                                      select dnnRole.RoleID).ToArray();

                    if (dnnRolesId.Length > 0)
                    {
                        var ckeToolbarSettings = GetCKEditorToolbarSettings(dnnPortalId, dnnRolesId).ToList();
                        if ((ckeToolbarSettings != null) && (ckeToolbarSettings.Count() > 0))
                        {
                            if (ckeToolbarSettings.Select(s => s.SettingValue.ToLower()).Contains("full")) //full
                            {
                                sb.Append(XMLToolbarTemplateToString("full"));
                            }
                            else if (ckeToolbarSettings.Select(s => s.SettingValue.ToLower()).Contains("standard")) //standard
                            {
                                sb.Append(XMLToolbarTemplateToString("standard"));
                            }
                            else //basic
                            {
                                sb.Append(XMLToolbarTemplateToString("basic"));
                            }
                        }
                    }
                }
            }
            catch {}

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="toolbarModel"></param>
        /// <returns></returns>
        private string XMLToolbarTemplateToString(string toolbarModel)
        {
            var sb = new StringBuilder();
            var _sb = new StringBuilder();
            var xmlNameValue = "";
            var xmlItemsValue = "";
            XmlNodeList xnltoolbarModel = null;
            XmlNodeList xnltoolbarGroup = null;
            XmlNodeList xnltoolbarItems = null;
            XmlNode xnltoolbarName = null;

            try
            {
                var xmlFileName = "Dnn.CKToolbarSets.xml";
                var xmlFileFullName = Path.Combine(DotNetNuke.Common.Globals.HostMapPath, xmlFileName);

                if (!File.Exists(xmlFileFullName))
                {
                    return sb.ToString();
                }

                XmlDocument xml = new XmlDocument();
                xml.Load(xmlFileFullName);

                xnltoolbarModel = xml.SelectNodes("ArrayOfToolbarSet/ToolbarSet");
                for (var i = 0; i < xnltoolbarModel.Count; i++)
                {
                    var xn = xnltoolbarModel[i];
                    if (xn.SelectSingleNode("Name").InnerText.Equals(toolbarModel, StringComparison.InvariantCultureIgnoreCase))
                    {
                        xnltoolbarGroup = xn.SelectNodes("ToolbarGroups/ToolbarGroup");
                        break;
                    }
                }
                if ((xnltoolbarGroup != null) && (xnltoolbarGroup.Count > 0))
                {
                    for (var i = 0; i < xnltoolbarGroup.Count; i++)
                    {
                        xmlNameValue = "";
                        xmlItemsValue = "";
                        xnltoolbarItems = xnltoolbarGroup[i].SelectNodes("items/string");
                        xnltoolbarName = xnltoolbarGroup[i].SelectSingleNode("name");
                        if ((xnltoolbarItems != null) && (xnltoolbarItems.Count > 0))
                        {
                            _sb.Clear();
                            for (var n = 0; n < xnltoolbarItems.Count; n++)
                            {
                                var _value = xnltoolbarItems[n].InnerText;
                                if (string.IsNullOrWhiteSpace(_value)) { continue; }
                                if (n == (xnltoolbarItems.Count - 1))
                                    _sb.Append(string.Format("'{0}'", _value));
                                else
                                    _sb.Append(string.Format("'{0}',", _value));
                            }
                        }
                        if (_sb.Length > 0) { xmlItemsValue = _sb.ToString(); }
                        if (xnltoolbarName != null)
                            xmlNameValue = (!xnltoolbarName.InnerText.Equals("rowBreak", StringComparison.InvariantCultureIgnoreCase)) ? xnltoolbarName.InnerText : "";

                        if ((!string.IsNullOrWhiteSpace(xmlNameValue.Trim())) && (!string.IsNullOrWhiteSpace(xmlItemsValue.Trim())))
                            sb.Append("{").Append(string.Format("name:'{0}',items:[{1}]", xmlNameValue, xmlItemsValue)).Append("}");
                        else if (!string.IsNullOrWhiteSpace(xmlNameValue.Trim()))
                            sb.Append("{").Append(string.Format("name:'{0}'", xmlNameValue)).Append("}");
                        else if (!string.IsNullOrWhiteSpace(xmlItemsValue.Trim()))
                            sb.Append(string.Format("{0}", xmlItemsValue));

                        if (((!string.IsNullOrWhiteSpace(xmlNameValue.Trim())) || (!string.IsNullOrWhiteSpace(xmlItemsValue.Trim()))) && (i < (xnltoolbarGroup.Count - 1)))
                            sb.Append(",");
                    }
                }

            }
            catch { sb.Clear(); }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsSettingValueBool(string value)
        {
            return ((value.Equals("false", StringComparison.InvariantCultureIgnoreCase))
                || (value.Equals("true", StringComparison.InvariantCultureIgnoreCase)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsSettingValueNumeric(string value)
        {
            var rx = new System.Text.RegularExpressions.Regex(@"^[\-]{0,1}[0-9]+$");
            return (rx.IsMatch(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsSettingValueArray(string value)
        {
            var rx = new System.Text.RegularExpressions.Regex(@"^\[.*?\]$");
            return (rx.IsMatch(value));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        private IEnumerable<CKEditorSetting> GetCKEditorSettings(int portalId)
        {
            IEnumerable<CKEditorSetting> ckeSettings = null;

            if (portalId >= 0)
            {
                try
                {
                    var argPattern = "DNNCKP#" + portalId.ToString() + "#";
                    var sb = new StringBuilder()
                        .Append("SELECT SettingName")
                        .Append(", ((LOWER(SUBSTRING(SUBSTRING(SettingName,LEN(@0) + 1,LEN(SettingName)),1,1))) + (SUBSTRING(SUBSTRING(SettingName,LEN(@0) + 1,LEN(SettingName)),2,LEN(SUBSTRING(SettingName,LEN(@0) + 1,LEN(SettingName)))))) AS SettingKey")
                        .Append(", SettingValue")
                        .Append(" FROM CKE_Settings")
                        .Append(" WHERE (SUBSTRING(SettingName,1,LEN(@0))=@0)")
                        .Append(" AND (LEN(LTRIM(RTRIM(ISNULL(CONVERT(VARCHAR,SettingValue),N''))))>0)")
                        .Append(" AND (CHARINDEX(N'#',SUBSTRING(SettingName,LEN(@0) + 1,LEN(SettingName)))=0)")
                        .Append(" ORDER BY SettingName");
                    using (IDataContext ctx = DataContext.Instance())
                    {
                        ckeSettings = ctx.ExecuteQuery<CKEditorSetting>(CommandType.Text, sb.ToString(), argPattern).OrderBy(s => s.SettingName);
                    }
                }
                catch { }
            }
            return ckeSettings;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="portalId"></param>
        /// <returns></returns>
        private IEnumerable<CKEditorSetting> GetCKEditorToolbarSettings(int portalId, int[] dnnRolesId)
        {
            IEnumerable<CKEditorSetting> ckeSettings = null;

            if (portalId >= 0)
            {
                try
                {
                    var argPattern = "DNNCKP#" + portalId.ToString() + "#toolb#";
                    var sb = new StringBuilder()
                        .Append("SELECT SettingName")
                        .Append(",(SUBSTRING(SettingName,LEN(@0) + 1,LEN(SettingName))) AS SettingKey")
                        .Append(", SettingValue")
                        .Append(" FROM CKE_Settings")
                        .Append(" WHERE (SUBSTRING(SettingName,1,LEN(@0))=@0)")
                        .Append(" ORDER BY SettingName");
                    using (IDataContext ctx = DataContext.Instance())
                    {
                        ckeSettings = ctx.ExecuteQuery<CKEditorSetting>(CommandType.Text, sb.ToString(), argPattern).Where(s => dnnRolesId.Contains(MVCModuleBase.StringToInt(s.SettingKey)));
                    }
                }
                catch (Exception Ex)
                {
                    Exceptions.LogException(Ex);
                }
            }
            return ckeSettings;
        }

        #endregion

    }
}