var lendMoneyController = ['$scope', '$routeParams', '$window', 'dataService',
function($scope, $routeParams, $window, dataService) {
    $scope.peopleInRoadTrip = [];
    $scope.lendMoney = {};
    $scope.borrower = null;
    $window.navigator.geolocation.getCurrentPosition(function (position) {
        $scope.$apply(function () {
            $scope.position = position;
        });
    }, function (error) {
        //position Error
    });
    
    dataService.getPeopleInRoadTrip($routeParams.roadTripId)
            .then(function (result) {
                //success
                angular.copy(result.data, $scope.peopleInRoadTrip);
            }, function () {
                //error
            });

    $scope.save = function() {
        $scope.lendMoney.ownerId = $('input#hfUserId').val();
        $scope.lendMoney.borrowerId = $scope.borrower.id;
        $scope.lendMoney.longitude = $scope.position.coords.longitude;
        $scope.lendMoney.latitude = $scope.position.coords.latitude;
        $scope.lendMoney.roadTripId = $routeParams.roadTripId;

        dataService.saveNewRoadTripExpense($scope.lendMoney).then(function(result) {
            $window.location.href = "#/expenses/" + $routeParams.roadTripId;
        }, function() {
            //error
        });

    };

}]