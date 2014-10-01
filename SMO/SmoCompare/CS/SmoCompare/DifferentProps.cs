/*=====================================================================

  File:      DifferentProps.cs
  Summary:   Different Properties class to contain the results of property comparisons.
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
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer;
using Microsoft.SqlServer.Management.Sdk.Sfc;

#endregion

namespace Microsoft.Samples.SqlServer
{
    public struct DifferentProperties
    {
        private Urn objUrn1;
        private Urn objUrn2;
        private String objPropertyName;
        private String objValue1;
        private String objValue2;

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if (obj == null || this.GetType() != obj.GetType())
                return false;

            DifferentProperties dp = (DifferentProperties)obj;
            return (this.objPropertyName == dp.objPropertyName)
                && (this.objUrn1 == dp.objUrn1)
                && (this.objUrn2 == dp.objUrn2)
                && (this.objValue1 == dp.objValue1)
                && (this.objValue2 == dp.objValue2);
        }

        public override int GetHashCode()
        {
            return this.GetHashCode();
        }

        public static bool operator ==(DifferentProperties leftHandSide, DifferentProperties rightHandSide)
        {
            return (leftHandSide.objPropertyName == rightHandSide.objPropertyName)
                && (leftHandSide.objUrn1 == rightHandSide.objUrn1)
                && (leftHandSide.objUrn2 == rightHandSide.objUrn2)
                && (leftHandSide.objValue1 == rightHandSide.objValue1)
                && (leftHandSide.objValue2 == rightHandSide.objValue2);
        }

        public static bool operator !=(DifferentProperties leftHandSide, DifferentProperties rightHandSide)
        {
            return !(leftHandSide == rightHandSide);
        }

        /// <summary>
        /// First object Urn
        /// </summary>
        /// <value></value>
        public Urn Urn1
        {
            get
            {
                return this.objUrn1;
            }

            set
            {
                this.objUrn1 = value;
            }
        }

        /// <summary>
        /// Second object Urn
        /// </summary>
        /// <value></value>
        public Urn Urn2
        {
            get
            {
                return this.objUrn2;
            }

            set
            {
                this.objUrn2 = value;
            }
        }

        /// <summary>
        /// Property name
        /// </summary>
        /// <value></value>
        public String PropertyName
        {
            get
            {
                return this.objPropertyName;
            }

            set
            {
                this.objPropertyName = value;
            }
        }

        /// <summary>
        /// First object property value 
        /// </summary>
        /// <value></value>
        public String ObjectValue1
        {
            get
            {
                return this.objValue1;
            }

            set
            {
                this.objValue1 = value;
            }
        }

        /// <summary>
        /// Second object property value 
        /// </summary>
        /// <value></value>
        public String ObjectValue2
        {
            get
            {
                return this.objValue2;
            }

            set
            {
                this.objValue2 = value;
            }
        }
    }
}
