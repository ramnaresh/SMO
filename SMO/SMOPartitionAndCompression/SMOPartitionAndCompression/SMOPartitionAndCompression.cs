/*============================================================================
  File:    SMOPartitionAndCompression.cs 

  Summary: Implements a sample code demonstrate the partition and compression feature using SMO in C#.

  Date:    June 25, 2008
------------------------------------------------------------------------------
  This file is part of the Microsoft SQL Server Code Samples.

  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
============================================================================*/

#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//SMO
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using System.Globalization;
#endregion

namespace SMOPartitionAndCompression
{
    class SMOPartitionAndCompression
    {
        static void Main()
        {
            Server server = new Server(@".");
            string dbName = @"SMOPartitionAndCompressionSampleDB";
            Database db = new Database(server, dbName);
            db.Create();

            db.FileGroups.Add(new FileGroup(db, "TABLE_PART1_FG"));
            db.FileGroups.Add(new FileGroup(db, "TABLE_PART2_FG"));
            db.FileGroups.Add(new FileGroup(db, "TABLE_PART3_FG"));
            db.FileGroups.Add(new FileGroup(db, "TABLE_PART4_FG"));
            db.FileGroups.Add(new FileGroup(db, "TABLE_PART5_FG"));
            db.FileGroups.Add(new FileGroup(db, "TABLE_PART6_FG"));


            string temp_dir = server.Information.MasterDBPath;
            //db.FileGroups[0] is primary
            db.FileGroups[1].Files.Add(new DataFile(db.FileGroups[1], "tbl_datafile1", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_1.mdf", temp_dir)));
            db.FileGroups[2].Files.Add(new DataFile(db.FileGroups[2], "tbl_datafile2", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_2.mdf", temp_dir)));
            db.FileGroups[3].Files.Add(new DataFile(db.FileGroups[3], "tbl_datafile3", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_3.mdf", temp_dir)));
            db.FileGroups[4].Files.Add(new DataFile(db.FileGroups[4], "tbl_datafile4", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_4.mdf", temp_dir)));
            db.FileGroups[5].Files.Add(new DataFile(db.FileGroups[5], "tbl_datafile5", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_5.mdf", temp_dir)));
            db.FileGroups[6].Files.Add(new DataFile(db.FileGroups[6], "tbl_datafile6", string.Format(CultureInfo.InvariantCulture, "{0}\\PPSampledb_6.mdf", temp_dir)));

            db.Alter();

            PartitionFunction tbl_pf = new PartitionFunction(db, "MyTablePartitionFunction");
            tbl_pf.PartitionFunctionParameters.Add(new PartitionFunctionParameter(tbl_pf, DataType.Int));
            tbl_pf.RangeType = RangeType.Left;
            tbl_pf.RangeValues = new object[] { 5000, 10000, 15000, 20000, 25000, 30000 };
            tbl_pf.Create();
            PartitionScheme tbl_ps = new PartitionScheme(db, "MyTablePartitionScheme");
            tbl_ps.PartitionFunction = "MyTablePartitionFunction";
            tbl_ps.FileGroups.Add("PRIMARY");
            tbl_ps.FileGroups.Add("TABLE_PART1_FG");
            tbl_ps.FileGroups.Add("TABLE_PART2_FG");
            tbl_ps.FileGroups.Add("TABLE_PART3_FG");
            tbl_ps.FileGroups.Add("TABLE_PART4_FG");
            tbl_ps.FileGroups.Add("TABLE_PART5_FG");
            tbl_ps.FileGroups.Add("TABLE_PART6_FG");
            tbl_ps.Create();

            Table table = new Table(db, "MyTable");
            table.Columns.Add(new Column(table, "col1", DataType.Int));
            table.Columns.Add(new Column(table, "col2", DataType.Int));
            table.PartitionScheme = "MyTablePartitionScheme";
            table.PartitionSchemeParameters.Add(new PartitionSchemeParameter(table, "col1"));

            //Add PhysicalPartition objects for the partitions on which you like to apply compression.

            //Make Partition 5 as Page compressed
            table.PhysicalPartitions.Add(new PhysicalPartition(table, 5, DataCompressionType.Page));

            //Make Partition 6 and 7 Row compressed
            table.PhysicalPartitions.Add(new PhysicalPartition(table, 6, DataCompressionType.Row));
            table.PhysicalPartitions.Add(new PhysicalPartition(table, 7, DataCompressionType.Row));
           
            table.Create();

          
        }
    }
}
