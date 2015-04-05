$(document).ready(function () {
    listarClientes();
    exibirModalInserirCliente();
    inserirCliente();

});

var clienteValido = true;

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

    if (!validaCliente())
        return;

    var dados = $("#form-inserir-cliente").serialize();
    $.post("/Cliente/Inserir/", dados, function () {

    }).done(function() {
            console.log("Deu certo");
        }
    ).fail(function (data) {
        $('#diverror').html(data.responseText);
        console.log("Deu merda");
    });
}

function validaCliente() {

    if ($('#Nome').val() != "") {
        $('#Nome').parent().removeClass('has-error');
    } else {
        $('#Nome').parent().addClass('has-error');
        clienteValido = false;
    }

    if ($('#Cpf').val() != "") {
        $('#Cpf').parent().removeClass('has-error');
    } else {
        $('#Cpf').parent().addClass('has-error');
        clienteValido = false;
    }

    if ($('#DataNascimento').val() != "") {
        $('#DataNascimento').parent().removeClass('has-error');
    } else {
        $('#DataNascimento').parent().addClass('has-error');
        clienteValido = false;
    }

    if ($('#Telefone').val() != "") {
        $('#Telefone').parent().removeClass('has-error');
    } else {
        $('#Telefone').parent().addClass('has-error');
        clienteValido = false;
    }

    if ($('#Email').val() != "") {
        $('#Email').parent().removeClass('has-error');
    } else {
        $('#Email').parent().addClass('has-error');
        clienteValido = false;
    }

    if (($('#Nome').val() != "") && ($('#Cpf').val() != "") && ($('#DataNascimento').val() != "") && ($('#Telefone').val() != "") && ($('#Email').val() != ""))
        return true;
    else
        return false;
}

