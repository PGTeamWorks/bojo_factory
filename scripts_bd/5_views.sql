DROP VIEW IF EXISTS vs_cliente;
CREATE OR REPLACE VIEW vs_cliente AS
SELECT
	id_cliente,
	nome,
	cpf,
	email,
	telefone,
	to_char(data_nascimento, 'DD/MM/YYYY') AS data_nascimento,
	extract(YEAR FROM age(data_nascimento))  AS idade_cliente,
	usuario_criador,
	to_char(data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	usuario_atualizador,
	to_char(data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_cliente;

DROP VIEW IF EXISTS vs_pedido;
CREATE OR REPLACE VIEW vs_pedido AS
SELECT
	p.id_pedido,
	to_char(p.data_pedido, 'DD/MM/YYYY HH24:MI:SS') AS data_pedido,
	to_char(p.valor_total, '"R$" 999D99') AS valor_total,
	c.id_cliente,
	c.nome AS nome_cliente,
	c.cpf AS cpf_cliente,
	c.email AS email_cliente,
	c.telefone AS telefone_cliente,
	to_char(c.data_nascimento, 'DD/MM/YYYY') AS data_nascimento_cliente,
	extract(YEAR FROM age(c.data_nascimento))  AS idade_cliente,
	p.usuario_criador,
	to_char(p.data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	p.usuario_atualizador,
	to_char(p.data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_pedido p
INNER JOIN tb_cliente c
	ON (p.id_cliente = c.id_cliente);

DROP VIEW IF EXISTS vs_produto;
CREATE OR REPLACE VIEW vs_produto AS
SELECT 
	id_produto,
	descricao,
	tamanho,
	cor,
	to_char(preco, '"R$" 999D99') AS preco,
	saldo_estoque,
	usuario_criador,
	to_char(data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	usuario_atualizador,
	to_char(data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_produto;	

DROP VIEW IF EXISTS vs_materia_prima;
CREATE OR REPLACE VIEW vs_materia_prima AS
SELECT
	id_materia_prima,
	descricao,
	saldo_estoque,
	to_char(preco_custo, '"R$" 999D99') AS preco_custo,
	usuario_criador,
	to_char(data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	usuario_atualizador,
	to_char(data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_materia_prima;

DROP VIEW IF EXISTS vs_pedido_produto;
CREATE OR REPLACE VIEW vs_pedido_produto AS
SELECT
	pp.id_pedido_produto,
	pp.id_pedido,
	to_char(pedido.data_pedido, 'DD/MM/YYYY') AS data_pedido,
	to_char(pedido.valor_total, '"R$" 999D99') AS valor_total_pedido,
	pedido.id_cliente,
	cliente.nome AS nome_cliente,
	cliente.cpf AS cpf_cliente,
	cliente.email AS email_cliente,
	cliente.telefone AS telefone_cliente,
	to_char(cliente.data_nascimento, 'DD/MM/YYYY') AS data_nascimento_cliente,
	extract(YEAR FROM age(cliente.data_nascimento))  AS idade_cliente,
	pp.id_produto,
	produto.descricao AS descricao_produto,
	produto.tamanho AS tamanho_produto,
	produto.cor AS cor_produto,
	to_char(produto.preco, '"R$" 999D99') AS preco_produto,
	produto.saldo_estoque AS saldo_estoque_produto,
	pp.quantidade,
	to_char(pp.valor_unitario, '"R$" 999D99') AS valor_unitario,
	pp.usuario_criador,
	to_char(pp.data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	pp.usuario_atualizador,
	to_char(pp.data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_pedido_produto pp
INNER JOIN tb_pedido pedido
	ON (pp.id_pedido = pedido.id_pedido)
INNER JOIN tb_produto produto
	ON (pp.id_produto = produto.id_produto)
INNER JOIN tb_cliente cliente
	ON (pedido.id_cliente = cliente.id_cliente);

DROP VIEW IF EXISTS vs_formula;
CREATE OR REPLACE VIEW vs_formula AS
SELECT
	f.id_formula,
	f.id_produto,
	p.descricao AS descricao_produto,
	p.tamanho AS tamanho_produto,
	p.cor AS cor_produto,
	to_char(p.preco, '"R$" 999D99') AS preco_produto,
	p.saldo_estoque AS saldo_estoque_produto,
	f.id_materia_prima,
	m.descricao AS descricao_materia_prima,
	m.saldo_estoque AS saldo_estoque_materia_prima,
	to_char(m.preco_custo, '"R$" 999D99') AS preco_custo_materia_prima,
	f.quantidade,
	f.usuario_criador,
	to_char(f.data_criacao, 'DD/MM/YYYY HH24:MI:SS') AS data_criacao,
	f.usuario_atualizador,
	to_char(f.data_atualizacao, 'DD/MM/YYYY HH24:MI:SS') AS	data_atualizacao
FROM tb_formula f
INNER JOIN tb_produto p
	ON (f.id_produto = p.id_produto)
INNER JOIN tb_materia_prima m
	ON (m.id_materia_prima = f.id_materia_prima);


-- Usando as views

-- SELECT * FROM vs_cliente;

-- SELECT * FROM vs_pedido;

-- SELECT * FROM vs_produto;

-- SELECT * FROM vs_materia_prima;

-- SELECT * FROM vs_pedido_produto;

-- SELECT * FROM vs_formula;