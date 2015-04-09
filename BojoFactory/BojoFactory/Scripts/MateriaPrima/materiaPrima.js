$(document).ready(function () {
    listarMateriaPrima();
    exibirModalInserirMateriaPrima();
});

var materiaPrimaValida = true;

function exibirModalInserirMateriaPrima() {
    $("#btn-modal-inserir").click(function () {
        $("#modal").load("/MateriaPrima/Inserir/").modal('show');
    });
}

function modalEditarMateriaPrima(id) {
    $("#modal").load("/MateriaPrima/Editar/" + id).modal('show');
}

function modalDetalharMateriaPrima(id) {
    $("#modal").load("/MateriaPrima/Detalhar/" + id).modal('show');
}

function modalExcluirMateriaPrima(id) {
    $("#modal").load("/MateriaPrima/Excluir/" + id).modal('show');
}

function listarMateriaPrima() {

    var tbMateriaPrima = $("#tb-materia-prima");
    var actionListar = '/MateriaPrima/Listar/';;

    $(function () {

        $.getJSON(actionListar, function (data) {
            $.each(data.materiasPrima, function (i, item) {
                tbMateriaPrima.append(
                    '<tr id="' + item.Id + '">' +
                    '   <td>' + item.Descricao + '</td>' +
                    '   <td>' + item.PrecoCusto + '</td>' +
                    '   <td><label id="btn-modal-editar-materia-prima" class="btn btn-success" onclick="modalEditarMateriaPrima(' + item.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-materia-prima" class="btn btn-default" onclick="modalDetalharMateriaPrima(' + item.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-materia-prima" class="btn btn-danger" onclick="modalExcluirMateriaPrima(' + item.Id + ');">Excluir</label></td>' +
                    '</tr>'
                );
            });
        });
    });
}

function inserirMateriaPrima() {

    var tbMateriaPrima = $("#tb-materia-prima");

    if (!validaMateriaPrima())
        return;

    var dados = $("#form-inserir-materia-prima").serialize();
    $.post("/MateriaPrima/Inserir/", dados, function () {

    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-materia-prima')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            tbMateriaPrima.append(
               '<tr id="' + data.objInserido.Id + '">' +
                    '   <td>' + data.objInserido.Descricao + '</td>' +
                    '   <td>' + data.objInserido.PrecoCusto + '</td>' +
                    '   <td><label id="btn-modal-editar-materia-prima" class="btn btn-success" onclick="modalEditarMateriaPrima(' + data.objInserido.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-materia-prima" class="btn btn-default" onclick="modalDetalharMateriaPrima(' + data.objInserido.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-materia-prima" class="btn btn-danger" onclick="modalExcluirMateriaPrima(' + data.objInserido.Id + ');">Excluir</label></td>' +
                    '</tr>'
            );
            $('#modal').modal('hide');
        }
    }
    ).fail(function (data) {
        // implementar erro não tratado
    });
}

function editarMateriaPrima(id) {

    if (!validaMateriaPrima())
        return;

    var dados = $("#form-inserir-materia-prima").serialize();
    $.post("/MateriaPrima/Inserir/", dados, function () {

    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-materia-prima')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            $('#' + id).empty();
            $('#' + id).append(
                     '   <td>' + data.objInserido.Descricao + '</td>' +
                     '   <td>' + data.objInserido.PrecoCusto + '</td>' +
                     '   <td><label id="btn-modal-editar-materia-prima" class="btn btn-success" onclick="modalEditarMateriaPrima(' + data.objInserido.Id + ');">Editar</label></td>' +
                     '   <td><label id="btn-modal-detalhar-materia-prima" class="btn btn-default" onclick="modalDetalharMateriaPrima(' + data.objInserido.Id + ');">Detalhar</label></td>' +
                     '   <td><label id="btn-modal-excluir-materia-prima" class="btn btn-danger" onclick="modalExcluirMateriaPrima(' + data.objInserido.Id + ');">Excluir</label></td>'

             );
            $('#modal').modal('hide');
        }
    }
    ).fail(function (data) {
        // implementar erro não tratado
    });
}

function excluirMateriaPrima(id) {
    $.post("/MateriaPrima/ExcluirPorId/" + id, function () {

    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-produto')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            $('#' + id).empty();
            $('#modal').modal('hide');
        }
    });

}

function validaMateriaPrima() {

    if ($('#Descricao').val() != "") {
        $('#Descricao').parent().removeClass('has-error');
    } else {
        $('#Descricao').parent().addClass('has-error');
        materiaPrimaValida = false;
    }

    if ($('#PrecoCusto').val() != "") {
        $('#PrecoCusto').parent().removeClass('has-error');
    } else {
        $('#PrecoCusto').parent().addClass('has-error');
        materiaPrimaValida = false;
    }

    if ($('#SaldoEstoque').val() != "") {
        $('#SaldoEstoque').parent().removeClass('has-error');
    } else {
        $('#SaldoEstoque').parent().addClass('has-error');
        materiaPrimaValida = false;
    }

    if (($('#SaldoEstoque').val() != "") && ($('#Descricao').val() != "") && ($('#PrecoCusto').val() != ""))
        return true;
    else
        return false;
}

