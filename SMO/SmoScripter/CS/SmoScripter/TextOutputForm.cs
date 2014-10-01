/*============================================================================
  File:    TextOutputForm.cs

  Summary: Implements a sample text output form in C#.

  Date:    September 30, 2005
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
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Microsoft.Samples.SqlServer
{
    public partial class TextOutputForm : Form
    {
        public TextOutputForm(StringCollection strings)
        {
            InitializeComponent();

            if (strings != null)
            {
                foreach (string str in strings)
                {
                    OutputTextBox.AppendText(str + Environment.NewLine);
                }

                OutputTextBox.ScrollToCaret();
            }
        }

        public string OutputText
        {
            get
            {
                return OutputTextBox.Text;
            }
            set
            {
                OutputTextBox.Text = value;
            }
        }

    }
}