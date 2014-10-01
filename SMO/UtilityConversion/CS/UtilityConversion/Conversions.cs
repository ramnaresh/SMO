/*============================================================================
  File:    Conversions.cs 

  Summary: Implements a sample CLR Assembly in C# for use with the SQLCLR.

  Date:    March 14, 2005
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
using System;
using System.Data.Sql;
using System.Data.SqlTypes;
using System.Text;

namespace Microsoft.Samples.SqlServer
{
    public sealed partial class Conversions
    {
        private Conversions()
        {
        }

        /// <summary>
        /// Returns the hexadecimal string representation of the given binary. 
        /// </summary>
        /// <param name="sb"></param>
        /// <returns></returns>
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlString BinaryToString(byte[] sb)
        {
            // If input is NULL then return NULL;
            if (sb == null)
            {
                return null;
            }

            int lensb = sb.Length;

            // Use StringBuilder instead of String for efficiency
            // Create a StringBuilder with enough capacity to hold the 
            // hexadecimal representation
            StringBuilder strb = new StringBuilder("0x", (lensb * 2) + 2);

            for (int i = 0; i < lensb; i++)
            {
                strb.Append(sb[i].ToString("X2",
                    System.Globalization.CultureInfo.InvariantCulture));
            }

            return (strb.ToString());
        }

        /// <summary>
        /// Returns the hexadecimal representation of the given binary.
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlInt32 HexStringToInt32(string ss)
        {
            // If input is NULL then return zero;
            if (ss == null)
            {
                return 0;
            }
            else
            {
                return (Convert.ToInt32(ss, 16));
            }
        }

        /// <summary>
        /// Returns the numeric equivalent of the hexadecimal representation.
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        [Microsoft.SqlServer.Server.SqlFunction]
        public static SqlInt32 StringToInt32(string ss)
        {
            // If input is NULL then return zero;
            if (ss == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(ss,
                    System.Globalization.CultureInfo.InvariantCulture);
            }
        }
    }
}