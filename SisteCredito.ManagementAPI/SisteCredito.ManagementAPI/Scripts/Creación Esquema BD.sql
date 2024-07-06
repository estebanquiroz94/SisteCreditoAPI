CREATE DATABASE SisteCredito;

go
use SisteCredito;

CREATE TABLE Charge(
Id INT IDENTITY (1,1) NOT NULL, 
Code VARCHAR(10) UNIQUE NOT NULL,
Name VARCHAR(200) NOT NULL, 
CONSTRAINT PK_Charge PRIMARY KEY (Id));

drop table Employee;
CREATE TABLE Employee(
Id INT IDENTITY(1,1) NOT NULL,
IdSupervisor INT NULL,
IdCharge INT NOT NULL,
Name VARCHAR(200) NOT NULL,
Document VARCHAR(50) NOT NULL,
Phone VARCHAR (20) UNIQUE NOT NULL,
CONSTRAINT PK_Employee PRIMARY KEY (Id),
CONSTRAINT FK_Supervisor FOREIGN KEY (IdSupervisor) REFERENCES Employee (Id),
CONSTRAINT FK_Charge FOREIGN KEY (IdCharge) REFERENCES Charge (Id));

CREATE TABLE Area(
Id INT IDENTITY (1,1) NOT NULL, 
IdHumangestor INT NOT NULL,
IdSupervisor INT NOT NULL,
Code VARCHAR(10) UNIQUE NOT NULL,
Name VARCHAR(200) NOT NULL, 
CONSTRAINT PK_Area PRIMARY KEY (Id),
CONSTRAINT FK_HumanGestor FOREIGN KEY (IdHumangestor) REFERENCES Employee (Id),
CONSTRAINT FK_Supervidor FOREIGN KEY (IdSupervisor) REFERENCES Employee (Id));

CREATE TABLE ExtraHour(
Id INT IDENTITY(1,1) NOT NULL,
IdEmployee INT NOT NULL,
Status VARCHAR (50),
Observations VARCHAR(500) NOT NULL,
QuantityHours INT NOT NULL,
DateRegister DATETIME,
CONSTRAINT FK_Employee_Extra PRIMARY KEY (Id));


INSERT INTO Charge (Code, Name) values ('EMPL','Empleado');
INSERT INTO Charge (Code, Name) values ('LIDE','Lider Area');
INSERT INTO Charge (Code, Name) values ('GERE','Gerente');
INSERT INTO Charge (Code, Name) values ('RRHH','Recursos Humanos');

