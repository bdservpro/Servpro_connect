<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SyncTasks.aspx.cs" Inherits="SyncTasks" Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sync Tasks</title>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <script src="http://jdewit.github.io/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>


    <style>
        .btn-primary {
            color: #fff;
            background-color: #ff7d1d;
            border-color: #ff7d1d;
        }

            .btn-primary:hover, .btn-primary:focus, .btn-primary:active, .btn-primary.active, .open > .dropdown-toggle.btn-primary {
                color: #fff;
                background-color: #ff7d1d;
                border-color: #ff7d1d;
            }

        .navbar-inverse {
            background-color: #ff7d1d;
            border-color: #ff7d1d;
        }

            .navbar-inverse .navbar-nav > .active > a, .navbar-inverse .navbar-nav > .active > a:focus, .navbar-inverse .navbar-nav > .active > a:hover {
                color: #fff;
            }

            .navbar-inverse .navbar-nav > li > a {
                color: #9d9d9d;
            }

            .navbar-inverse .navbar-brand {
                color: white;
            }

        .paging {
        }

            .paging a {
                background-color: #ff7d1d;
                padding: 5px 7px;
                text-decoration: none;
                border: 1px solid #ff7d1d;
            }

        td a {
            color: grey;
        }

        .paging a:hover {
            border: 1px solid #ff862e;
            color: White;
            background-color: #f7bd93;
        }

        .paging span {
            /* background-color: #E1FFEF; */
            padding: 5px 7px;
            /* color: #00C157; */
            border: 1px solid #ff862e;
            color: White;
            background-color: #f7bd93;
            font-weight: bold;
        }

        .paging span {
            background-color: #f1f1f1;
            padding: 5px 7px;
            color: #ff862e;
            border: 1px solid #ff862e;
        }

        tr.paging {
            background: none !important;
        }

            tr.paging tr {
                background: none !important;
            }

            tr.paging td {
                border: none;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#"> Navision Data</a>
                </div>
                <ul class="nav navbar-nav">
                    <%--<li class="active"><a href="#">Home</a></li>--%>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li style="top: 10px; left: -15px"><a href="Task_management.aspx" style="color: white">Plannning Lines</a></li>
                    <li style="top: 10px; left: -15px"><a href="Approved.aspx" id="lblApproved" runat="server" style="color: white">Approval</a></li>
                    <li style="top: 10px; left: -15px"><a href="QA_Data.aspx" id="lblQA" runat="server" style="color: white">QA</a></li>
                    <li style="top: 10px; left: -15px">
                        <asp:Button ID="Button1" Text="Logout" runat="server" class="btn btn-group" Style="background: white; color: #ff7d1d; font-weight: bold;"
                            OnClick="Logout_Click" UseSubmitBehavior="false" CausesValidation="false" /></li>

                </ul>

            </div>
        </nav>
        <div class="container">
            <%--            <div class="row">
                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnJob" Text="Job" CssClass="btn btn-primary btn-block" OnClick="btnJob_Click" />

                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnTask" runat="server" Text="Task" CssClass="btn btn-primary btn-block" OnClick="btnTask_Click" />
                </div>
                <div class="col-md-3">
                    <asp:Button ID="btnResource" runat="server" Text="Resource" CssClass="btn btn-primary btn-block" OnClick="btnResource_Click" />

                </div>
                <div class="col-md-3">
                    <asp:Button ID="btncontactperson" runat="server" Text="Sales Cycle" CssClass="btn btn-primary btn-block" OnClick="btncontactperson_Click" />

                </div>
            </div>--%>
            <div class="row">
                <div class="col-xs-12 col-md-6">

                    <asp:RadioButtonList ID="rdoStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged" RepeatDirection="Horizontal" CellSpacing="20" Style="text-decoration-color: #B41B63; color: #B41B63;">
                        <asp:ListItem Selected="True">Job</asp:ListItem>

                        <asp:ListItem>Task</asp:ListItem>

                        <asp:ListItem>Resource</asp:ListItem>

                        <asp:ListItem>Sales Cycle</asp:ListItem>

                    </asp:RadioButtonList>
                    ​
                    
                </div>
            </div>
            <br />
            <br />

            <div id="divjob" runat="server">
                <div class="row">
                    <%--<div class="col-md-12">--%>
                    <div class="col-md-2">Project Number* </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtprojectNo" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Project Name*</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtprojectname" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Contact Person</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtcontactperson" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">Blocked</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtblock" runat="server" CssClass="form-control" Enabled="false" Text="NO"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnADD" runat="server" CssClass="btn btn-primary btn-block" Text="ADD" OnClick="btnADD_Click" CausesValidation="true" />
                    </div>
                </div>

                <%--</div>--%>
                <br />
                <asp:GridView ID="grdjob" runat="server"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d" Style="height: 200px; width: 100%"
                    AllowPaging="true"
                    PageSize="10"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White"
                    OnRowDataBound="grdjob_RowDataBound"
                   
                    OnRowUpdating="grdjob_RowUpdating"
                    OnRowCommand="grdjob_RowCommand"
                    OnRowEditing="grdjob_RowEditing"
                    OnRowCancelingEdit="grdjob_RowCancelingEdit"
                    OnPageIndexChanging="grdjob_PageIndexChanging">
                    <Columns>
                        <%--<asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="Project_Number" HeaderText="Project_Number" />
                        <asp:BoundField DataField="Project_Name" HeaderText="Project_Name" />
                        <asp:BoundField DataField="Contact_Person" HeaderText="Contact_Person" />
                        <asp:BoundField DataField="Blocked" HeaderText="Blocked" />--%>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Number">

                            <ItemTemplate>
                                <asp:Label ID="lblProject_Number" runat="server" Text='<%# Eval("Project_Number") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtProject_Number" runat="server" Text='<%# Eval("Project_Number") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="EditPNoRequiredValidation"
                                    runat="server"
                                    ErrorMessage="Required"
                                    ForeColor="Red"
                                    ControlToValidate="txtProject_Number"
                                    Display="Dynamic"
                                    SetFocusOnError="True"
                                    ValidationGroup="JobEditValidationGrp">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name">

                            <ItemTemplate>
                                <asp:Label ID="lblproject_name" runat="server" Text='<%# Eval("project_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtproject_name" runat="server" Text='<%# Eval("project_name") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="EditPNameRequiredValidation"
                                    runat="server"
                                    ErrorMessage="Required"
                                    ForeColor="Red"
                                    ControlToValidate="txtproject_name"
                                    Display="Dynamic"
                                    SetFocusOnError="True"
                                    ValidationGroup="JobEditValidationGrp">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Contact_Person">
                            <ItemTemplate>
                                <asp:Label ID="lblContact_Person" runat="server" Text='<%# Eval("Contact_Person") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtContact_Person" runat="server" Text='<%# Eval("Contact_Person") %>'></asp:TextBox>
                                <asp:RequiredFieldValidator
                                    ID="EditCPRequiredValidation"
                                    runat="server"
                                    ErrorMessage="Required"
                                    ForeColor="Red"
                                    ControlToValidate="txtContact_Person"
                                    Display="Dynamic"
                                    SetFocusOnError="True"
                                    ValidationGroup="JobEditValidationGrp">
                                </asp:RequiredFieldValidator>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Blocked">
                            <ItemTemplate>
                                <asp:Label ID="lblBlocked" runat="server" Text='<%# Eval("Blocked") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEditBlocked" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" Width="100%">
                                   <%-- <asp:ListItem>Select</asp:ListItem>--%>
                                    <asp:ListItem>Yes</asp:ListItem>
                                    <asp:ListItem>No</asp:ListItem>
                                </asp:DropDownList>

                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnedit" CommandName="Edit" runat="server">
                                                 <i class="fa fa-edit"></i> Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:LinkButton ID="btn_Update" runat="server" Text="Update" CommandName="Update" ValidationGroup="JobEditValidationGrp"></asp:LinkButton>
                                <asp:LinkButton ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"></asp:LinkButton>

                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="divTask" runat="server">
                <div class="row">
                    <%--<div class="col-md-12">--%>
                    <div class="col-md-2">Job No </div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtJobno" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Project Number</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtp_no" CssClass="form-control" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Task Name</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtTaskName" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <div class="col-md-12">
                            <asp:Button ID="btnTask1" runat="server" CssClass="btn btn-primary btn-block" Text="ADD" OnClick="btnTask_Click1" />
                        </div>
                    </div>
                </div>
                <br />
                <asp:GridView ID="grdTask" runat="server"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d" Style="height: 200px; width: 100%"
                    AllowPaging="true"
                    PageSize="10"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White"
                    OnPageIndexChanging="grdTask_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" />
                        <asp:BoundField DataField="JobNo" HeaderText="JobNo" />
                        <asp:BoundField DataField="Project_Number" HeaderText="Project_Number" />
                        <asp:BoundField DataField="TaskName" HeaderText="TaskName" />

                    </Columns>
                </asp:GridView>
            </div>

            <div id="divResource" runat="server">
                <div class="row">
                    <%--<div class="col-md-12">--%>
                    <div class="col-md-2">Resource No</div>
                    <div class="col-md-2">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtResourceno" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">User Name</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtusername" CssClass="form-control" runat="server" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-2">Name</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control" required="required"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-2">Role</div>
                    <div class="col-md-2">
                        <asp:TextBox ID="txtrole" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnaddResource" runat="server" CssClass="btn btn-primary btn-block" Text="ADD" OnClick="btnaddResource_Click" />
                    </div>
                </div>
                <br />
                <asp:GridView ID="grdresource" runat="server"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d" Style="height: 200px; width: 100%"
                    AllowPaging="true"
                    PageSize="10"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White"
                    OnRowUpdating="grdresource_RowUpdating"
                    OnRowEditing="grdresource_RowEditing"
                    OnRowCancelingEdit="grdresource_RowCancelingEdit"
                    OnPageIndexChanging="grdresource_PageIndexChanging">
                    <Columns>
                        <%--<asp:BoundField DataField="ResourceNo" HeaderText="ResourceNo" />
                        <asp:BoundField DataField="UserName" HeaderText="UserName" />
                        <asp:BoundField DataField="Name" HeaderText="Name" />
                        <asp:BoundField DataField="Role" HeaderText="Role" />--%>
                        <asp:TemplateField HeaderText="Sr No">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblID" runat="server" Text='<%# Eval("Id")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Resource Number">

                            <ItemTemplate>
                                <asp:Label ID="lblResourceNo" runat="server" Text='<%# Eval("ResourceNo") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtResourceNo" runat="server" Text='<%# Eval("ResourceNo") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="User Name">

                            <ItemTemplate>
                                <asp:Label ID="lblUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtUserName" runat="server" Text='<%# Eval("UserName") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblName" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtName" runat="server" Text='<%# Eval("Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Role">
                            <ItemTemplate>
                                <asp:Label ID="lblRole" runat="server" Text='<%# Eval("Role") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlEditRole" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" Width="100%">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Admin</asp:ListItem>
                                    <asp:ListItem>TL</asp:ListItem>
                                    <asp:ListItem>PMO</asp:ListItem>
                                    <asp:ListItem>NULL</asp:ListItem>
                                </asp:DropDownList>


                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnedit" CommandName="Edit" runat="server">
                                                 <i class="fa fa-edit"></i> Edit
                                </asp:LinkButton>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:LinkButton ID="btn_Update" runat="server" Text="Update" CommandName="Update"></asp:LinkButton>
                                <asp:LinkButton ID="btn_Cancel" runat="server" Text="Cancel" CommandName="Cancel"></asp:LinkButton>

                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>

            <div id="divSalesCycle" runat="server">
                <div class="row">

                    <div class="col-md-3">Sales Code </div>
                    <div class="col-md-3">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtsalecode" required="required"></asp:TextBox>
                    </div>
                    <div class="col-md-3">Sales Description</div>
                    <div class="col-md-3">
                        <asp:TextBox ID="txtSalesDescription" CssClass="form-control" runat="server" required="required"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-5"></div>
                    <div class="col-md-2">
                        <div class="col-md-12">
                            <asp:Button ID="btnSalesCycle" runat="server" CssClass="btn btn-primary btn-block" Text="ADD" OnClick="btnSalesCycle_Click" />
                        </div>
                    </div>
                </div>
                <br />
                <asp:GridView ID="grdSalesCycle" runat="server" Visible="true"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d" Style="height: 200px; width: 100%"
                    AllowPaging="true"
                    PageSize="10"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White"
                    OnPageIndexChanging="grdSalesCycle_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="SalesCode" HeaderText="Sales Code" />
                        <asp:BoundField DataField="SalesDescription" HeaderText="Sales Description" />
                    </Columns>
                </asp:GridView>

            </div>
        </div>
    </form>
</body>
</html>
