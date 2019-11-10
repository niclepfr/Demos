<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Privacy.ascx.cs" Inherits="SDDotNet.DNN.Skins.NicLep.Privacy" %>
<%@ Register TagPrefix="dnn" TagName="LOGO" Src="~/Admin/Skins/Logo.ascx" %>
<%@ Register TagPrefix="dnn" TagName="SEARCH" Src="~/Admin/Skins/Search.ascx" %>
<%@ Register TagPrefix="dnn" TagName="USER" Src="~/Admin/Skins/User.ascx" %>
<%@ Register TagPrefix="dnn" TagName="LOGIN" Src="~/Admin/Skins/Login.ascx" %>
<%@ Register TagPrefix="dnn" TagName="PRIVACY" Src="~/Admin/Skins/Privacy.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TERMS" Src="~/Admin/Skins/Terms.ascx" %>
<%@ Register TagPrefix="dnn" TagName="COPYRIGHT" Src="~/Admin/Skins/Copyright.ascx" %>
<%@ Register TagPrefix="dnn" TagName="JQUERY" Src="~/Admin/Skins/jQuery.ascx" %>
<%@ Register TagPrefix="dnn" TagName="META" Src="~/Admin/Skins/Meta.ascx" %>
<%@ Register TagPrefix="dnn" TagName="MENU" Src="~/DesktopModules/DDRMenu/Menu.ascx" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:META ID="mobileScale" runat="server" Name="viewport" Content="width=device-width,initial-scale=1" />

<section class="header-holder sd-no-js" id="sectionHead">
    <div class="ws-page-topbar">
        <div class="grid-x">
            <div class="cell small-12 large-4 medium-4 text-center">
                <div class="grid-x grid-padding-x">
                    <div class="cell large-3 medium-2 small-2">
                        <%--<dnn:LOGO runat="server" id="dnnLOGO" />--%>
                        <div class="icon">
                            <a href="/" target="_self"><i class="fas fa-home"></i></a>
                        </div>
                    </div>
                </div>                                
            </div>
            <div class="cell small-12 large-8 medium-8 text-center"></div>            
        </div>
    </div>        
</section>

<section class="content-holder ws-page sd-no-js">

    <div class="sd-bloc-content">
        <div class="grid-container">
            <div class="grid-x grid-margin-x align-center">
                <div class="cell large-9 medium-12 small-12">
                    <div id="ContentPane" runat="server"></div>
                </div>                
            </div>
            <div class="grid-x grid-margin-x align-center">
                <div class="cell large-9 medium-12 small-12">
                    <div id="ContentTerms">
                        <div class="ws-main-container">
                            <article>
                                <%= GetPortalPrivacy() %>
                            </article>
                        </div>                                                
                    </div>
                </div>                
            </div>
        </div>
    </div>

</section>

<section class="footer-holder sd-no-js">
    <!--#include file = "Resources/includes/footer.inc" -->
</section>  
<a href="#sectionHead" class="ws-page-scrollup"><i class="fa fa-chevron-up"></i></a>
<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="Resources/foundation/js/vendor/what-input.js" PathNameAlias="SkinPath"   />
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="Resources/foundation/js/vendor/foundation.min.js" PathNameAlias="SkinPath"   />
<dnn:DnnJsInclude ID="DnnJsInclude4" runat="server" FilePath="Resources/js/sd.dnn.skin.min.js" PathNameAlias="SkinPath"   />

<script>
    
    $('html').toggleClass('no-js', true);
    $(document).foundation();
    $(window).on('load', function () {
        $('section.sd-no-js').toggleClass('sd-no-js', false);
        $('body').toggleClass('sd-loaded', true);
    });
    $(document).ready(function () {
        SkinPageReady();
    });
        

</script>