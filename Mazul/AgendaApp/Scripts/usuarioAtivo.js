var usuarioAtivoApp = angular.module("usuarioAtivoApp", ["usuarioAtivoApp.directives"]);
var itemId;

usuarioAtivoApp.controller('UsuarioAtivoController', function ($scope, $http, $window) {
     $scope.master = {};

    $scope.update = function (user) {
        $scope.master = angular.copy(user);
    };
    //$scope.listar = function () {
    //    $http({
    //        url: '/UsuarioAtivo/Listar',
    //        method: 'GET'
    //    })
    //        .success(function (data, status, headers, config) {
    //            $scope.usuariosAtivos = data;
    //        })
    //        .error(function (error) {
    //            alert("DEU ERRO!");
    //            $scope.status = 'Unable to load customer data: ' + error.message;
    //            console.log($scope.status);
    //        });
    //}

    $scope.login = function (usuarioAtivo) {
        $http({
            traditional: true,
            url: '/UsuarioAtivo/Login',
            method: 'POST',
            data: JSON.stringify(usuarioAtivo)
        })
            .success(function (data, status, headers, config) {
                //if (!data) {
                    //$window.location.reload();
                //} else {
                //    alert("entrou aqui")
                //    $window.location = "/Evento/Index"
                //}
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.cadastrar = function (usuarioAtivo) {
        $scope.master = angular.copy(usuarioAtivo);
        $http({
            traditional: true,
            url: '/UsuarioAtivo/Inserir',
            method: 'POST',
            data: JSON.stringify(usuarioAtivo)
        })
            .success(function (data, status, headers, config) {
                $window.location = "/UsuarioAtivo/Index"
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.registrar = function () {
        $http({
            traditional: true,
            url: '/UsuarioAtivo/Inserir',
            method: 'GET'
        })
            .success(function (data, status, headers, config) {              
              
                //$window.location = "/UsuarioAtivo/Index"
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.editar = function (id) {
        $http({
            url: '/UsuarioAtivo/Editar',
            method: 'POST',
            params: { id: id }
        })
            .success(function (data, status, headers, config) {
                alert("Usuário editado!")
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.recuperarDados = function (id) {
        $http({
            url: '/UsuarioAtivo/RecuperarDados',
            method: 'GET',
            params: { id: id }
        })
            .success(function (data, status, headers, config) {
                if (json.isRedirect) {
                    window.location.href = json.redirectUrl;
                }
                $scope.nome = data.Nome + ' ' + data.Sobrenome;
                $scope.email = data.Email;
                $scope.telefone = data.Telefone;
                $scope.endereco = data.Endereco;
                $scope.sexo = data.Sexo;
                itemId = id;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.excluir = function () {
        $http({
            url: '/UsuarioAtivo/Excluir',
            method: 'GET',
            params: { id: itemId }
        })
            .success(function (data, status, headers, config) {
                if (data == "") {
                    $window.location = "/UsuarioAtivo/Login";
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    //var compareTo = function () {
    //    return {
    //        require: "ngModel",
    //        scope: {
    //            otherModelValue: "=compareTo"
    //        },
    //        link: function (scope, element, attributes, ngModel) {

    //            ngModel.$validators.compareTo = function (modelValue) {
    //                return modelValue == scope.otherModelValue;
    //            };

    //            scope.$watch("otherModelValue", function () {
    //                ngModel.$validate();
    //            });
    //        }
    //    };
    //};
});


angular.module("usuarioAtivoApp.directives", []).directive('pwCheck', [function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, ctrl) {

            var me = attrs.ngModel;
            var matchTo = attrs.pwCheck;

            scope.$watch('[me, matchTo]', function (value) {
                ctrl.$setValidity('pwmatch', scope[me] === scope[matchTo]);
            });

        }
    }
}]);
//module.directive("compareTo", compareTo);

//function check() {
//    $http({
//        url: '/Home/CheckSession',
//        method: 'GET'
//    })
//        .success(function (data, status, headers, config) {
//            var result = data;
//            if (result == "false") {
//                window.location.href = hostpath + "/UsuarioAtivo/Login";
//            }
//        })
//}
//check();

//var mymodal = angular.module('mymodal', []);

//mymodal.controller('MainCtrl', function ($scope) {
//    $scope.showModal = false;
//    $scope.toggleModal = function () {
//        $scope.showModal = !$scope.showModal;
//    };
//});

//mymodal.directive('modal', function () {
//    return {
//        template:
//          '<div class="modal">' +
//              '<div class="modal-content">' +
//                    '<div class="modal-body" ng-transclude>' +
//                    '</div>' +
//                    '<div class="modal-footer">' +
//                        '<a type="button" class="btn-flat left" data-dismiss="modal" aria-hidden="true">Cancelar</a>' +
//                        '<a ng-click="excluir()" class="btn-flat right" data-dismiss="modal" aria-hidden="true">Excluir</a>' +
//                    '</div>' +
//              '</div>' +
//          '</div>',
//        restrict: 'E',
//        transclude: true,
//        replace: true,
//        scope: true,
//        link: function postLink(scope, element, attrs) {
//            scope.title = attrs.title;

//            scope.$watch(attrs.visible, function (value) {
//                if (value == true)
//                    $(element).modal('show');
//                else
//                    $(element).modal('hide');
//            });

//            $(element).on('shown.bs.modal', function () {
//                scope.$apply(function () {
//                    scope.$parent[attrs.visible] = true;
//                });
//            });

//            $(element).on('hidden.bs.modal', function () {
//                scope.$apply(function () {
//                    scope.$parent[attrs.visible] = false;
//                });
//            });
//        }
//    };
//});