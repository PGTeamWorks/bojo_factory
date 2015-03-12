# Bojo Factory Project


* 1 placa => 8 bojos extatamente
* pedido de 20 peças => 24 peças são produzidas, pois não é fabricado pedido fracionado
* 10% da produção são perdidos devido a falhas no processo de fabricação
* O ideal é que seja produzido 10% a mais do que o cliente pediu

 Dessa forma, caso haja um pedido de 100 peças, deverão ser produzidas 110, das quais levando em consideração o número de bojos por placa, deverão ser fabricados 112 bojos que correspondem a 14 placas.
 
 1 placa => 1.2 metros de espuma
 1 placa => 0.4 kg de tecido
 
Cálculo de matéria-prima gasta para um pedido de **100** peças:

* **Quantidade com margem de perda:** 110
* **Quantidade de placas:** 14
* **Quantidade de tecido:** 5.6 Kg
* **Quantidade de espuma:** 16.8 m
* **Quantidade de bojos produzidos:** 112

*Números fictícios*

Considere que nesta fábrica de bojos há o seguinte estoque de matéria-prima

**Matéria-Prima** 	-	**Saldo em Estoque**

* Tecido Vermelho - 40 Kg
* Tecido Branco - 60 Kg
* Tecido Preto - 50 Kg
* Espuma - 600 metros

**Pedido 1** 	-	**Cliente:** Alan Turing

* 500 - Bojos vermelhos
* 300 - Bojos brancos

**Pedido 2** 	-	**Cliente:** Bill Gates

* 200 - Bojos vermelhos
* 200 - Bojos brancos
* 300 - Bojos pretos

**Pedido 3** 	-	**Cliente:** Steve Jobs

* 400 - Bojos preto
* 650 - Bojos brancos

### Objetivo
---

1. Fazer um programa que seja capaz de receber a quantidade de bojos pedidos, calcule e mostre a quantidade de tecido e espuma gasta.
2. Na situação apresentada não será possível fabricar todos os produtos pedidos, pois não há matéria-prima suficiente. Escolha qual pedido deverá ficar sem produzir, levando em conta de que é preferível entregar os pedidos com maior quantidade de produtos e que um pedido não pode ser entregue parcialmente. Calcule quanto de matéria-prima restará no estoque após a produção dos 2 pedidos selecionados.
