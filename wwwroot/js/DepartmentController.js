/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />


angular.module("DepartmentApp", []).controller("DepartmentCtrl", ['$scope',
    function ($scope) {
        $scope.DepartmentArray = [];
        $scope.load;
        $scope.load = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Department/GetDepartments',
                success: function (data) {
                    $scope.DepartmentArray = data;
                    $scope.$apply();
                }
            });
        };



        $scope.load();
        $scope.departmentInfo = { departmentID: '', departmentName: '', availableSeate: '', isActive: '' };
        $scope.clear = function () {
            $scope.departmentInfo.departmentID = '';
            $scope.departmentInfo.departmentName = '';
            $scope.departmentInfo.availableSeate = '';
            $scope.departmentInfo.isActive = '';

        };

        //Add
        $scope.departmentInsert = function (departmentInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/Department/AddDepartment',
                data: JSON.stringify(departmentInfo),
                success: function (data) {
                    $scope.DepartmentArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };


        //Update
        $scope.updateStart = function (departmentInfo) {
            $scope.departmentInfo.departmentID = departmentInfo.departmentID;
            $scope.departmentInfo.departmentName = departmentInfo.departmentName;
            $scope.departmentInfo.availableSeate = departmentInfo.availableSeate;
            $scope.departmentInfo.isActive = departmentInfo.isActive;

        };

        $scope.updateConfirm = function (departmentInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/Department/UpdateDepartment',
                data: JSON.stringify(departmentInfo),
                success: function (data) {
                    $scope.DepartmentArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (departmentInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/Department/DeleteDepartment',
                    data: JSON.stringify(departmentInfo),
                    success: function (data) {
                        $scope.DepartmentArray = data;
                        $scope.load();
                        $scope.clear();
                    }
                });
            }
        };

        $scope.cancel = function () {
            $scope.clear();
        };
    }
]);


