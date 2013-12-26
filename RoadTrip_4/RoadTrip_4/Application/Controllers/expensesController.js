var expensesController = ['$scope', '$routeParams', 'dataService',
    function ($scope, $routeParams, dataService) {
        $scope.roadTripId = $routeParams.roadTripId;

        $scope.expenses = [];
        $scope.myExpenses = [];

        dataService.getExpensesForRoadTrip($routeParams.roadTripId)
            .then(function(result) {
                angular.copy(result.data, $scope.expenses);
            }, function() {
                //error
            });
        dataService.getMyExpensesForRoadTrip($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function(result) {
                angular.copy(result.data, $scope.myExpenses);
            }, function() {
                //error
            });
    }];