<%@ Page Language="C#" AutoEventWireup="true" CodeFile="print-options.aspx.cs" Inherits="learning_printing_print_options" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Print Options</title>
    <link href="../../styles/style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    
    a,
    a:link,
    a:visited,
    a:active
    {
	    font-family: Arial, Helvetica, Sans-Serif;
	    font-size: 12px;
	    text-decoration: none;
	    color: #3b3b3b;
    }

    a:hover
    {
        text-decoration:underline;
        color: #039;
    }
    
    fieldset
    {
        border:0px;
    }
    
    .aspbtn,
    .aspbtnContents
    {
        height:24px;
        border:solid 1px #454545;
        color:#525252;
        background:url('/images/aspbtn_bg2.png') repeat-x;
        vertical-align:middle;
        font-weight:bold;
        font-size:9pt;
        display:block;
        width:80px;
        margin:0 auto;
        vertical-align:top\9 /* IE Hack */
    }

    .aspbtn:hover,
    .aspbtnContents:hover
    {
        border:solid 1px #ff6600;
        cursor:pointer;
        color:#000000;
    }
    </style>
</head>
<body style="	
    font-family: Arial, Helvetica, Sans-Serif;
	font-size: 12px;
	color:#666;
	text-align:left;">
    <form id="form1" runat="server">
    <div>
        <fieldset style="background:#eee; border-top:solid 1px #AAA; border-bottom:solid 1px #AAA;">
            <div>
                <asp:RadioButtonList runat="server" ID="rbl_options">
                    <asp:ListItem Value="Training" Text="Print all slides." />
                    <asp:ListItem Value="Slide" Text="Print current slide only." />
                </asp:RadioButtonList>
            </div>
        </fieldset>
        <fieldset>
            <div>
                <asp:CheckBox runat="server" ID="cb_include_notebook" Text="Include notebook." />
            </div>
        </fieldset>
    </div>
    <br /><br />
    <div>
        <asp:Button runat="server" ID="lb_print" OnClick="lb_print_OnClick" Text="Ok" CssClass="aspbtn" />
    </div>
    <asp:Literal runat="server" ID="lit_message_log" />
    </form>
</body>
</html>
