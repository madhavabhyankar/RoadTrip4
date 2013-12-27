var lendMoneyController = ['$scope', '$routeParams', '$window', 'dataService',
function($scope, $routeParams, $window, dataService) {
    $scope.peopleInRoadTrip = [];
    $scope.lendMoney = {};
    $scope.borrower = null;
    $scope.roadTripId = $routeParams.roadTripId;
    $window.navigator.geolocation.getCurrentPosition(function (position) {
        $scope.$apply(function () {
            $scope.position = position;
        });
    }, function (error) {
        //position Error
    });
    var lendingUser = parseInt($('input#hfUserId').val());
    dataService.getPeopleInRoadTrip($routeParams.roadTripId)
            .then(function (result) {
                //success
                allInRoadTrip = [];
                angular.copy(result.data, allInRoadTrip);
                $scope.peopleInRoadTrip = _.filter(allInRoadTrip, function(val) {
                    return val.id !== lendingUser;
                });
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