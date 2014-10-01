//============================================================================
//  File:    TextForm.cs
//
//  Summary: Implements a text display form in C#.
//
//  Date:    May 20, 2005
//------------------------------------------------------------------------------
//  This file is part of the Microsoft SQL Server Code Samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
//============================================================================

using System;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer
{
    public partial class TextForm : Form
    {
        public TextForm()
        {
            InitializeComponent();
        }

        public void DisplayText(string display)
        {
            this.TextBox.Text = display;
        }

        public void DisplayText(StringCollection sc)
        {
            if (sc == null)
            {
                throw new ArgumentNullException("sc");
            }

            this.TextBox.Text = String.Empty;
            foreach (string s in sc)
            {
                this.TextBox.AppendText(s + Environment.NewLine);
            }
        }

        public void DisplayText(Microsoft.SqlServer.Management.Smo.PropertyCollection propertyCollection)
        {
            if (propertyCollection == null)
            {
                throw new ArgumentNullException("propertyCollection");
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Microsoft.SqlServer.Management.Smo.Property prop in propertyCollection)
            {
                if (prop.Retrieved)
                {
                    sb.AppendFormat("{0,-20} {1}", prop.Name, prop.Value.ToString());
                    sb.Append(Environment.NewLine);
                }
            }

            this.TextBox.Text = sb.ToString();
        }
    }
}
