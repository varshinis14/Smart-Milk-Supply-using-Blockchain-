<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true"
    CodeBehind="ApproveMBMPOrder.aspx.cs" Inherits="SmartMilkSupply.ApproveMBMPOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="">
        <div class="main-page">
            <div class="forms">
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                           Milk Product:</h4>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <label for="exampleInputEmail1">
                                Select Product</label><asp:DropDownList ID="ddlProduct" class="form-control" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="tables">
                <div class="table-responsive bs-example widget-shadow">
                    <h4>
                        Milk Booth Order Place Details</h4>
                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                    </asp:Table>
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server">
                <div class="tables">
                    <div class="table-responsive bs-example widget-shadow">
                        <h4>
                            Milk Product Stock Details</h4>
                        <asp:Table ID="Table2" runat="server" class="table table-bordered">
                        </asp:Table>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
