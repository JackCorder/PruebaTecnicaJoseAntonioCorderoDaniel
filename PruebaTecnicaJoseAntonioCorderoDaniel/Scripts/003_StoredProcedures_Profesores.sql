-- Stored Procedures de Profesores

CREATE PROCEDURE CrearProfesor
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @NumeroIdentificacion NVARCHAR(20),
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Profesores WHERE NumeroIdentificacion = @NumeroIdentificacion)
            THROW 51001, 'Ya existe un profesor con ese número de identificación.', 1;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 51002, 'La escuela especificada no existe.', 1;

        INSERT INTO Profesores (Nombre, Apellido, NumeroIdentificacion, EscuelaID)
        VALUES (@Nombre, @Apellido, @NumeroIdentificacion, @EscuelaID);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ActualizarProfesor
    @ProfesorID INT,
    @Nombre NVARCHAR(50),
    @Apellido NVARCHAR(50),
    @NumeroIdentificacion NVARCHAR(20),
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 51003, 'No se encontró un profesor con el ID proporcionado.', 1;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 51004, 'La escuela especificada no existe.', 1;

        IF EXISTS (
            SELECT 1 FROM Profesores
            WHERE NumeroIdentificacion = @NumeroIdentificacion AND ProfesorID <> @ProfesorID
        )
            THROW 51005, 'Ya existe otro profesor con ese número de identificación.', 1;

        UPDATE Profesores
        SET Nombre = @Nombre,
            Apellido = @Apellido,
            NumeroIdentificacion = @NumeroIdentificacion,
            EscuelaID = @EscuelaID
        WHERE ProfesorID = @ProfesorID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


CREATE PROCEDURE EliminarProfesor
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 51006, 'No se puede eliminar. El profesor no existe.', 1;

        DELETE FROM Profesores
        WHERE ProfesorID = @ProfesorID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO


CREATE PROCEDURE ObtenerProfesores
AS
BEGIN
    BEGIN TRY
        SELECT ProfesorID, Nombre, Apellido, NumeroIdentificacion, EscuelaID
        FROM Profesores
        ORDER BY Apellido, Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerProfesorPorId
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 51007, 'No se encontró un profesor con el ID proporcionado.', 1;

        SELECT ProfesorID, Nombre, Apellido, NumeroIdentificacion, EscuelaID
        FROM Profesores
        WHERE ProfesorID = @ProfesorID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO


