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

    var estoque = {
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
        bojoPreto: 0,
        valido: false
    };

    var materialGasto = {
        tecidoVermelho: 0,
        tecidoBranco: 0,
        tecidoPreto: 0,
        espuma: 0
    };

    var somaTecido = 0;


    btnGravar.click(function (e) {
        e.preventDefault();

        pedido.numPedido = inputNumPedido.val();
        pedido.nomeCliente = inputNomeCliente.val();
        pedido.bojoVermelho = inputBojoVermelho.val();
        pedido.bojoBranco = inputBojoBranco.val();
        pedido.bojoPreto = inputBojoPreto.val();

        verificaPedidoENome(pedido);
        if (!pedido.valido)
            return;

        verificaPedido(pedido);
        if (!pedido.valido) {

            $('.modal').modal('show');

            return;
        }

        calculaGasto(pedido);
        if (!pedido.valido)
            return;

        showPedido(pedido);
        showMaterialGasto(materialGasto);

        $('html, body').animate({scrollTop: 1000}, 1000);
    });

    /* Funções */

    /**
     * @Param Pedido
     * Verifica se tem pe menos um pedido de bojo
     * para efetuar o pedido... se não tiver mostra linhas vermelhas para
     * usuário saber que está inválido e não pode fazer o pedido
     */
    function verificaPedidoENome(pedido) {

        if (pedido.nomeCliente != "")
            $('#nomeCliente').parent().removeClass('has-error');
        else
            $('#nomeCliente').parent().addClass('has-error');

        if (pedido.numPedido != "")
            $('#numPedido').parent().removeClass('has-error');
        else
            $('#numPedido').parent().addClass('has-error');

        if (pedido.nomeCliente == "" || pedido.numPedido == "")
            pedido.valido = false;
        else
            pedido.valido = true;

    }

    /**
     * @param pedido
     * Se tiver pelo menos um tipo de bojo no pedido,
     *  o Pedido poderáser realizado, caso contrário retornará um modal
     *  para o usuário preenche-lo
     */
    function verificaPedido(pedido) {

        pedido.valido = false;

        if (pedido.bojoVermelho != "") {
            pedido.valido = true;
        }
        if (pedido.bojoBranco != "") {
            pedido.valido = true;
        }
        if (pedido.bojoPreto != "") {
            pedido.valido = true;
        }
    }

    /**
     * @param pedido
     * Mostra um pedio feito pelo usuário
     */

    function showPedido(pedido) {

        var boxPedido = '<div class="list-group-item">' +
            '<h4 class="list-group-item-heading page-header">Pedido Nº ' + pedido.numPedido + ', ' + pedido.nomeCliente + '</h4>' +
            '<dl class="dl-horizontal">' +
            '<dt>Bojos vermelhos</dt>' +
            '<dd>' + pedido.bojoVermelho + '</dd>' +
            '<dt>Bojos Brancos</dt>' +
            '<dd>' + pedido.bojoBranco + '</dd>' +
            '<dt>Bojos Pretos</dt>' +
            '<dd>' + pedido.bojoPreto + '</dd>' +
            '</dl>' +
            '</div>';

        pedidoList.append(boxPedido).hide().fadeIn(1000);

        $('form').trigger('reset');
    }

    /**
     * @param materialGasto
     * Mostra o Material gasto no Pedido feito pelo usário
     */
    function showMaterialGasto(materialGasto) {

        var boxMaterialGasto = '<div class="list-group-item">' +
            '<h4 class="list-group-item-heading page-header">Pedido Nº ' + pedido.numPedido + ', ' + pedido.nomeCliente + '</h4>' +
            '<dl class="dl-horizontal mg-zero">' +
            '<dt>Tecido vermelhos</dt>' +
            '<dd>' + materialGasto.tecidoVermelho + '</dd>' +
            '<dt>Tecido Brancos</dt>' +
            '<dd>' + materialGasto.tecidoBranco + '</dd>' +
            '<dt>Tecido Pretos</dt>' +
            '<dd>' + materialGasto.tecidoPreto + '</dd>' +
            '<dt>Espuma</dt>' +
            '<dd>' + materialGasto.espuma + '</dd>' +
            '</dl>' +
            '</div>';

        materialGastoList.append(boxMaterialGasto).hide().fadeIn(1000);

        $('form').trigger('reset');
    }

    /**
     *
     * @param pedido
     * Calcula a quantidade de materiais gastos para fabricar um pedido
     * gasto de tecido vermelho, tecido branco, tecido preto e espuma
     */

    /* calcula-se o gasto necessario para que possa se realizar o pedido de um cliente e salva em um loq no console */
    function calculaGasto(pedido) {

        pedido.valido = true;

        materialGasto.tecidoVermelho = calculaTecido(calculaMaterial(pedido.bojoVermelho));
        materialGasto.tecidoBranco = calculaTecido(calculaMaterial(pedido.bojoBranco));
        materialGasto.tecidoPreto = calculaTecido(calculaMaterial(pedido.bojoPreto));
        materialGasto.espuma = calculaEspuma(somaTecido);

        if (verifcaEstoque(materialGasto.tecidoVermelho, materialGasto.tecidoBranco, materialGasto.tecidoPreto, materialGasto.espuma) == false) {
            pedido.valido = false;
            return;
        }

        qtdeTecidoVermelho.html((estoque.tecido_vermelho -= materialGasto.tecidoVermelho).toFixed(1));
        qtdeTecidoBranco.html((estoque.tecido_branco -= materialGasto.tecidoBranco).toFixed(1));
        qtdeTecidoPreto.html((estoque.tecido_preto -= materialGasto.tecidoPreto).toFixed(1));
        qtdeEspuma.html((estoque.espuma -= materialGasto.espuma).toFixed(1));

        console.log('Materias gastos:\nTecido Vermelho: ' + materialGasto.tecidoVermelho + 'm' +
        '\nTecido Branco: ' + materialGasto.tecidoBranco + 'm' +
        '\nTecido Preto: ' + materialGasto.tecidoPreto + 'm' +
        '\nEspuma: ' + materialGasto.espuma + 'kg');
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

        if (qtdeBojoPedido == "" || qtdeBojoPedido == 0 || qtdeBojoPedido == null) {
            return 0;
        }

        // Acrescente 10% à quantidade solicidada
        var pedidoMais10p = (qtdeBojoPedido * 1.1).toFixed();

        // Divide por 8
        var dividido = (pedidoMais10p / placa).toFixed();

        //Se a quantidade solicitada não for um múltiplo de 8, então serão produzido mais bojos para evitar
        // que a prensa seja sub-utilizada.
        quantidade = (qtdeBojoPedido % placa != 0 ? dividido * placa : (dividido * placa) + placa);

        /* Mensagem para debug */
        var resultado = 'Quantidade produzida: ' + quantidade +
            '. Pedido + 10%: ' + pedidoMais10p +
            '. ' + pedidoMais10p + ' dividido por ' + placa + ' = ' + dividido + ', sobra ' + pedidoMais10p % placa;

        somaTecido += quantidade;

        console.log(resultado);

        return quantidade;
    }

    /* Calcula a quantidade de tecido total para o calculo da espuma que de ver feito com toda a fabricação
     * não apenas com o pedido feito pelo cliente */
    function calculaTecido(qntTecido) {
        qntTecido = qntTecido * 0.05;
        return qntTecido.toFixed(1);
    }

    /* Calculo da espuma e subtração do estoque total de espuma*/
    function calculaEspuma(qntEspuma) {
        somaTecido = 0;
        qntEspuma = qntEspuma * 0.15;
        return qntEspuma.toFixed(1);
    }

    /* verifica estoque
     * se nao tiver o estoque é adicionada classes para avisar o usuário
     * que não possui estoque suficiente para pedido */
    function verifcaEstoque(tv, tb, tp, espuma) {

        var temEstoque = true;

        if (tv > estoque.tecido_vermelho) {

            $(".tecido-vermelho").addClass('changeEstoqueInsuficiente');
            $(".tecido-vermelho").addClass('estoqueInsuficiente');
            temEstoque = false;
        }
        if (tb > estoque.tecido_branco) {
            $(".tecido-branco").addClass('changeEstoqueInsuficiente');
            $(".tecido-branco").addClass('estoqueInsuficiente');
            temEstoque = false;
        }
        if (tp > estoque.tecido_preto) {

            $(".tecido-preto").addClass('changeEstoqueInsuficiente');
            $(".tecido-preto").addClass('estoqueInsuficiente');
            temEstoque = false;
        }

        if (espuma > estoque.espuma) {
            $(".espuma").addClass('changeEstoqueInsuficiente');
            $(".espuma").addClass('estoqueInsuficiente');
            temEstoque = false;
        }

        if (temEstoque == true) {

            $(".tecido-vermelho").removeClass('estoqueInsuficiente');
            $(".tecido-vermelho").removeClass('changeEstoqueInsuficiente');
            $(".tecido-branco").removeClass('estoqueInsuficiente');
            $(".tecido-branco").removeClass('changeEstoqueInsuficiente');
            $(".tecido-preto").removeClass('estoqueInsuficiente');
            $(".tecido-preto").removeClass('changeEstoqueInsuficiente');
            $(".espuma").removeClass('estoqueInsuficiente');
            $(".espuma").removeClass('changeEstoqueInsuficiente');
        }

        return temEstoque;
    }

});

//$('#nomeCliente').parent().addClass('has-error');