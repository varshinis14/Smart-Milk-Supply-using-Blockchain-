<%@ Page Title="" Language="C#" MasterPageFile="~/MilkBooth.Master" AutoEventWireup="true" CodeBehind="AddMBWallet.aspx.cs" Inherits="SmartMilkSupply.AddMBWallet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
        <div class="main-page">
            <div class="forms">
               
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                            Wallet Details</h4>
                    </div>
                    <div class="form-body">
                        <div class="form-group">
                            <label for="exampleInputEmail1">
                               Enter Amount</label><asp:TextBox ID="txtAmt" class="form-control" runat="server"></asp:TextBox>
                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="Enter Amount" ControlToValidate="txtAmt" ForeColor="Red" 
                                ValidationGroup="A" ></asp:RequiredFieldValidator>
                            
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnSave" runat="server" class="btn btn-primary pull-right" Text="Save" ValidationGroup="A"
                            onclick="btnSave_Click" />
                        
                    </div>
                </div>
          </div>
          </div>     
    </div>
</asp:Content>
