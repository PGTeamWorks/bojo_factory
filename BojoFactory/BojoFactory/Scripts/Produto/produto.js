$(document).ready(function () {
    listarProduto();
    //carregarMateriaPrima();
    exibirModalInserirCliente();
});

var produtoValido = true;

function exibirModalInserirCliente() {
    $("#btn-modal-inserir-produto").click(function () {
        $("#modal").load("/Produto/Inserir/").modal('show');
    });
}

function modalEditarProduto(id) {
    $("#modal").load("/Produto/Editar/" + id).modal('show');
}

function modalDetalharProduto(id) {
    $("#modal").load("/Produto/Detalhar/" + id).modal('show');
}

function modalExcluirProduto(id) {
    $("#modal").load("/Produto/Excluir/" + id).modal('show');
}

//function carregarMateriaPrima() {
//    var select = $('#slc-materia-prima');
//    var htmlOptions = '';

//    $.getJSON('/MateriaPrima/Listar/', function (data) {
//        $.each(data.materiasPrima, function (i, materiaPrima) {
//            htmlOptions += '<option value="' + materiaPrima.Id + '">' + materiaPrima.Descricao + '</option>';
//        });
//        select.append(htmlOptions);
//    });
//}

function listarProduto() {

    var tbProduto = $("#tb-produtos");
    var actionListar = '/Produto/Listar/';;

    $(function () {

        $.getJSON(actionListar, function (data) {
            $.each(data.produtos, function (i, item) {
                tbProduto.append(
                    '<tr id="' + item.Id + '">' +
                    '   <td>' + item.Descricao + '</td>' +
                    '   <td>' + item.Preco + '</td>' +
                    '   <td><label id="btn-modal-editar-produto" class="btn btn-success" onclick="modalEditarProduto(' + item.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-produto" class="btn btn-default" onclick="modalDetalharProduto(' + item.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-produto" class="btn btn-danger" onclick="modalExcluirProduto(' + item.Id + ');">Excluir</label></td>' +
                    '</tr>'
                );
            });
        });
    });
}

function inserirProduto() {

    var tbProduto = $("#tb-produtos");

    if (!validaProduto())
        return;

    //var idMateriaPrima = $('#slc-materia-prima option:selected').val();
    //var qntMateriaPrima = $('#quantidadeMp').val();
    //var objMp = new Object();
    //objMp.IdMateriaPrima = idMateriaPrima;
    //objMp.Quantidade = qntMateriaPrima;

    var dados = $("#form-inserir-produto").serialize();

    alert(dados);
    //alert(objMp);

    //$.post("/Produto/Inserir/", { produto: dados, formula: objMp }, function () {
    $.post("/Produto/Inserir/", dados, function () {
    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-produto')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            tbProduto.append(
               '<tr id="' + data.objInserido.Id + '">' +
                    '   <td>' + data.objInserido.Descricao + '</td>' +
                    '   <td>' + data.objInserido.Preco + '</td>' +
                    '   <td><label id="btn-modal-editar-produto" class="btn btn-success" onclick="modalEditarProduto(' + data.objInserido.Id + ');">Editar</label></td>' +
                    '   <td><label id="btn-modal-detalhar-produto" class="btn btn-default" onclick="modalDetalharProduto(' + data.objInserido.Id + ');">Detalhar</label></td>' +
                    '   <td><label id="btn-modal-excluir-produto" class="btn btn-danger" onclick="modalExcluirProduto(' + data.objInserido.Id + ');">Excluir</label></td>' +
               '</tr>'
            );
            $('#modal').modal('hide');
        }
    }
    ).fail(function (data) {
        // implementar erro não tratado
    });
}

function editarProduto(id) {

    if (!validaProduto())
        return;

    var dados = $("#form-inserir-produto").serialize();
    $.post("/Produto/Inserir/", dados, function () {

    }).done(function (data) {
        if (data.erro == true) {
            $('#alerta-produto')
                .html(' <div class="alert alert-dismissible alert-danger">' +
                    '           <button type="button" class="close" data-dismiss="alert">×</button>' +
                    data.Message +
                    '   <div>').delay(400).fadeIn(800);
        } else {
            $('#' + id).empty();
            $('#' + id).append(
                     '   <td>' + data.objInserido.Descricao + '</td>' +
                     '   <td>' + data.objInserido.Preco + '</td>' +
                     '   <td><label id="btn-modal-editar-produto" class="btn btn-success" onclick="modalEditarProduto(' + data.objInserido.Id + ');">Editar</label></td>' +
                     '   <td><label id="btn-modal-detalhar-produto" class="btn btn-default" onclick="modalDetalharProduto(' + data.objInserido.Id + ');">Detalhar</label></td>' +
                     '   <td><label id="btn-modal-excluir-produto" class="btn btn-danger" onclick="modalExcluirProduto(' + data.objInserido.Id + ');">Excluir</label></td>'

             );
            $('#modal').modal('hide');
        }
    }
    ).fail(function (data) {
        // implementar erro não tratado
    });
}

function excluirProduto(id) {
    $.post("/Produto/ExcluirPorId/" + id, function () {

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

function validaProduto() {

    if ($('#Descricao').val() != "") {
        $('#Descricao').parent().removeClass('has-error');
    } else {
        $('#Descricao').parent().addClass('has-error');
        produtoValido = false;
    }

    if ($('#Cor').val() != "") {
        $('#Cor').parent().removeClass('has-error');
    } else {
        $('#Cor').parent().addClass('has-error');
        produtoValido = false;
    }

    if ($('#Tamanho').val() != "") {
        $('#Tamanho').parent().removeClass('has-error');
    } else {
        $('#Tamanho').parent().addClass('has-error');
        produtoValido = false;
    }

    if ($('#Preco').val() != "") {
        $('#Preco').parent().removeClass('has-error');
    } else {
        $('#Preco').parent().addClass('has-error');
        produtoValido = false;
    }

    if ($('#SaldoEstoque').val() != "") {
        $('#SaldoEstoque').parent().removeClass('has-error');
    } else {
        $('#SaldoEstoque').parent().addClass('has-error');
        produtoValido = false;
    }

    if (($('#SaldoEstoque').val() != "") && ($('#Descricao').val() != "") && ($('#Preco').val() != "") && ($('#Tamanho').val() != "") && ($('#Cor').val() != ""))
        return true;
    else
        return false;
}

