<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VariableElimination.aspx.cs" Inherits="VariableElimination" %>

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
                        <font style="font-weight: bold; font-size: large"> Loan Prediction - Using Variable Elimination </font>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-top: 10px">*Please select query</td>
                </tr>
                <tr>
                    <td style="padding-top: 10px; width: 200px">Select Query: </td>
                    <td style="padding-top: 10px;">
                        <asp:DropDownList ID="ddlQuery" runat="server" AutoPostBack="false">
                            <asp:ListItem Text="Select" Value="0"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Loan Type = Auto)" Value="loan_type"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Loan Type = Credit)" Value="loan_type"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Loan Type = Home)" Value="loan_type"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Loan Type = Personal)" Value="loan_type"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Credit Score = Excellent)" Value="credit_score"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Credit Score = Good)" Value="credit_score"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Credit Score = Fair)" Value="credit_score"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Credit Score = Poor)" Value="credit_score"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Employment Status = Employed)" Value="employment_status"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Employment Status = Unemployed)" Value="employment_status"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Residency = Resident)" Value="residency_status"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Residency = Non-Resident)" Value="residency_status"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Age = <25)" Value="age"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Age = 25-50)" Value="age"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Age = >50)" Value="age"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Debt-to-Income Ratio = <15)" Value="dbi_ratio"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Debt-to-Income Ratio = 15-25)" Value="dbi_ratio"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Debt-to-Income Ratio = 25-35)" Value="dbi_ratio"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Debt-to-Income Ratio = >35)" Value="dbi_ratio"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Marital Status = Married)" Value="marital_status"></asp:ListItem>
                            <asp:ListItem Text="P(Loan Decision | Marital Status = Single)" Value="marital_status"></asp:ListItem>
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
