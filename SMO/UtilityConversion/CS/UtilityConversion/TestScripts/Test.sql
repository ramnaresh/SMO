/*****************************************************************************
-- 
-- Tests the sample SQLCLR Assembly in C#.
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

-- Examples for queries that exercise different SQL objects implemented by this assembly

PRINT 'dbo.HexStringToInt32 tests';

SELECT dbo.HexStringToInt32('0x00');
SELECT dbo.HexStringToInt32('0xFF');
SELECT dbo.HexStringToInt32('0xFF00');
SELECT dbo.HexStringToInt32('0xFFff');
SELECT dbo.HexStringToInt32('0xFFFFFF');
SELECT dbo.HexStringToInt32('0x7FFFFFFF');
SELECT dbo.HexStringToInt32('0x80000000');
SELECT dbo.HexStringToInt32('0xFFFFFFFFFF');  -- Overflow
SELECT dbo.HexStringToInt32(NULL);  -- Not allowed

PRINT 'dbo.StringToInt32 tests';

SELECT dbo.StringToInt32('255');
SELECT dbo.StringToInt32('32768');
SELECT dbo.StringToInt32('2147483647');
SELECT dbo.StringToInt32('2147483648');  -- Overflow
SELECT dbo.StringToInt32('-2147483648');
SELECT dbo.StringToInt32('-2147483649');  -- Overflow
SELECT dbo.StringToInt32(NULL);  -- Not allowed

PRINT 'dbo.BinaryToString tests';

SELECT dbo.BinaryToString(0x00009);
SELECT dbo.BinaryToString(0x0123456789);
SELECT dbo.BinaryToString(0x9090989389998def9898989);
SELECT dbo.BinaryToString(NULL); -- Not allowed
