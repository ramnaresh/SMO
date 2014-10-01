/*=====================================================================

  File:      ProperyAndObject.cs
  Summary:   Property and Object data type
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
    /// Pair of property name and object
    /// </summary>
    public struct PropertyAndObject
    {
        private string propertyName;
        private Uri urn;

        public PropertyAndObject(string propertyName, Uri urn)
        {
            this.propertyName = propertyName;
            this.urn = urn;
        }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null
                || this.GetType() != obj.GetType())
            {
                return false;
            }

            PropertyAndObject po = (PropertyAndObject)obj;
            return (this.propertyName == po.PropertyName) 
                && (this.urn == po.Urn);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public static bool operator == (PropertyAndObject leftHandSide, PropertyAndObject rightHandSide)
        {
            return (leftHandSide.propertyName == rightHandSide.propertyName) 
                && (leftHandSide.urn == rightHandSide.urn);
        }

        public static bool operator != (PropertyAndObject leftHandSide, PropertyAndObject rightHandSide)
        {
            return !(leftHandSide == rightHandSide);
        }

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }

            set
            {
                this.propertyName = value;
            }
        }

        public Uri Urn
        {
            get
            {
                return this.urn;
            }

            set
            {
                this.urn = value;
            }
        }
    }
}
