<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoanPrediction.aspx.cs" Inherits="LoanPrediction" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td colspan="2" style="padding-top: 10px; text-align: center">
                        <font style="font-weight: bold; font-size: large"> Loan Prediction - Using Naive Bayes </font>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 10px">*Select atleast one attribute on the basis of which the probability of loan decision will be calculated
                    </td>
                </tr>
                <tr>
                    <td style="padding-top: 10px; width: 200px">Age: </td>
                    <td style="padding-top: 10px;">
                        <asp:DropDownList ID="ddlAge" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="< 25" Value="1"></asp:ListItem>
                            <asp:ListItem Text="25 to 50" Value="2"></asp:ListItem>
                            <asp:ListItem Text="> 50" Value="3"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Residency Status: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlResidency" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Resident" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Non-Resident" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Marital Status: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Married" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Single" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Employment Status: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlEmploymentStatus" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Employed" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Unemployed" Value="2"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Credit Score: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlCreditScore" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Excellent(> 800)" Value="Excellent"></asp:ListItem>
                            <asp:ListItem Text="Good(700 to 800)" Value="Good"></asp:ListItem>
                            <asp:ListItem Text="Fair(600 to700)" Value="Fair"></asp:ListItem>
                            <asp:ListItem Text="Poor(< 600)" Value="Poor"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Debt-to-Income Ratio: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlDTI" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="< 15" Value="1"></asp:ListItem>
                            <asp:ListItem Text="15 to 25" Value="2"></asp:ListItem>
                            <asp:ListItem Text="25 to 35" Value="3"></asp:ListItem>
                            <asp:ListItem Text="> 35" Value="4"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="padding-top: 10px">Loan Type: </td>
                    <td style="padding-top: 10px">
                        <asp:DropDownList ID="ddlLoanType" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Auto" Value="1"></asp:ListItem>
                            <asp:ListItem Text="Credit" Value="2"></asp:ListItem>
                            <asp:ListItem Text="Home" Value="3"></asp:ListItem>
                            <asp:ListItem Text="Personal" Value="4"></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td></td>
                    <td style="padding-top: 20px">
                        <asp:Button ID="btnCalculate" runat="server" Text="Calculate Probability" OnClick="btnCalculate_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 10px">
                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
