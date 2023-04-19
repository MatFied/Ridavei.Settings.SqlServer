IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.f_ridaveiSettings_GetAllValues') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[f_ridaveiSettings_GetAllValues]
GO

CREATE FUNCTION [dbo].[f_ridaveiSettings_GetAllValues] (@DictionaryNamePar NVARCHAR(64))
RETURNS @AllValues TABLE
(
    [KeyName] NVARCHAR(64) PRIMARY KEY NOT NULL,
    [Value] NVARCHAR(1024) NOT NULL
)
AS
BEGIN
	INSERT INTO @AllValues ([KeyName], [Value])
	SELECT [KeyName], [Value]
	FROM [dbo].[ridaveiSettings]
	WHERE [DictionaryName] = @DictionaryNamePar;

    RETURN
END;
GO

IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'dbo.f_ridaveiSettings_GetValue') AND type IN (N'FN', N'IF', N'TF', N'FS', N'FT'))
	DROP FUNCTION [dbo].[f_ridaveiSettings_GetValue]
GO

CREATE FUNCTION [dbo].[f_ridaveiSettings_GetValue] (@DictionaryNamePar NVARCHAR(64), @KeyNamePar NVARCHAR(64))
RETURNS NVARCHAR(1024)
AS
BEGIN
	DECLARE @Result NVARCHAR(1024) = (SELECT [Value] FROM [dbo].[ridaveiSettings] WHERE [DictionaryName] = @DictionaryNamePar AND [KeyName] = @KeyNamePar);

    RETURN @Result;
END;
GO
