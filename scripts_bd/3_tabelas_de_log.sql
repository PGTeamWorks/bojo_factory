CREATE SCHEMA IF NOT EXISTS auditoria;

DROP TABLE IF EXISTS auditoria.tb_cliente_log CASCADE;
CREATE TABLE auditoria.tb_cliente_log(
	id_cliente_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_cliente_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_cliente_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_cliente_log_comando_executado NOT NULL,
	id_cliente integer CONSTRAINT nn_auditoria_tb_cliente_log_id_cliente NOT NULL,
	nome varchar(60) CONSTRAINT nn_tb_cliente_log_nome NOT NULL,
	cpf varchar(14),
	email varchar(60),
	telefone varchar(15),
	data_nascimento date,
	usuario_criador varchar(63) CONSTRAINT nn_tb_cliente_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_cliente_log_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_cliente_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_cliente_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_cliente_id_cliente_log PRIMARY KEY (id_cliente_log),
	CONSTRAINT chk_tb_cliente_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);

DROP TABLE IF EXISTS auditoria.tb_pedido_log CASCADE;
CREATE TABLE auditoria.tb_pedido_log(
	id_pedido_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_pedido_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_pedido_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_pedido_log_comando_executado NOT NULL,
	id_pedido integer,
	data_pedido timestamp,
	valor_total numeric(5,2),
	id_cliente integer,
	usuario_criador varchar(63) CONSTRAINT nn_tb_pedido_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_pedido_log_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_pedido_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_pedido_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_pedido_log_id_pedido_log PRIMARY KEY (id_pedido_log),
	CONSTRAINT chk_tb_pedido_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);

DROP TABLE IF EXISTS auditoria.tb_produto_log CASCADE;
CREATE TABLE auditoria.tb_produto_log(
	id_produto_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_produto_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_produto_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_produto_log_comando_executado NOT NULL,
	id_produto integer,
	descricao varchar(60),
	tamanho numeric(5,2),
	cor varchar(20),
	preco numeric(5,2),
	saldo_estoque numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_produto_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_produto__logdata_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_produto_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_produto_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_produto_log_id_produto_log PRIMARY KEY (id_produto_log),
	CONSTRAINT chk_tb_produto_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);

DROP TABLE IF EXISTS auditoria.tb_materia_prima_log CASCADE;
CREATE TABLE auditoria.tb_materia_prima_log(
	id_materia_prima_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_materia_prima_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_materia_prima_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_materia_prima_log_comando_executado NOT NULL,
	id_materia_prima integer,
	descricao varchar(60),
	saldo_estoque numeric(5,2),
	preco_custo numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_materia_prima_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_materia_prima_log_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_materia_prima_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_materia_prima_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_materia_prima_log_id_materia_prima_log PRIMARY KEY (id_materia_prima_log),
	CONSTRAINT chk_tb_produto_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);

DROP TABLE IF EXISTS auditoria.tb_pedido_produto_log CASCADE;
CREATE TABLE auditoria.tb_pedido_produto_log(
	id_pedido_produto_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_pedido_produto_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_pedido_produto_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_pedido_produto_log_comando_executado NOT NULL,
	id_pedido_produto integer,
	id_pedido integer,
	id_produto integer,
	quantidade numeric(5,2),
	valor_unitario numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_pedido_produto_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_pedido_produto_log_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_pedido_produto_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_pedido_produto_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_pedido_produto_log_id_pedido_produto_log PRIMARY KEY (id_pedido_produto_log),
	CONSTRAINT chk_tb_pedido_produto_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);

DROP TABLE IF EXISTS auditoria.tb_formula_log CASCADE;
CREATE TABLE auditoria.tb_formula_log(
	id_formula_log serial,
	usuario_execucao varchar(63) CONSTRAINT nn_auditoria_tb_formula_log_usuario_execucao NOT NULL,
	data_execucao timestamp CONSTRAINT nn_auditoria_tb_formula_log_data_execucao NOT NULL,
	comando_executado char(1) CONSTRAINT nn_auditoria_tb_formula_log_comando_executado NOT NULL,
	id_formula integer,
	id_produto integer,
	id_materia_prima integer,
	quantidade numeric(5,2),
	usuario_criador varchar(63) CONSTRAINT nn_tb_formula_log_usuario_criador NOT NULL,
	data_criacao timestamp CONSTRAINT nn_tb_formula_log_data_criacao NOT NULL,
	usuario_atualizador varchar(63) CONSTRAINT nn_tb_formula_log_usuario_atualizador NOT NULL,
	data_atualizacao timestamp CONSTRAINT nn_tb_formula_log_data_atualizacao NOT NULL,
	CONSTRAINT pk_tb_formula_log_id_formula_log PRIMARY KEY (id_formula_log),
	CONSTRAINT chk_tb_formula_log_comando_executado CHECK (comando_executado IN ('I','U','D'))
);