CREATE DATABASE [bikes_bg]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'bikes_bg', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\bikes_bg.mdf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'bikes_bg_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\bikes_bg_log.ldf' , SIZE = 8192KB , FILEGROWTH = 65536KB )
GO

USE [bikes_bg]
GO
IF NOT EXISTS (SELECT name FROM sys.filegroups WHERE is_default=1 AND name = N'PRIMARY') ALTER DATABASE [bikes_bg] MODIFY FILEGROUP [PRIMARY] DEFAULT
GO
