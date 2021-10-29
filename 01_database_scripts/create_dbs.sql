USE master;
GO

-- **************************** LOGIN ****************************


-- Create a server login for the database

-- Check that the login name didnt use before
IF NOT EXISTS (
        -- Search the name in the server
        SELECT name
        FROM [sys].[server_principals]
        WHERE [type] = N'S' AND  [name] = N'TuRutaUN_admin'
    )
BEGIN
    -- If not exist the login, execute the create sentence
    CREATE LOGIN [TuRutaUN_admin] WITH PASSWORD='dummy', CHECK_POLICY = OFF;
END
GO

-- **************************** DB ****************************

-- Check that the database name didnt use before
IF NOT EXISTS(
    -- Search the name in the server
    SELECT name
    FROM master.dbo.sysdatabases
    WHERE name=N'TuRutaUN'
)
BEGIN
    -- Create database
    CREATE DATABASE [TuRutaUN]
END    
GO

-- Check that the database name didnt use before
IF NOT EXISTS(
    -- Search the name in the server
    SELECT name
    FROM master.dbo.sysdatabases
    WHERE name=N'ExternalLoginDB'
)
BEGIN
    -- Create database
    CREATE DATABASE [ExternalLoginDB]
END    
GO


-- **************************** DB OWNER ****************************

-- Change the database owner of TuRutaUN
USE [TuRutaUN]
GO
SP_changedbowner [TuRutaUN_admin]
GO

-- Change the database owner of ExternalLoginDB
USE [ExternalLoginDB]
GO
SP_changedbowner [TuRutaUN_admin]
GO


-- **************************** PERMITIONS ****************************

-- Deny view for the databases where the user does not are the owner

USE master
GO
DENY VIEW ANY DATABASE TO [TuRutaUN_admin];
GO

-- **************************** CHECK SOME INFO ****************************

-- Show the username from the owner of TuRutaUN database
use master
Go
select SUSER_SNAME(owner_sid) from sys.databases where name = 'TuRutaUN'
go