$(document).ready(function() {
    inserirCliente();
});

function inserirCliente() {
    $("#btn-salvar-cliente").click(function () {
        var dados = $("#form-inserir-cliente").serialize();
        $.post("/Cliente/Inserir/", dados, function () {

        });

    });
}