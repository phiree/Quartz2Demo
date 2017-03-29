<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" href="css/screen.css" type="text/css" media="screen, projection" />
    <title></title>
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div>
                <div>
                    <asp:Button runat="server" ID="btnPayNewOrder" Text="CreateOrderNeedShipping" OnClick="btnPayNewOrder_Click" />
                </div>
                <asp:Repeater runat="server" ID="rptRuningJobs" OnItemCommand="rptRuningJobs_ItemCommand">
                    <HeaderTemplate>
                        <table>
                            <thead>
                                <tr><td></td>
                                    <td>OrderId</td>
                                    <td>JobStartTime</td>
                                    <td>PreviousFireTime</td>
                                    <td>NextFireTime </td>
                                    <td>TriggerState </td>
                                    <td></td>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td><%# Container.ItemIndex + 1 %></td>
                            <td><%#Eval("OrderId") %> </td>
                            <td><%#Eval("StartTime") %> </td>
                            <td><%#Eval("PreviousFireTime")%></td>
                            <td><%#Eval("NextFireTime") %> </td>
                            <td><%#Eval("TriggerState") %></td>
                                <td>
                                    <asp:Button runat="server" ID="btnDeleteJob" CommandName="deletejob" CommandArgument='<%#Eval("JobKeyName") %>' Text="DeleteThisJob" />
                                </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
              </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
        </form>

    </div>
</body>
</html>
