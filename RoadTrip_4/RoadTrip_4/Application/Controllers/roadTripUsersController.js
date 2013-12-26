var roadTripUsersController = ['$scope', '$routeParams', 'dataService',
    function ($scope, $routeParams, dataService) {
        $scope.peopleInRoadTrip = [];
        $scope.roadTripId = $routeParams.roadTripId;
        dataService.getPeopleInRoadTrip($routeParams.roadTripId)
            .then(function(result) {
                //success
                angular.copy(result.data, $scope.peopleInRoadTrip);
            }, function() {
                //error
            });

    }]