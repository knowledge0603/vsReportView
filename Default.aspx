<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>无标题页</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server" 
            onselectedindexchanged="DropDownList1_SelectedIndexChanged" 
            AutoPostBack="True">
            <asp:ListItem>2011</asp:ListItem>
            <asp:ListItem>2012</asp:ListItem>
            <asp:ListItem>2013</asp:ListItem>
            <asp:ListItem>2014</asp:ListItem>
            <asp:ListItem>2015</asp:ListItem>
            <asp:ListItem>2016</asp:ListItem>
        </asp:DropDownList>
        <asp:Label ID="Label1" runat="server" Text="年土地储备融资情况（面积：公顷，金额：万元）"></asp:Label>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" 
            Font-Size="8pt" InteractiveDeviceInfos="(集合)" WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt" Width="923px" Height="748px">
            <LocalReport ReportPath="Report1.rdlc">
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource4" Name="储备标识数据集" />
                </DataSources>
                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource5" Name="T_data" />
                </DataSources>

                <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource6" Name="资金来源" />
                </DataSources>
                   <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource8" Name="土地储备情况" />
                </DataSources>
                 <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource9" Name="供应情况" />
                </DataSources>

                 <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource10" Name="土地融资" />
                </DataSources>

                 <DataSources>
                    <rsweb:ReportDataSource DataSourceId="ObjectDataSource11" Name="融资抵押面积" />
                </DataSources>
            </LocalReport>
        </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource4" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.储备标识TableAdapter">
        </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="ObjectDataSource5" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.DataTable1TableAdapter">
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="ObjectDataSource6" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.DataTable2TableAdapter">
        </asp:ObjectDataSource>
         <asp:ObjectDataSource ID="ObjectDataSource8" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.TDCBQKTableAdapter">
        </asp:ObjectDataSource>
           <asp:ObjectDataSource ID="ObjectDataSource9" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.V_CBTDGYQKTableAdapter">
        </asp:ObjectDataSource>

        <asp:ObjectDataSource ID="ObjectDataSource10" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.T_TDRZTableAdapter">
        </asp:ObjectDataSource>

            <asp:ObjectDataSource ID="ObjectDataSource11" runat="server" 
            SelectMethod="GetData" TypeName="TDCB_DataSetTableAdapters.融资抵押面积TableAdapter">
        </asp:ObjectDataSource>
    </div>
    </form>
</body>
</html>
