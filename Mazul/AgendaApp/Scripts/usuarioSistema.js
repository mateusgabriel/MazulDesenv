var usuarioSistemaApp = angular.module("usuarioSistemaApp", []);

usuarioSistemaApp.controller('usuarioSistemaController', function ($scope, $http, $window) {

    $scope.login = function (usuarioSistema) {
        $http({
            traditional: true,
            url: '/UsuarioSistema/Login',
            method: 'POST',
            data: JSON.stringify(usuarioSistema),
            dataType: "json"
        })
            .success(function (data) {
                $window.location = "/Evento/Index"
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }
});