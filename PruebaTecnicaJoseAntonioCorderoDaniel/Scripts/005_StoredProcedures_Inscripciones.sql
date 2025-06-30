-- Stored Procedures de Inscripciones

CREATE PROCEDURE CrearInscripcion
    @AlumnoID INT,
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 53001, 'El alumno especificado no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 53002, 'La escuela especificada no existe.', 1;

        IF EXISTS (
            SELECT 1 FROM Inscripciones
            WHERE AlumnoID = @AlumnoID AND EscuelaID = @EscuelaID
        )
            THROW 53003, 'El alumno ya está inscrito en esa escuela.', 1;

        INSERT INTO Inscripciones (AlumnoID, EscuelaID)
        VALUES (@AlumnoID, @EscuelaID);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ActualizarInscripcion
    @InscripcionID INT,
    @AlumnoID INT,
    @EscuelaID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Inscripciones WHERE InscripcionID = @InscripcionID)
            THROW 53004, 'No se encontró la inscripción con el ID proporcionado.', 1;

        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 53005, 'El alumno especificado no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM Escuelas WHERE EscuelaID = @EscuelaID)
            THROW 53006, 'La escuela especificada no existe.', 1;

        -- Validar que no haya otra inscripción duplicada
        IF EXISTS (
            SELECT 1 FROM Inscripciones
            WHERE AlumnoID = @AlumnoID AND EscuelaID = @EscuelaID AND InscripcionID <> @InscripcionID
        )
            THROW 53007, 'Ya existe otra inscripción con ese alumno y escuela.', 1;

        UPDATE Inscripciones
        SET AlumnoID = @AlumnoID,
            EscuelaID = @EscuelaID
        WHERE InscripcionID = @InscripcionID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE EliminarInscripcion
    @InscripcionID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Inscripciones WHERE InscripcionID = @InscripcionID)
            THROW 53008, 'No se puede eliminar. La inscripción no existe.', 1;

        DELETE FROM Inscripciones
        WHERE InscripcionID = @InscripcionID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerInscripciones
AS
BEGIN
    BEGIN TRY
        SELECT I.InscripcionID,
               I.FechaInscripcion,
               A.AlumnoID,
               A.Nombre AS NombreAlumno,
               A.Apellido AS ApellidoAlumno,
               E.EscuelaID,
               E.Nombre AS NombreEscuela
        FROM Inscripciones I
        INNER JOIN Alumnos A ON I.AlumnoID = A.AlumnoID
        INNER JOIN Escuelas E ON I.EscuelaID = E.EscuelaID
        ORDER BY I.FechaInscripcion DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerInscripcionPorId
    @InscripcionID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Inscripciones WHERE InscripcionID = @InscripcionID)
            THROW 53009, 'No se encontró la inscripción con el ID proporcionado.', 1;

        SELECT I.InscripcionID,
               I.FechaInscripcion,
               A.AlumnoID,
               A.Nombre AS NombreAlumno,
               A.Apellido AS ApellidoAlumno,
               E.EscuelaID,
               E.Nombre AS NombreEscuela
        FROM Inscripciones I
        INNER JOIN Alumnos A ON I.AlumnoID = A.AlumnoID
        INNER JOIN Escuelas E ON I.EscuelaID = E.EscuelaID
        WHERE I.InscripcionID = @InscripcionID;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
