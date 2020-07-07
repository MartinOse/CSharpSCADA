<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="AlarmSystem.Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- Required meta tags -->
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Alarm System - Home</title>

    <%--Bootstrap--%>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" integrity="sha384-Vkoo8x4CGsO3+Hhxv8T/Q5PaXtkKtu6ug5TOeNV6gBiFeWPGFN9MuhOf23Q9Ifjh" crossorigin="anonymous">
</head>
<body ng-app="myApp" ng-controller="homeCtrl">
    <form id="form1" runat="server">
        <div class="container">
            <h1 class="text-center">Operator Dashboard</h1>
            <hr />
            <div class="row">
                <div class="col-sm-12 text-right">
                    <asp:Button ID="btnSignout" runat="server" OnClick="btnSingout_Clicked" Text="Signout" CssClass="btn btn-warning" />
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <h3>Alarm List</h3>
                </div>
                <div class="col-sm-6">
                    <h2 class="text-right">
                        <asp:Label ID="lblOperator" runat="server" Text=""></asp:Label>
                    </h2>
                </div>
            </div>

            <table class="table table-striped">
                <thead>
                    <tr>
                        <th scope="col">Alarm id</th>
                        <th scope="col">Tag Id</th>
                        <th scope="col">Alarm Type</th>
                        <th scope="col">Data Id</th>
                        <th scope="col">Activation time</th>
                        <th scope="col">Acknowledge time</th>
                    </tr>
                </thead>
                <tbody>
                    <tr ng-repeat="alarm in alarms">
                        <th scope="row">{{alarm.AlarmId}}</th>
                        <td>{{alarm.TagId}}</td>
                        <td>{{alarm.AlarmType}}</td>
                        <td>{{alarm.DataId}}</td>
                        <td>{{alarm.ActivationTime}}</td>
                        <td ng-if="alarm.AcknowledgeTime">{{alarm.AcknowledgeTime}}</td>
                        <td ng-if="!alarm.AcknowledgeTime">
                            <button type="button" ng-click="ack(alarm.AlarmId)" class="btn btn-primary">
                                Ack</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

    <%--Bootstrap & Jquery--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.4.1/jquery.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js" integrity="sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo" crossorigin="anonymous"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js" integrity="sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6" crossorigin="anonymous"></script>

    <%--angular js--%>
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.9/angular.min.js"></script>

    <script>
        var app = angular.module('myApp', []);

        app.controller('homeCtrl', function ($scope, $http) {
            $scope.getAllAlarms = function () {
                $http({
                    method: "POST",
                    url: "Default.aspx/GetAllAlarms",
                    data: {}
                }).then(
                    function mySucces(response) {
                        var json = JSON.parse(response.data.d)
                        $scope.alarms = json.Records;
                        setTimeout(function () { //call this function again after 2 seconds
                            $scope.getAllAlarms();

                        }, 2000);
                    },
                    function myError(error) {

                    });
            }

            $scope.chartUpdate = function () {
                $http({
                    method: "POST",
                    url: "Default.aspx/chartUpdate"
                }).then(
                    setTimeout(function () {
                        $scope.chartUpdate();
                    }, 2000));
            }

            $scope.ack = function (AlarmId) {
                $http({
                    method: "POST",
                    url: "Default.aspx/AcknowledgeAlarm",
                    data: { id: AlarmId }
                }).then(
                function mySucces(response) {
                    var json = JSON.parse(response.data.d)
                    if (json.Success){
                        $scope.getAllAlarms();
                    }
                    else {
                        alert("Error while updating record!");
                    }
                },
                function myError(error) {
                    alert("Error while sending request!");
                });
            }

            $scope.getAllAlarms();
            $scope.chartUpdate();
        });
    </script>
        <p>
            &nbsp;</p>
        <p>
            <asp:Chart ID="Chart1" runat="server" DataSourceID="SqlTemp" Height="262px" Width="545px">
                <series>
                    <asp:Series ChartType="Line" Name="Series1" YValueMembers="Temperature">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <asp:Chart ID="Chart2" runat="server" DataSourceID="SqlTemp" Height="262px" Width="545px">
                <series>
                    <asp:Series ChartType="Line" Name="Series1" YValueMembers="Signal">
                    </asp:Series>
                </series>
                <chartareas>
                    <asp:ChartArea Name="ChartArea1">
                    </asp:ChartArea>
                </chartareas>
            </asp:Chart>
            <asp:SqlDataSource ID="SqlTemp" runat="server" ConnectionString="<%$ ConnectionStrings:SCADAConnectionString %>" SelectCommand="SELECT DISTINCT [Temperature], [Timestamp], [Signal] FROM [TAG_DATA]"></asp:SqlDataSource>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
        <p>
            &nbsp;</p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
    </form>

    </body>
</html>
