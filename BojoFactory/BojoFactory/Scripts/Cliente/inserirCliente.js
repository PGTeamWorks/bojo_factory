<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> 029fb231ab0dd2c8d359d20a928cec4ad967009c
﻿$(document).ready(function() {
    olk();
});

function olk() {
    $("#btn-salvar-cliente").click(function () {
        var dados = $("#form-inserir-cliente").serialize();
        $.post("/Cliente/Inserir/", dados, function () {
<<<<<<< HEAD
=======
=======
﻿$(document).ready(function () {
    inserirCliente();
});

function inserirCliente() {
>>>>>>> 5b289786dfc4d3b6609d19beeedd2a4412834add
>>>>>>> 029fb231ab0dd2c8d359d20a928cec4ad967009c

        });

    });
}