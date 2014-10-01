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

-- Create master database key
USE master
-- Enter your password to create the master database key
CREATE MASTER KEY ENCRYPTION BY PASSWORD = ''
GO

-- Create service database key
USE ssb_ConfigurationSample
-- Enter your password to create the master database key
CREATE MASTER KEY ENCRYPTION BY PASSWORD = ''
GO