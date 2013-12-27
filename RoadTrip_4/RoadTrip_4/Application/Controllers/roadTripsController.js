var roadTripsController = ['$scope', '$http', 'dataService',
    function ($scope, $http, dataService) {
        $scope.activeRoadTrps = [];
        $scope.deletedRoadTrips = [];
        dataService.getRoadTripsForUser($('input#hfUserId').val())
            .then(function(result) {
                $scope.activeRoadTrps = _.filter(result, function (val) {
                    return val.roadTripStatus !== 1;
                });
                $scope.deletedRoadTrips = _.filter(result, function (val) {
                    return val.roadTripStatus === 1;
                });
            }, function() {
                alert('Error!');
            });
    }];