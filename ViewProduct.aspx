<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="ViewProduct.aspx.cs" Inherits="SmartMilkSupply.ViewProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="">
        <div class="main-page">
            <div class="tables">
                <div class="table-responsive bs-example widget-shadow">
                    <h4>
                        Product Details</h4>
                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                    </asp:Table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
