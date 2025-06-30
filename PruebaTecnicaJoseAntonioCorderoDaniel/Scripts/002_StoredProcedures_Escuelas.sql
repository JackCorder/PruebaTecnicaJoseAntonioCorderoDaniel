-- Stored Procedures de Escuelas

CREATE PROCEDURE CrearEscuela
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(250)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF EXISTS (SELECT 1 FROM Escuelas WHERE Nombre = @Nombre)
            THROW 50001, 'Ya existe una escuela con ese nombre.', 1;

        INSERT INTO Escuelas (Nombre, Descripcion)
        VALUES (@Nombre, @Descripcion);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ActualizarEscuela
    @EscuelaID INT,
    @Nombre NVARCHAR(100),
    @Descripcion NVARCHAR(250)
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 50002, 'No se encontró una escuela con el ID proporcionado.', 1;

        -- Evita que dos escuelas tengan el mismo nombre (excepto la que se está actualizando)
        IF EXISTS (
            SELECT 1 FROM Escuelas
            WHERE Nombre = @Nombre AND EscuelaID <> @EscuelaID
        )
            THROW 50003, 'Ya existe otra escuela con ese nombre.', 1;

        UPDATE Escuelas
        SET Nombre = @Nombre,
            Descripcion = @Descripcion
        WHERE EscuelaID = @EscuelaID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE EliminarEscuela
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 50004, 'No se puede eliminar. La escuela no existe.', 1;

        DELETE FROM Escuelas
        WHERE EscuelaID = @EscuelaID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerEscuelas
AS
BEGIN
    BEGIN TRY
        SELECT EscuelaID, Nombre, Descripcion, Codigo
        FROM Escuelas
        ORDER BY Nombre
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END;
GO 

CREATE PROCEDURE ObtenerEscuelaPorId
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 50002, 'No se encontró una escuela con el ID proporcionado.', 1;

        SELECT EscuelaID, Nombre, Descripcion, Codigo
        FROM Escuelas
        WHERE EscuelaID = @EscuelaID
    END TRY
    BEGIN CATCH
        THROW
    END CATCH
END;
GO