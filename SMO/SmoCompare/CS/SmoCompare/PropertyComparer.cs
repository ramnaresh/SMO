/*=====================================================================

  File:      PropertyComparer.cs
  Summary:   Compares properties of the objects.
  Date:         08-20-2004

---------------------------------------------------------------------

  This file is part of the Microsoft SQL Server Code Samples.
  Copyright (C) Microsoft Corporation.  All rights reserved.

  This source code is intended only as a supplement to Microsoft
  Development Tools and/or on-line documentation.  See these other
  materials for detailed information regarding Microsoft code samples.

  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
  PARTICULAR PURPOSE.
=======================================================================*/

#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

#endregion

namespace Microsoft.Samples.SqlServer
{
    public class PropertyComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            PropertyInfo pi1 = x as PropertyInfo;
            PropertyInfo pi2 = y as PropertyInfo;

            if (pi1 == null && pi2 == null)
            {
                return 0;
            }

            if (pi1 == null)
            {
                return 1;
            }

            if (pi2 == null)
            {
                return -1;
            }

            return pi1.Name.CompareTo(pi2.Name);
        }
    }
}
