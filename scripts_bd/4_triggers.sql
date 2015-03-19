CREATE OR REPLACE FUNCTION auditoria.fn_tg_cliente_log() RETURNS TRIGGER AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO auditoria.tb_cliente_log (SELECT nextval('auditoria.tb_cliente_log_id_cliente_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_cliente_log (SELECT nextval('auditoria.tb_cliente_log_id_cliente_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_cliente_log (SELECT nextval('auditoria.tb_cliente_log_id_cliente_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_tb_cliente_log
AFTER INSERT OR UPDATE OR DELETE ON tb_cliente
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_cliente_log();

CREATE OR REPLACE FUNCTION auditoria.fn_tg_pedido_log() RETURNS TRIGGER AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO auditoria.tb_pedido_log (SELECT nextval('auditoria.tb_pedido_log_id_pedido_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_pedido_log (SELECT nextval('auditoria.tb_pedido_log_id_pedido_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_pedido_log (SELECT nextval('auditoria.tb_pedido_log_id_pedido_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_tb_pedido_log
AFTER INSERT OR UPDATE OR DELETE ON tb_pedido
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_pedido_log();

CREATE OR REPLACE FUNCTION auditoria.fn_tg_produto_log() RETURNS TRIGGER AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO auditoria.tb_produto_log (SELECT nextval('auditoria.tb_produto_log_id_produto_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_produto_log (SELECT nextval('auditoria.tb_produto_log_id_produto_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_produto_log (SELECT nextval('auditoria.tb_produto_log_id_produto_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_produto_log
AFTER INSERT OR UPDATE OR DELETE ON tb_produto
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_produto_log();

CREATE OR REPLACE FUNCTION auditoria.fn_tg_materia_prima() RETURNS TRIGGER AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO auditoria.tb_materia_prima_log (SELECT nextval('auditoria.tb_materia_prima_log_id_materia_prima_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_materia_prima_log (SELECT nextval('auditoria.tb_materia_prima_log_id_materia_prima_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_materia_prima_log (SELECT nextval('auditoria.tb_materia_prima_log_id_materia_prima_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_materia_prima_log
AFTER INSERT OR UPDATE OR DELETE ON tb_materia_prima
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_materia_prima();

CREATE OR REPLACE FUNCTION auditoria.fn_tg_pedido_produto() RETURNS TRIGGER AS
$$
BEGIN
	IF TG_OP = 'INSERT' THEN
		INSERT INTO auditoria.tb_pedido_produto_log (SELECT nextval('auditoria.tb_pedido_produto_log_id_pedido_produto_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_pedido_produto_log (SELECT nextval('auditoria.tb_pedido_produto_log_id_pedido_produto_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_pedido_produto_log (SELECT nextval('auditoria.tb_pedido_produto_log_id_pedido_produto_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_pedido_produto_log
AFTER INSERT OR UPDATE OR DELETE ON tb_pedido_produto
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_pedido_produto();

CREATE OR REPLACE FUNCTION auditoria.fn_tg_formula() RETURNS TRIGGER AS
$$
BEGIN 
	IF TG_OP = 'INSERT'  THEN
		INSERT INTO auditoria.tb_formula_log (SELECT nextval('auditoria.tb_formula_log_id_formula_log_seq'), current_user, current_timestamp, 'I', new.*);
	ELSIF TG_OP = 'UPDATE' THEN
		INSERT INTO auditoria.tb_formula_log (SELECT nextval('auditoria.tb_formula_log_id_formula_log_seq'), current_user, current_timestamp, 'U', new.*);
	ELSIF TG_OP = 'DELETE' THEN
		INSERT INTO auditoria.tb_formula_log (SELECT nextval('auditoria.tb_formula_log_id_formula_log_seq'), current_user, current_timestamp, 'D', old.*);
	END IF;

	RETURN NULL;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER tg_forumula_log
AFTER INSERT OR UPDATE OR DELETE ON tb_formula
FOR EACH ROW EXECUTE PROCEDURE auditoria.fn_tg_formula();