<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Approved.aspx.cs" Inherits="Approved" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <title></title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link rel="stylesheet" href="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css" />
    <script src="http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/js/bootstrap.min.js"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.9/themes/base/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/font-awesome/4.4.0/css/font-awesome.min.css" />
    <script src="http://jdewit.github.io/bootstrap-timepicker/js/bootstrap-timepicker.js"></script>


    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=80);
            opacity: 0.8;
            z-index: 10000;
        }
    </style>
    <style type="text/css">
        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            height: 600px;
        }
    </style>
    <script type="text/javascript">
        function ShowModalPopup() {
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {
            $find("mpe").hide();
            return false;
        }
    </script>
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

    <link href="Classes/CSS/style1.css" rel="stylesheet" />
</head>
<body>
    <script>
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
    </script>
    <script type="text/javascript">
        $(function () {
            $("#ddlReason").change(function () {
                if ($(this).val() == "Other") {
                    $("#divddlReason").show();
                } else {
                    $("#divddlReason").hide();
                }
            });
        });
    </script>
    <form id="form1" runat="server">
        <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <ajax:ToolkitScriptManager runat="server"></ajax:ToolkitScriptManager>
        <nav class="navbar navbar-inverse">
            <div class="container-fluid">
                <div class="navbar-header">
                    <a class="navbar-brand" href="#">Approve Data</a>
                </div>
                <ul class="nav navbar-nav">
                    <%--<li class="active"><a href="#">Home</a></li>--%>
                </ul>
                <ul class="nav navbar-nav navbar-right">
                    <li style="top: 10px; left: -15px"><a href="QA_Data.aspx" id="lblQA" runat="server" style="color: white">QA</a></li>
                    <li style="top: 10px; left: -15px"><a href="SyncTasks.aspx" id="lblsyncnav" runat="server" style="color: white">NAV</a></li>
                    <li style="top: 10px; left: -15px"><a href="Task_management.aspx" style="color: white">Plannning Lines</a></li>
                    <li style="top: 10px; left: -15px">
                        <asp:Button ID="Button1" Text="Logout" runat="server" class="btn btn-group" Style="background: white; color: #ff7d1d; font-weight: bold;"
                            OnClick="Button1_Click" UseSubmitBehavior="false" /></li>
                </ul>

            </div>
        </nav>
        <div class="container">
            <div id="div1" runat="server">
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
                        <%--<asp:Button ID="Button2" CssClass="btn btn-primary" runat="server" Text="Apply" OnClientClick="return validateapply();" OnClick="btnapply_Click1" />--%>
                        <%--<asp:Button ID="Button3" CssClass="btn btn-primary" runat="server" Text="Apply" OnClientClick="return validateapply();" OnClick="btnapplyapproved_Click" />
                            <asp:Button ID="Button4" CssClass="btn btn-primary" runat="server" Text="Apply" OnClientClick="return validateapply();" OnClick="btnapplyPending_Click" />
                            <asp:Button ID="Button5" CssClass="btn btn-primary" runat="server" Text="Apply" OnClientClick="return validateapply();" OnClick="btnapplyrejected_Click" />--%>
                    </div>
                    <br />
                    <br />
                </div>
            </div>

            <%--<div class="row">
                <div class="col-md-3">
                  
                    <asp:RadioButtonList ID="rdbPending" runat="server" OnSelectedIndexChanged="rdbPending_SelectedIndexChanged" ></asp:RadioButtonList>
                    
                </div>
                <div class="col-md-3">
                   
                    <asp:RadioButtonList  ID="rbApproved" runat="server"  OnSelectedIndexChanged="rbApproved_SelectedIndexChanged"></asp:RadioButtonList>
                </div>
                <div class="col-md-3">
                  
                    <asp:RadioButtonList ID="rbRejected" runat="server"  OnSelectedIndexChanged="rbRejected_SelectedIndexChanged" ></asp:RadioButtonList>
                </div>
                <div class="col-md-3">
                 
                    <asp:RadioButtonList ID="rbAll" runat="server" OnSelectedIndexChanged="rbAll_SelectedIndexChanged"></asp:RadioButtonList>
                </div>
            </div>--%>
            <div class="row">
                <div class="col-xs-12 col-md-6">
                    <h3>Status:</h3>
                    <asp:RadioButtonList ID="rdoStatus" runat="server" OnSelectedIndexChanged="rdoStatus_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" CellSpacing="20" Style="text-decoration-color: #B41B63; color: #B41B63;">
                        <asp:ListItem Selected="True">Pending</asp:ListItem>

                        <asp:ListItem>Approved</asp:ListItem>

                        <asp:ListItem>Rejected</asp:ListItem>

                        <%-- <asp:ListItem>ALL</asp:ListItem>--%>
                    </asp:RadioButtonList>
                    <div class="col-md-12"></div>
                    <div class="col-md-4">
                        <asp:Button ID="btnapply" runat="server" Text="Apply" CssClass="btn btn-primary btn-block" OnClick="btnapply_Click" />
                    </div>           
                </div>
            </div>
            <br />
            <div class="container">
            </div>
            <asp:GridView ID="grdAll" runat="server" DataKeyNames="Id"
                AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                AllowPaging="false"
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

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Task Description">
                        <ItemStyle Width="20%" />

                        <ItemTemplate>
                            <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("task_description") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addtional Description">
                        <ItemTemplate>
                            <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("additional_descrption") %>'></asp:Label>
                        </ItemTemplate>
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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_date" runat="server" Text='<%# Eval("start_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_time" runat="server" Text='<%# Eval("start_time") %>'></asp:Label>
                        </ItemTemplate>

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
                    <asp:TemplateField HeaderText="Assign name">
                        <ItemTemplate>
                            <asp:Label ID="lblassinname" runat="server" Text='<%# Eval("asignname") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="status">
                        <ItemTemplate>
                            <asp:Button ID="btnApprvoedFlag" runat="server" Text='<%# Eval("ApprvoedFlag") %>' OnClick="btnApprvoedFlag_Click" />
                            <br />
                            <asp:Button ID="btnRejectedFlag" runat="server" Text='<%# Eval("RejectedFlag") %>' OnClick="btnRejectedFlag_Click" Width="100%" />
                            <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%-- <asp:TemplateField HeaderText="RejectedFlag">
                            <ItemTemplate>
                                <asp:Button ID="btnRejectedFlag" runat="server" Text='<%# Eval("RejectedFlag") %>' OnClick="btnRejectedFlag_Click" Width="100%" />
                            </ItemTemplate>
                        </asp:TemplateField>--%>

                    <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ItemStyle-Width="150" HeaderText="Action" />--%>
                </Columns>
            </asp:GridView>


            <asp:GridView ID="grdApproved" runat="server" DataKeyNames="Id"
                AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                AllowPaging="true"
                PageSize="10"
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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Task Description">
                        <ItemStyle Width="20%" />

                        <ItemTemplate>
                            <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("task_description") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addtional Description">
                        <ItemTemplate>
                            <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("additional_descrption") %>'></asp:Label>
                        </ItemTemplate>

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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_date" runat="server" Text='<%# Eval("start_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_time" runat="server" Text='<%# Eval("start_time") %>'></asp:Label>
                        </ItemTemplate>

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
                    <asp:TemplateField HeaderText="Assign name">
                        <ItemTemplate>
                            <asp:Label ID="lblassinname" runat="server" Text='<%# Eval("asignname") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="btnApprvoedFlag" runat="server" Text='<%# Eval("Status") %>' ForeColor="#339966" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ItemStyle-Width="150" HeaderText="Action" />--%>
                </Columns>
            </asp:GridView>



            <asp:GridView ID="grdpending" runat="server" DataKeyNames="Id"
                AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                AllowPaging="true"
                PageSize="10"
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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Task Description">
                        <ItemStyle Width="20%" />

                        <ItemTemplate>
                            <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("task_description") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addtional Description">
                        <ItemTemplate>
                            <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("additional_descrption") %>'></asp:Label>
                        </ItemTemplate>

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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_date" runat="server" Text='<%# Eval("start_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_time" runat="server" Text='<%# Eval("start_time") %>'></asp:Label>
                        </ItemTemplate>

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
                    <asp:TemplateField HeaderText="Assign name">
                        <ItemTemplate>
                            <asp:Label ID="lblassinname" runat="server" Text='<%# Eval("asignname") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="status">
                        <ItemTemplate>
                            <asp:Button ID="btnApprvoedFlag" runat="server" Text="Approve" OnClick="btnApprvoedFlag_Click" />
                            <br />
                            <asp:Button ID="btnRejectedFlag" runat="server" Text="Reject" OnClick="btnRejectedFlag_Click" Width="100%" />
                            <asp:Label ID="lblstatus" runat="server" Visible="false"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%-- <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="btnApprvoedFlag" runat="server" Text='<%# Eval("ApprvoedFlag") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>--%>

                    <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ItemStyle-Width="150" HeaderText="Action" />--%>
                </Columns>
            </asp:GridView>


            <asp:GridView ID="grdRejected" runat="server" DataKeyNames="Id"
                AutoGenerateColumns="false" CssClass="table table-striped" HeaderStyle-BackColor="#ff7d1d"
                AllowPaging="true"
                PageSize="10"
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

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Task Description">
                        <ItemStyle Width="20%" />

                        <ItemTemplate>
                            <asp:Label ID="lbltask_description" runat="server" Text='<%# Eval("task_description") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Addtional Description">
                        <ItemTemplate>
                            <asp:Label ID="lbladditional_descrption" runat="server" Text='<%# Eval("additional_descrption") %>'></asp:Label>
                        </ItemTemplate>

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
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Start Date">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_date" runat="server" Text='<%# Eval("start_date","{0:dd-MM-yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Start Time">
                        <ItemTemplate>
                            <asp:Label ID="lblstart_time" runat="server" Text='<%# Eval("start_time") %>'></asp:Label>
                        </ItemTemplate>

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
                    <asp:TemplateField HeaderText="Assign name">
                        <ItemTemplate>
                            <asp:Label ID="lblassinname" runat="server" Text='<%# Eval("asignname") %>'></asp:Label>
                        </ItemTemplate>

                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status">
                        <ItemTemplate>
                            <asp:Label ID="btnApprvoedFlag" runat="server" Text='<%# Eval("Status") %>' ForeColor="#ff5050" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rejected Reasons">
                        <ItemTemplate>
                            <asp:Label ID="lblRejectedreasos" runat="server" Text='<%# Eval("RejectedReason") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rejected Date">
                        <ItemTemplate>
                            <asp:Label ID="lblrejecteddate" runat="server" Text='<%# Eval("RejectedDate") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <%--<asp:CommandField ButtonType="Link" ShowEditButton="true" ItemStyle-Width="150" HeaderText="Action" />--%>
                </Columns>
            </asp:GridView>
        </div>
        <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
        <ajax:ModalPopupExtender
            ID="ModalPopupExtender1"
            BehaviorID="mpe"
            runat="server"
            PopupControlID="pnlPopup"
            TargetControlID="lnkDummy"
            BackgroundCssClass="modalBackground"
            CancelControlID="lnkbtnclose">
        </ajax:ModalPopupExtender>
        <div>
            <asp:Panel
                ID="pnlPopup"
                runat="server"
                CssClass="modalPopup col-sm-6"
                Style="display: none;">
                <div class="modal-header">
                    <asp:LinkButton ID="lnkbtnclose" data-dismiss="modal" Style="margin-left: 95%;" runat="server"><i class="fa fa-remove"></i></asp:LinkButton>
                    <h3 class="box-title">Reason</h3>
                </div>

                <div class="modal-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <!-- Horizontal Form -->
                            <!-- /.box-header -->
                            <!-- form start -->
                            <div class="form-horizontal" style="overflow-y: scroll; height: 380px;">
                                <div class="box-body">
                                    <div class="form-group form_group">
                                        <div class="form-group">
                                            <label id="lblHeading" runat="server" class="col-sm-3 control-label">Select Reason</label>
                                            <div class="col-sm-8">
                                                <asp:DropDownList ID="ddlReason" runat="server" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlReason_SelectedIndexChanged"></asp:DropDownList>
                                                <asp:RequiredFieldValidator
                                                    InitialValue="-- Select EmpCode--"
                                                    ID="ddlRejectRequiredFieldValidation"
                                                    runat="server"
                                                    ErrorMessage="Required"
                                                    ForeColor="Red"
                                                    ControlToValidate="ddlReason"
                                                    Display="Dynamic"
                                                    SetFocusOnError="True"
                                                    ValidationGroup="VacancyValidationGrp">
                                                </asp:RequiredFieldValidator>
                                            </div>

                                        </div>
                                        <div class="modal-footer">
                                            <label id="lblother" runat="server" class="col-sm-3 control-label" visible="false">Other Reason</label>
                                            <div class="col-sm-8">

                                                <asp:TextBox ID="txtother" runat="server" TextMode="MultiLine" class="form-control" Visible="false"></asp:TextBox>

                                            </div>
                                            <div>
                                                <asp:Button ID="btnsubmit" runat="server" OnClick="btnsubmit_Click" Text="Save" type="button" class="btn btn-primary" ValidationGroup="VacancyValidationGrp" />
                                            </div>
                                        </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
