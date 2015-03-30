$(document).ready(function () {
    listarClientes();
    exibirModalInserirCliente();
    inserirCliente();
});

function listarClientes() {

    var tbClientes = $("#tb-clientes");
    var actionEdit = '/Cliente/Editar/';

    $(function () {
        $.ajax({
            url: "/Cliente/Listar/",
            type: 'GET',
            data: '',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            async: true,
            success: function (data) {
                $.each(data.clientes, function (i, item) {
                    tbClientes.append(
                        "<tr>" +
                        "   <td>" + item.Id + "</td>" +
                        "   <td>" + item.Nome + "</td>" +
                        "   <td>" + item.Email + "</td>" +
                        '   <td><a href="' + actionEdit + item.Id + '"><span class="glyphicon glyphicon-pencil" /></a></td>' +
                        "</tr>"
                    );
                });
            },
        });
    });
}

function inserirCliente() {

    $("#btn-gravar-cliente").click(function () {

        var dados = $("#form-inserir-cliente").serialize();

        $.post("/Cliente/Inserir/", dados, function() {
            $("#tb-clientes").load();
        });

    });
}

function exibirModalInserirCliente() {
    $("#btn-inserir").click(function () {
        $("#modal").load("/Cliente/Inserir/").modal('show');
    });
}