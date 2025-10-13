/*** script para iniciar o POSTGRES***/

CREATE TABLE IF NOT EXISTS pessoa (
    cpf VARCHAR(11) PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    genero VARCHAR(20) NOT NULL,
    telefone VARCHAR(15),
    password_hash VARCHAR(255) NOT NULL,
    data_nascimento DATE NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS cliente(
    cpf VARCHAR(11) PRIMARY KEY REFERENCES pessoa(cpf) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE IF NOT EXISTS funcionario (
    cpf VARCHAR(11) PRIMARY KEY REFERENCES pessoa(cpf) ON DELETE CASCADE ON UPDATE CASCADE,
    data_admissao DATE,
    data_demissao DATE,
    cargo VARCHAR(50) NOT NULL,
    salario NUMERIC(10, 2) NOT NULL
);

CREATE TABLE IF NOT EXISTS filme (
    id SERIAL PRIMARY KEY,
    nome_filme VARCHAR(100) NOT NULL,
    sinopse VARCHAR(1500) NOT NULL,
    genero VARCHAR(50) NOT NULL,
    duracao INT NOT NULL,
    classificacao_etaria INT NOT NULL,
    idioma VARCHAR(50) NOT NULL,
    pais_origem VARCHAR(50) NOT NULL,
    produtor VARCHAR(50) NOT NULL,
    data_lancamento DATE NOT NULL,
    diretor VARCHAR(50) NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS elenco(
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    genero VARCHAR(20) NOT NULL,
    personagem VARCHAR(100) NOT NULL,
    data_nascimento DATE NOT NULL,
    nacionalidade VARCHAR(50) NOT NULL
);

CREATE TABLE IF NOT EXISTS filme_elenco (
    filme_id INT REFERENCES filme(id) ON DELETE CASCADE ON UPDATE CASCADE,
    elenco_id INT REFERENCES elenco(id) ON DELETE CASCADE ON UPDATE CASCADE,
    papel VARCHAR(100) NOT NULL,
    PRIMARY KEY (filme_id, elenco_id)
);

CREATE TABLE IF NOT EXISTS sala (
    id SERIAL PRIMARY KEY,
    tipo_sala VARCHAR(50) NOT NULL,
    capacidade INT NOT NULL
);

CREATE TABLE IF NOT EXISTS assento(
    id SERIAL PRIMARY KEY,
    sala_id INT REFERENCES sala(id) ON DELETE CASCADE,
    fila CHAR(1) NOT NULL,
    numero INT NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'disponivel'
    CHECK (status IN ('disponivel', 'reservado', 'ocupado'))
);

CREATE TABLE IF NOT EXISTS sessao (
    id VARCHAR(64) PRIMARY KEY,
    filme_id INT REFERENCES filme(id) ON DELETE CASCADE ON UPDATE CASCADE,
    sala_id INT REFERENCES sala(id) ON DELETE CASCADE ON UPDATE CASCADE,
    horario_inicio TIMESTAMP NOT NULL,
    horario_fim TIMESTAMP NOT NULL,
    preco_sessao NUMERIC(10, 2) NOT NULL,
    idioma_audio VARCHAR(20) NOT NULL,
    idioma_legenda VARCHAR(20) NOT NULL
);


CREATE TABLE IF NOT EXISTS reserva(
    id VARCHAR(64) PRIMARY KEY,
    cliente_cpf VARCHAR(11) REFERENCES pessoa(cpf) ON DELETE CASCADE ON UPDATE CASCADE,
    sessao_id VARCHAR(64) REFERENCES sessao(id) ON DELETE CASCADE ON UPDATE CASCADE,
    status VARCHAR(20) NOT NULL DEFAULT 'pendente'
    CHECK (status IN ('pendente', 'paga', 'cancelada', 'expirado')),
    data_reserva TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE IF NOT EXISTS ingresso (
    id VARCHAR(64) PRIMARY KEY,
    sessao_id VARCHAR(64) NOT NULL REFERENCES sessao(id) ON DELETE CASCADE,
    reserva_id VARCHAR(64) NOT NULL REFERENCES reserva(id) ON DELETE CASCADE,
    assento_id INT NOT NULL REFERENCES assento(id) ON DELETE CASCADE,
    preco_pago NUMERIC(10, 2) NOT NULL,
    codigo_qr VARCHAR(255) UNIQUE NOT NULL,
    data_gerado TIMESTAMP NOT NULL,
    data_validacao TIMESTAMP,
    utilizado BOOLEAN DEFAULT FALSE
);


CREATE TABLE IF NOT EXISTS avaliacao(
    id SERIAL PRIMARY KEY,
    filme_id INT NOT NULL REFERENCES filme(id) ON DELETE CASCADE, 
    cliente_cpf VARCHAR(11) NOT NULL REFERENCES pessoa(cpf) ON DELETE CASCADE,
    reserva_id VARCHAR(64) NOT NULL REFERENCES reserva(id) ON DELETE CASCADE,
    nota INT CHECK (nota >= 1 AND nota <= 5) NOT NULL,
    comentario VARCHAR(300),
    data_avaliacao TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS pagamento (
    id VARCHAR(64) PRIMARY KEY,
    reserva_id VARCHAR(64) NOT NULL REFERENCES reserva(id) ON DELETE CASCADE,
    valor_pago NUMERIC(10, 2) NOT NULL,
    metodo_pagamento VARCHAR(50) NOT NULL,
    status VARCHAR(20) NOT NULL DEFAULT 'pendente'
    CHECK (status IN ('pendente', 'aprovado', 'falhado', 'recusado')),
    transacao_gateway VARCHAR(100) NOT NULL,
    data_pagamento TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);



/** indice nas FKS**/

CREATE INDEX idx_cliente_cpf ON cliente(cpf);
CREATE INDEX idx_funcionario_cpf ON funcionario(cpf);
CREATE INDEX idx_filme_elenco_filme ON filme_elenco(filme_id);
CREATE INDEX idx_filme_elenco_elenco ON filme_elenco(elenco_id);
CREATE INDEX idx_assento_sala ON assento(sala_id);
CREATE INDEX idx_sessao_filme ON sessao(filme_id);
CREATE INDEX idx_sessao_sala ON sessao(sala_id);
CREATE INDEX idx_reserva_cliente ON reserva(cliente_cpf);
CREATE INDEX idx_reserva_sessao ON reserva(sessao_id);
CREATE INDEX idx_pagamento_reserva ON pagamento(reserva_id);
CREATE INDEX idx_ingresso_reserva ON ingresso(reserva_id);
CREATE INDEX idx_ingresso_sessao ON ingresso(sessao_id);
CREATE INDEX idx_avaliacao_filme ON avaliacao(filme_id);
CREATE INDEX idx_avaliacao_cliente ON avaliacao(cliente_cpf);


/** indice para buscas comuns**/

CREATE INDEX idx_filme_nome ON filme(nome_filme);
CREATE INDEX idx_filme_genero ON filme(genero);
CREATE INDEX idx_sessao_horario ON sessao(horario_inicio);
CREATE INDEX idx_reserva_status ON reserva(status);
CREATE INDEX idx_pagamento_status ON pagamento(status);
CREATE INDEX idx_ingresso_qr ON ingresso(codigo_qr);


COMMENT ON TABLE pessoa IS 'Tabela base para clientes e funcionários';
COMMENT ON TABLE cliente IS 'Clientes do cinema que podem fazer reservas';
COMMENT ON TABLE funcionario IS 'Funcionários do cinema';
COMMENT ON TABLE filme IS 'Catálogo de filmes disponíveis';
COMMENT ON TABLE elenco IS 'Atores e atrizes';
COMMENT ON TABLE filme_elenco IS 'Relacionamento N:N entre filmes e elenco';
COMMENT ON TABLE sala IS 'Salas do cinema';
COMMENT ON TABLE assento IS 'Assentos de cada sala';
COMMENT ON TABLE sessao IS 'Sessões/horários dos filmes';
COMMENT ON TABLE reserva IS 'Reservas de ingressos';
COMMENT ON TABLE pagamento IS 'Pagamentos das reservas';
COMMENT ON TABLE ingresso IS 'Ingressos gerados após pagamento';
COMMENT ON TABLE avaliacao IS 'Avaliações de filmes por clientes';