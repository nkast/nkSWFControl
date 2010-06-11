<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleNestedObjects.aspx.cs" Inherits="TestWeb.SampleNestedObjects" %>

<%@ Register assembly="nkSWFControl" namespace="nkSWFControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:SWFControl ID="SWFControl1" runat="server" Height="320px" 
            PublishingMethod="NestedObject" Width="540px" 
             >
            <AlternativeContentTemplate>
            <div>
				<h1>Alternative content</h1>
				<p><a href="http://www.adobe.com/go/getflashplayer"><img src="http://www.adobe.com/images/shared/download_buttons/get_flash_player.gif" alt="Get Adobe Flash player" /></a></p>
			</div>
            </AlternativeContentTemplate>
        </cc1:SWFControl>
    
    </div>
    <br />
    </form>
</body>
</html>
