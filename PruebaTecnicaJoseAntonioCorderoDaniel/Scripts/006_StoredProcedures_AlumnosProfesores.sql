-- Stored Procedures de relacion AlumnosProfesores

CREATE PROCEDURE AsignarProfesorAAlumno
    @AlumnoID INT,
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 54001, 'El alumno especificado no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 54002, 'El profesor especificado no existe.', 1;

        IF EXISTS (
            SELECT 1 FROM AlumnosProfesores
            WHERE AlumnoID = @AlumnoID AND ProfesorID = @ProfesorID
        )
            THROW 54003, 'El alumno ya está asignado a ese profesor.', 1;

        INSERT INTO AlumnosProfesores (AlumnoID, ProfesorID)
        VALUES (@AlumnoID, @ProfesorID);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE EliminarAsignacionAlumnoProfesor
    @AlumnoID INT,
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (
            SELECT 1 FROM AlumnosProfesores
            WHERE AlumnoID = @AlumnoID AND ProfesorID = @ProfesorID
        )
            THROW 54004, 'La asignación especificada no existe.', 1;

        DELETE FROM AlumnosProfesores
        WHERE AlumnoID = @AlumnoID AND ProfesorID = @ProfesorID;

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerProfesoresDeAlumno
    @AlumnoID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Alumnos WHERE AlumnoID = @AlumnoID)
            THROW 54005, 'El alumno especificado no existe.', 1;

        SELECT P.ProfesorID, P.Nombre, P.Apellido, P.NumeroIdentificacion
        FROM AlumnosProfesores AP
        INNER JOIN Profesores P ON AP.ProfesorID = P.ProfesorID
        WHERE AP.AlumnoID = @AlumnoID
        ORDER BY P.Apellido, P.Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerAlumnosDeProfesor
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 54006, 'El profesor especificado no existe.', 1;

        SELECT A.AlumnoID, A.Nombre, A.Apellido, A.NumeroIdentificacion
        FROM AlumnosProfesores AP
        INNER JOIN Alumnos A ON AP.AlumnoID = A.AlumnoID
        WHERE AP.ProfesorID = @ProfesorID
        ORDER BY A.Apellido, A.Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO
