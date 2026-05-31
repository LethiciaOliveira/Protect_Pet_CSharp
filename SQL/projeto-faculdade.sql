CREATE TABLE tutores (
    id SERIAL PRIMARY KEY,
    nome VARCHAR(100) NOT NULL,
    sobrenome VARCHAR(100) NOT NULL,
    cpf VARCHAR(11) UNIQUE NOT NULL,
    telefone VARCHAR(20) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);

CREATE TABLE usuarios (
    id SERIAL PRIMARY KEY,

    login VARCHAR(100) UNIQUE NOT NULL
        CHECK (TRIM(login) <> ''),

    senha VARCHAR(100) NOT NULL
        CHECK (TRIM(senha) <> ''),

    tutor_id INTEGER UNIQUE NOT NULL,

    CONSTRAINT fk_usuario_tutor
        FOREIGN KEY (tutor_id)
        REFERENCES tutores(id)
        ON DELETE CASCADE
);

CREATE TABLE coleiras (
    id SERIAL PRIMARY KEY,

    tipo VARCHAR(30) NOT NULL UNIQUE
);

CREATE TABLE pets (
    id SERIAL PRIMARY KEY,

    nome VARCHAR(100) NOT NULL,

    especie VARCHAR(30) NOT NULL,

    raca VARCHAR(100) NOT NULL,

    sexo VARCHAR(15) NOT NULL,

    data_nascimento DATE,

    tutor_id INTEGER NOT NULL,

    coleira_id INTEGER,

    CONSTRAINT fk_pet_tutor
        FOREIGN KEY (tutor_id)
        REFERENCES tutores(id)
        ON DELETE CASCADE,

    CONSTRAINT fk_pet_coleira
        FOREIGN KEY (coleira_id)
        REFERENCES coleiras(id)
);

INSERT INTO coleiras (tipo) VALUES ('Basica'), ('Premium');

INSERT INTO tutores (nome, sobrenome, cpf, telefone, email)
VALUES
('Ismael', 'Santiago', '48617893893', '11999999999', 'ismael@gmail.com'),

('Lethicia', 'Oliveira', '78913475670', '11988888888', 'lethicia@gmail.com'),

('Elaine', 'Rosa', '21765869745', '11977777777', 'elaine@gmail.com');

INSERT INTO usuarios (login, senha, tutor_id) VALUES ('ismael', '123', 1), ('lethicia', '456', 2), ('elaine', '789', 3);

INSERT INTO pets
(nome, especie, raca, sexo, data_nascimento, tutor_id, coleira_id)
VALUES

('Atlas', 'Cachorro', 'Vira-Lata', 'Macho', '2025-01-05', 1, 1),

('Pandora', 'Cachorro', 'Pitbull', 'Fêmea', '2024-11-24', 2, 2),

('Mel', 'Gato', 'Persa', 'Fêmea', '2023-10-10', 3, 1);

INSERT INTO tutores (nome, sobrenome, cpf, telefone, email)
VALUES ('Daiane', 'Oliveira', '98765429105', '11098873627', 'daianeoliveira@gmail.com');

INSERT INTO usuarios (login, senha, tutor_id) VALUES ('daiane', 'dai1987', 4);

SELECT
    p.id,
    p.nome,
    p.especie,
    p.raca,

    t.nome AS tutor,

    c.tipo AS coleira

FROM pets p

JOIN tutores t
    ON p.tutor_id = t.id

LEFT JOIN coleiras c
    ON p.coleira_id = c.id;


SELECT * FROM usuarios;
SELECT * FROM pets;
SELECT * FROM tutores;


SELECT * FROM tutores WHERE sobrenome = 'Oliveira';
