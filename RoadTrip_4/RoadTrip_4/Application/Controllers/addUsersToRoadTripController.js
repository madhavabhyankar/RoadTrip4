var addUsersToRoadTripController = ['$scope', '$window', '$routeParams', 'dataService',
function($scope, $window, $routeParams, dataService) {
    $scope.roadTripId = $routeParams.roadTripId;
    $scope.addUserToRoadTrip = {};
    
    $scope.addUser = function () {
        $scope.addUserToRoadTrip.roadTripId = $routeParams.roadTripId;
        $scope.addUserToRoadTrip.ownerId = $('input#hfUserId').val();
        dataService.addUerToRoadTrip($scope.addUserToRoadTrip, $('input#hfUserId').val())
            .then(function (result) {
                $window.location.href = "#/roadTripDetail/" + $routeParams.roadTripId;
                
                //success
            }, function() {
                //errror
            });
    }
}]