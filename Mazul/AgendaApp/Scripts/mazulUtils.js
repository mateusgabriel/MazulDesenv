function alerta(mensagem, tipo) {
    Materialize.toast(mensagem, 70000, tipo == 'sucesso' ? 'green white-text' : 'red white-text');
}