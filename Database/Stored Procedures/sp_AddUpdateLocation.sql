USE [POC1]
GO

IF OBJECT_ID (N'dbo.SP_AddUpdateLocation', N'P') IS NOT NULL DROP PROCEDURE dbo.SP_AddUpdateLocation;
GO

/*
Performs a t-sql merge to either add or update a location record based on a user id
and associated lat/long
*/

CREATE PROCEDURE [dbo].[SP_AddUpdateLocation]
    @UserID bigint,
    @Latitude numeric(18,15),
    @Longitude numeric(18,15),
	@Accuracy numeric(6) = 0,
	@TimeStamp numeric(13) = 0,
	@TotalCount int OUTPUT
AS

SET NOCOUNT ON
BEGIN TRY

DECLARE @g geography = geography::STPointFromText('POINT(' + CAST(@Longitude AS VARCHAR(20)) + ' ' + CAST(@Latitude AS VARCHAR(20)) + ')', 4326);

MERGE [dbo].[Location] AS [Target]
USING (SELECT @UserID, @Latitude, @Longitude, @Accuracy, @TimeStamp)
   AS [Source] ([UserID], [Latitude], [Longitude], [Accuracy], [TimeStamp] )
ON [Target].[UserID] = [Source].[UserID]

WHEN MATCHED THEN
    UPDATE SET	[Latitude] = [Source].[Latitude], 
				[Longitude] = [Source].[Longitude],
				[Accuracy] = [Source].[Accuracy],
				[TimeStamp] = [Source].[TimeStamp],
				[SpatialLocation] = @g
WHEN NOT MATCHED THEN
    INSERT ( [UserID], [Latitude], [Longitude], [Accuracy], [TimeStamp], [SpatialLocation] )
    VALUES ( [Source].[UserID], [Source].[Latitude], [Source].[Longitude], [Source].[Accuracy], [Source].[TimeStamp], @g);

SET @TotalCount = @@ROWCOUNT;

END TRY	

BEGIN CATCH

DECLARE @ErrorMessage NVARCHAR(MAX)
  DECLARE @ErrorSeverity INT
  DECLARE @ErrorState INT
  SELECT
    @ErrorMessage = ERROR_MESSAGE(),
    @ErrorSeverity = ERROR_SEVERITY(),
    @ErrorState = ERROR_STATE()
  RAISERROR (@ErrorMessage, -- Message text.
    @ErrorSeverity, -- Severity.
    @ErrorState) -- State.

END CATCH;