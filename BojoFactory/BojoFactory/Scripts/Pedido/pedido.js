$(document).ready(function () {
   carregaProdutos();
    carregaItensPedidos();
    confirmarPedido();
    carregarClientes();
});

function carregarClientes() {
    var select = $('#slc-clientes');
    var htmlOptions = '';
    $.getJSON('/Cliente/Listar', function (data) {
        $.each(data.clientes, function (i, cliente) {
            htmlOptions += '<option value="' + cliente.Id + '">' + cliente.Nome + '</option>';
        });
        select.append(htmlOptions);
    });
}

function carregaProdutos() {
    var select = $('#slc-produtos');
    var htmlOptions = '';
    $.getJSON('/Produto/Listar/', function (data) {
        $.each(data.produtos, function (i, item) {
            htmlOptions += '<option data-id="' + item.Id + '"value="' + item.Preco + '">' + item.Descricao + '</option>';
        });
        select.append(htmlOptions);
    });
};

function carregaItensPedidos() {
    $('#btn-inserir-produto-pedido').click(function () {

        var idCliente = $('#slc-clientes').val();

        var nomeProd = $('#slc-produtos option:selected').text();
        if (nomeProd == '')
            return null;

        var idProd = $('#slc-produtos option:selected').data('id');

        var qntProd = $('#qtdItens').val();
        if (qntProd == '')
            return null;
        var preco = $('#slc-produtos :selected').val();

        var index = $("#tb-itens-pedidos > tr").length;

        var htmlItens = $('#tb-itens-pedidos');
        htmlItens.append(
            '<tr>' +
                '<td>' + nomeProd + ' </td>' +
                '<td>' + qntProd + '</td>' +
                '<td> R$ ' + preco +
                    '<input type="hidden" name="[' + index + '].Id" value="' + idProd + '" />' +
                    '<input type="hidden" name="[' + index + '].Quantidade" value="' + qntProd + '" />' +
                    '<input type="hidden" name="[' + index + '].Valor" value="' + preco.replace('.', ',') + '" />' +
                    '<input type="hidden" name="[' + index + '].IdCliente"  value="' + idCliente + '" />' +
                '</td>' +
                '<td> R$ ' +(qntProd*preco).toFixed(2)+ '</td>'+
            '</tr>'

            );
    });
}

function confirmarPedido() {
    $('#btn-confirmar-pedido').click(function () {
        var form = $('#frm-pedidos');
        var action = form.attr('action');
        var serializedForm = form.serializeArray();
        $.post(action, serializedForm, function (data) {
            alert(data);
        });
    });
}
