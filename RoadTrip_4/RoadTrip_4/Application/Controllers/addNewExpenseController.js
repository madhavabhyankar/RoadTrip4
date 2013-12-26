var addNewExpenseController = ['$scope', '$window', '$routeParams', 'dataService',
function($scope, $window, $routeParams, dataService) {
    $scope.roadTripId = $routeParams.roadTripId;
    $scope.newExpense = {};
    $window.navigator.geolocation.getCurrentPosition(function (position) {
        $scope.$apply(function () {
            $scope.position = position;
        });
    }, function (error) {
        //position Error
    });

    $scope.saveExpense = function() {
        $scope.newExpense.ownerId = $('input#hfUserId').val();

        $scope.newExpense.longitude = $scope.position.coords.longitude;
        $scope.newExpense.latitude = $scope.position.coords.latitude;
        $scope.newExpense.roadTripId = $routeParams.roadTripId;


        dataService.saveNewRoadTripExpense($scope.newExpense).then(function(result) {
            $window.location.href = "#/expenses/" + $routeParams.roadTripId;
        }, function() {
            //error
        });
    };
}]