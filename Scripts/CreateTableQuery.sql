IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'RidaveiSettings')
BEGIN
	CREATE TABLE [RidaveiSettings]
	(
		[DictionaryName] NVARCHAR(64) NOT NULL,
		[SettingsKey] NVARCHAR(64) NOT NULL,
		[SettingsValue] NVARCHAR(1024) NOT NULL,
		CONSTRAINT PK_RidaveiSettings PRIMARY KEY([DictionaryName], [SettingsKey])
	)
END
GO
