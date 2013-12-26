var roadTripDetail = ['$scope', '$routeParams', 'dataService',
    function($scope, $routeParams, dataService) {
        $scope.roadTripId = $routeParams.roadTripId;
        $scope.payouts = [];
        dataService.getPayoutForRoadTrip($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function(result) {
                angular.copy(result.data, $scope.payouts);
            }, function() {

            });
    }];

