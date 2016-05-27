function alerta(mensagem, tipo) {
    Materialize.toast(mensagem, 7000, tipo == 'sucesso' ? 'green white-text' : 'red white-text');
}