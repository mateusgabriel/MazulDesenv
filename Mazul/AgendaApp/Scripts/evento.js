var eventoApp = angular.module('eventoApp', []);
var itemId;
var primeiroClickSemana = true;
var primeiroClickMes = true;

eventoApp.controller('EventoController', function ($scope, $http, $window) {
    
    $scope.month = [];
    $scope.month[0]="Janeiro";
    $scope.month[1]="Fevereiro";
    $scope.month[2]="Março";
    $scope.month[3]="Abril";
    $scope.month[4]="Maio";
    $scope.month[5]="Junho ";
    $scope.month[6]="Julho";
    $scope.month[7]="Agosto";
    $scope.month[8]="Setembro";
    $scope.month[9]="Outubro";
    $scope.month[10]="Novembro";
    $scope.month[11]="Dezembro";

    var dataDoDia = new Date();
    var m = dataDoDia.getMonth();
    var d = dataDoDia.getDate();
    $scope.mes = $scope.month[m]
    $scope.dia = d;

    $scope.selection = [];
    $scope.toggleSelection = function (eventoId) {
        var idx = $scope.selection.indexOf(eventoId);

        if (idx > -1) {
            $scope.selection.splice(idx, 1);
        }
        else {
            $scope.selection.push(eventoId);
        }
    };

    $scope.excluir = function () {
        var ids = $scope.selection;
        $window.location = "/Evento/Excluir/" + ids
    }

    $scope.visualizar = function () {
        var id = $scope.selection;
        $window.location = "/Evento/Visualizar/" + id
    }

    $scope.recuperarDados = function (id) {
        $http({
            url: '/Evento/RecuperarDados',
            method: 'GET',
            params: { id: id }
        })
            .success(function (data, status, headers, config) {
                $scope.nome = data.Nome;
                $scope.dataEvento = new Date(parseInt(data.DataEvento.substr(6)));
                $scope.descricao = data.Descricao;
                $scope.periodicidade = data.Periodicidade;
                $scope.contatos = data.ContatosNome;
                itemId = id;
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    $scope.deletarEventos = function (id) {
        $http({
            url: '/Evento/DeletarEventos',
            method: 'GET',
            params: {
                ids: id
            }
            //params: { id: itemId }
        })
            .success(function (data, status, headers, config) {
                if (data == "") {
                    $window.location = "/Evento/Index/"
                }
            })
            .error(function (error) {
                $scope.status = 'Unable to load customer data: ' + error.message;
                console.log($scope.status);
            });
    }

    //$scope.listarEventosDiarios = function () {
    //    Materialize.showStaggeredList("#eventosDiariosList");
    //}

    $scope.listarEventosSemanais = function () {
        if (primeiroClickSemana) {
            Materialize.showStaggeredList("#eventosSemanaisList");
            Materialize.showStaggeredList("#eventosDomingo");
            Materialize.showStaggeredList("#eventosSegunda");
            Materialize.showStaggeredList("#eventosTerca");
            Materialize.showStaggeredList("#eventosQuarta");
            Materialize.showStaggeredList("#eventosQuinta");
            Materialize.showStaggeredList("#eventosSexta");
            Materialize.showStaggeredList("#eventosSabado");
            primeiroClickSemana = false
        }
    }

    $scope.listarEventosMensais = function () {
        if (primeiroClickMes) {
            Materialize.showStaggeredList("#eventosMensaisList");
            Materialize.showStaggeredList("#eventosPrimeiroDomingo");
            Materialize.showStaggeredList("#eventosSegundoDomingo");
            Materialize.showStaggeredList("#eventosTerceiroDomingo");
            Materialize.showStaggeredList("#eventosQuartoDomingo");
            primeiroClickMes = false
        }
    }

    $scope.iniciarPickdate = function () {
        var $input = $('.datepicker').pickadate()
        var picker = $input.pickadate('picker')

        var data = new Date();
        var dia = data.getDate();
        var mes = data.getMonth();
        var ano = data.getFullYear();
        picker.set('select', [ano, mes, dia])

    }

    $scope.formatarPickdate = function (dataEvento) {
        var $input = $('.datepicker').pickadate()
        var picker = $input.pickadate('picker')

        var data = dataEvento.split(" ");
        data = data[0].split("/");
        var dia = parseInt(data[0]);
        //Janeiro equivale a 0!
        var mes = parseInt(data[1]) - 1;
        var ano = parseInt(data[2]);
        picker.set('select', [ano, mes, dia])

    }

});
