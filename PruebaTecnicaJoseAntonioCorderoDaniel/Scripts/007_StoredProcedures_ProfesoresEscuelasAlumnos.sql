CREATE PROCEDURE ObtenerAlumnosInscritosPorProfesor
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 55001, 'El profesor especificado no existe.', 1;

        SELECT 
            A.AlumnoID,
            A.Nombre AS NombreAlumno,
            A.Apellido AS ApellidoAlumno,
            A.NumeroIdentificacion,
            E.EscuelaID,
            E.Nombre AS NombreEscuela
        FROM AlumnosProfesores AP
        INNER JOIN Alumnos A ON AP.AlumnoID = A.AlumnoID
        INNER JOIN Inscripciones I ON A.AlumnoID = I.AlumnoID
        INNER JOIN Escuelas E ON I.EscuelaID = E.EscuelaID
        WHERE AP.ProfesorID = @ProfesorID
        ORDER BY E.Nombre, A.Apellido, A.Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;
GO

CREATE PROCEDURE ObtenerEscuelasYAlumnosDeProfesor
    @ProfesorID INT
AS
BEGIN
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM Profesores WHERE ProfesorID = @ProfesorID)
            THROW 55002, 'El profesor especificado no existe.', 1;

        SELECT 
            E.EscuelaID,
            E.Nombre AS NombreEscuela,
            A.AlumnoID,
            A.Nombre AS NombreAlumno,
            A.Apellido AS ApellidoAlumno,
            A.NumeroIdentificacion
        FROM AlumnosProfesores AP
        INNER JOIN Alumnos A ON AP.AlumnoID = A.AlumnoID
        INNER JOIN Inscripciones I ON A.AlumnoID = I.AlumnoID
        INNER JOIN Escuelas E ON I.EscuelaID = E.EscuelaID
        WHERE AP.ProfesorID = @ProfesorID
        ORDER BY E.Nombre, A.Apellido, A.Nombre;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH
END;