$(document).ready(function () {
    listarClientes();
    exibirModalInserirCliente();
    inserirCliente();
});

function exibirModalInserirCliente() {
    $("#btn-modal-inserir").click(function () {
        $("#modal").load("/Cliente/Inserir/").modal('show');
    });
}

function listarClientes() {

    var tbClientes = $("#tb-clientes");
    var actionListar = '/Cliente/Listar/';
    var actionEditar = '/Cliente/Editar/';
    var actionExcluir = '/Cliente/Excluir';
    var actionDetalhes = '/Cliente/Detalhes';

    $(function () {

        $.getJSON(actionListar, function (data) {
            $.each(data.clientes, function (i, item) {
                tbClientes.append(
                    "<tr>" +
                    '   <td>               </td>' +
                    "   <td>" + item.Id + "</td>" +
                    "   <td>" + item.Nome + "</td>" +
                    "   <td>" + item.Email + "</td>" +
                    '   <td><a href="' + actionEditar + item.Id + '"><span class="" /></a></td>' +
                    "</tr>"
                );
            });
        });
    });
}

function inserirCliente() {
    var dados = $("#form-inserir-cliente").serialize();
    $.post("/Cliente/Inserir/", dados, function () {

    });
}

//$.ajax({
//    url: "/Cliente/Listar/",
//    type: 'GET',
//    data: '',
//    dataType: 'json',
//    contentType: "application/json; charset=utf-8",
//    async: true,
//    success: function (data) {
//        $.each(data.clientes, function (i, item) {
//            tbClientes.append(
//                "<tr>" +
//                "   <td>" + item.Id + "</td>" +
//                "   <td>" + item.Nome + "</td>" +
//                "   <td>" + item.Email + "</td>" +
//                '   <td><a href="' + actionEditar + item.Id + '"><span class="" /></a></td>' +
//                "</tr>"
//            );
//        });
//    },
//});
