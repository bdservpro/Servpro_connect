<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Login</title>
    <link rel="stylesheet" href="https://netdna.bootstrapcdn.com/bootstrap/3.0.2/css/bootstrap.min.css"/>

    <link href="CSS/login.css" rel="stylesheet" />
</head>
<body >
    
  <div class="wrapper">
    <form id="Form1" class="form-signin"  runat="server">       
      <h2 class="form-signin-heading">Please login</h2>


      <input type="text" class="form-control" name="username" id="username" runat="server" placeholder="Username" required="" autofocus="" />
      <input type="password" class="form-control" name="password" id="password" runat="server" placeholder="Password" required=""/>      
      <asp:Button ID="Button1" runat="server" CssClass="btn btn-lg btn-primary btn-block" Text="Login" OnClick="btnLogin_Click" />
      
    </form>
  </div>
    
</body>
</html>

