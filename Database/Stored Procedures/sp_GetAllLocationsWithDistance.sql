USE [POC1]
GO


IF OBJECT_ID (N'dbo.SP_GetAllLocationsWithDistance', N'P') IS NOT NULL DROP PROCEDURE dbo.SP_GetAllLocationsWithDistance;
GO

/*
Based on a UserID, pulls back all location records from the database with a distance calculation for each between the lat/long and 
the user id lat long.  If optional lat/long are supplied (i.e., user's "home location" or determined by mobile device
then use those for the distance calculation instead.
*/

CREATE PROCEDURE [dbo].[SP_GetAllLocationsWithDistance]
    @UserID bigint,
	@Latitude numeric (18,15) = NULL,
	@Longitude numeric (18,15) = NULL
AS

SET NOCOUNT ON
BEGIN TRY

IF (@Latitude IS NULL AND @Longitude IS NULL)
BEGIN
	SELECT @Latitude = l.Latitude, @Longitude =l.Longitude FROM dbo.Location l WHERE l.UserID = @UserID;
	--SELECT @Latitude, @Longitude;
END

DECLARE @g geography = geography::STPointFromText('POINT(' + CAST(@Longitude AS VARCHAR(20)) + ' ' + CAST(@Latitude AS VARCHAR(20)) + ')', 4326);



SELECT  [l].[ID], [l].[UserID] UserID, [l].[UserID]  UserID_nn, 
	[l].[Latitude], [l].[Longitude], [l].[Accuracy], [l].[TimeStamp] [Timestamp], [l].[SpatialLocation].STDistance(@g) Distance
FROM [dbo].[Location] l
WHERE [l].[SpatialLocation].STDistance(@g) IS NOT NULL
ORDER BY [l].[SpatialLocation].STDistance(@g);
/**/

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