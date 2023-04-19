IF EXISTS (SELECT 1 FROM [sys].[objects] WHERE [type] = N'P' AND [name] = N'p_ridaveiSettings_AddOrUpdate')
	DROP PROCEDURE [dbo].[p_ridaveiSettings_AddOrUpdate]
GO

CREATE PROCEDURE [dbo].[p_ridaveiSettings_AddOrUpdate]
	@DictionaryNamePar NVARCHAR(64),
	@KeyNamePar NVARCHAR(64),
	@ValuePar NVARCHAR(1024)
AS
BEGIN
	MERGE [dbo].[ridaveiSettings] AS [trg]
	USING (
		SELECT @DictionaryNamePar AS [DictionaryName], @KeyNamePar AS [KeyName], @ValuePar AS [Value]
	) AS [src]
	ON ([trg].[DictionaryName] = [src].[DictionaryName] AND [trg].[KeyName] = [src].[KeyName])
	WHEN MATCHED THEN
	UPDATE SET [Value] = [src].[Value]
	WHEN NOT MATCHED THEN
	INSERT ([DictionaryName], [KeyName], [Value])
	VALUES ([src].[DictionaryName], [src].[KeyName], [src].[Value]);
END
GO
