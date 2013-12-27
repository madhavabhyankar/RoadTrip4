joinRoadTripController = ['$scope', '$window', 'dataService',
function($scope, $window, dataService) {
    $scope.joinRoadTrip = {};
    $scope.join =  function() {
        dataService.joinRoadTrip($scope.joinRoadTrip.roadTripCodedId, $('input#hfUserId').val(),$scope.joinRoadTrip.ownerEmailId)
            .then(function(result) {
                $window.location.href = "/#";
            }, function(e) {
                //error
            });
    }
}]