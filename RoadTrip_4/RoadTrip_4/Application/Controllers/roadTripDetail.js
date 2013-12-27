var roadTripDetail = ['$scope', '$routeParams', 'dataService',
    function($scope, $routeParams, dataService) {
        $scope.roadTripId = $routeParams.roadTripId;
        $scope.payouts = [];
        $scope.roadTripHashCode = '';
        dataService.getRoadTripById($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function(result) {
                $scope.roadTripHashCode = result.roadTripHashId;
            }, function(e) {
                //error
            });
        dataService.getPayoutForRoadTrip($routeParams.roadTripId, $('input#hfUserId').val())
            .then(function(result) {
                angular.copy(result.data, $scope.payouts);
            }, function() {

            });
        
    }];

