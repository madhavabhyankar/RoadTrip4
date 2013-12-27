var deleteRoadTripController = ['$scope', '$routeParams', '$window','dataService',
function($scope, $routeParams,$window, dataService) {
    $scope.roadTripId = $routeParams.roadTripId;

    $scope.deleteRoadTrip = function() {
        dataService.deleteRoadTrip($scope.roadTripId)
            .then(function() {
                $window.location.href = "#/";
            }, function(e) {
                //error
            });
    };
}]