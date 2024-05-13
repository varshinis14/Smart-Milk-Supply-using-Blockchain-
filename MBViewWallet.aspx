<%@ Page Title="" Language="C#" MasterPageFile="~/MilkBooth.Master" AutoEventWireup="true" CodeBehind="MBViewWallet.aspx.cs" Inherits="SmartMilkSupply.MBViewWallet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
			<div class="main-page">
           
				<div class="tables">
                <div class="table-responsive bs-example widget-shadow">
						<h4>Wallet Details</h4>
                    <asp:Table ID="Table1" runat="server" class="table table-bordered">
                    </asp:Table>
                     <asp:Label ID="lblMsg" class="pull-right" runat="server" Text="Label"></asp:Label>
                     <br />
					</div>
                   
          </div>
          </div>     
    </div>
</asp:Content>
