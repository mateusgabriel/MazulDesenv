var usuarioPassivoApp = angular.module("usuarioPassivoApp", []);
var itemId;

usuarioPassivoApp.controller('UsuarioPassivoController', function ($scope, $http, $window) {

    $scope.selection = [];
    $scope.toggleSelection = function (contatoId) {
        var idx = $scope.selection.indexOf(contatoId);

        if (idx > -1) {
            $scope.selection.splice(idx, 1);
        }
        else {
            $scope.selection.push(contatoId);
        }
    };

    $scope.excluir = function () {
        var ids = $scope.selection;
        $window.location = "/UsuarioPassivo/Excluir/" + ids
    }

    $scope.visualizar = function () {
        var id = $scope.selection;
        $window.location = "/UsuarioPassivo/Visualizar/" + id
    }

    $scope.editar = function (id) {
        $http({
            url: '/UsuarioPassivo/Editar',
            method: 'POST',
            params: { id: id }
        })
            .success(function (data, status, headers, config) {
                alert("Usuário editado!")
                //$scope.usuariosPassivos = data;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.cadastrar = function(usuarioPassivo) {
        $http({
            traditional: true,
            url: '/UsuarioPassivo/Inserir',
            method: 'POST',
            data: JSON.stringify(usuarioPassivo),
            dataType: "json"
        })
            .success(function (data) {
                $window.location = "/UsuarioPassivo/Index"
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.recuperarDados = function (id) {
        $http({
            url: '/UsuarioPassivo/RecuperarDados',
            method: 'GET',
            params: { id: id }
        })
            .success(function (data, status, headers, config) {
                $scope.nome = data.Nome + ' ' + data.Sobrenome;
                $scope.email = data.Email;
                $scope.endereco = data.Endereco;
                $scope.telefone = data.Telefone;
                $scope.sexo = data.Sexo;
                itemId = id;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.deletarContatos = function (id) {
        $http({
            url: '/UsuarioPassivo/DeletarUsuariosPassivos',
            method: 'GET',
            params: {
                ids: id
            }
            //params: { id: itemId }
        })
            .success(function (data, status, headers, config) {
                if (data == "") {
                    $window.location = "/UsuarioPassivo/Index/"
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

});
//$scope.nameRequired = '';
//$scope.emailRequired = '';
//$scope.passwordRequired = '';

//if (!$scope.usuarioPassivo.Nome) {
//$scope.nomeObrigatorio = 'Campo Nome Obrigatório';
//}

//if (!$scope.usuarioPassivo.Sobrenome) {
//    $scope.sobrenomeObrigatorio = 'Campo Sobrenome Obrigatório';
//}

//if (!$scope.usuarioPassivo.Endereco) {
//    $scope.enderecoObrigatorio = 'Campo Endereco Obrigatório';
//}

//if (!$scope.usuarioPassivo.Email) {
//    $scope.emailObrigatorio = 'Campo Email Obrigatório';
//}

//if (!$scope.usuarioPassivo.Telefone) {
//    $scope.telefoneObrigatorio = 'Campo Telefone Obrigatório';
//}