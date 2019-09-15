<%@ Page Title="" Language="C#" MasterPageFile="~/View/PP/Main.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="GCBC_NextGen.View.PP.SITE2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container pb-4">
        <div class="row">
            <div class="col-12 p-4 textCenter">
                <h5 class="display-3 largeFont_2x">Welcome, <span class="customRed"><b>
                    <asp:Label runat="server" ID="lblLoginName"></asp:Label></b></span>!</h5>
            </div>
            <div class="col-12 pb-2">
                <ul class="p-0 m-0 liNone centerAlign mb-2">
                    <li class="largeFont_1x liInline active pb-1 mr-3">
                        <a class="blackIcon pl-3 pr-3 tabButtons customRed setBold" href="javascript:void(0);" onclick="switchTabs('browsePortal', this);">Browse Portals
                        </a>
                    </li>
                    <li class="largeFont_1x liInline pb-1 ml-3">
                        <a class="blackIcon pl-3 pr-3 tabButtons" href="javascript:void(0);" onclick="switchTabs('newPortal', this);">See What's New
                        </a>
                    </li>
                </ul>
            </div>
            <div class="col-12 pt-2 portalPreview" id="browsePortal">
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('BC')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Brand Center</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('BI')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Business Intelligence</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('SS')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Sales Support</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('SA')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Sitel Analytics</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('TS')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">TSaas</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('TSC')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">TSC</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('LT')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Learning Tribes</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('IN')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Innso</div>
                </div>
                <div class="col-12 col-xs-12 col-sm-12 col-md-6 col-lg-4 col-xl-4 leftFloat mb-2 mt-2 rounded " onclick="navigate('PC')">
                    <div class="white_BG p-4 display-5 textCenter ForShadowTiles pointer">Proposal Content</div>
                </div>
            </div>
            <div class="col-12 pt-2 portalPreview" id="newPortal" style="display: none;">
            </div>
        </div>
    </div>
    <div class="container-fluid pt-4" id="favourites">
        <div class="row customRed_BG pt-4 pb-4">
            <div class="col-12 pl8 textCenter white">
                <svg class="whiteStar leftFloat mr-2" width="40" height="40" viewBox="0 0 22 25">
                    <use href="../../Content/custom_icons/starIcon.svg#star"></use>
                </svg>
                <h5 class="display-3 largeFont_2x leftFloat">Your Favourites
                </h5>
            </div>
            <div class="col-1">
            </div>
            <div class="col-9 pt-2 ml-5" id="divFavourites">
            </div>
            <div class="col-12 textCenter fullWidth">
                <div class="fullWidth" id="showMore">
                    <span class="display-3 largeFont_1x leftFloat m-0 fullWidth">Show All</span>
                    <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" width="35" height="35" viewBox="0 0 54 54" class="pointer" onclick="showMore('showMore');">
                        <defs>
                            <clipPath id="a">
                                <rect width="54" height="54" transform="translate(-0.477 -0.477)" fill="#fff" />
                            </clipPath>
                        </defs>
                        <g transform="translate(0.477 53.523) rotate(-90)" opacity="0.4" clip-path="url(#a)">
                            <path d="M35.337,28.679,46.627,17.526a3.794,3.794,0,0,0,0-5.41,3.9,3.9,0,0,0-5.474,0L27.131,25.975a3.791,3.791,0,0,0,0,5.4L41.154,45.238a3.9,3.9,0,0,0,5.471,0,3.792,3.792,0,0,0,0-5.407Z" transform="translate(-10.476 -2.473)" />
                        </g>
                    </svg>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
