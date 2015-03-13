$(document).ready(function () {
    $('.numero').mask("99999999");

    var placa = 8;

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

    var divResultado = $('#resultado');

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
        calculaMaterial(pedido.bojoVermelho);
        $('html, body').animate({scrollTop: 300}, 300);
    });

    /* Funções */
    /**
     * Recebe um pedido
     * @param pedido
     */
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

    /**
     * Recebe uma quantidade do pedido de Bojo
     * @param qtdeBojoPedido
     * @returns {number} = Quantidade que será produzida
     * considerando que só podem ser produzidos múltiplos de 8
     * e a produção é sempre 10% maior para compensar falhas na produção
     */
    function calculaMaterial(qtdeBojoPedido) {
        // Acrescente 10% à quantidade solicidada
        var pedidoMais10p = (qtdeBojoPedido * 1.1).toFixed();
        // Divide por 8
        var dividido = (pedidoMais10p / placa).toFixed();

        //Se a quantidade solicitada não for um múltiplo de 8, então serão produzido mais bojos para evitar
        // que a prensa seja sub-utilizada.
        var quantidade = (qtdeBojoPedido % placa != 0 ? dividido * placa : (dividido * placa) + placa);

        /* Mensagem para debug */
        var resultado = 'Quantidade produzida: '+quantidade+
                        '. Pedido + 10%: '+pedidoMais10p+
                        '. '+pedidoMais10p+' dividido por '+placa+' = '+dividido+', sobra '+pedidoMais10p % placa;

        console.log(resultado);

        return quantidade;
    }

});