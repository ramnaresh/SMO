/*****************************************************************************
-- 
-- Drops the sample SQLCLR Assembly in C#.
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

IF EXISTS (SELECT * FROM sys.server_principals WHERE [name] = 'UtilityConversion_Login')
DROP LOGIN UtilityConversion_Login;
GO

IF EXISTS (SELECT * FROM sys.asymmetric_keys WHERE [name] = 'UtilityConversion_Key')
DROP ASYMMETRIC KEY UtilityConversion_Key;
GO

USE [AdventureWorks];
GO