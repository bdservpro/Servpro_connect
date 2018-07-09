<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Task_management.aspx.cs" Inherits="Task_management" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Planning Lines</title>

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
    <script type="text/javascript">



        $(function () {

            var Todaydate = new Date();

            document.getElementById('txt_startdate').valueAsDate = Todaydate;

            //    document.getElementById("txt_todate").valueAsDate = Todaydate;
            var date2 = new Date();
            date2.setDate(date2.getDate() - 7);

            //document.getElementById('txt_fromdate').valueAsDate = date2;

            $("[id$=txt_projectname]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Task_management.aspx/GetProjectList") %>',
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
                minLength: 2
            });
        });
        $(function () {
            $("[id$=txtproject_name]").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/Task_management.aspx/GetProjectList") %>',
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
                minLength: 3
            });


        });

    </script>
    <%--<link href="CSS/style1.css" rel="stylesheet" />--%>
    <link href="Classes/CSS/style1.css" rel="stylesheet" />
</head>
<body onload="todates();">
    <form id="Form1" class="form-signin" runat="server">
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">Project Planning</a>
                    
                </div>
                <ul class="nav navbar-nav">
                </ul>
                <ul class="nav navbar-nav navbar-right">
                     <li style="top:10px;left:-15px"><a href="Approved.aspx" id="lblApproved" runat="server" style="color: white">Approval</a></li>
                    <li style="top:10px;left:-15px"><a href="QA_Data.aspx" id="lblQA" runat="server" style="color: white">QA</a></li>
                    <li style="top: 10px; left: -15px"><a href="SyncTasks.aspx" id="lblsyncnav" runat="server" style="color: white">NAV</a></li>
                    <li style="top: 10px; left: -15px">
                        <asp:Button Text="Logout" runat="server" class="btn btn-group" Style="background: white; color: #ff7d1d; font-weight: bold;"
                            OnClick="Logout_Click" UseSubmitBehavior="false" /></li>

                </ul>

            </div>
        </nav>
        <div class="container">
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-3">

                    <div class="col-md-12">
                        <h3>Add Records</h3>
                    </div>
                </div>
                <div class="col-md-5"></div>
            </div>

            <div class="row" id="ResourceName_Row" runat="server">
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-6">
                            Resource Name
                        </div>
                        <div class="col-md-6">
                            <asp:DropDownList runat="server" ID="drpresource_Admin" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                        </div>
                    </div>
                </div>

                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-3"></div>
                        <div class="col-md-3">
                            <asp:FileUpload runat="server" ID="fpExcelUpload" class="form-control" />
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="Button2" runat="server" Text="Upload Task" CssClass="btn btn-primary" OnClick="Upload_Click" />
                        </div>
                        <div class="col-md-3">
                            <a href="PlanningLinesUploadTemplate.xlsx">Download Template<i class="fa fa-upload" aria-hidden="true" style="padding-left: 10px; color: green; font-size: 20px;"></i></a>
                       
                        </div>
                    </div>

                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-6">
                            Project Name
                        
                       
                        </div>
                        <div class="col-md-6">
                            <asp:TextBox runat="server" CssClass="form-control" ID="txt_projectname" AutoPostBack="true" onfocus="resetdata();" OnTextChanged="txt_projectname_TextChanged"></asp:TextBox>
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


                            <asp:DropDownList runat="server" CssClass="form-control" ID="drp_taskname" Enabled="false" AutoPostBack="true" OnSelectedIndexChanged="drp_taskname_SelectedIndexChanged">
                            </asp:DropDownList>




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
                            <asp:DropDownList runat="server" CssClass="form-control" ID="drp_salescyles" AutoPostBack="true" OnSelectedIndexChanged="drp_salescyles_SelectedIndexChanged">
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
                        <div class="col-md-6">Start Date</div>
                        <div class="col-md-6">
                            <input type="date" class="form-control" id="txt_startdate" runat="server" />
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-6">Start Time</div>
                        <div class="col-md-6">
                            <div class="input-group bootstrap-timepicker timepicker" >
                                <input id="timepicker1" type="text" class="form-control input-small" runat="server" />
                                <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-4">
                    <div class="row">
                        <div class="col-md-4">Efforts</div>
                        <div class="col-md-4">
                            <select id="drp_hours" class="form-control" runat="server" required="required">
                                <option value="Hours"></option>
                                <option value="0">00</option>
                                <option value="1">01</option>
                                <option value="2">02</option>
                                <option value="3">03</option>
                                <option value="4">04</option>
                            </select>
                        </div>
                        <div class="col-md-4">
                            <select id="drp_minutes" class="form-control" runat="server">
                                <option value="Minutes"></option>
                                <option value="00"></option>
                                <option value="15"></option>
                                <option value="30"></option>
                                <option value="45"></option>
                            </select>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <div class="row">
                <div class="col-md-2">Task Description</div>
                <div class="col-md-4">
                    <asp:TextBox runat="server" MaxLength="55" CssClass="form-control" ID="txt_description"></asp:TextBox>
                </div>
                <div class="col-md-2">Assigned By</div>
                <div class="col-md-2">

                    <asp:DropDownList ID="ddlAssign" runat="server" CssClass="form-control" >
                    </asp:DropDownList>
                </div>

            </div>
            <%-- </div>--%>

            <%--</div>--%>

            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-2">Additonal Description</div>
                        <div class="col-md-10">

                            <textarea runat="server" maxlength="250" class="form-control" id="txt_additonaldescription" />
                            <asp:Label ID="lblshowdate" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>

            </div>
            <br />
            <div class="row">

                <div class="col-md-5"></div>
                <div class="col-md-2">

                    <div class="col-md-12">
                        <asp:Button ID="Button1" runat="server" Text="Save" CssClass="btn btn-primary btn-block" OnClientClick="return validate();" OnClick="Save_Click" />
                    </div>
                </div>
                <div class="col-md-5"></div>
            </div>
        </div>

        <hr style="border-top: 3px solid red" />
        <div class="container">
            <div class="row">
                <div class="col-md-4"></div>
                <div class="col-md-3">

                    <div class="col-md-12">
                        <h3>View Records</h3>
                    </div>
                </div>
                <div class="col-md-5"></div>
            </div>
        </div>
        <hr />
        <div class="container">

            <div class="row">
                <div class="col-md-2">Resource Name</div>
                <div class="col-md-2">
                    <asp:DropDownList runat="server" ID="drp_resourcename" CssClass="form-control" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="col-md-1">From Date</div>
                <div class="col-md-2">
                    <input type="date" class="form-control" id="txt_fromdate" runat="server" />

                </div>
                <div class="col-md-1">To Date</div>
                <div class="col-md-2">
                    <input type="date" class="form-control" id="txt_todate" runat="server" />

                </div>
                <div class="col-md-1">
                    <asp:Button CssClass="btn btn-primary" runat="server" Text="Apply" OnClick="Apply_Click" OnClientClick="return validateapply();"  />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="Download_employe" CssClass="btn btn-primary" runat="server" Text="Download" OnClientClick="return validateapply();" OnClick="Download_employe_Click" />
                </div>
                <br />
                <br />
            </div>
            <hr />
            <div class="row" id="updatediv" runat="server">
                <%--<div class="col-md-12">--%>
                <div class="col-md-2">Updated After </div>
                <div class="col-md-2">
                    <input type="date" class="form-control" id="txt_updated" runat="server" />
                </div>
                <div class="col-md-1">Time</div>
                <div class="col-md-2">
                    <div class=" input-group bootstrap-timepicker timepicker">
                        <input id="timepicker2" type="text" class="form-control input-small" runat="server" />
                        <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                    </div>
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btnapply" runat="server" CssClass="btn btn-primary" Text="Apply" OnClick="btnapply_Click" />
                </div>
                <div class="col-md-1">
                    <asp:Button ID="btndownload" runat="server" CssClass="btn btn-primary" Text="Download" OnClick="btndownload_Click" />
                </div>
                <%--</div>--%>
            </div>
            <br />

            <br />
            <hr />
            <div class="row">
                <asp:GridView ID="gvDetails" runat="server" DataKeyNames="Id"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                    OnRowEditing="gvDetails_RowEditing"
                    OnRowUpdating="gvDetails_RowUpdating"
                    OnRowCancelingEdit="gvDetails_RowCancelingEdit"
                    OnRowDeleting="gvDetails_RowDeleting"
                    AllowPaging="true"
                    OnPageIndexChanging="gvDetails_PageIndexChanging"
                    OnRowDataBound="gvDetails_RowDataBound"
                    PageSize="20"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White">
                    <Columns>
                        
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Label ID="lblserial" Text='<%#Container.DataItemIndex+1 %>' runat="server"></asp:Label>
                                <asp:Label ID="lblid" Visible="false" Text='<%# Eval("id") %>' runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Name">

                            <ItemTemplate>
                                <asp:Label ID="lblproject_name" runat="server" Text='<%# Eval("Project_Name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtproject_name" ClientIDMode="Static" runat="server" OnTextChanged="txtproject_name_TextChanged" AutoPostBack="true" CssClass="form-control" Width="250" Columns="20" Rows="2" TextMode="MultiLine" Text='<%# Eval("project_name") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Project Number">
                            <ItemTemplate>
                                <asp:Label ID="lblproject_number" runat="server" Text='<%# Eval("project_number") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Rescource Id">
                            <ItemTemplate>
                                <asp:Label ID="lblrescource_id" runat="server" Text='<%# Eval("rescource_id") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Resource Name">
                            <ItemTemplate>
                                <asp:Label ID="lblresorcename" runat="server" Text='<%# Eval("resorcename") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Task Name">
                            <ItemTemplate>
                                <asp:Label ID="lbltask_name" runat="server" Text='<%# Eval("task_name") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lbltask_name" runat="server" Text='<%# Eval("task_name") %>' Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="drptask_name" CssClass="form-control" Width="200">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Task Description">
                            <ItemStyle Width="20%" />

                            <ItemTemplate>
                                <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("task_description") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txttask_description" Width="250" MaxLength="49" CssClass="form-control" runat="server" Text='<%# Eval("task_description") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Addtional Description">
                            <ItemTemplate>
                                <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("additional_descrption") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtadditional_descrption" runat="server" Width="300" MaxLength="250" CssClass="form-control" Columns="20" Rows="2" TextMode="MultiLine" Text='<%# Eval("additional_descrption") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sales Cycle number" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblsales_cycleNumber" runat="server" Text='<%# Eval("sales_cycleNumber") %>'></asp:Label>

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="task number " Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbltask_nameNumber" runat="server" Text='<%# Eval("task_number") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sales Cycle">
                            <ItemTemplate>
                                <asp:Label ID="lblsales_cycle" runat="server" Text='<%# Eval("sales_cycle") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:Label ID="lblsales_cycle" runat="server" Text='<%# Eval("sales_cycle") %>' Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="drpsales_cycle" CssClass="form-control" Width="250">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate>
                                <asp:Label ID="lblstart_date" runat="server" Text='<%# Eval("start_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtstart_date" runat="server" CssClass="form-control" TextMode="Date" Width="200" Text='<%# Eval("start_date", "{0:yyyy-MM-dd}") %>'></asp:TextBox>

                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Start Time">
                            <ItemTemplate>
                                <asp:Label ID="lblstart_time" runat="server" Text='<%# Eval("start_time") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <div class="input-group bootstrap-timepicker timepicker">
                                    <asp:TextBox ID="txtstart_time" ClientIDMode="Static" runat="server" CssClass="form-control" Width="100" Text='<%# Eval("start_time") %>'></asp:TextBox>

                                    <span class="input-group-addon"><i class="glyphicon glyphicon-time"></i></span>
                                </div>

                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-Width="150px" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbl_hours12" runat="server" Text='<%# Eval("hours") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Hours" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <asp:Label ID="lbl_h" runat="server" Text='<%# Eval("hours") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>

                                <asp:DropDownList runat="server" ID="drphours" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" Width="100">
                                    <asp:ListItem Text="Hours" Value=""></asp:ListItem>
                                    <asp:ListItem Text="00" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="01" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="02" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="03" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="04" Value="4"></asp:ListItem>

                                </asp:DropDownList>
                                <asp:DropDownList runat="server" ID="drpminutes" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" Width="100">
                                    <asp:ListItem Text="Minutes" Value=""></asp:ListItem>
                                    <asp:ListItem Text="00" Value="00"></asp:ListItem>
                                    <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                    <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                    <asp:ListItem Text="45" Value="45"></asp:ListItem>
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate>
                                <asp:Label ID="lblend_date" runat="server" Text='<%# Eval("end_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="End Time">
                            <ItemTemplate>
                                <asp:Label ID="lblend_time" runat="server" Text='<%# Eval("end_time","{0:hh\\:mm\\:ss} ") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Created By">
                            <ItemTemplate>
                                <asp:Label ID="lblcreated_by" runat="server" Text='<%# Eval("created_by") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Created Date">
                            <ItemTemplate>
                                <asp:Label ID="lblcreated_date" runat="server" Text='<%# Eval("created_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                         <%--asp:TemplateField HeaderText="AssignedBy " Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAssignedByName" runat="server" Text='<%# Eval("AssignedBy") %>'></asp:Label>

                            </ItemTemplate>

                        </asp:TemplateField>--%>


                        <asp:TemplateField HeaderText="Assign name">
                            <ItemTemplate>
                                <asp:Label ID="lblasignname" runat="server" Text='<%# Eval("asignname") %>'></asp:Label>
                            </ItemTemplate>
                             <EditItemTemplate>
                                <asp:Label ID="lblasignname" runat="server" Text='<%# Eval("asignname") %>' Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="drpasignname" CssClass="form-control" Width="250">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected Reason">
                            <ItemTemplate>
                                <asp:Label ID="lblRejectedReason" runat="server" Text='<%# Eval("RejectedReason") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Rejected BY">
                            <ItemTemplate>
                                <asp:Label ID="lblRejected_by" runat="server" Text='<%# Eval("RejectedBy") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rejected Date">
                            <ItemTemplate>
                                <asp:Label ID="lblrejected_date" runat="server" Text='<%# Eval("RejectedDate") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="ApprvoedFlag">
                            <ItemTemplate>
                                <asp:Button ID="btnApprvoedFlag" runat="server" Text='<%# Eval("ApprvoedFlag") %>' OnClick="btnApprvoedFlag_Click" />
                            </ItemTemplate>

                        </asp:TemplateField>--%>
                        <asp:CommandField  ButtonType="Link" ShowEditButton="true" ItemStyle-Width="150" HeaderText="Action" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="gvUpdate" runat="server"
                    AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                    AllowPaging="true"
                    PageSize="20"
                    PagerStyle-CssClass="paging"
                    HeaderStyle-Font-Bold="true"
                    HeaderStyle-ForeColor="White">
                    <Columns>
                        <asp:BoundField DataField="Job Name" HeaderText="Job Name" />
                        <asp:BoundField DataField="Job No." HeaderText="Job No." />
                        <asp:BoundField DataField="Resource No." HeaderText="Resource No." />
                        <asp:BoundField DataField="Resource Name" HeaderText="Resource Name" />
                        <asp:BoundField DataField="Task Name" HeaderText="Task Name" />
                        <asp:BoundField DataField="Task Description" HeaderText="Task Description" />
                        <asp:BoundField DataField="Extended Description" HeaderText="Extended Description" />
                        <asp:BoundField DataField="Sales Cycle" HeaderText="Sales Cycle" />
                        <asp:BoundField DataField="Start Date" HeaderText="Start Date" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="start_time " HeaderText="start_time" />
                        <asp:BoundField DataField="End Date" HeaderText="End Date" DataFormatString="{0:d}" />
                        <asp:BoundField DataField="End Time " HeaderText="End Time" />
                        <%--<asp:BoundField DataField="created_by  " HeaderText="Job No." />--%>
                    </Columns>
                </asp:GridView>

                <asp:HiddenField runat="server" ID="hfgridProjectid" />
            </div>
        </div>

    </form>

    <script>
        $('#timepicker1').timepicker({
            showMeridian: false,
            timeFormat: "HH:mm:ss"

        });
        $('#txtstart_time').timepicker({
            showMeridian: false,
            timeFormat: "HH:mm:ss"

        });

        function validateapply() {
            var fromdate = $("#txt_fromdate").val();
            var todate = $("#txt_todate").val();
            if (fromdate == "") {
                alert("Please Select From  Date");

                return false;

            }
            if (todate == "") {
                alert("Please Select To Date");

                return false;

            }
            return true;
        }

        function validate() {

            var hours = $("#drp_hours").val();
            var minutes = $("#drp_minutes").val();
            var startime = $("#timepicker1").val();
            var txt_startdate = $("#txt_startdate").val();
            var project_name = $("#txt_projectname").val();
            var task_name = $("#drp_taskname").val();
            var salescycles = $("#drp_salescyles").val();
            var task_description = $("#txt_description").val();
            var Addtional_description = $("#txt_additonaldescription").val();

            if (project_name == "") {
                alert("Please Enter Project Name");
                return false;

            }
            if (task_name == "") {
                alert("Please Enter Task Name");
                return false;

            }
            if (txt_startdate == "") {
                alert("Please Enter Start Date");
                return false;

            }
            if (salescycles == "") {
                alert("Please Select Sales Cycle");
                return false;

            }
            if (task_description == "") {
                alert("Please Enter Task Description");
                return false;

            }

            if (hours == "Hours") {
                alert("Please Select hours");
                return false;

            }

            if (minutes == "Minutes") {
                alert("Please Select Minutes");
                return false;

            }

            return true;
        }

    </script>
</body>
</html>
