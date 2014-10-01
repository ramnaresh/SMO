/*============================================================================
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

using System;
using System.Data;

namespace Microsoft.Samples.SqlServer
{
    public interface IConfiguration
    {
        string FullName
        {
            get;
        }

        string Name
        {
            get;
            set;
        }

        Uri Urn
        {
            get;
            set;
        }

        string SqlScript
        {
            get;
        }


        void Create();

        void Drop();

        void Alter();

        string Export(string fileName);

        void Import(string fileName);

        DataSet Query();
    }
}
