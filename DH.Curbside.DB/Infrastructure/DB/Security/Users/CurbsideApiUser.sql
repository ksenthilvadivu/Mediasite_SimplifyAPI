---this is temp during development, for release remove this script n request DBA to setit up

--SQL server login with SQL authentication type
CREATE LOGIN CurbsideApiUser 
	WITH PASSWORD = N'dnj{uemaurt:np.gObzGyjk|msFT7_&#$!~<RyHskqzzf0sr'
GO

--SQL user mapped from server login
CREATE USER CurbsideApiUser
	FOR LOGIN CurbsideApiUser
	WITH DEFAULT_SCHEMA = dbo

GO

GRANT CONNECT TO CurbsideApiUser
GO
