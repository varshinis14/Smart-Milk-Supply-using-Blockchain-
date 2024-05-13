<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="SmartMilkSupply.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="en">
<head>
    <title>Smart Milk Supply</title>
    <!-- Meta tag Keywords -->
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="Trendy Flat Login Form Responsive Widget,Login form widgets, Sign up Web forms , Login signup Responsive web form,Flat Pricing table,Flat Drop downs,Registration Forms,News letter Forms,Elements" />
    <script type="application/x-javascript"> addEventListener("load", function() { setTimeout(hideURLbar, 0); }, false);
function hideURLbar(){ window.scrollTo(0,1); } </script>
    <!-- Meta tag Keywords -->
    <!--font-awesome-css-->
    <link href="css/font-awesome.css" rel="stylesheet">
    <!--//font-awesome-css-->
    <!-- css files -->
    <link href="css/style_css.css" rel="stylesheet" type="text/css" media="all">
    <!-- online-fonts -->
    <link href="//fonts.googleapis.com/css?family=Roboto+Slab:100,300,400,700Roboto:100,100i,300,300i,400,400i,500,500i,700,700i,900,900iSlabo+27px&subset=cyrillic,cyrillic-ext,greek,greek-ext,latin-ext,vietnamese"
        rel="stylesheet">
    <!--//online-fonts -->
    <body>
        <!--header-->
        <div class="agileheader">
            <h1>
                Smart Milk Supply Login</h1>
        </div>
        <!--//header-->
        <!--main-->
        <section class="main-w3l">
	<div class="w3layouts-main">
		<h2>Login Now</h2>
			<div class="w3ls-form">
				<form id="form1" runat="server">
                <div class="email-w3ls">
                      Select User Type:<asp:DropDownList 
                          runat="server" ID="ddlUserType">
                          <asp:ListItem>--Select--</asp:ListItem>
                          <asp:ListItem>Admin</asp:ListItem>
                          <asp:ListItem>VillageDairy</asp:ListItem>
                          <asp:ListItem>MilkBooth</asp:ListItem>
                          <asp:ListItem>Farmer</asp:ListItem>
                      </asp:DropDownList>
						 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                          ErrorMessage="Select User Type" ForeColor="Red" ValidationGroup="A" 
                          ControlToValidate="ddlUserType" InitialValue="--Select--"></asp:RequiredFieldValidator>
					</div>	
					<div class="email-w3ls">
                        <asp:TextBox runat="server" placeholder="Enter User Id" ID="txtUserId"></asp:TextBox>
						<span class="icon1"><i class="fa fa-envelope" aria-hidden="true"></i></span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter User Id" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtUserId"></asp:RequiredFieldValidator>
					</div>	
					<div class="w3ls-password">
						<asp:TextBox runat="server" placeholder="Enter Password" ID="txtPassword" 
                            TextMode="Password"></asp:TextBox>
						<span class="icon3"><i class="fa fa-lock" aria-hidden="true"></i></span>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter User Id" ForeColor="Red" ValidationGroup="A" ControlToValidate="txtPassword"></asp:RequiredFieldValidator>
					</div>	
					
						<div class="clear"></div>
                        <asp:Button runat="server" Text="Login" ID="btnLogin" ValidationGroup="A"
                        onclick="btnLogin_Click"></asp:Button>
                        <asp:Button runat="server" Text="Home" ID="btnHome"  onclick="btnHome_Click"></asp:Button>

                        <div class="clear"></div>
                       
				</form>
			</div>	
			<div class="w3ls-bottom">
            <asp:Label runat="server" Text="" ID="lblMsg"></asp:Label>
				
			</div>	
	</div>	
</section>
        <!--//main-->
        <!--footer-->
        <div class="footer-w3l">
        </div>
        <!--//footer-->
    </body>
</html>
