<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RptAuditList.aspx.cs" Inherits="ePro.Reports.RptAuditList" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="285px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1033px">
            <LocalReport ReportPath="Reports\rptaudit.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="AuditData_DS" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:cmdstrings %>" SelectCommand="SELECT * FROM [Audits]"></asp:SqlDataSource>
    </form>
</body>
</html>
