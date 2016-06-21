USE [POC1]
GO


IF OBJECT_ID (N'dbo.SP_GetNearestNeighbors', N'P') IS NOT NULL DROP PROCEDURE dbo.SP_GetNearestNeighbors;
GO

/*
Performs a t-sql spatial query to pull back all points in the location database from a point specified as lat/long input parms
within a radius also passed in as an input parm
*/

CREATE PROCEDURE [dbo].[SP_GetNearestNeighbors]
    @Latitude numeric(18,15),
    @Longitude numeric(18,15),
	@Radius int = 0
AS

SET NOCOUNT ON
BEGIN TRY

DECLARE @g geography = geography::STPointFromText('POINT(' + CAST(@Longitude AS VARCHAR(20)) + ' ' + CAST(@Latitude AS VARCHAR(20)) + ')', 4326);

SELECT  [l].[ID], [l].[UserID] UserID, [l].[UserID]  UserID_nn, 
	[l].[Latitude], [l].[Longitude], [l].[Accuracy], [l].[TimeStamp] [Timestamp], [l].[SpatialLocation].STDistance(@g) Distance
FROM [dbo].[Location] l
WHERE [l].[SpatialLocation].STDistance(@g) IS NOT NULL
AND [l].[SpatialLocation].STDistance(@g)  <= @Radius
ORDER BY [l].[SpatialLocation].STDistance(@g);


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