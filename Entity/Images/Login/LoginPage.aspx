<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link rel="stylesheet"  href="CSS/Style.css" type="text/css" /> 

</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID ="ScriptManager1" runat="server" EnablePartialRendering="true"  >
  </asp:ScriptManager>
      
                <!-- header wrapper -->
                <%--<div style="width: 73.5%; margin-left: 13.3%">
                    <div class="" style=" margin-top: -6%;">



                        
                        <div class="loginDetails">
                            <div class="DivUserNameNew">
                                <asp:Label ID="Label11" runat="server" Font-Bold="true" Text="" ForeColor="#B41B63"> </asp:Label>
                                &nbsp;                                    
                    
                            </div>
                            <div class="signOut">
                                <asp:Label ID="lblName" Visible="false" runat="server" Text="Label"></asp:Label>                              
                            </div>
                        </div>

                        <div class="clr"></div>
                    </div>
                </div>--%>
                <!-- header wrapper -->

                <!-- top menu wrapper -->
               <%-- <div style="width: 73.5%; margin-left: 13.3%; box-shadow: 0 15px 10px -10px rgba(0, 0, 0, 0.5), 0 1px 4px rgba(0, 0, 0, 0.3), 0 0 40px rgba(0, 0, 0, 0.1) inset;height: 20px;background-color:#050100 ;">
                    <div style="margin-left: 40%;">                        
                    </div>
                </div>--%>
                <!-- top menu wrapper -->
         
            <!-- page content wrapper -->


        <div class="pageContentWrapper" style="padding-top: 180px; height: 100%; width: 100%; margin-left:11%;     margin-top:3%;background-image:url(Images/background_Image.png); background-repeat:no-repeat;" >
            <div class="Content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>

                     <asp:Panel ID="pnldetails" runat="server" DefaultButton="btnLogin">
                       
                      <%--  <div class="pageField">

                            <div class="field" style="margin-left: 21%; border-style: double; border-color: gray; width: 50%; height: 200px; margin-top: -10%; background-color:#F3F3F3">


                                <div style="padding: 10% 1% 1% 10%;">

                                    <div class="field field300">
                                       
                                        <strong class="strong80"><b>User Name :</b> </strong>
                                        <asp:TextBox ID="txtUserName" type="text" CssClass="txt txt210" runat="server"></asp:TextBox>
                                          <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtUserName" ErrorMessage="*" ForeColor="Red" />
                                    </div>
                                    <div class="clr">
                                    </div>

                                    <div class="field field300">
                                        <strong class="strong80"><b>Password :</b> </strong>
                                        <asp:TextBox ID="txtPassword" type="password" runat="server" CssClass="txt txt210"></asp:TextBox>
                                       <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtPassword" ErrorMessage="*" ForeColor="Red" />
                                    </div>

                                    <div class="clr">
                                    </div>
                                    <div class="buttonHolder" style="margin-left: 38%; margin-top: 2%;">
                                        <asp:Button ID="btnLogin" runat="server" Text="Login"   CssClass="MyButton blueButton" OnClick="btnLogin_Click" />
                                    </div>
                                     

                                    <div class="clr">
                                    </div>
                                </div>
                            </div>
                            <div class="clr">
                            </div>

                        </div>--%>


                        <div class="pageField"  >
                                                                              
                                                    <div style="margin-top:10%; margin-left:30%  " >                                
                                       <div class="field field250">
                                            <strong class="strong10" ></strong>
                                           <asp:TextBox ID="txtUserName" type="text" runat="server"  class="box1 border1" placeholder= "Username" ></asp:TextBox>
                                            <asp:Label ID="Label4" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                                        </div>
                                       <br />
                                                       
                                     <div class="clr">
                                    </div>
                                        <div class="field field250">
                                            <strong class="strong10" > </strong>
                                        <asp:TextBox ID="txtPassword" type="password" runat="server" class="box1 border2" placeholder="Password"   ></asp:TextBox>
                                         
                                              <asp:Label ID="Label1" runat="server" Text="*" ForeColor="#FF3300"></asp:Label>
                                        </div>
                                            
                                          <div class="clr">
                                    </div>
                                                        <br />
                                       <div class="field field200">
                                          <%-- <asp:Button ID="btnLogin" runat="server" Text="LOGIN" CssClass="css_button" onclick="btnLogin_Click"  />--%>
                                           <asp:ImageButton ID="btnLogin" runat="server" onclick="btnLogin_Click" style="margin-left: 5%;" ImageUrl="~/Images/login_button.png" mouseOver="~/Images/login_pressed.png"/>
                                        </div>

                                  
                                              </div>
                            <div class="clr">
                                    </div>
                                                  </div>
                            
                         </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="clr">
                                    </div>

            </div>
            <div class="clr">
                                    </div>
                </div>

        
    

            <!-- page content wrapper -->


            <!-- footer wrapper -->
           <!-- <div style="margin-left:13.3%">
                <div class="footerWrapper">
                    <div class="footer" style="width:73.5%;">
                        <div style="float: left"></div>
                         <asp:Label ID="Label12" runat="server" Text="Copyright © 2016 ServPRO Technologies Pvt. Ltd. All Rights Reserved." ForeColor="#FFFFFF"></asp:Label>
                        
                 <%--All Rights Reserved © ServPRO Technologies Pvt. Ltd.--%>
                    </div>


                    <div class="clr"></div>
                </div>
            </div> -->

            <!-- footer wrapper -->
            
    <!-- page wrapper -->



    
    </form>
</body>
</html>
