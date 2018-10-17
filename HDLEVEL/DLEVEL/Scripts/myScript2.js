/// <reference path="angular.min.js" />

var mApp = angular.module("myModule", []);

mApp.controller("myController", function ($scope, $location, $anchorScroll) {
    var employee = {
        firstName: "Edward",
        lastName: "Chen"
    }
    var employees = [
{ name: "Ben", gender: "male", salary: "50000" },
{ name: "Sara", gender: "Female", salary: "40000" },
{ name: "Pam", gender: "Female", salary: "60000" },
{ name: "Todd", gender: "Male", salary: "30000" }

    ];

    var country = {
        name: "USA",
        flag: ""
    };
    var technologies = [
        { name: "C#", likes: 0, dislikes: 0 },
         { name: "Java", likes: 0, dislikes: 0 },
          { name: "C++", likes: 0, dislikes: 0 },
           { name: "PHP", likes: 0, dislikes: 0 }
    ];
    $scope.employees = employees;
    $scope.message = "";
    $scope.employee = employee;
    $scope.country = country;
    $scope.technologies = technologies;
    $scope.notice = "";
    $scope.textStyling = "boldtext";
    $scope.tableStyling = "table";
    $scope.myData = [2,4,7,5,10];

    $scope.todos = [{
        'title': 'Welcome to todo list !!!', 'done': false
    }];

    $scope.addTodo = function () {
        $scope.todos.push({ 'title': $scope.newTodo, 'done': false })
        $scope.newTodo = '';
    }

    $scope.clear = function () {
        $scope.todos = $scope.todos.filter(function (item) {
            return !item.done
        })
    }
   if ($scope.message.length==0) {
        $scope.notice = "You are about to tell them:"
   }

  //  $scope.showNootice = function (message) {
 //       if (message.length != 0) {
 //           notice = "You are about the tell them:"
 //       }
    //    }

    $scope.incrementLikes = function (technology) {
        technology.likes++;
    }
    $scope.incrementDislikes = function (technology) {
        technology.dislikes++;
    }
    $scope.docView = "~/Home/Documentation.cshtml";
    $scope.scrollTo = function (scrollLocation) {
        $location.hash(scrollLocation);
        $anchorScroll.yOffset = 20;
        $anchorScroll();
    }

});