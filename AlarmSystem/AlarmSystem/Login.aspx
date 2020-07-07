<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AlarmSystem.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Alarm System - Login</title>

    <%--Bootstrap--%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="text-center">Alarm System</h1>
            <hr />
            <div class="row">
                <div class="col-md-4 offset-md-4 col-sm-12">
                    <div class="form-group">
                        <label>Username</label>
                        <asp:TextBox ID="txtUsername" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 offset-md-4 col-sm-12">
                    <div class="form-group">
                        <label>Password</label>
                        <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-4 offset-md-4 col-sm-12">
                    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text=""></asp:Label>
                </div>
                <div class="col-md-4 offset-md-4 col-sm-12">
                    <asp:Button ID="btnLogin" CssClass="btn btn-primary" OnClick="btnLogin_Clicked" runat="server" Text="Login" />
                </div>
            </div>
        </div>
    </form>

    <%--Bootstrap & Jquery--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>

    <%--angular js--%>
    <%--<script src="https://cdnjs.cloudflare.com/ajax/libs/angular.js/1.3.4-build.3588/angular.min.js" integrity="sha256-UYxsBli+wc8cHs1SVw0hYf3llQlUwLuKnRii303Ivk0=" crossorigin="anonymous"></script>--%>
</body>
</html>
