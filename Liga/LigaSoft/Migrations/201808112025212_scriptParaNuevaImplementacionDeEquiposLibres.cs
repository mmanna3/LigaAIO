namespace LigaSoft.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class scriptParaNuevaImplementacionDeEquiposLibres : DbMigration
    {
        public override void Up()
        {
			Sql(@"	DECLARE @fechaId int
					DECLARE @equipoLibreId int

						DECLARE MiCursor CURSOR LOCAL FAST_FORWARD FOR
						select id from fecha
						where EquipoLibreId <> 0


					OPEN MiCursor
					FETCH NEXT FROM MiCursor INTO @fechaId
					WHILE @@FETCH_STATUS = 0 BEGIN

					SET @equipoLibreId = (SELECT equipoLibreId FROM fecha WHERE id = @fechaId)
					INSERT jornada(fechaid, localid, quedolibre)
					VALUES(@fechaId, @equipoLibreId, 1)

					FETCH NEXT FROM MiCursor INTO @fechaId
					END

					CLOSE MiCursor
					DEALLOCATE MiCursor
					GO");

			Sql(@"	Declare @JornadaID int;
					Declare @CategoriaID int;

					DECLARE Cur1 CURSOR FOR
						select id from jornada
						where VisitanteId is null and localid is not null
						and QuedoLibre = 1 and JuegaInterzonal = 0

					OPEN Cur1
					FETCH NEXT FROM Cur1 INTO @JornadaID;
					WHILE @@FETCH_STATUS = 0
					BEGIN
    

    
						DECLARE Cur2 CURSOR FOR
        						SELECT id FROM categoria
								WHERE torneoid in (
									SELECT torneoid FROM zona
									WHERE id in (
										SELECT zonaid FROM fecha
										WHERE id in (
											SELECT fechaid FROM jornada
											WHERE id = @JornadaID
											)
									)
								)
						OPEN Cur2;
						FETCH NEXT FROM Cur2 INTO @CategoriaID;
						WHILE @@FETCH_STATUS = 0
						BEGIN        
							INSERT partido (jornadaid, categoriaid, goleslocal, GolesVisitante)
							VALUES (@jornadaid, @categoriaid, 2, 0)
        
							FETCH NEXT FROM Cur2 INTO @CategoriaID;
						END;
						CLOSE Cur2;
						DEALLOCATE Cur2;
    
					FETCH NEXT FROM Cur1 INTO @JornadaID;
					END;
					CLOSE Cur1;
					DEALLOCATE Cur1;");
        }
        
        public override void Down()
        {
        }
    }
}
