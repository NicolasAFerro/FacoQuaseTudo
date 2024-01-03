INSERT INTO Clientes (Nome, Quadra, Lote, Telefone, Observacao)
VALUES ('Nicolas', 'A10', '39', '15991037688', 'FILHO DO ALEMÃO');
 
DROP TABLE Servicos 
DROP TABLE Clientes
CREATE TABLE Clientes (
    Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
    Nome VARCHAR(255) NOT NULL,
    Quadra VARCHAR(10) NOT NULL,
    Lote VARCHAR(10) NOT NULL,
    Telefone VARCHAR(50) NOT NULL,
    Observacao VARCHAR(255)
);  
CREATE TABLE Servicos( 
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL, 
	Data DATETIME NOT NULL, 
	Valor Decimal (10,2) NOT NULL, 
	Observacao VARCHAR(255) NOT NULL, 
	Tipo_id INTEGER NOT NULL, 
	Clientes_id INTEGER NOT NULL, 
	FOREIGN KEY (Tipo_id) 
		REFERENCES Tipos(Id), 
	FOREIGN KEY (Clientes_id) 
		REFERENCES CLIENTES(Id)


) 
INSERT INTO Tipos (Descricao) VALUES ('JARDINAGEM') 
DROP TABLE Tipos  
CREATE TABLE Tipos( 
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,  
	Descricao VARCHAR(100) NOT NULL,
) 
ALTER TABLE Servicos 
ADD  Tipos_Id INT 
FOREIGN KEY (Tipos_Id)
REFERENCES Tipos(Id);

ALTER TABLE Servicos 
DROP COLUMN Tipo_id 

SELECT * FROM Tipos 
INSERT INTO Tipos (Descricao) VALUES ('MARIDO DE ALUGUEL') 

SELECT Nome, Quadra, Lote FROM Clientes 

INSERT INTO Servicos (Data, Valor,Observacao, Clientes_id, Tipos_Id)
VALUES ('10/11/1997', '106', 'toneira', '5', '2'); 
SELECT * FROM Servicos

SELECT * FROM Clientes 

SELECT * FROM Servicos
INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id 

SELECT Servicos.*, Clientes.Nome, Clientes.Quadra, Clientes.Lote
FROM Servicos
INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id; 

SELECT Servicos. *, Clientes.Nome AS NomeCliente, Clientes.Quadra, Clientes.Lote, Tipos.Descricao AS DescricaoTipo
FROM Servicos
INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id
INNER JOIN Tipos ON Servicos.Tipos_id= Tipos.Id; 

SELECT Servicos.Id, Servicos.Data, Servicos.Valor, Servicos.Observacao, Clientes.Nome AS NomeCliente, Clientes.Quadra, Clientes.Lote, Tipos.Descricao AS DescricaoTipo
FROM Servicos
INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id
INNER JOIN Tipos ON Servicos.Tipos_id = Tipos.Id; 

SELECT Servicos.Id, Servicos.Data, Servicos.Valor, Servicos.Observacao, 
       Clientes.Nome, Clientes.Quadra, Clientes.Lote, 
       Tipos.Descricao
FROM Servicos
INNER JOIN Clientes ON Servicos.Clientes_id = Clientes.Id
INNER JOIN Tipos ON Servicos.Tipos_Id = Tipos.Id
ORDER BY Servicos.Data DESC;