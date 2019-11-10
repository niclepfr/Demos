<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Auth.ascx.cs" Inherits="SDDotNet.DNN.Skins.NicLep.Auth" %>
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

<section class="content-holder ws-auth">
    
    <div class="ws-bloc-auth">
        <div class="grid-container">
            <div class="grid-x grid-margin-x align-center">
                <div class="cell large-6 medium-9 small-12">
                    <div class="ws-auth-head">
                        <h2><a id="lkbAuthHome" href='<%= DotNetNuke.Common.Globals.NavigateURL(PortalSettings.HomeTabId) %>'><span class="sd-no-hover"></span><span class="sd-hover"><%= PortalSettings.PortalName %></span></a></h2>    
                    </div>
                    <div id="ContentPane" runat="server" class="ws-auth-body"></div>
                </div>
            </div>
        </div>
    </div>

</section>

<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="Resources/foundation/js/vendor/what-input.js" PathNameAlias="SkinPath" />
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="Resources/foundation/js/vendor/foundation.min.js" PathNameAlias="SkinPath" />

<script>
    $('html').toggleClass('no-js', true);
    $(document).foundation();
    $('body').toggleClass('sd-loaded', true);
    $(document).ready(function () {
        AnimAuthHeadStart();
    });
    var iAnimECQueue = 0;
    function AnimAuthHeadLoad(){
        var _txtbak = '<%= PortalSettings.PortalName %>';
        var _txtnoh = "";
        var _txth = "";
        var itime = 0;
        $('#lkbAuthHome').clearQueue('sdAnimAuthHead');
        $('#lkbAuthHome').queue('sdAnimAuthHead', function (next) {
            next();
        }).delay(300, 'sdAnimAuthHead')
        itime += 300;
        for (var i = 0; i < _txtbak.length; i++) {
            $('#lkbAuthHome').queue('sdAnimAuthHead', function (next) {
                _txtbak = $('.ws-auth .ws-bloc-auth .ws-auth-head h2 a span.sd-hover').text();
                _txtnoh = _txtbak.substring(0, 1);
                _txth = _txtbak.substring(1, (_txtbak.length));
                $('.ws-auth .ws-bloc-auth .ws-auth-head h2 a span.sd-no-hover').text($('.ws-auth .ws-bloc-auth .ws-auth-head h2 a span.sd-no-hover').text() + _txtnoh);
                $('.ws-auth .ws-bloc-auth .ws-auth-head h2 a span.sd-hover').text(_txth);
                next();
            }).delay(100, 'sdAnimAuthHead')
            itime += 100;
        }
        $('#lkbAuthHome').queue('sdAnimAuthHead', function (next) {
            $(this).css('opacity', '1');
            next();
        }).delay(200, 'sdAnimAuthHead').queue('sdAnimAuthHead', function (next) {
            AnimAuthHeadStop();
        });
        return (itime + 500);
    }
    function AnimAuthHeadStart() {
        if (iAnimECQueue == 0) {
            var i = 0;
            iAnimECQueue++;
            i = AnimAuthHeadLoad();
            if ($('#lkbAuthHome').length > 0) {
                $('#lkbAuthHome').dequeue('sdAnimAuthHead');
            } else {
                AnimAuthHeadStop();
            }
            setTimeout(function () {
                if ($('#lkbAuthHome').length > 0) {
                    if (iAnimECQueue <= 0) {
                        AnimModulContentStep3_Start(sid);
                    }
                }
            }, i);
        }
    }
    function AnimAuthHeadStop() {
        $('#lkbAuthHome').css('opacity', '1');
        $('.ws-auth .ws-bloc-auth .ws-auth-body').css('opacity', '1');
        //iAnimECQueue = 0;
        return true;
    }
   
</script>