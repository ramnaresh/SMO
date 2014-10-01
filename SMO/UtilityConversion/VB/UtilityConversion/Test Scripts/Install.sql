/*****************************************************************************
-- 
-- Creates a sample SQLCLR Assembly in VB.NET.
-- 
-- July 11, 2005
*****************************************************************************/
/****************************************************************************
  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
*****************************************************************************/

USE [AdventureWorks];
GO

-- Drop functions
IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'BinaryToString')
DROP FUNCTION BinaryToString;
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'HexStringToInt32')
DROP FUNCTION HexStringToInt32;
GO

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_NAME = 'StringToInt32')
DROP FUNCTION StringToInt32;
GO

-- Drop assembly
IF EXISTS (SELECT * FROM sys.assemblies WHERE name = 'UtilityConversion')
DROP ASSEMBLY UtilityConversion;
GO

USE master
GO

IF EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'UnsafeSample_Login')
DROP LOGIN UnsafeSample_Login;
GO

IF EXISTS (SELECT * FROM sys.asymmetric_keys WHERE [name] = 'UnsafeSample_Key')
DROP ASYMMETRIC KEY UnsafeSample_Key;
GO

--Before we register the assembly to SQL Server, we must arrange for the appropriate permissions.
--Assemblies with unsafe or external_access permissions can only be registered and operate correctly
--if either the database trustworthy bit is set or if the assembly is signed with a key,
--that key is registered with SQL Server, a server principal is created from that key,
--and that principal is granted the external access or unsafe assembly permission.  We choose
--the latter approach as it is more granular, and therefore safer.  You should never
--register an assembly with SQL Server (especially with external_access or unsafe permissions) without
--thoroughly reviewing the source code of the assembly to make sure that its actions 
--do not pose an operational or security risk for your site.

DECLARE @SamplesPath nvarchar(1024);
-- You may need to modify the value of the this variable if you have installed the sample someplace other than the default location.
SELECT @SamplesPath = substring(physical_name, 1, 
  patindex('%Microsoft SQL Server%', physical_name) - 1) 
  + 'Microsoft SQL Server\100\Samples\' 
FROM master.sys.database_files 
WHERE name = 'master';


EXEC('CREATE ASYMMETRIC KEY UnsafeSample_Key FROM EXECUTABLE FILE = ''' + @SamplesPath + 'Engine\Programmability\SMO\UtilityConversion\VB\UtilityConversion\bin\UtilityConversion.dll'';');
CREATE LOGIN UnsafeSample_Login FROM ASYMMETRIC KEY UnsafeSample_Key
GRANT UNSAFE ASSEMBLY TO UnsafeSample_Login
GO

USE AdventureWorks
GO
-- Add the assembly which contains the CLR methods we want to invoke on the server.
DECLARE @SamplesPath nvarchar(1024);
-- You may need to modify the value of the this variable if you have installed the sample someplace other than the default location.
SELECT @SamplesPath = substring(physical_name, 1, 
  patindex('%Microsoft SQL Server%', physical_name) - 1) 
  + 'Microsoft SQL Server\100\Samples\' 
FROM master.sys.database_files 
WHERE name = 'master';

-- Register assembly
CREATE ASSEMBLY UtilityConversion 
FROM @SamplesPath + 'Engine\Programmability\SMO\UtilityConversion\VB\UtilityConversion\bin\UtilityConversion.dll'
WITH PERMISSION_SET = UNSAFE;
GO

-- Register function
CREATE FUNCTION HexStringToInt32(@in NVARCHAR(255))
RETURNS INT
EXTERNAL NAME UtilityConversion.[Microsoft.Samples.SqlServer.Conversions].HexStringToInt32;
GO

-- Register function
CREATE FUNCTION StringToInt32(@in NVARCHAR(255))
RETURNS INT
EXTERNAL NAME UtilityConversion.[Microsoft.Samples.SqlServer.Conversions].StringToInt32;
GO

-- Register function
CREATE FUNCTION BinaryToString(@in VARBINARY(4000))
RETURNS NVARCHAR(4000)
EXTERNAL NAME UtilityConversion.[Microsoft.Samples.SqlServer.Conversions].BinaryToString;
GO
