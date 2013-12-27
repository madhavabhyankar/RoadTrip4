

var module = angular.module('myRoadTrips', ['ngRoute']);

module.config(['$routeProvider', function($routeProvider) {
    $routeProvider.when("/", {
        controller: 'roadTripsController',
        templateUrl: '../templates/roadTripsView.html'
    });
    $routeProvider.when('/newRoadTrip', {
        controller: 'newRoadTripController',
        templateUrl: '../templates/newRoadTrip.html'
    });
    $routeProvider.when('/roadTripDetail/:roadTripId', {
        controller: 'roadTripDetail',
        templateUrl: '../templates/roadTripDetail.html'
    });
    $routeProvider.when('/roadTripUsers/:roadTripId', {
        controller: 'roadTripUsersController',
        templateUrl: '../templates/roadTripUsers.html'
    });
    $routeProvider.when('/addUsersToRoadTrip/:roadTripId', {
        controller: 'addUsersToRoadTripController',
        templateUrl: '../templates/addUsersToRoadTrip.html'
    });
    $routeProvider.when('/expenses/:roadTripId', {
        controller: 'expensesController',
        templateUrl: '../templates/expensesView.html'
    });
    $routeProvider.when('/addNewExpense/:roadTripId', {
        controller: 'addNewExpenseController',
        templateUrl: '../templates/addNewExpense.html'
    });
    $routeProvider.when('/lendMoney/:roadTripId', {
        controller: 'lendMoneyController',
        templateUrl: '../templates/lendMoney.html'
    });
    $routeProvider.when('/deleteRoadTrip/:roadTripId', {
        controller: 'deleteRoadTripController',
        templateUrl: '../templates/deleteRoadTrip.html'
    });
    $routeProvider.when('/joinRoadTrip', {
        controller: 'joinRoadTripController',
        templateUrl: '../templates/joinRoadTrip.html'
    })
}]);