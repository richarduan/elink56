<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="GroupLister.aspx.cs" Inherits="GroupLister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="header" Runat="Server">

<style type="text/css">
    .groupinfo tr th{width:100px;}

</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentWrapper" Runat="Server">


    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        组织架构管理
        <small>Optional description</small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> 首页</a></li>
        <li class="active">组织架构管理</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">

        <div class="row">
            <div class="col-md-12">



                <%if(loadShow){ %>
                <div class="box box-warning">
                    <div class="box-header with-border">
                    <h3 class="box-title"><%=groupinfo.Group_name+"-" %>基本信息</h3>
                        <%=Backhref %>
                    </div>

                    <div class="box-body">

                        <div class="col-xs-10">
                            <div class="table-responsive">

                                <table class="table table-bordered groupinfo">
                                    <tbody>
                                        <tr>
                                            <th>部门名称:</th>
                                            <td><%=groupinfo.Group_name %></td>

                                            <th>部门编码:</th>
                                            <td><%=groupinfo.Group_id %></td>


                                        </tr>
                                        <tr>
                                            <th>上级部门:</th>
                                            <td><%=groupinfo.Group_subjectionName %></td>

                                            <th>部门层次:</th>
                                            <td><%=groupinfo.Group_subjectionNames %></td>


                                        </tr>

                                        <tr>
                                            <th>部门等级:</th>
                                            <td><%=getGrouplevel(groupinfo.Group_level) %></td>
                                            <th>部分负责人:</th>
                                            <td>sss</td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                        </div>
                    </div>
                </div>
                <%} %>


                <div class="box box-primary">

                     <%if(loadShow){ %>
                    <div class="box-header with-border">
                    <h3 class="box-title"><%=groupinfo.Group_name %>-下属部门-<%=groupinfo.Group_lower %></h3>
                        <%=Backhref %>
                    </div>
                    <%} %>
                    <%--<div class="box-header with-border"></div>--%>
                    <div class="box-body">

                        <div class="row">
<%--                            <div class="col-sm-6">
                                <div id="example1_filter" class="dataTables_filter">
                                    <label>
                                        <button class="btn btn-block btn-success" type="button">创建新部门</button>
                                    </label>
                                </div>
                            </div>--%>
                        </div>

                        <div class="row">

                            <div class="col-sm-12"><%=GetGroupTable %></div>

                        </div>

                        
                    </div>
  <%--                  <div class="box-footer clearfix">
                        <%=Backhref %>
                    </div>--%>

                </div>


                <div class="box ">
                    <div class="box-body">
                        <a class="btn btn-primary margin" href="GroupEditor.aspx?subjectionId=<%=subid %>">创建<%=""+groupinfo.Group_name+"" %>下级部门</a>
                         <%if(loadShow){ %>
                        <a class="btn btn-default margin" href="GroupEditor.aspx?GroupId=<%=groupinfo.Group_id %>">编辑部门信息</a>
                        <%} %>
                    </div>
                </div>


            </div>
        </div>

    </section>
    <!-- /.content -->


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footer" Runat="Server">
</asp:Content>

