/*****************************************************************************
-- 
-- Creates sample backup devices and backs up the AdventureWorks database into 
-- each one for the VerifyBackup SMO sample.
-- 
-- January 27, 2005
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

USE [master];
GO

IF EXISTS (SELECT * FROM sys.backup_devices WHERE name = N'VerifyBackupDevice1')
EXEC master.dbo.sp_dropdevice @logicalname = N'VerifyBackupDevice1';
GO

IF EXISTS (SELECT * FROM sys.backup_devices WHERE name = N'VerifyBackupDevice2')
EXEC master.dbo.sp_dropdevice @logicalname = N'VerifyBackupDevice2';
GO

-- Create backup file path
DECLARE @SqlPath nvarchar(256);

SELECT @SqlPath = SUBSTRING([physical_name], 1, CHARINDEX(N'master.mdf', LOWER([physical_name])) - 1)
FROM [master].[sys].[master_files] 
WHERE [database_id] = 1 
    AND [file_id] = 1;

SELECT @SqlPath = REPLACE(@SqlPath, '\DATA\', '\Backup\');

PRINT @SqlPath;

PRINT '*** Create first backup device ***'

EXECUTE (N'EXEC master.dbo.sp_addumpdevice 
    @devtype = N''disk'', 
    @logicalname = N''VerifyBackupDevice1'', 
    @physicalname = ''' + @SqlPath + N'VerifyBackupDevice1.bak'';');

PRINT '*** Create second backup device ***'

EXECUTE (N'EXEC master.dbo.sp_addumpdevice 
    @devtype = N''disk'', 
    @logicalname = N''VerifyBackupDevice2'', 
    @physicalname = ''' + @SqlPath + N'VerifyBackupDevice2.bak'';');

SELECT * FROM [sys].[backup_devices];

PRINT '*** Backup AdventureWorks database to the first backup device ***'

BACKUP DATABASE [AdventureWorks] 
    TO [VerifyBackupDevice1] 
    WITH NOFORMAT, 
    NOINIT, 
    NAME = N'AdventureWorks-Full Database Backup', 
    DESCRIPTION = N'DESC AdventureWorks-Full Database Backup', 
    SKIP, NOREWIND, NOUNLOAD,  STATS = 25;

PRINT '*** Backup master database to the second backup device ***'

BACKUP DATABASE [master] 
    TO [VerifyBackupDevice2] 
    WITH NOFORMAT, 
    NOINIT, 
    NAME = N'master-Full Database Backup', 
    SKIP, NOREWIND, NOUNLOAD,  STATS = 20;

PRINT '*** Done ***'
GO
