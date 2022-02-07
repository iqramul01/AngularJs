/// <reference path="../lib/angular.js/angular.js" />
/// <reference path="../lib/angular-route/angular-route.js" />



angular.module("DoctorApp", []).controller("DoctorCtrl", ['$scope',
    function ($scope) {
        $scope.DoctorArray = [];
        $scope.load;

        $scope.load = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Doctor/GetDoctors',
                success: function (data) {
                    $scope.DoctorArray = data;
                    $scope.$apply();
                }
            });
        };

       

        $scope.DepartmentArray = [];
        $scope.getDepartmentList = function () {
            $.ajax({
                type: 'GET',
                contentType: 'application/json',
                url: '/Department/GetDepartments',
                success: function (data) {
                    $scope.DepartmentArray  = data;
                    $scope.$apply();
                }
            });
        };


        $scope.checkselection = function () {
            if ($scope.departmentID != "" && $scope.departmentID != undefined) {
                $scope.msg = 'Selected Value : ' + $scope.departmentID;
            }
            else {
                $scope.msg = 'Please Select Dropdown Value';
            }
        }

        $scope.load();
        $scope.doctorInfo = { doctorID: '', doctorName: '', contactAddress: '', emailAddress: '', joiningDate: '', salary: '', isActive: '', departmentID: '' };
        $scope.clear = function () {
            $scope.doctorInfo.doctorID = '';
            $scope.doctorInfo.doctorName = '';
            $scope.doctorInfo.contactAddress = '';
            $scope.doctorInfo.emailAddress = '';
            $scope.doctorInfo.joiningDate = '';
            $scope.doctorInfo.salary = '';
            $scope.doctorInfo.isActive = '';
            $scope.doctorInfo.departmentID = '';

        };

        //Add
        $scope.doctorInsert = function (doctorInfo) {
            $.ajax({
                type: 'POST',
                contentType: 'application/json',
                url: '/Doctor/AddDoctor',
                data: JSON.stringify(doctorInfo),
                success: function (data) {
                    $scope.DoctorArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Update
        $scope.updateStart = function (doctorInfo) {
            $scope.doctorInfo.doctorID = doctorInfo.doctorID;
            $scope.doctorInfo.doctorName = doctorInfo.doctorName;
            $scope.doctorInfo.contactAddress = doctorInfo.contactAddress;
            $scope.doctorInfo.emailAddress = doctorInfo.emailAddress;
            $scope.doctorInfo.joiningDate = doctorInfo.joiningDate;
            $scope.doctorInfo.salary = doctorInfo.salary;
            $scope.doctorInfo.isActive = doctorInfo.isActive;
            $scope.doctorInfo.departmentID = doctorInfo.departmentID;



        };

        $scope.updateConfirm = function (doctorInfo) {
            $.ajax({
                type: 'PUT',
                contentType: 'application/json',
                url: '/Doctor/UpdateDoctor',
                data: JSON.stringify(doctorInfo),
                success: function (data) {
                    $scope.DoctorArray = data;
                    $scope.load();
                    $scope.clear();
                }
            });
        };

        //Delete
        $scope.deleteInformation = function (doctorInfo) {
            var state = confirm("Do you want to delete data????");
            if (state === true) {
                $.ajax({
                    type: 'DELETE',
                    contentType: 'application/json',
                    url: '/Doctor/DeleteDoctor',
                    data: JSON.stringify(doctorInfo),
                    success: function (data) {
                        $scope.DoctorArray = data;
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
])