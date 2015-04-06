$(document).ready(function () {
    listarClientes();
    exibirModalInserirCliente();
});

var clienteValido = true;

function exibirModalInserirCliente() {
    $("#btn-modal-inserir").click(function () {
        $("#modal").load("/Cliente/Inserir/").modal('show');
    });
}

function modalEditarCliente(id) {
    $("#modal").load("/Cliente/Editar/" + id).modal('show');
}

function modalDetalharCliente(id) {
    $("#modal").load("/Cliente/Detalhar/" + id).modal('show');
}

function modalExcluirCliente(id) {
    $("#modal").load("/Cliente/Excluir/" + id).modal('show');
}

function listarClientes() {

    var tbClientes = $("#tb-clientes");
    var actionListar = '/Cliente/Listar/';;

    $(function () {

        $.getJSON(actionListar, function (data) {
            $.each(data.clientes, function (i, item) {
                tbClientes.append(
                    "<tr>" +
                    '   <td>               </td>' +
                    '   <td>' + item.Id + '</td>' +
                    '   <td>' + item.Nome + '</td>' +
                    '   <td>' + item.Email + '</td>' +
                    '   <td><label id="btn-modal-editar-cliente" class="btn btn-success" onclick="modalEditarCliente(' + item.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-cliente" class="btn btn-default" onclick="modalDetalharCliente(' + item.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-cliente" class="btn btn-danger" onclick="modalExcluirCliente(' + item.Id + ');">Excluir</label></td>' +
                    '   </tr>'
                );
            });
        });
    });
}

function inserirCliente() {

    var tbClientes = $("#tb-clientes");

    if (!validaCliente())
        return;

    var dados = $("#form-inserir-cliente").serialize();
    $.post("/Cliente/Inserir/", dados, function () {

    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-cliente')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            tbClientes.append(
               "<tr>" +
                    '   <td>               </td>' +
                    '   <td>' + data.objetoInserido.Id + '</td>' +
                    '   <td>' + data.objetoInserido.Nome + '</td>' +
                    '   <td>' + data.objetoInserido.Email + '</td>' +
                    '   <td><label id="btn-modal-editar-cliente" class="btn btn-success" onclick="modalEditarCliente(' + data.objetoInserido.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-cliente" class="btn btn-default" onclick="modalDetalharCliente(' + data.objetoInserido.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-cliente" class="btn btn-danger" onclick="modalExcluirCliente(' + data.objetoInserido.Id + ');">Exluir</label></td>' +
                    '   </tr>'
            );
            $('#modal').modal('hide');
            location.reload();
        }
    }
    ).fail(function (data) {
        // implementar erro não tratado
    });
}

function excluirCliente(id) {
    $.post("/Cliente/ExcluirPorId/" + id, function() {

    }).done(function(data) {
        console.log(data.message);
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

