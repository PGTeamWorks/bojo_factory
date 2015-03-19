/*
CREATE DATABASE bojo_factory
TEMPLATE = TEMPLATE0
ENCODING 'UTF-8'
CONNECTION LIMIT 10;
*/

DROP TABLE IF EXISTS tb_cliente CASCADE;
CREATE TABLE tb_cliente(
	id_cliente serial,
	nome varchar(60) CONSTRAINT nn_tb_cliente_nome NOT NULL,
	cpf varchar(14),
	email varchar(60),
	telefone varchar(15),
	data_nascimento date,
	usuario_criador varchar(63) CONSTRAINT nn_tb_cliente_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_cliente_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_cliente_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_cliente_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_cliente_id_cliente PRIMARY KEY (id_cliente),
	CONSTRAINT uq_tb_cliente_cpf UNIQUE (cpf),
	CONSTRAINT uq_tb_cliente_email UNIQUE (email)
);

DROP TABLE IF EXISTS tb_pedido CASCADE;
CREATE TABLE tb_pedido(
	id_pedido serial,
	data_pedido timestamp,
	valor_total numeric(5,2),
	id_cliente integer,
	usuario_criador varchar(63) CONSTRAINT nn_tb_pedido_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_pedido_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_pedido_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_pedido_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_pedido_id_pedido PRIMARY KEY (id_pedido),
	CONSTRAINT fk_tb_pedido_id_cliente FOREIGN KEY (id_cliente)
		REFERENCES tb_cliente(id_cliente)
);

DROP TABLE IF EXISTS tb_produto CASCADE;
CREATE TABLE tb_produto(
	id_produto serial,
	descricao varchar(60),
	tamanho numeric(5,2),
	cor varchar(20),
	preco numeric(5,2),
	saldo_estoque numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_produto_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_produto_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_produto_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_produto_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_produto_id_produto PRIMARY KEY (id_produto)
);

DROP TABLE IF EXISTS tb_materia_prima CASCADE;
CREATE TABLE tb_materia_prima(
	id_materia_prima serial,
	descricao varchar(60),
	saldo_estoque numeric(5,2),
	preco_custo numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_materia_prima_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_materia_prima_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_materia_prima_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_materia_prima_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_materia_prima_id_materia_prima PRIMARY KEY (id_materia_prima)
);

DROP TABLE IF EXISTS tb_pedido_produto CASCADE;
CREATE TABLE tb_pedido_produto(
	id_pedido_produto serial,
	id_pedido integer,
	id_produto integer,
	quantidade numeric(5,2),
	valor_unitario numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_pedido_produto_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_pedido_produto_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_pedido_produto_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_pedido_produto_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_pedido_produto_id_pedido_produto PRIMARY KEY (id_pedido_produto),
	CONSTRAINT fk_tb_pedido_produto_id_pedido FOREIGN KEY (id_pedido)
		REFERENCES tb_pedido(id_pedido),
	CONSTRAINT fk_tb_pedido_produto_id_produto FOREIGN KEY (id_produto)
		REFERENCES tb_produto(id_produto)
);

DROP TABLE IF EXISTS tb_formula CASCADE;
CREATE TABLE tb_formula(
	id_formula serial,
	id_produto integer,
	id_materia_prima integer,
	quantidade numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_formula_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_formula_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_formula_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_formula_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_formula_id_formula PRIMARY KEY (id_formula),
	CONSTRAINT fk_tb_formula_id_produto FOREIGN KEY (id_produto)
		REFERENCES tb_produto(id_produto),
	CONSTRAINT fk_tb_formula_id_materia_prima FOREIGN KEY (id_materia_prima)
		REFERENCES tb_materia_prima(id_materia_prima)
);