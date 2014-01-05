var roadTripDetail = ['$scope', '$routeParams', 'dataService',
    function($scope, $routeParams, dataService) {
        $scope.roadTripId = $routeParams.roadTripId;
        $scope.inComingMoney = [];
        $scope.outGoing = [];
        
        $scope.roadTripHashCode = '';
        dataService.getRoadTripById($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function(result) {
                $scope.roadTripHashCode = result.roadTripHashId;
            }, function(e) {
                //error
            });
        dataService.getPayoutForRoadTrip($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function (result) {
                angular.forEach(result.data, function(value, key) {
                    if (value.amount > 0) {
                        $scope.outGoing.push(value);
                    } else {
                        $scope.inComingMoney.push(value);
                    }
                });
                angular.copy(result.data, $scope.payouts);
            }, function() {

            });

        $scope.isIncoming = function(payment) {
            return payment.amount < 0;
        };

    }];

