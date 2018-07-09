<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QA_Data.aspx.cs" Inherits="QA_Data" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> QA Data</title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.0/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>

    <script src="assets/js/jquery-3.2.1.min.js"></script>
    <script src="assets/js/bootstrap.min.js"></script>
    <script src="assets/js/script.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <script src="http://jdewit.github.io/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>
    <script src="../js/script.js"></script>
    <script src="../Scripts/script.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/jquery-3.2.1.min.js"></script>

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
    <script type="text/javascript">
        $(function () {

            $("[id$=txt_projectname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/QA_Data.aspx/GetProjectList") %>',
                        data: "{ 'projectName': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",

                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                //alert(item.Project_Name);
                                return {
                                    label: item.Project_Name,
                                    val: item.Project_Number
                                }
                            }))
                            // GetTasks();
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    $("[id$=hfProjectid]").val(i.item.val);
                    $("#txt_projectname").val(i.item.val);


                },
                //select: function (e, i) {

                //    $("[id$=hfContPersonNo]").val(i.item.val);
                //    $("#lblhfContPersonNo").val(i.item.val);


                //},
                minLength: 2
            });
        });
        $(function () {
            $("[id$=txtproject_name]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Pages/QA_Data.aspx/GetProjectList") %>',
                        data: "{ 'projectName': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",

                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                //alert(item.Project_Name);
                                return {
                                    label: item.Project_Name,
                                    val: item.Project_Number
                                }
                            }))
                            // GetTasks();
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {

                    $("[id$=hfgridProjectid]").val(i.item.val);
                    $("#lblproject_number").val(i.item.val);


                },
                //select: function (e, i) {

                //    $("[id$=hfContPersonNo]").val(i.item.val);
                //    $("#lblhfContPersonNo").val(i.item.val);


                //},
                minLength: 3
            });


        });

    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$=txtproject_name]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Pages/Add_Task.aspx/GetProjectList") %>',
                        data: "{ 'projectName': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",

                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                //alert(item.Project_Name);
                                return {
                                    label: item.Project_Name,
                                    val: item.Project_Number
                                }
                            }))
                            // GetTasks();
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {

                    $("[id$=hfContPersonNo]").val(i.item.val);
                    $("#lblhfContPersonNo").val(i.item.val);


                },
                minLength: 3
            });


        });
    </script>
</head>
<body>
     <form id="Form1" class="form-signin" runat="server">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">QA DATA</a>
                </div>
                <ul class="nav navbar-nav">
                </ul>
                <ul class="nav navbar-nav navbar-right">
                     <li style="top:10px;left:-15px"><a href="Approved.aspx" id="lblApproved" runat="server" style="color: white">Approval</a></li>
                    <li style="top: 10px; left: -15px"><a href="Task_management.aspx" style="color: white">Plannning Lines</a></li>
                    <li style="top: 10px; left: -15px"><a href="SyncTasks.aspx" id="lblsyncnav" runat="server" style="color: white">NAV</a></li>
                    <li style="top: 10px; left: -15px">
                        <asp:Button ID="Logout" Text="Logout" runat="server" class="btn btn-group" Style="background: white; color: #ff7d1d; font-weight: bold;"
                           OnClick="Logout_Click" UseSubmitBehavior="false"/></li>

                </ul>

            </div>
        </nav>
         <div class="container">

        <br />
        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-6">
                        Project Name
                        
                       
                    </div>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txt_projectname" AutoPostBack="true" OnTextChanged="txt_projectname_TextChanged" onfocus="resetdata();" required="required"></asp:TextBox>
                        <asp:HiddenField ID="hfProjectid" runat="server" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">Project Number</div>
                    <div class="col-md-6">
                        <asp:Label runat="server" ID="lblprojectNumber" CssClass="form-control" BackColor="#d4d4d4"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-6">Task Name</div>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="drp_taskname" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="drp_taskname_SelectedIndexChanged" required="required">
                        </asp:DropDownList>
                        <asp:Label for="usr" runat="server" ID="Label1" Enabled="false"></asp:Label>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">Task Number</div>
                    <div class="col-md-6">
                        <asp:Label runat="server" ID="lbltasknumber" CssClass="form-control" Enabled="false" BackColor="#d4d4d4"></asp:Label>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-6">Sales Cycle</div>
                    <div class="col-md-6">
                        <asp:DropDownList runat="server" CssClass="form-control" ID="drp_salescyles" AutoPostBack="true" OnSelectedIndexChanged="drp_salescyles_SelectedIndexChanged" required="required">
                        </asp:DropDownList>

                        <asp:HiddenField ID="hfTaskid" runat="server" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">Sales Cycle Number</div>
                    <div class="col-md-6">

                        <asp:Label runat="server" ID="lblcyclenumber" CssClass="form-control" BackColor="#d4d4d4"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <br />

        <div class="row">
            <div class="col-md-4">
                <div class="row">
                    <div class="col-md-6">
                        Release Date     
                    </div>
                    <div class="col-md-6">
                        <input type="date" class="form-control" id="ReleaseDate" runat="server" required="required" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">Version No</div>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtApkIos" AutoPostBack="true" onfocus="resetdata();" required="required"></asp:TextBox>
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6"> Description</div>
                    <div class="col-md-6">
                        <asp:TextBox runat="server" CssClass="form-control" ID="txtDescription" AutoPostBack="true" onfocus="resetdata();" required="required"></asp:TextBox>
                    </div>
                </div>
            </div>


            
                <div class="col-md-8">

                    <div class="row">
                        <div class="col-md-3">SVN Information</div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtSVNInfo" AutoPostBack="true" onfocus="resetdata();" required="required"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-3">Retesting Status</div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtRetesting_status" AutoPostBack="true" onfocus="resetdata();" required="required"></asp:TextBox>

                        </div>
                    </div>

                    <br />
                    <div class="row">
                        <div class="col-md-3">Aditional Description</div>
                        <div class="col-md-9">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txtAditionalDescription" AutoPostBack="true" onfocus="resetdata();"></asp:TextBox>
                        </div>
                    </div>

                </div>
            </div>
        </div>
<%--    </div>--%>
    <br />
    <div class="row">
        <div class="col-md-5"></div>
        <div class="col-md-2">
            <div class="col-md-12">
                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
            </div>
        </div>
    </div>
    <br />
    <asp:HiddenField runat="server" ID="hfgridProjectid" />
    <div class="container-fluid main">

        <div class="panel-heading">
            <asp:GridView ID="gvQaDate" runat="server" DataKeyNames="Id"
                AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                AllowPaging="true"
                PageSize="10"
                PagerStyle-CssClass="paging"
                HeaderStyle-Font-Bold="true"
                HeaderStyle-ForeColor="White"
                ItemStyle-HorizontalAlign="Center"
                OnSelectedIndexChanged="gvQaDate_SelectedIndexChanged"
                OnPageIndexChanging="gvQaDate_PageIndexChanging"
                OnRowEditing="gvQaDate_RowEditing"
                OnRowUpdating="gvQaDate_RowUpdating"
                OnRowCancelingEdit="gvQaDate_RowCancelingEdit"
                OnRowDataBound="gvQaDate_RowDataBound">

                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:Label ID="lblserial" Text='<%#Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Number">
                        <ItemTemplate>
                            <asp:Label ID="lblproject_number" runat="server" Text='<%# Eval("Project Number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Project Name">
                        <ItemTemplate>
                            <asp:Label ID="lblproject_name" runat="server" Text='<%# Eval("Project Name") %>'></asp:Label>
                        </ItemTemplate>
                        <%--   <EditItemTemplate>
                            <asp:TextBox ID="txtproject_name" ClientIDMode="Static" runat="server" AutoPostBack="true" OnTextChanged="txtproject_name_TextChanged" CssClass="form-control" Width="250" Columns="20" Rows="2" Text='<%# Eval("Project Name") %>'></asp:TextBox>
                        </EditItemTemplate>--%>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Task Number">
                        <ItemTemplate>
                            <asp:Label ID="lblTaskNumber" runat="server" Text='<%# Eval("Task Number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Task Name">
                        <ItemTemplate>
                            <asp:Label ID="lbltask_name" runat="server" Text='<%# Eval("Task Name") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lbltask_name" runat="server" Text='<%# Eval("Task Name") %>' Visible="false"></asp:Label>
                            <asp:DropDownList runat="server" ID="drptask_name" CssClass="form-control" Width="200">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Sales Cycle number" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblsales_cycleNumber" runat="server" Text='<%# Eval("Sale Cycle No") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Sales Cycle">
                        <ItemTemplate>
                            <asp:Label ID="lblsales_cycle" runat="server" Text='<%# Eval("Sale Cycle") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblsales_cycle" runat="server" Text='<%# Eval("Sale Cycle") %>' Visible="false"></asp:Label>
                            <asp:DropDownList runat="server" ID="drpsales_cycle" CssClass="form-control" Width="250">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Date of Release">
                        <ItemTemplate>
                            <asp:Label ID="lblRelease_date" runat="server" Text='<%# Eval("Date of Release","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRelease_date" runat="server" CssClass="form-control" TextMode="Date" Width="200" Text='<%# Eval("Date of Release", "{0:yyyy-MM-dd}") %>'></asp:TextBox>

                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Version No">
                        <ItemTemplate>
                            <asp:Label ID="lblApk_IOS" runat="server" Text='<%# Eval("APK/IOS") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtApk_IOS" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control" Width="250" Columns="20" Rows="2" Text='<%# Eval("APK/IOS") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Task Description">
                        <ItemStyle Width="20%" />
                        <ItemTemplate>
                            <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txttask_description" Width="250" MaxLength="49" CssClass="form-control" runat="server" Text='<%# Eval("Description") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addtional Description">
                        <ItemTemplate>
                            <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("Additional Description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtadditional_descrption" runat="server" Width="300" MaxLength="250" CssClass="form-control" Columns="20" Rows="2" Text='<%# Eval("Additional Description") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Retesting Status">
                        <ItemTemplate>
                            <asp:Label ID="lblRetesting_status" runat="server" Text='<%# Eval("Retesting Status") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtRetesting_status" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control" Width="250" Columns="20" Rows="2" Text='<%# Eval("Retesting Status") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="SVN Information">
                        <ItemTemplate>
                            <asp:Label ID="lblSVN_Information" runat="server" Text='<%# Eval("SVN Information") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSVN_Information" ClientIDMode="Static" runat="server" AutoPostBack="true" CssClass="form-control" Width="250" Columns="20" Rows="2" Text='<%# Eval("SVN Information") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="Created By" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCreated_By" runat="server" Text='<%# Eval("Created_By") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Created Date" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCreated_Date" runat="server" Text='<%# Eval("Created_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="modify By" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblmodify_By" runat="server" Text='<%# Eval("modify_By") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText=" Modify Date" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblModify_Date" runat="server" Text='<%# Eval("Modify_Date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <asp:CommandField ButtonType="Link" ShowEditButton="true" HeaderText="Action" />

                </Columns>

            </asp:GridView>

        </div>
    </div>
    </form>
</body>
</html>
