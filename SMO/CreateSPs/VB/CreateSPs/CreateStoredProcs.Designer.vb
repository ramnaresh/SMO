Partial Public Class CreateStoredProcedures
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CreateStoredProcedures))
        Me.DropOnlyCheckBox = New System.Windows.Forms.CheckBox
        Me.PrefixTextBox = New System.Windows.Forms.TextBox
        Me.ConnectedPanel = New System.Windows.Forms.Panel
        Me.PrefixLabel = New System.Windows.Forms.Label
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.ExecuteButton = New System.Windows.Forms.Button
        Me.DatabaseLabel = New System.Windows.Forms.Label
        Me.sbrStatus = New System.Windows.Forms.StatusBar
        Me.ScriptTextBox = New System.Windows.Forms.TextBox
        Me.ConnectedPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'DropOnlyCheckBox
        '
        resources.ApplyResources(Me.DropOnlyCheckBox, "DropOnlyCheckBox")
        Me.DropOnlyCheckBox.Name = "DropOnlyCheckBox"
        '
        'PrefixTextBox
        '
        resources.ApplyResources(Me.PrefixTextBox, "PrefixTextBox")
        Me.PrefixTextBox.Name = "PrefixTextBox"
        '
        'ConnectedPanel
        '
        resources.ApplyResources(Me.ConnectedPanel, "ConnectedPanel")
        Me.ConnectedPanel.Controls.Add(Me.DropOnlyCheckBox)
        Me.ConnectedPanel.Controls.Add(Me.PrefixTextBox)
        Me.ConnectedPanel.Controls.Add(Me.PrefixLabel)
        Me.ConnectedPanel.Controls.Add(Me.DatabasesComboBox)
        Me.ConnectedPanel.Controls.Add(Me.ExecuteButton)
        Me.ConnectedPanel.Controls.Add(Me.DatabaseLabel)
        Me.ConnectedPanel.Name = "ConnectedPanel"
        '
        'PrefixLabel
        '
        resources.ApplyResources(Me.PrefixLabel, "PrefixLabel")
        Me.PrefixLabel.Name = "PrefixLabel"
        '
        'DatabasesComboBox
        '
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'ExecuteButton
        '
        resources.ApplyResources(Me.ExecuteButton, "ExecuteButton")
        Me.ExecuteButton.Name = "ExecuteButton"
        '
        'DatabaseLabel
        '
        resources.ApplyResources(Me.DatabaseLabel, "DatabaseLabel")
        Me.DatabaseLabel.Name = "DatabaseLabel"
        '
        'sbrStatus
        '
        resources.ApplyResources(Me.sbrStatus, "sbrStatus")
        Me.sbrStatus.Name = "sbrStatus"
        '
        'ScriptTextBox
        '
        resources.ApplyResources(Me.ScriptTextBox, "ScriptTextBox")
        Me.ScriptTextBox.Name = "ScriptTextBox"
        '
        'CreateStoredProcedures
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ConnectedPanel)
        Me.Controls.Add(Me.sbrStatus)
        Me.Controls.Add(Me.ScriptTextBox)
        Me.Name = "CreateStoredProcedures"
        Me.ConnectedPanel.ResumeLayout(False)
        Me.ConnectedPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Friend WithEvents DropOnlyCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents PrefixTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ConnectedPanel As System.Windows.Forms.Panel
    Friend WithEvents PrefixLabel As System.Windows.Forms.Label
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Friend WithEvents DatabaseLabel As System.Windows.Forms.Label
    Friend WithEvents sbrStatus As System.Windows.Forms.StatusBar
    Friend WithEvents ScriptTextBox As System.Windows.Forms.TextBox

End Class
