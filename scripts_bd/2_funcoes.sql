CREATE OR REPLACE FUNCTION fn_cliente(p_id_cliente tb_cliente.id_cliente%TYPE,
									  p_nome tb_cliente.nome%TYPE,
									  p_cpf tb_cliente.cpf%TYPE,
									  p_email tb_cliente.email%TYPE,
									  p_telefone tb_cliente.telefone%TYPE,
									  p_data_nascimento tb_cliente.data_nascimento%TYPE,
									  operacao char(1))
RETURNS tb_cliente AS
$$
DECLARE
	registro tb_cliente%ROWTYPE;
	msg_exception text;
BEGIN
	SELECT * INTO registro
	FROM tb_cliente
	WHERE id_cliente = p_id_cliente;

	IF operacao = 'I' 	THEN
		IF p_id_cliente IS NULL THEN
			INSERT INTO tb_cliente (
				nome, 
			 	cpf, 
			 	email, 
			 	telefone, 
			 	data_nascimento, 
			 	usuario_criador, 
			 	data_criacao,
			 	usuario_atualizador,
			 	data_atualizacao
			 )
			VALUES (
				p_nome,
				p_cpf,
				p_email,
				p_telefone,
				p_data_nascimento,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp
				) 
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_cliente deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_cliente IS NOT NULL THEN
			UPDATE tb_cliente SET
				nome = p_nome, 
			 	cpf = p_cpf,
			 	email = p_email, 
			 	telefone = p_telefone, 
			 	data_nascimento = p_data_nascimento, 
			 	usuario_atualizador = current_user,
			 	data_atualizacao = current_timestamp
		 	WHERE id_cliente = p_id_cliente 
			RETURNING * INTO registro;
		ELSE 
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_cliente;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_cliente IS NOT NULL THEN
		 	DELETE FROM tb_cliente
		 	WHERE id_cliente = p_id_cliente 
		 	RETURNING * INTO registro;
		 ELSE
		 	RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_cliente;
		 END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;

	EXCEPTION WHEN unique_violation THEN
	    GET STACKED DIAGNOSTICS msg_exception := CONSTRAINT_NAME;

	    IF msg_exception = 'uq_tb_cliente_cpf' THEN
	        RAISE unique_violation USING MESSAGE = 'O cpf '||p_cpf||' já existe no cadastro';
	    ELSIF msg_exception = 'uq_tb_cliente_email' THEN
	        RAISE unique_violation USING MESSAGE = 'O e-mail '||p_email||' já existe no cadastro';
	    END IF;

	RETURN registro;
END;
$$ LANGUAGE plpgsql;

-- Exemplo de INSERT
-- SELECT * FROM fn_cliente(null,'Bruna','000.000.000-10','bruna@gmail.com','(16)98137-7798','23/11/1986','I');

CREATE OR REPLACE FUNCTION fn_pedido(p_id_pedido tb_pedido.id_pedido%TYPE,
									 p_data_pedido tb_pedido.data_pedido%TYPE,
									 p_valor_total tb_pedido.valor_total%TYPE,
									 p_id_cliente tb_pedido.id_cliente%TYPE,
									 operacao char(1))
RETURNS tb_pedido AS
$$
DECLARE 
	registro tb_pedido%ROWTYPE;
BEGIN
	SELECT * INTO registro
	FROM tb_pedido
	WHERE id_pedido = p_id_pedido;

	IF operacao = 'I' THEN
		IF p_id_pedido IS NULL THEN
			INSERT INTO tb_pedido(
				data_pedido,
				valor_total,
				id_cliente,
				usuario_criador,
				data_criacao,
				usuario_atualizador,
				data_atualizacao
				)
			VALUES (
				p_data_pedido,
				p_valor_total,
				p_id_cliente,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp
				) 
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_pedido deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_pedido IS NOT NULL THEN
			UPDATE tb_pedido SET
				data_pedido = p_data_pedido,
				valor_total = p_valor_total,
				id_cliente = p_id_cliente,
				usuario_atualizador = current_user,
				data_atualizacao = current_timestamp 
			WHERE id_pedido = p_id_pedido
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_pedido;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_pedido IS NOT NULL THEN
			DELETE FROM tb_pedido
			WHERE id_pedido = p_id_pedido 
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_pedido;
		END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;

	RETURN registro;

END;
$$ LANGUAGE plpgsql;

-- Exemplo de insert
-- SELECT * FROM fn_pedido(1,'18/03/2015 20:40',350.00,16,'U');

CREATE OR REPLACE FUNCTION fn_produto(p_id_produto tb_produto.id_produto%TYPE,
									  p_descricao tb_produto.descricao%TYPE,
									  p_tamanho tb_produto.tamanho%TYPE,
									  p_cor tb_produto.cor%TYPE,
									  p_preco tb_produto.preco%TYPE,
									  p_saldo_estoque tb_produto.saldo_estoque%TYPE,
									  operacao char(1))
RETURNS tb_produto AS
$$
DECLARE
	registro tb_produto%ROWTYPE;
BEGIN
	SELECT * INTO registro
	FROM tb_produto
	WHERE id_produto = p_id_produto;

	IF operacao = 'I' THEN
		IF p_id_produto IS NULL THEN
			INSERT INTO tb_produto (
				descricao,
				tamanho,
				cor,
				preco,
				saldo_estoque,
				usuario_criador,
				data_criacao,
				usuario_atualizador,
				data_atualizacao
				)
			VALUES (
				p_descricao,
				p_tamanho,
				p_cor,
				p_preco,
				p_saldo_estoque,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp
				) 
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_produto deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_produto IS NOT NULL THEN
			UPDATE tb_produto SET
				descricao = p_descricao,
				tamanho = p_tamanho,
				cor = p_cor,
				preco = p_preco,
				saldo_estoque = p_saldo_estoque,
				usuario_atualizador = current_user,
				data_atualizacao = current_timestamp
			WHERE id_produto = p_id_produto
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_produto;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_produto IS NOT NULL THEN
			DELETE FROM tb_produto
			WHERE id_produto = p_id_produto
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_produto;
		END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;

	RETURN registro;

END;
$$ LANGUAGE plpgsql;

-- Exemplo de insert
-- SELECT * FROM fn_produto(null, 'Bojo', 43, 'preto', 20.7, 600, 'I');

CREATE OR REPLACE FUNCTION fn_materia_prima(p_id_materia_prima tb_materia_prima.id_materia_prima%TYPE,
											p_descricao tb_materia_prima.descricao%TYPE,
											p_saldo_estoque tb_materia_prima.saldo_estoque%TYPE,
											p_preco_custo tb_materia_prima.preco_custo%TYPE,
											operacao char(1))
RETURNS tb_materia_prima AS
$$
DECLARE
	registro tb_materia_prima%ROWTYPE;
BEGIN
	SELECT * INTO registro
	FROM tb_materia_prima
	WHERE id_materia_prima = p_id_materia_prima;

	IF operacao = 'I' THEN
		IF p_id_materia_prima IS NULL THEN
			INSERT INTO tb_materia_prima (
				descricao,
				saldo_estoque,
				preco_custo,
				usuario_criador,
				data_criacao,
				usuario_atualizador,
				data_atualizacao)
			VALUES (
				p_descricao,
				p_saldo_estoque,
				p_preco_custo,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp)
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_materia_prima deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_materia_prima IS NOT NULL THEN
			UPDATE tb_materia_prima SET
				descricao = p_descricao,
				saldo_estoque = p_saldo_estoque,
				preco_custo = p_preco_custo,
				usuario_atualizador = current_user,
				data_atualizacao = current_timestamp
			WHERE id_materia_prima = p_id_materia_prima
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_materia_prima;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_materia_prima IS NOT NULL THEN
			DELETE FROM tb_materia_prima
			WHERE id_materia_prima = p_id_materia_prima
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_materia_prima;
		END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;

	RETURN registro;

END;
$$ LANGUAGE plpgsql;

-- Exemplo de insert
-- SELECT * FROM fn_materia_prima(NULL, 'Tecido Preto', 456, 60.55, 'I');

CREATE OR REPLACE FUNCTION fn_pedido_produto(p_id_pedido_produto tb_pedido_produto.id_pedido_produto%TYPE,
											 p_id_pedido tb_pedido_produto.id_pedido%TYPE,
											 p_id_produto tb_pedido_produto.id_produto%TYPE,
											 p_quantidade tb_pedido_produto.quantidade%TYPE,
											 p_valor_unitario tb_pedido_produto.valor_unitario%TYPE,
											 operacao char(1))
RETURNS tb_pedido_produto AS
$$
DECLARE
	registro tb_pedido_produto%ROWTYPE;
BEGIN
	SELECT * INTO registro
	FROM tb_pedido_produto
	WHERE id_pedido_produto = p_id_pedido_produto;

	IF operacao = 'I' THEN
		IF p_id_pedido_produto IS NULL THEN
			INSERT INTO tb_pedido_produto (
				id_pedido,
				id_produto,
				quantidade,
				valor_unitario,
				usuario_criador,
				data_criacao,
				usuario_atualizador,
				data_atualizacao)
			VALUES (
				p_id_pedido,
				p_id_produto,
				p_quantidade,
				p_valor_unitario,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp)
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_pedido_produto deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_pedido_produto IS NOT NULL THEN
			UPDATE tb_pedido_produto SET
				id_pedido = p_id_pedido,
				id_produto = p_id_produto,
				quantidade = p_quantidade,
				valor_unitario = p_valor_unitario,
				usuario_atualizador =current_user,
				data_atualizacao = current_timestamp
			WHERE id_pedido_produto = p_id_pedido_produto
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_pedido_produto;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_pedido_produto IS NOT NULL THEN
			DELETE FROM tb_pedido_produto
			WHERE id_pedido_produto = p_id_pedido_produto
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_pedido_produto;
		END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;

	RETURN registro;
END;
$$ LANGUAGE plpgsql;

-- Exemplo de insert
-- SELECT * FROM fn_pedido_produto(NULL, 2, 2, 50, 7.90, 'I');

CREATE OR REPLACE FUNCTION fn_formula(p_id_formula tb_formula.id_formula%TYPE,
									  p_id_produto tb_formula.id_formula%TYPE,
									  p_id_materia_prima tb_formula.id_materia_prima%TYPE,
									  p_quantidade tb_formula.quantidade%TYPE,
									  operacao char(1))
RETURNS tb_formula AS
$$
DECLARE
	registro tb_formula%ROWTYPE;
BEGIN
	SELECT * INTO registro
	FROM tb_formula
	WHERE id_formula = p_id_formula;

	IF operacao = 'I' THEN
		IF p_id_formula IS NULL THEN
			INSERT INTO tb_formula (
				id_produto,
				id_materia_prima,
				quantidade,
				usuario_criador,
				data_criacao,
				usuario_atualizador,
				data_atualizacao)
			VALUES (
				p_id_produto,
				p_id_materia_prima,
				p_quantidade,
				current_user,
				current_timestamp,
				current_user,
				current_timestamp)
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Para operações de INSERT o id_formula deve ser NULL';
		END IF;
	ELSIF operacao = 'U' THEN
		IF registro.id_formula IS NOT NULL THEN
			UPDATE tb_formula SET
				id_produto = p_id_produto,
				id_materia_prima = p_id_materia_prima,
				quantidade = p_quantidade,
				usuario_atualizador = current_user,
				data_atualizacao = current_timestamp
			WHERE id_formula = p_id_formula
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível atualizar.', p_id_formula;
		END IF;
	ELSIF operacao = 'D' THEN
		IF registro.id_formula IS NOT NULL THEN
			DELETE FROM tb_formula
			WHERE id_formula = p_id_formula
			RETURNING * INTO registro;
		ELSE
			RAISE EXCEPTION 'Registro nº % não foi encontrado. Impossível apagar.', p_id_formula;
		END IF;
	ELSE
		RAISE EXCEPTION 'Operação inválida: %', operacao;
	END IF;
		
	RETURN registro;
END;
$$ LANGUAGE plpgsql;

-- Exemplo de insert
-- SELECT * FROM fn_formula(NULL, 2, 4, 200, 'I');
