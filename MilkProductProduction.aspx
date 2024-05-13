<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="MilkProductProduction.aspx.cs" Inherits="SmartMilkSupply.MilkProductProduction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="">
        <div class="main-page">
            <div class="forms">
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                            Milk Product Production:</h4>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <label>
                                Select Product</label>
                            <asp:DropDownList ID="ddlProduct" runat="server" class="form-control">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlProduct"
                                ForeColor="#FF3300" ValidationGroup="A" InitialValue="--Select--" runat="server"
                                ErrorMessage="Please Select Product"></asp:RequiredFieldValidator>
                        </div>
                        <div class="form-group">
                            <label for="exampleInputEmail1">
                                Production Qty</label><asp:TextBox ID="txtQty" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter Production Qty"
                                ControlToValidate="txtQty" ForeColor="Red" ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnSave" runat="server" class="btn btn-primary pull-right" Text="Save"
                            OnClick="btnSave_Click" ValidationGroup="A" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
