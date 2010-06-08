<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SampleGordon.aspx.cs" Inherits="TestWeb.SampleGordon" %>

<%@ Register assembly="nkSWFControl" namespace="nkSWFControl" tagprefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <cc1:SWFControl ID="SWFControl1" runat="server" 
            PublishingMethod="Gordon"  
            SitePath="C:\Projects\nkSWFControl\nkSWFControl\Samples" 
            Movie="trip.swf"
            Width="162px" Height="162px"
        >
        </cc1:SWFControl>
    
    </div>
    </form>
</body>
</html>
