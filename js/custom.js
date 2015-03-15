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
    var materialGastoList = $('#materialGastoList');


    var qtdeTecidoVermelho = $('#qtde_tecido_vermelho');
    var qtdeTecidoBranco = $('#qtde_tecido_branco');
    var qtdeTecidoPreto = $('#qtde_tecido_preto');
    var qtdeEspuma = $('#qtde_espuma');

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

    var somaTecido = 0;

    btnGravar.click(function(e){
        materialGasto.espuma = 0;
        e.preventDefault();

        pedido.numPedido = inputNumPedido.val();
        pedido.nomeCliente = inputNomeCliente.val();
        pedido.bojoVermelho = inputBojoVermelho.val();
        pedido.bojoBranco = inputBojoBranco.val();
        pedido.bojoPreto = inputBojoPreto.val();

        calculaGasto(pedido);
        showPedido(pedido);
        showMaterialGasto(materialGasto);

       $('html, body').animate({scrollTop: 300}, 300);
    });

    /* Funções */
    /**
     * @param pedido
     * Mostra um pedio feito pelo usuário
     */
    function showPedido(pedido) {

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
     * @param materialGasto
     * Mostra o Material gasto no pedido feito pelo usário
     */
    function showMaterialGasto(materialGasto){

        var boxMaterialGasto = '<div class="list-group-item">'+
            '<h4 class="list-group-item-heading page-header">Pedido Nº '+pedido.numPedido+', '+pedido.nomeCliente+'</h4>'+
            '<dl class="dl-horizontal mg-zero">'+
            '<dt>Tecido vermelhos</dt>'+
            '<dd>'+materialGasto.tecidoVermelho+'</dd>'+
            '<dt>Tecido Brancos</dt>'+
            '<dd>'+materialGasto.tecidoBranco+'</dd>'+
            '<dt>Tecido Pretos</dt>'+
            '<dd>'+materialGasto.tecidoPreto+'</dd>'+
            '<dt>Espuma</dt>'+
            '<dd>'+materialGasto.espuma+'</dd>'+
            '</dl>'+
            '</div>';

        materialGastoList.append(boxMaterialGasto).hide().fadeIn(1000);
    }

    /**
     *
     * @param pedido
     * Calcula a quantidade de materiais gastos para fabricar um pedido
     * gasto de tecido vermelho, tecido branco, tecido preto e espuma
     */
   function calculaGasto(pedido){

       var tv = calculaTecido(calculaMaterial(pedido.bojoVermelho));
       var tb = calculaTecido(calculaMaterial(pedido.bojoBranco));
       var tp = calculaTecido(calculaMaterial(pedido.bojoPreto));

       materialGasto.tecidoVermelho = tv;
       materialGasto.tecidoBranco = tb
       materialGasto.tecidoPreto = tp


       qtdeTecidoVermelho.html((estoque.tecido_vermelho -= tv).toFixed(1));
       qtdeTecidoBranco.html((estoque.tecido_branco -= tb).toFixed(1));
       qtdeTecidoPreto.html((estoque.tecido_preto -= tp).toFixed(1));

       materialGasto.espuma = calculaEspuma(somaTecido);

       console.log('Materias gasto: \n Tecido Vermelho: '+tv+'m'+
                    '\n Tecido Branco: '+tb+'m'+
                    '\n Tecido Preto: '+tp+'m'+
                    '\n Espuma: '+materialGasto.espuma);
    }

    /**
     * Recebe uma quantidade do pedido de Bojo
     * @param qtdeBojoPedido
     * @returns {number} = Quantidade que será produzida
     * considerando que só podem ser produzidos múltiplos de 8
     * e a produção é sempre 10% maior para compensar falhas na produção
     */
    function calculaMaterial(qtdeBojoPedido) {

        var quantidade = 0;

        if(qtdeBojoPedido == "" || qtdeBojoPedido == 0 || qtdeBojoPedido == null)
            return 0;

        // Acrescente 10% à quantidade solicidada
        var pedidoMais10p = (qtdeBojoPedido * 1.1).toFixed();

        // Divide por 8
        var dividido = (pedidoMais10p / placa).toFixed();

        //Se a quantidade solicitada não for um múltiplo de 8, então serão produzido mais bojos para evitar
        // que a prensa seja sub-utilizada.
        quantidade = (qtdeBojoPedido % placa != 0 ? dividido * placa : (dividido * placa) + placa);

        /* Mensagem para debug */
        var resultado = 'Quantidade produzida: '+quantidade+
            '. Pedido + 10%: '+pedidoMais10p+
            '. '+pedidoMais10p+' dividido por '+placa+' = '+dividido+', sobra '+pedidoMais10p % placa;

        somaTecido += quantidade;

        console.log(resultado);

        return quantidade;
    }

    function calculaTecido(qntTecido){

        qntTecido = qntTecido * 0.05;
        return qntTecido.toFixed(1);
    }

    function calculaEspuma(qntEspuma){

        somaTecido = 0;

        qntEspuma = qntEspuma * 0.15;

        qtdeEspuma.html((estoque.espuma -= qntEspuma).toFixed(1));

        return qntEspuma.toFixed(1);
    }

});