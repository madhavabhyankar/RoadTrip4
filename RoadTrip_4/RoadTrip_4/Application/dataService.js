module.factory('dataService', ['$http', '$q',
function ($http, $q) {

    _roadTrips = [];
    _roadTripsInitialized = false;

    _getRoadTripsForUser = function(userDetailId) {
        var deferred = $q.defer();
        if (!_roadTripsInitialized) {
            $http.get('/api/RoadTrips/' + userDetailId)
                .then(function(result) {
                    angular.copy(result.data, _roadTrips);
                    _roadTripsInitialized = true;
                    deferred.resolve(_roadTrips);
                }, function() {
                    deferred.reject();
                });
        } else {
            deferred.resolve(_roadTrips);
        }
        return deferred.promise;
    };

    _saveNewRoadTrip = function(roadTrip) {
        var deferred = $q.defer();
        $http.post('/api/RoadTrips', roadTrip)
            .then(function(result) {
                var createdRoadTrip = result.data;
                _roadTrips.splice(0, 0, createdRoadTrip);
                deferred.resolve();
            }, function(e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _getPeopleInRoadTrip = function(roadTripId) {
        var deferred = $q.defer();
        $http.get('/api/UsersInRoadTrip/' + roadTripId)
            .then(function(result) {
                deferred.resolve(result);
            }, function() {
                deferred.reject();
            });
        return deferred.promise;
    };
    _addUerToRoadTrip = function(newUserToAdd) {
        var deferred = $q.defer();
        $http.post('/api/UsersInRoadTrip', newUserToAdd)
            .then(function(result) {
                deferred.resolve(result);
            }, function(e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _getExpensesForRoadTrip = function(roadTripId) {
        var deferred = $q.defer();
        $http.get('/api/expenses/' + roadTripId)
            .then(function(result) {
                deferred.resolve(result);
            }, function() {
                deferred.reject();
            });
        return deferred.promise;
    };
    _getMyExpensesForRoadTrip = function(roadTripId, userId) {
        var deferred = $q.defer();
        $http.get('/api/expenses/' + roadTripId + '/' + userId)
            .then(function(result) {
                deferred.resolve(result);
            }, function() {
                deferred.reject();
            });
        return deferred.promise;
    };
    _getMoneyLentForRoadTrip = function(roadTripId, userId) {
        var deferred = $q.defer();
        $http.get('/api/expenses/' + roadTripId + '/' + userId + '?getBorrowed=false')
            .then(function(result) {
                deferred.resolve(result);
            }, function(e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _getMoneyBorrowedForRoadTrip = function (roadTripId, userId) {
        var deferred = $q.defer();
        $http.get('/api/expenses/' + roadTripId + '/' + userId + '?getBorrowed=true')
            .then(function (result) {
                deferred.resolve(result);
            }, function (e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _saveNewRoadTripExpense = function(expense) {
        var deferred = $q.defer();
        $http.post('/api/expenses', expense)
            .then(function(result) {
                deferred.resolve(result);
            }, function() {
                deferred.reject();
            });
        return deferred.promise;
    };
    _getPayoutForRoadTrip = function(roadTripId, userId) {
        var deferred = $q.defer();
        $http.get('/api/payout/' + roadTripId + '/' + userId)
            .then(function(result) {
                deferred.resolve(result);
            }, function() {
                deferred.reject();
            });
        return deferred.promise;
    };
    _deleteRoadTrip = function(roadTripId) {
        var deferred = $q.defer();
        $http.delete('/api/RoadTrips/'+ roadTripId)
            .then(function(result) {
                var trips = [];
                angular.copy(_roadTrips, trips);
                _roadTrips = _.filter(trips, function(val) {
                    return val.id !== roadTripId;
                });
                deferred.resolve(_roadTrips);
            }, function(e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _joinRoadTrip = function(roadTripCodedId, userId, ownerEmailId) {
        var deferred = $q.defer();
        $http.get('/api/RoadTrips/' + roadTripCodedId + '/' + userId + '?ownerEmailId=' + ownerEmailId)
            .then(function(result) {
                _roadTrips.splice(0, 0, result.data);
                deferred.resolve(result.data);
            }, function(e) {
                deferred.reject(e);
            });
        return deferred.promise;
    };
    _getRoadTripById = function (roadTripId, userid) {
        deferred = $q.defer();
        
        if (!_roadTripsInitialized) {
            _getRoadTripsForUser(userid).then(function(result) {
                var data = _.filter(result, function(val) {
                    return val.id === roadTripId;
                });
                deferred.resolve(data[0]);
            }, function(e) {
                deferred.reject(e);
            });
        } else {
            var data = _.filter(_roadTrips, function(val) {
                return val.id === roadTripId;
            });
            deferred.resolve(data[0]);
        }
        return deferred.promise;
    }
    return {
        roadTrips: _roadTrips,
        getRoadTripsForUser: _getRoadTripsForUser,
        saveNewRoadTrip: _saveNewRoadTrip,
        getPeopleInRoadTrip: _getPeopleInRoadTrip,
        addUerToRoadTrip: _addUerToRoadTrip,
        getExpensesForRoadTrip: _getExpensesForRoadTrip,
        getMyExpensesForRoadTrip: _getMyExpensesForRoadTrip,
        saveNewRoadTripExpense: _saveNewRoadTripExpense,
        getPayoutForRoadTrip: _getPayoutForRoadTrip,
        deleteRoadTrip: _deleteRoadTrip,
        joinRoadTrip: _joinRoadTrip,
        getRoadTripById: _getRoadTripById,
        getMoneyLentForRoadTrip: _getMoneyLentForRoadTrip,
        getMoneyBorrowedForRoadTrip: _getMoneyBorrowedForRoadTrip
    };
    



}])