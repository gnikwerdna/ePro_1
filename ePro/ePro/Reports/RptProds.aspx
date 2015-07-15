<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptProds.aspx.cs" Inherits="ePro.Reports.RptProds" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 447px; width: 831px">
    
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="827px">
            <LocalReport ReportPath="Reports\rptprods.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlProdList_Ds" Name="ProdList_DS" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlProdList_Ds" runat="server" ConnectionString="<%$ ConnectionStrings:cmdstrings %>" SelectCommand="SELECT * FROM [ProductListings]"></asp:SqlDataSource>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    
    </div>
    </form>
</body>
</html>
