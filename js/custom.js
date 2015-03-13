$(document).ready(function () {
    $('.numero').mask("99999999");

    var inputNumPedido = $('#numPedido');
    var inputNomeCliente = $('#nomeCliente');
    var inputBojoVermelho = $('#bojoVermelho');
    var inputBojoBranco = $('#bojoBranco');
    var inputBojoPreto = $('#bojoPreto');
    var btnGravar = $('#btn-gravar');

    var pedidoList = $('#pedidoList');

    var qtdeTecidoVermelho = $('#qtde_tecido_vermelho');
    var qtdeTecidoBranco = $('#qtde_tecido_branco');
    var qtdeTecidoPreto = $('#qtde_tecido_preto');
    var qtdeEspuma = $('#qtde_espuma');

    var tecidoVermelhoUtilizado = $('#tecido_vermelho_utilizado');
    var tecidoBrancoUtilizado = $('#tecido_branco_utilizado');
    var tecidoPretoUtilizado = $('#tecido_preto_utilizado');
    var espumaUtilizada = $('#espuma_utilizada');

    var estoque ={
        tecido_vermelho: 40,
        tecido_branco: 60,
        tecido_preto: 50,
        espuma: 600
    };

    qtdeTecidoVermelho.html(estoque.tecido_vermelho);
    qtdeTecidoBranco.html(estoque.tecido_branco);
    qtdeTecidoPreto.html(estoque.tecido_preto);
    qtdeEspuma.html(estoque.espuma);

    var pedido = {
        numPedido: 0,
        nomeCliente: '',
        bojoVermelho: 0,
        bojoBranco: 0,
        bojoPreto: 0
    };

    var materialGasto = {
        tecidoVermelho: 0,
        tecidoBranco: 0,
        tecidoPreto: 0,
        espuma: 0
    };

    btnGravar.click(function(e){
        e.preventDefault();

        pedido.numPedido = inputNumPedido.val();
        pedido.nomeCliente = inputNomeCliente.val();
        pedido.bojoVermelho = inputBojoVermelho.val();
        pedido.bojoBranco = inputBojoBranco.val();
        pedido.bojoPreto = inputBojoPreto.val();

        calculaPedido(pedido);
        calculaMaterial(pedido);
        $('html, body').animate({scrollTop: 300}, 300);
    });

    /* Funções */

    function calculaPedido(pedido) {

        tecidoVermelhoUtilizado.html(materialGasto.tecidoVermelho);
        tecidoBrancoUtilizado.html(materialGasto.tecidoBranco);
        tecidoPretoUtilizado.html(materialGasto.tecidoPreto);
        espumaUtilizada.html(materialGasto.espuma);

        var boxPedido = '<div class="list-group-item">'+
                            '<h4 class="list-group-item-heading page-header">Pedido Nº '+pedido.numPedido+', '+pedido.nomeCliente+'</h4>'+
                            '<dl class="dl-horizontal">'+
                            '<dt>Bojos vermelhos</dt>'+
                                '<dd>'+pedido.bojoVermelho+'</dd>'+
                            '<dt>Bojos Brancos</dt>'+
                                '<dd>'+pedido.bojoBranco+'</dd>'+
                            '<dt>Bojos Pretos</dt>'+
                                '<dd>'+pedido.bojoPreto+'</dd>'+
                            '</dl>'+
                        '</div>';
        pedidoList.append(boxPedido).hide().fadeIn(1000);
        $('form').trigger('reset');
    }

    function calculaMaterial(pedido) {
        var fabricado = (pedido.bojoVermelho * 1.1).toFixed();
        var resto = (fabricado % 8).toFixed();
        var dividido = (fabricado / 8).toFixed();
        console.log(dividido * 8);
        console.log('dividido: '+dividido+', resto: '+resto+', fabricado: '+fabricado);
    }

});