<<<<<<< HEAD
﻿$(document).ready(function() {
    olk();
});

function olk() {
    $("#btn-salvar-cliente").click(function () {
        var dados = $("#form-inserir-cliente").serialize();
        $.post("/Cliente/Inserir/", dados, function () {
=======
﻿$(document).ready(function () {
    inserirCliente();
});

function inserirCliente() {
>>>>>>> 5b289786dfc4d3b6609d19beeedd2a4412834add

    var dados = $("#form-inserir-cliente").serialize();
    $.post("/Cliente/Inserir/", dados, function () {

    });
}