<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Home.ascx.cs" Inherits="SDDotNet.DNN.Skins.NicLep.Home" %>
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
    <div class="ws-bloc-topbar">
        <div class="ws-topbar-userlog">
            <dnn:LOGIN ID="dnnLogin" CssClass="LoginLink" runat="server" LegacyMode="false" />           
        </div>    
    </div>
</section>

<section class="content-holder sd-home sd-no-js">

    <div id="ContentPane" runat="server" />

    <div class="moncv hide">
        <div class="grid-container full">
            <div class="grid-x align-center">    
                <div class="cell small-12 text-center">                
                    <div class="head">
                        <div class="grid-x">
                            <div class="cell small-12">
                                <div class="hide-for-small-only">
                                    <div class="tof">
                                        <img src="" title="nom prenom, age situation fam." />
                                    </div>
                                </div>                                
                                <div class="id">
                                    <h1>
                                        <span class="fname">nom</span>
                                        <span class="lname">pr&eacute;nom</span>
                                    </h1>
                                    <ul>
                                        <li>Adresse</li>
                                        <li><i class="fas fa-phone-square-alt">&nbsp;</i>Tel</li>
                                        <li><i class="fas fa-mail-bulk"></i>&nbsp;<a href="">Courriel</a></li>
                                    </ul>
                                </div>
                                <div class="link">
                                    <ul>
                                        <li>
                                            <div class="icon">
                                                <a href="" target="_blank"><i class="fas fa-home"></i></a>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="icon">
                                                <a href="" target="_blank"><i class="fab fa-linkedin-in"></i></a>
                                            </div>
                                        </li>
                                        <li>
                                            <div class="icon hide">
                                                <a href="" target="_blank"><i class="fab fa-github-alt"></i></a>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                                <!--<div class="title hide">
                                    <h1>Programmeur</h1>
                                </div>-->
                                <%--<div class="skills">
                                    <ul>
                                        <li>
                                            <h3>R�seau, Hardware</h3>
                                            <ul>
                                                <li>Administration r�seaux locaux pour PME</li>
                                                <li>Int�gration solution SaaS</li>                        
                                                <li>Sp�cifications et maintenance mat�riel</li>
                                            </ul>
                                        </li>
                                        <li>
                                            <h3>D�veloppement</h3>
                                            <ul>
                                                <li>Logiciel (VB)</li>
                                                <li>Back-end (ASP,C#/.NET,SQL)</li>
                                                <li>Front-end (CSS,JQuery)</li>
                                            </ul>
                                        </li>
                                        <li>
                                            <h3>Management</h3>
                                            <ul>
                                                <li>Organisation de l'activit� d'un service</li>
                                                <li>D�finition des �volutions strat�giques</li>
                                                <li>Gestion des ressources humaines</li>
                                            </ul>
                                        </li>
                                    </ul>
                                </div>--%>
                            </div>
                        </div>
                    </div>
                    <div class="body">
                        <div class="grid-x">
                            <div class="cell small-12">
                                <div class="skills">
                                    <div id="sd-orbit-media" class="orbit cv-orbit" role="region" aria-label="Slide" data-orbit>
                                        <ul class="orbit-container">
                                            <li class="orbit-slide">
                                                <figure class="orbit-figure">
                                                    <img src=" " class="orbit-image">
                                                    <figcaption class="orbit-caption">
                                                        <div class="sd-slide-legend">
                                                            <h2>Texte de la slide</h2>
                                                            <a href="" class="sd-legend-link"></a>
                                                        </div>
                                                    </figcaption>
                                                </figure>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="grid-x">
                            <div class="cell small-12">                            
                                <div class="grid-x grid-padding-x">
                                    <div class="cell small-12 large-6 small-order-2 medium-order-2 large-order-1">
                                        <div class="exprof">
                                            <h3><i class="far fa-id-badge"></i>&nbsp;Exp&eacute;riences professionnelles</h3>
                                                <ul>
                                                    <li>
                                                        <ul>
                                                            <li><h4>exp. prof.</h4></li>
                                                            <li><h5>Soci&eacute;t&eacute;&nbsp;<span>(activit&eacute;)</span></h5></li>
                                                            <li><p>De date deb. &agrave; date fin</p></li>
                                                        </ul>
                                                        <ul class="description">
                                                            <li><p><i class="fas fa-palette"></i>&nbsp;Description 1.</p></li>                                                              </ul>                        
                                                    </li>                                                    
                                                </ul>
                                            <%--<ul>
                                                <li>
                                                    <ul>
                                                        <li><h4>programmeur</h4></li>
                                                        <li><h5>Strateges-Developpement&nbsp;<span>(Logiciels informatiques)</span></h5></li>
                                                        <li><p>De sept. 2009 � sept. 2019</p></li>
                                                    </ul>
                                                    <ul class="description">
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Programmation et maintenance d'un logiciel de gestion pour les Comit�s d'Entreprise d�velopp� sous VB.</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Conception, r�alisation et maintenance de sites Internet sous Classic ASP et C#/.NET.</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Conception et administration d'une infrastructure SaaS pour l'exploitation commerciale des solutions logicielles de l'entreprise.</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;S�lection et suivi du mat�riel (serveurs, switch, PC, portables, imprimantes...).</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Gestion des licences Microsoft (Open, Office 365).</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Gestion des noms de domaines, DNS, certificats SSL.</p></li>
                                                    </ul>                        
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li><h4>manager administratif</h4></li>
                                                        <li><h5>Generali Patrimoine S.A.<span>(Assurances)</span></h5></li>
                                                        <li><p>De janv. 2006 � sept. 2009</p></li>
                                                    </ul>
                                                    <ul class="description">
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Organisation, suivi de l�activit� et gestion administrative du service de num�risation.</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;Prise en charge des projets, des �volutions techniques et strat�giques du service.</p></li>
                                                        <li><p><i class="fas fa-palette"></i>&nbsp;�valuation des comp�tences des collaborateurs (15 personnes).</p></li>                            
                                                    </ul>                        
                                                </li>
                                            </ul>--%>
                                        </div>
                                        <%--<div class="formation">
                                            <h3><i class="fas fa-user-graduate"></i>&nbsp;Formation</h3>
                                            <ul>
                                                <li>
                                                    <ul>
                                                        <li><h4>BTS, Informatique de Gestion option Administrateur de R�seaux Locaux</h4></li>
                                                        <!--<li><h5></h5></li>-->
                                                        <li><p>De juil. 2008 � juil. 2009</p></li>
                                                    </ul>
                                                    <ul class="description">
                                                        <li><p>Dipl�me obtenu � l'issue d'une formation dans le cadre d'un FONGECIF.</p></li>
                                                    </ul>
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li><h4>Baccalaur�at, Sciences Economiques et Sociales</h4></li>
                                                        <li><h5>Lyc�e Joliot Curie - Dammarie L�s Lys (77)</h5></li>
                                                        <li><p>Ann�e 1990</p></li>
                                                    </ul>                        
                                                </li>
                                            </ul>
                                        </div>--%>
                                        <div class="formation">
                                            <h3><i class="fas fa-user-graduate"></i>&nbsp;Formation</h3>
                                            <ul>
                                                <li>
                                                    <ul>
                                                        <li><h4>formation universitaiire</h4></li>
                                                        <li><p>De date deb &agrave; date fin</p></li>
                                                    </ul>
                                                    <ul class="description">
                                                        <li><p>Description</p></li>
                                                    </ul>
                                                </li>
                                                <li>
                                                    <ul>
                                                        <li><h4>formation scolaire</h4></li>
                                                        <li><h5>Etablissement</h5></li>
                                                        <li><p>Ann&eacute;e</p></li>
                                                    </ul>                        
                                                </li>
                                            </ul>
                                        </div>             
                                    </div>                                
                                    <div class="cell small-12 large-6 small-order-1 medium-order-1  large-order-2">
                                        <div class="adm">
                                            <h3><i class="fas fa-server"></i>&nbsp;Administration</h3>
                                            <ul>
                                                <li><p><b>Comp&eacute;tence 1</b>&nbsp;:&nbsp;Liste</p></li>                                                
                                            </ul>
                                        </div>
                                        <div class="dev">
                                            <h3><i class="fab fa-stack-overflow"></i>&nbsp;Programmation</h3>
                                            <ul>
                                                <li><p><b>Comp&eacute;tence 1</b>&nbsp;:&nbsp;Liste</p></li>
                                            </ul>
                                        </div>
                                        <div class="sys">
                                            <h3><i class="fab fa-linux"></i>&nbsp;Syst&egrave;me d'exploitation</h3>
                                            <ul>
                                                <li><p><b>Comp&eacute;tence 1</b>&nbsp;:&nbsp;Liste</p></li>
                                            </ul>                
                                        </div>
                                        <div class="etc">
                                            <h3><i class="fas fa-expand"></i>&nbsp;Autres</h3>
                                            <ul>
                                                <li><p><b>Comp&eacute;tence 1</b>&nbsp;:&nbsp;Liste</p></li>
                                            </ul>
                                        </div>
                                        <%--<div class="adm">
                                            <h3><i class="fas fa-server"></i>&nbsp;Administration</h3>
                                            <ul>
                                                <li><p><b>Annuaire</b>&nbsp;:&nbsp;Active-Directory</p></li>
                                                <li><p><b>Base de donn�es</b>&nbsp;:&nbsp;SQL Server (jusqu'� 2017) (lab.: MySQL)</p></li>
                                                <li><p><b>Gestionnaire de contenu</b>&nbsp;:&nbsp;DotNetNuke</p></li>
                                                <li><p><b>Infrastructure</b>&nbsp;:&nbsp;OVH (serveurs d�di�s et vrack)</p></li>
                                                <li><p><b>Messagerie</b>&nbsp;:&nbsp;Exchange (2003 et 2013), HMailServer, GFI MailEssentials (lab.: Postfix)</p></li>
                                                <li><p><b>Pare-Feu</b>&nbsp;:&nbsp;CISCO ASA 5505, PFSense (lab.: Iptables)</p></li>
                                                <li><p><b>Sauvegarde</b>&nbsp;:&nbsp;Veeam, Acronis</p></li>
                                                <li><p><b>Services</b>&nbsp;:&nbsp;DNS, DHCP, Windows Remote Desktop Services (Remote App)</p></li>
                                                <li><p><b>Virtualisation</b>&nbsp;:&nbsp;Hyper-V (lab.: Proxmox et Dockers for Windows)</p></li>
                                                <li><p><b>Web</b>&nbsp;:&nbsp;IIS (lab.: Apache)</p></li>
                                            </ul>
                                        </div>
                                        <div class="dev">
                                            <h3><i class="fab fa-stack-overflow"></i>&nbsp;Programmation</h3>
                                            <ul>
                                                <li><p><b>Languages</b>&nbsp;:&nbsp;VB, C#, Cshtml, ASP, SQL, HTML, CSS, Javascript, Powershell</p></li>
                                                <li><p><b>Biblioth�ques</b>&nbsp;:&nbsp;JQuery, Knockout</p></li>
                                                <li><p><b>Gestionnaire de contenu</b>&nbsp;:&nbsp;Conception d'extensions pour le CMS DotNetNuke</p></li>
                                            </ul>
                                        </div>
                                        <div class="sys">
                                            <h3><i class="fab fa-linux"></i>&nbsp;Syst�me d'exploitation</h3>
                                            <ul>
                                                <li><p><b>Microsoft</b>&nbsp;:&nbsp;Windows Server (jusqu'� 2016), Windows 7 � 10.</p></li>
                                                <li><p><b>Unix</b>&nbsp;:&nbsp;FreeBSD (lab.: Debian, Linux, Kali).</p></li>
                                            </ul>                
                                        </div>
                                        <div class="etc">
                                            <h3><i class="fas fa-expand"></i>&nbsp;Autres</h3>
                                            <ul>
                                                <li><p><b>Graphisme</b>&nbsp;:&nbsp;PAINT.NET, Inkscape.</p></li>
                                                <li><p><b>Bureautique</b>&nbsp;:&nbsp;Office 2007 � 2013.</p></li>
                                            </ul>
                                        </div>--%>            
                                    </div>
                                </div>                                  
                                <div class="show-for-large">
                                    <span class="separator"></span>
                                </div>                                
                            </div>
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

<dnn:DnnJsInclude ID="DnnJsInclude1" runat="server" FilePath="Resources/foundation/js/vendor/what-input.js" PathNameAlias="SkinPath"   />
<dnn:DnnJsInclude ID="DnnJsInclude3" runat="server" FilePath="Resources/foundation/js/vendor/foundation.min.js" PathNameAlias="SkinPath"   />
<dnn:DnnJsInclude ID="DnnJsInclude4" runat="server" FilePath="Resources/js/sd.dnn.skin.min.js" PathNameAlias="SkinPath"   />

<script>
    
    $('html').toggleClass('no-js', true);
    $(document).foundation();
    $(window).on('load', function () {
        Foundation.reInit($('#cv-orbit'));
        setTimeout(function () {
            $('section.sd-no-js').toggleClass('sd-no-js', false);
            $('body').toggleClass('sd-loaded', true);
        }, 1050);
    });    
    $(document).ready(function () {
        SkinPageReady();        
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
            SkinPageReady();
        });
    }); 
        
</script>

