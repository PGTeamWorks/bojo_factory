$(document).ready(function() {
    olk();
});

function olk() {
    $("#btn-salvar-cliente").click(function () {
        var dados = $("#form-inserir-cliente").serialize();
        $.post("/Cliente/Inserir/", dados, function () {

        });

    });
}