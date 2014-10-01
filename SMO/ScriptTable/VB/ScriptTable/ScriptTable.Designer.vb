Partial Public Class ScriptTable
    Inherits System.Windows.Forms.Form

    <System.Diagnostics.DebuggerNonUserCode()> _
    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptTable))
        Me.TablesComboBox = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.DependenciesCheckBox = New System.Windows.Forms.CheckBox
        Me.ScriptDropCheckBox = New System.Windows.Forms.CheckBox
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.ScriptTextBox = New System.Windows.Forms.RichTextBox
        Me.sbrStatus = New System.Windows.Forms.StatusBar
        Me.ScriptTableButton = New System.Windows.Forms.Button
        Me.DatabaseLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TablesComboBox
        '
        resources.ApplyResources(Me.TablesComboBox, "TablesComboBox")
        Me.TablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TablesComboBox.FormattingEnabled = True
        Me.TablesComboBox.Name = "TablesComboBox"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Name = "Label2"
        '
        'DependenciesCheckBox
        '
        resources.ApplyResources(Me.DependenciesCheckBox, "DependenciesCheckBox")
        Me.DependenciesCheckBox.Name = "DependenciesCheckBox"
        '
        'ScriptDropCheckBox
        '
        resources.ApplyResources(Me.ScriptDropCheckBox, "ScriptDropCheckBox")
        Me.ScriptDropCheckBox.Name = "ScriptDropCheckBox"
        '
        'DatabasesComboBox
        '
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'ScriptTextBox
        '
        resources.ApplyResources(Me.ScriptTextBox, "ScriptTextBox")
        Me.ScriptTextBox.Name = "ScriptTextBox"
        '
        'sbrStatus
        '
        resources.ApplyResources(Me.sbrStatus, "sbrStatus")
        Me.sbrStatus.Name = "sbrStatus"
        '
        'ScriptTableButton
        '
        resources.ApplyResources(Me.ScriptTableButton, "ScriptTableButton")
        Me.ScriptTableButton.BackColor = System.Drawing.SystemColors.Control
        Me.ScriptTableButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.ScriptTableButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ScriptTableButton.Name = "ScriptTableButton"
        Me.ScriptTableButton.UseVisualStyleBackColor = False
        '
        'DatabaseLabel
        '
        resources.ApplyResources(Me.DatabaseLabel, "DatabaseLabel")
        Me.DatabaseLabel.BackColor = System.Drawing.SystemColors.Control
        Me.DatabaseLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.DatabaseLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DatabaseLabel.Name = "DatabaseLabel"
        '
        'ScriptTable
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TablesComboBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.DependenciesCheckBox)
        Me.Controls.Add(Me.ScriptDropCheckBox)
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Controls.Add(Me.ScriptTextBox)
        Me.Controls.Add(Me.sbrStatus)
        Me.Controls.Add(Me.ScriptTableButton)
        Me.Controls.Add(Me.DatabaseLabel)
        Me.Name = "ScriptTable"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TablesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DependenciesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ScriptDropCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ScriptTextBox As System.Windows.Forms.RichTextBox
    Friend WithEvents sbrStatus As System.Windows.Forms.StatusBar
    Friend WithEvents ScriptTableButton As System.Windows.Forms.Button
    Friend WithEvents DatabaseLabel As System.Windows.Forms.Label

End Class
