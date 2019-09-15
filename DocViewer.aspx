<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DocViewer.aspx.cs" Inherits="GCBC_NextGen.View.PP.DocViewer" %>

<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="content-type" content="text/html;charset=utf-8"/>
    <title>Amazing Slider</title>
    
    <!-- Insert to your webpage before the </head> -->
    
    <script src="../../sliderengine/jquery.js"></script>
    <script src="../../sliderengine/amazingslider.js"></script>
    <link href="../../sliderengine/amazingslider-1.css" rel="stylesheet" />
    <script src="../../sliderengine/initslider-1.js"></script>
    <!-- End of head section HTML codes -->

    <style>
        .amazingslider-space-1 {
            height:450px !important;
        }
    </style>
    
</head>
<body style="background-color:darkgray;">
    
    <!-- Insert to your webpage where you want to display the slider -->
    <div id="amazingslider-wrapper-1" style="display:block;position:relative;max-width:900px;padding-left:0px; padding-right:148px;margin:0px auto 0px;">
        <div id="amazingslider-1" style="display:block;position:relative;margin:0 auto;">
            <ul class="amazingslider-slides" style="display:none;">
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/<%# Eval("Image_FilePath") %>" alt="<%# Eval("Image_FileName") %>" title="<%# Eval("Image_FileName") %>" />
                        </li>
                    </ItemTemplate>                    
                </asp:Repeater>
            </ul>
            <ul class="amazingslider-thumbnails" style="display:none;">
                <asp:Repeater ID="Repeater2" runat="server">
                    <ItemTemplate>
                        <li>
                            <img src="https://us145k12gcaspp1.nac.sitel-world.net/GrowthCenter/<%# Eval("Image_FilePath") %>" alt="<%# Eval("Image_FileName") %>" title="<%# Eval("Image_FileName") %>" />
                        </li>
                    </ItemTemplate>                    
                </asp:Repeater>
            </ul>
        <div class="amazingslider-engine"><a href="http://amazingslider.com" title="jQuery Slider">jQuery Slider</a></div>
        </div>
    </div>
    <!-- End of body section HTML codes -->
    
</body>
</html>