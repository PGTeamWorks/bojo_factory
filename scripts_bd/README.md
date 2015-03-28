# Scripts para o Banco de Dados
---
#### PostgreSQL v9.4.1
---

###Criando o usuário
```sql
CREATE USER bojofactory
WITH
SUPERUSER
ENCRYPTED PASSWORD 'bojofactory'
```

###Criando o banco
```sql
CREATE DATABASE bojo_factory
TEMPLATE = TEMPLATE0
ENCODING 'UTF-8'
CONNECTION LIMIT 10;
```

### Ordem de execução dos Scripts
Após conectar ao banco **bojo_factory** executar os scripts:

* 1_tabelas.sql
* 2_funcoes.sql
* 3_tabelas_de_log.sql
* 4_triggers.sql
* 5_views.sql

##Como usar as funções DML

O primeiro parâmetro sempre será a chave primária e deve ser NULL sempre que o parâmetro *operacao* for 'I' (**INSERT**).

O parâmetro *operacao* pode ser uma das seguintes opções:

* I para INSERT
* U para UPDATE
* D para DELETE

Ao efetuar operacao 'U' (**UPDATE**), todos os parâmetros devem ser passados, mesmo os que não tiveram alteração. Para esta operação a chave primária deverá, obrigatóriamente, ser informada.

Ao efetuar operacao 'D' (**DELETE**), apenas a chave primária e a operação são obrigatórios, os outros parâmetros podem se informados como **NULL**.

> É importante lembrar que, a quantidade parâmetros deve ser respeitada sempre, ou seja, se uma função usa 5 parãmetros, ela sempre deve ser executada com 5 parâmetros.

---
####Nota: Todas as funções DML retornam o registro inserido, atualizado ou deletado.
---
##Exemplos de uso com operação 'I' (INSERT)

#### tb_cliente
```sql
SELECT * FROM fn_cliente(
	NULL, -- id_cliente (int)
	'Bruna de Alencar Silva', -- nome (string)
	'000.000.000-10', -- cpf (string)
	'bruna@gmail.com', -- email (string)
	'(16) 98137-7798', -- telefone (string)
	'23/11/1986', -- data_nascimento (date)
	'I' -- operacao (string)
);
```
#### tb_pedido
```sql
SELECT * FROM fn_pedido(
	NULL, -- id_pedido (int)
	'18/03/2015 20:40', -- data_pedido (date) a parte da hora é opcional
	350.00, -- valor_total (double)
	16, -- id_cliente (int)
	'I' -- operacao (string)
);
```
#### tb_produto
```sql
SELECT * FROM fn_produto(
	NULL, -- id_produto (int)
	'Bojo', -- descricao (string)
	43, -- tamanho (double)
	'preto', -- cor (string)
	20.7, -- preco (double)
	600, -- saldo_estoque (double)
	'I' -- operacao (string)
);
```
#### tb_materia_prima
```sql
SELECT * FROM fn_materia_prima(
	NULL, -- id_materia_prima (int)
	'Tecido Preto', -- descricao (string)
	456, -- saldo_estoque (double)
	60.55, -- preco_custo (double)
	'I' -- operacao (string)
);
```
#### tb_pedido_produto
```sql
SELECT * FROM fn_pedido_produto(
	NULL, -- id_pedido_produto (int)
	2, -- id_pedido (int)
	2, -- id_produto (int)
	50, -- quantidade (double)
	7.90, -- valor_unitario (double)
	'I' -- operacao (string)
);
```
#### tb_formula
```sql
SELECT * FROM fn_formula(
	NULL, -- id_formula (int)
	2, -- id_produto (int)
	4, -- id_materia_prima (int)
	200, -- quantidade (double)
	'I' -- operacao (string)
);
```
