$(document).ready(function () {
    inserirCliente();
});

function inserirCliente() {

    var dados = $("#form-inserir-cliente").serialize();
    $.post("/Cliente/Inserir/", dados, function () {

    });
}