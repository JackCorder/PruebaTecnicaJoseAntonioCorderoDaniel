-- Stored Procedures de Alumnos

CREATE PROCEDURE CrearAlumno
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @NumeroIdentificacion NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Alumnos WHERE NumeroIdentificacion = @NumeroIdentificacion)
            THROW 52001, 'Ya existe un alumno con ese número de identificación.', 1;

        INSERT INTO Alumnos (Nombre, Apellido, FechaNacimiento, NumeroIdentificacion)
        VALUES (@Nombre, @Apellido, @FechaNacimiento, @NumeroIdentificacion);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ActualizarAlumno
    @AlumnoID INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @FechaNacimiento DATE,
    @NumeroIdentificacion NVARCHAR(20)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 52002, 'No se encontró un alumno con el ID proporcionado.', 1;

        IF EXISTS (
            SELECT 1 FROM Alumnos
            WHERE NumeroIdentificacion = @NumeroIdentificacion AND AlumnoID <> @AlumnoID
        )
            THROW 52003, 'Ya existe otro alumno con ese número de identificación.', 1;

        UPDATE Alumnos
        SET Nombre = @Nombre,
            Apellido = @Apellido,
            FechaNacimiento = @FechaNacimiento,
            NumeroIdentificacion = @NumeroIdentificacion
        WHERE AlumnoID = @AlumnoID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE EliminarAlumno
    @AlumnoID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 52004, 'No se puede eliminar. El alumno no existe.', 1;

        DELETE FROM Alumnos
        WHERE AlumnoID = @AlumnoID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerAlumnos
AS
BEGIN
    BEGIN TRY
        SELECT AlumnoID, Nombre, Apellido, FechaNacimiento, NumeroIdentificacion
        FROM Alumnos
        ORDER BY Apellido, Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerAlumnoPorId
    @AlumnoID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 52005, 'No se encontró un alumno con el ID proporcionado.', 1;

        SELECT AlumnoID, Nombre, Apellido, FechaNacimiento, NumeroIdentificacion
        FROM Alumnos
        WHERE AlumnoID = @AlumnoID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
