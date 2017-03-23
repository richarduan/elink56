<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>
<!DOCTYPE html>

<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>欢迎使用易联在线办公系统</title>
  <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
  <link rel="stylesheet" href="static/css/bootstrap.min.css">
  <link rel="stylesheet" href="static/css/font-awesome.min.css">
  <link rel="stylesheet" href="static/css/ionicons.min.css">
  <link rel="stylesheet" href="static/css/AdminLTE.css">
  <link rel="stylesheet" href="static/plugins/iCheck/square/blue.css">

</head>
<body class="hold-transition login-page">
<%=Token %>

<div class="login-box">
  <div class="login-logo">
    <a>易联在线办公系统</a>
  </div>
  <!-- /.login-logo -->
  <div class="login-box-body">
    <p class="login-box-msg">您好，欢迎使用易联在线办公系统</p>


      <div class="form-group has-feedback">
        <input type="text" class="form-control" placeholder="请输入手机号码进行登入" id="userPhone">
        <span class="glyphicon glyphicon-phone form-control-feedback"></span>
      </div>
      <div class="form-group has-feedback">
        <input type="password" class="form-control" placeholder="请输入登入密码" id="userPasswd">
        <span class="glyphicon glyphicon-lock form-control-feedback"></span>
      </div>
      <div class="row">
        <div class="col-xs-12">
          <div class="checkbox icheck">
            <label>
              <input type="checkbox"><span style="padding-left:10px; line-height:22px;display:block; float:right;">记住账号密码[请勿在公共计算机上使用！]</span>
            </label>
          </div>
        </div>

      </div>
      <div class="row">
        <!-- /.col -->
        <div class="col-xs-12" style="margin-bottom:10px; margin-top:10px;">
          <button type="button" id="login" class="btn btn-primary btn-block btn-flat">确认登入</button>
        </div>
        <!-- /.col -->          
      </div>



    <a href="#">忘记密码，我要找回密码.</a><br>

  </div>
  <!-- /.login-box-body -->
</div>
<!-- /.login-box -->

<script src="static/js/jquery.min.js"></script>
<script src="static/js/bootstrap.min.js"></script>
<script src="static/js/funciton.js"></script>

<script src="static/plugins/iCheck/icheck.min.js"></script>


<script>
    $(function () {

        $("#login").click(function () {

            var data = {};
            data.userName = $("#userPhone").val();
            data.userPasswd = $("#userPasswd").val();
            data.action="userlogin"

            $.postJSON("login.aspx?rnd=" + rnd(), data, function (restful) {
                if (restful.status == "1") {
                    window.location.href = "Default.aspx"
                    return false;
                }
                alert(restful.msg);
                return false;
            });






        });








        $('input').iCheck({
            checkboxClass: 'icheckbox_square-blue',
            radioClass: 'iradio_square-blue',
            increaseArea: '20%' // optional
        });
    });
</script>
</body>
</html>

