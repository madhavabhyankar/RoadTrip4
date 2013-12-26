var roadTripsController = ['$scope', '$http', 'dataService',
    function ($scope, $http, dataService) {
        $scope.data = dataService;
        dataService.getRoadTripsForUser($('input#hfUserId').val())
            .then(function(result) {

            }, function() {
                alert('Error!');
            });
    }];