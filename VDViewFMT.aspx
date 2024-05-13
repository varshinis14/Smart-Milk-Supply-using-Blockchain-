<%@ Page Title="" Language="C#" MasterPageFile="~/VillageDairy.Master" AutoEventWireup="true" CodeBehind="VDViewFMT.aspx.cs" Inherits="SmartMilkSupply.VDViewFMT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div id="">
        <div class="main-page">
            <div class="tables">
                <div class="table-responsive bs-example widget-shadow">
                    <h4>
                        Farmer Milk Transaction Details</h4>
                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                    </asp:Table>
                    <asp:Label ID="lblMsg" runat="server" Text="Label"></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
