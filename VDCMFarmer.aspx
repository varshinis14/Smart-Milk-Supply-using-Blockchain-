<%@ Page Title="" Language="C#" MasterPageFile="~/VillageDairy.Master" AutoEventWireup="true" CodeBehind="VDCMFarmer.aspx.cs" Inherits="SmartMilkSupply.VDCMFarmer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="">
        <div class="main-page">
            <div class="forms">
               
                <div class="form-grids row widget-shadow" data-example-id="basic-forms">
                    <div class="form-title">
                        <h4>
                          Farmer Milk Sales Details:</h4>
                    </div>
                   <div class="form-body">
                      <div class="form-group">
                           <label for="exampleInputEmail1">
                              Select Farmer</label><asp:DropDownList ID="ddlFarmer" class="form-control" runat="server">
                              </asp:DropDownList>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="Select Farmer" ControlToValidate="ddlFarmer" ForeColor="Red" 
                                InitialValue="--Select--" ValidationGroup="A"></asp:RequiredFieldValidator>
                        </div>
                         
                         
                         <div class="form-group">
                            <label for="exampleInputEmail1">
                               Milk Milliliter</label><asp:TextBox ID="txtMilkML" class="form-control" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                                ErrorMessage="Milk Milliliter" ControlToValidate="txtMilkML" ForeColor="Red" 
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
