/////// <reference path="../lib/angular.js/angular.js" />
/////// <reference path="../lib/angular-route/angular-route.js" />



var app = angular.module('PatientApp', []);



var baseUrl = 'https://localhost:44325/Patient/GetPatients';

 
app.factory('PatientFact', function ($http) {
    var factory = {};

    factory.GetPatients = function () {
        return $http.get(baseUrl);
    };

    factory.SavePatients = function (obj) {
        return $http.post(baseUrl, obj);
    };

    factory.UpdatePatients = function (obj) {
        return $http.put(baseUrl + obj.id, obj);
    };

    factory.DeletePatients = function (id) {
        return $http.delete(baseUrl + id);
    };
    return factory;
});
//app.directive('myOwnMessage', function () {
//    return {
//        template:'Medicare Hopital Powerd By BD'
//    }
//})

// Custom Directive
app.directive('myOwnMessage', function () {
    function linkedFunction($scope, element, attributes) {
        $scope.text ="Welcome To Medicare Hospital Limited";
        $scope.changeText = function (myText) {
            $scope.text = myText;
        }
    }
    return {
        link: linkedFunction,
        template: '<span ng-click="changeText(\'We are ready to serve you.Please fill up the following form.\')">{{text}}</span>',
        restrict: 'A'
    };
});

app.controller('PatientCtrl', function ($scope, PatientFact) {
    Init();
    function Init() {
        PatientFact.GetPatients().then(function (res) {
            $scope.PatientList = res.data;
        })
    }

    $scope.SavePatient = function () {
        PatientFact.SavePatients($scope.objPatient).then(function () {
            Init();
            Clear();
        })
    }

    $scope.EditPatient = function (p) {
        $scope.objPatient = p;
    }

    $scope.UpdatePatient = function () {
        PatientFact.UpdatePatients($scope.objPatient).then(function () {
            Init();
            Clear();
        })
    }

    $scope.DeletePatient = function (id) {
        PatientFact.DeletePatients(id).then(function () {
            Init();
        })
    }

    function Clear() {
        $scope.objPatient = null;
    }

});