DECLARE @tempTable TABLE
(
   id int
);

INSERT INTO @tempTable
SELECT Id FROM Activity

DECLARE @id int

DECLARE cur CURSOR FOR SELECT Id FROM @tempTable
OPEN cur

FETCH NEXT FROM cur INTO @id 

WHILE @@FETCH_STATUS = 0 BEGIN
		INSERT INTO [dbo].[ActivityVenue] ([Confirmed],[CreationTime],[ModificationTime],[ActivityId],[VenueId]) VALUES (1, GETDATE(), GETDATE(), @id, (SELECT venue_id from Activity where id = @id))
    FETCH NEXT FROM cur INTO @id
END

CLOSE cur    
DEALLOCATE cur