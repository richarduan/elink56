<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GroupEditor.aspx.cs" Inherits="GroupEditor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentWrapper" Runat="Server">


    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        管理部门信息
        <small>Optional description</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> 首页</a></li>
        <li><a href="#"><i class="fa fa-dashboard"></i> 组织架构管理</a></li>
        <li class="active">管理部门信息</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">

        <div class="row">
            <div class="col-md-12">


                <div class="box box-primary">

                    <div class="box-header with-border">
                        <h3 class="box-title">Quick Example</h3>
                    </div>

                    <div class="box-body">

                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>所属部门</label>
                                    <input id="group_id" class="form-control group_id val" placeholder="系统自动分配" type="text" disabled>
                                </div>
                            </div>
                             <div class="col-md-6">
                                <div class="form-group">
                                    <label>部门编码</label>
                                    <input id="group_id" class="form-control group_id val" placeholder="系统自动分配" type="text" disabled>
                                </div>
                            </div>

                        </div>


                        <div class="row">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label>部门名称</label>
                                    <input id="group_name" class="form-control group_name val" placeholder="请输入部门名称" type="text">
                                </div>
                            </div>


                        </div>


                    </div>

                    <div class="box-footer">
                        <button class="btn btn-primary" type="button" id="save">确认保存</button>
                    </div>

                </div>


            </div>
        </div>

    </section>
    <!-- /.content -->

<%=this.Token %>
</asp:Content>



<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">

<%=this.staticJavascript("/static/js/PushInput.js") %>
<script type="text/javascript">


    $(function () {



        $("#save").click(function () {
            var data = requestInput(".val");
            $.postJSON("GroupEditor.aspx?subjectionId=0", data, function () {

            
            });

        });







    });

</script>


</asp:Content>

