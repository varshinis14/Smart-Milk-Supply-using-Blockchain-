<%@ Page Title="" Language="C#" MasterPageFile="~/Farmer.Master" AutoEventWireup="true" CodeBehind="FarmerViewMilkTransaction.aspx.cs" Inherits="SmartMilkSupply.FarmerViewMilkTransaction" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
        <div class="main-page">
            <div class="forms">
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                           Milk Transaction - Village Dairy:</h4>
                    </div>
                    <div class="form-body">
                       
                         
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="tables">
                                <div class="table-responsive bs-example widget-shadow">
                                    <h4>
                                        Milk Transaction Details</h4>
                                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                                    </asp:Table>
                                    <br />
                                </div>
                            </div>
                            <div class="tables">
                                <div class="table-responsive bs-example widget-shadow">
                                    <h4>
                                        Milk Payment Details</h4>
                                    <asp:Table ID="Table2" runat="server" class="table table-bordered">
                                    </asp:Table>
                                    <br />
                                    <asp:Label ID="lblPayment" runat="server" Text=""></asp:Label>
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
