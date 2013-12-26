var newRoadTripController = ['$scope', '$location', 'dataService',
function ($scope, $location, dataService) {
    $scope.newRoadTrip = {};

    $scope.save = function() {
        $scope.newRoadTrip.ownerId = $('input#hfUserId').val();


        dataService.saveNewRoadTrip($scope.newRoadTrip).then(function(result) {
            $location.path('/');
        }, function() { //error
        });

    };
}]