<%@ Page MasterPageFile="~/View/PP/Main.Master" Language="C#" AutoEventWireup="true" CodeBehind="GCBCFolder.aspx.cs" Inherits="GCBC_NextGen.View.PP.GCBCFolder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    window.onload
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">
        <div class="row menuRow">
            <div class="col-1 col-sm-1 col-md-1 col-lg-1 col-xl-1 p-0">
                <div class="customRed_BG smallCol fullHeight p-1" id="smallCol">
                    <div class="col-12 p-1">
                        <span class="mr-1 white" onclick="sideNav('menuBar', this);">
                            <svg width="20" height="20" viewBox="0 0 30 27" title="Portal">
                                <use class="hamburger_Lines" href="../../Content/custom_icons/hamburgerIcon.svg#hamburger_Lines"></use>
                            </svg>
                        </span>
                    </div>
                    <div id="menuBar" style="display: none;">
                        <div class="col-12 p-1">
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont fullWidth m-2plus rounded-left portalSelect">Brand Center</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Business Intelligence</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Sales Support</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Sitel Analytics</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">TSaas</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">TSC</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Learning Tribes</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Innso</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="p-0 display-4 fullWidth ml-2 rounded-left" style="top: 0vh; left: 1vw;"><b>Category</b></span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left portalSelect_Black">Photo Bank</span>
                                <div class=" mt-1 radio ml-4">
                                    <label class="fullWidth m-0 p-0">
                                        <input type="radio" name="optPhoto" value="E" checked="checked" /><span class="radioSearch">Events</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0">
                                        <input type="radio" name="optPhoto" value="S" /><span class="radioSearch">Staff Photos</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0">
                                        <input type="radio" name="optPhoto" value="T" /><span class="radioSearch">Team</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0">
                                        <input type="radio" name="optPhoto" value="F" /><span class="radioSearch">Facilities</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0">
                                        <input type="radio" name="optPhoto" value="A" /><span class="radioSearch">Associates</span>
                                    </label>
                                </div>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Video Library</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="leftFloat p-2 mediumFont pointer fullWidth m-2plus rounded-left">Branding Material</span>
                            </span>
                            <span class="mt-1 white">
                                <span class="p-0 display-4 fullWidth ml-2 rounded-left" style="top: 0vh; left: 1vw;"><b>File Type</b></span>
                                <div class="mt-1 checkbox ml-4">
                                    <label class="fullWidth m-0 p-0 smallFont">
                                        <input type="checkbox" value="JPG / PNG"><span class="checkboxSearch">JPG / PNG</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0 smallFont">
                                        <input type="checkbox" value="Vector"><span class="checkboxSearch">Vector</span>
                                    </label>
                                    <label class="fullWidth m-0 p-0 smallFont">
                                        <input type="checkbox" value="Video"><span class="checkboxSearch">Video</span>
                                    </label>
                                </div>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-11 col-sm-11 col-md-11 col-lg-11 col-xl-11 pt-2 pb-2 scrollDiv">
                <div class="container">
                    <div class="row">
                        <div class="col-12">
                            <h5 class="display-3 largeFont_2x customRed"><b>Brand Center</b></h5>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <div class="input-group">
                                <p class="gcbc-text mb-0 mediumFont pt-0 pr-2">See What's: </p>
                                <span class="input-group-addon pointer">
                                    <select class="dropdown mediumFont" name="sortType">
                                        <option value="N" selected="selected">New</option>
                                    </select>
                                </span>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="clearfix"></div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                        <div class="col-xs-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 mt-2 mb-2">
                            <div class="thumbnail p-5 customRed_BG display-5 ForShadowTiles pointer white textCenter">Folder 1</div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
