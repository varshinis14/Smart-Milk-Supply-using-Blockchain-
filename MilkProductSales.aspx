<%@ Page Title="" Language="C#" MasterPageFile="~/MilkBooth.Master" AutoEventWireup="true" CodeBehind="MilkProductSales.aspx.cs" Inherits="SmartMilkSupply.MilkProductSales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
        <div class="main-page">
            <div class="forms">
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                            Milk Product Sales:</h4>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <label for="exampleInputEmail1">
                                Select Milk Product</label>
                            <asp:DropDownList ID="ddlProduct" runat="server" class="form-control" AutoPostBack="True"
                                OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select Milk Product"
                                ControlToValidate="ddlProduct" InitialValue="--Select--" ForeColor="Red"
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                         <div class="form-group">
                            <label for="exampleInputEmail1">
                                Sales Qty</label><asp:TextBox ID="txtQty" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Qty"
                                ControlToValidate="txtQty" ForeColor="Red" ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="tables">
                                <div class="table-responsive bs-example widget-shadow">
                                    <h4>
                                        Milk Product Stock Details</h4>
                                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                                    </asp:Table>
                                    <br />
                                </div>
                            </div>
                        </asp:Panel>
                        <br />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
