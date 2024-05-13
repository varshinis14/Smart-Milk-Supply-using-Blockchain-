<%@ Page Title="" Language="C#" MasterPageFile="~/Admin.Master" AutoEventWireup="true" CodeBehind="AddVillageDairy.aspx.cs" Inherits="SmartMilkSupply.AddVillageDairy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
        <div class="main-page">
            <div class="forms">
               
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                           Village Dairy Details:</h4>
                    </div>
                   <div class="form-body">
                        <div class="form-group">
                                    <label>
                                        Select Village</label>
                                    <asp:DropDownList ID="ddlvillage" runat="server" class="form-control">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlvillage"
                                        ForeColor="#FF3300" ValidationGroup="A" InitialValue="--Select--" runat="server"
                                        ErrorMessage="Please Select Village"></asp:RequiredFieldValidator>
                                </div>
                       
                        <div class="form-group">
                            <label for="exampleInputEmail1">
                               Enter Name</label><asp:TextBox ID="txtName" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="Enter Name" ControlToValidate="txtName" ForeColor="Red" 
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                        
                         <div class="form-group">
                            <label for="exampleInputEmail1">
                               Enter MobileNo</label><asp:TextBox ID="txtMobileNo" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ErrorMessage="Enter MobileNo" ControlToValidate="txtMobileNo" ForeColor="Red" 
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                 ErrorMessage="Only 10 Digits" ControlToValidate="txtMobileNo" ForeColor="Red" 
                                 ValidationExpression="[0-9]{10}" ValidationGroup="A"></asp:RegularExpressionValidator>
                        </div>
                         <div class="form-group">
                            <label for="exampleInputEmail1">
                               Enter Address</label><asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ErrorMessage="Enter APMC Address" ControlToValidate="txtAddress" ForeColor="Red" 
                                ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                        <asp:Button ID="btnSave" runat="server" class="btn btn-primary pull-right" Text="Save" 
                            onclick="btnSave_Click" ValidationGroup="A" />
                        
                    </div>
                </div>
          </div>
          </div>     
    </div>
</asp:Content>
