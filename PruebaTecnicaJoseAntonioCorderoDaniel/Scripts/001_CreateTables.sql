CREATE DATABASE PruebaItalika;
GO

USE PruebaItalika;
GO


CREATE TABLE Escuelas (
    EscuelaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Descripcion NVARCHAR(250),
	Codigo UNIQUEIDENTIFIER DEFAULT NEWID()
);

CREATE TABLE Profesores (
    ProfesorID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    NumeroIdentificacion NVARCHAR(20) UNIQUE NOT NULL,
    EscuelaID INT NOT NULL,
    FOREIGN KEY (EscuelaID) REFERENCES Escuelas(EscuelaID)
);

CREATE TABLE Alumnos (
    AlumnoID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(50) NOT NULL,
    Apellido NVARCHAR(50) NOT NULL,
    FechaNacimiento DATE NOT NULL,
    NumeroIdentificacion NVARCHAR(20) UNIQUE NOT NULL
);

-- Inscripción de Alumnos a Escuelas
CREATE TABLE Inscripciones (
    InscripcionID INT IDENTITY(1,1) PRIMARY KEY,
    AlumnoID INT NOT NULL,
    EscuelaID INT NOT NULL,
    FechaInscripcion DATE DEFAULT GETDATE(),
    FOREIGN KEY (AlumnoID) REFERENCES Alumnos(AlumnoID),
    FOREIGN KEY (EscuelaID) REFERENCES Escuelas(EscuelaID)
);

-- Relación de Alumnos con Profesores
CREATE TABLE AlumnosProfesores (
    AlumnoID INT NOT NULL,
    ProfesorID INT NOT NULL,
    PRIMARY KEY (AlumnoID, ProfesorID),
    FOREIGN KEY (AlumnoID) REFERENCES Alumnos(AlumnoID),
    FOREIGN KEY (ProfesorID) REFERENCES Profesores(ProfesorID)
);