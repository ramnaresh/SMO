/*=====================================================================

  File:      PropertyAndType.cs
  Summary:   Property and Type data type
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

#endregion

namespace Microsoft.Samples.SqlServer
{
    /// <summary>
    /// Pair of property name and Type
    /// </summary>
    public struct PropertyAndType
    {
        private string propName;
        private string propType;

        public PropertyAndType(string propertyName, string type)
        {
            this.propName = propertyName;
            this.propType = type;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null
                || this.GetType() != obj.GetType())
            {
                return false;
            }

            PropertyAndType pt = (PropertyAndType)obj;

            return (this.propName == pt.PropertyName)
                && (this.propType == pt.Type);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public static bool operator ==(PropertyAndType leftHandSide, PropertyAndType rightHandSide)
        {
            return (leftHandSide.propName == rightHandSide.propName)
                && (leftHandSide.propType == rightHandSide.propType);
        }

        public static bool operator !=(PropertyAndType leftHandSide, PropertyAndType rightHandSide)
        {
            return !(leftHandSide == rightHandSide);
        }

        public string PropertyName
        {
            get
            {
                return propName;
            }

            set
            {
                this.propName = value;
            }
        }

        public string Type
        {
            get
            {
                return this.propType;
            }

            set
            {
                this.propType = value;
            }
        }
    }
}
