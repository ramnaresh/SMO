Partial Public Class BackupRestore
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(BackupRestore))
        Me.label4 = New System.Windows.Forms.Label
        Me.ControlsPanel = New System.Windows.Forms.Panel
        Me.BackupButton = New System.Windows.Forms.Button
        Me.RestoreButton = New System.Windows.Forms.Button
        Me.label2 = New System.Windows.Forms.Label
        Me.GetBackupFileButton = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.BackupFileTextBox = New System.Windows.Forms.TextBox
        Me.ResultsTextBox = New System.Windows.Forms.TextBox
        Me.statusBar1 = New System.Windows.Forms.StatusBar
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.ControlsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'label4
        '
        resources.ApplyResources(Me.label4, "label4")
        Me.label4.Name = "label4"
        '
        'ControlsPanel
        '
        resources.ApplyResources(Me.ControlsPanel, "ControlsPanel")
        Me.ControlsPanel.Controls.Add(Me.BackupButton)
        Me.ControlsPanel.Controls.Add(Me.RestoreButton)
        Me.ControlsPanel.Controls.Add(Me.label2)
        Me.ControlsPanel.Controls.Add(Me.GetBackupFileButton)
        Me.ControlsPanel.Controls.Add(Me.label3)
        Me.ControlsPanel.Controls.Add(Me.DatabasesComboBox)
        Me.ControlsPanel.Controls.Add(Me.BackupFileTextBox)
        Me.ControlsPanel.Name = "ControlsPanel"
        '
        'BackupButton
        '
        resources.ApplyResources(Me.BackupButton, "BackupButton")
        Me.BackupButton.Name = "BackupButton"
        '
        'RestoreButton
        '
        resources.ApplyResources(Me.RestoreButton, "RestoreButton")
        Me.RestoreButton.Name = "RestoreButton"
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'GetBackupFileButton
        '
        resources.ApplyResources(Me.GetBackupFileButton, "GetBackupFileButton")
        Me.GetBackupFileButton.Name = "GetBackupFileButton"
        '
        'label3
        '
        resources.ApplyResources(Me.label3, "label3")
        Me.label3.Name = "label3"
        '
        'DatabasesComboBox
        '
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        Me.DatabasesComboBox.Sorted = True
        '
        'BackupFileTextBox
        '
        resources.ApplyResources(Me.BackupFileTextBox, "BackupFileTextBox")
        Me.BackupFileTextBox.Name = "BackupFileTextBox"
        '
        'ResultsTextBox
        '
        resources.ApplyResources(Me.ResultsTextBox, "ResultsTextBox")
        Me.ResultsTextBox.Name = "ResultsTextBox"
        Me.ResultsTextBox.ReadOnly = True
        '
        'statusBar1
        '
        resources.ApplyResources(Me.statusBar1, "statusBar1")
        Me.statusBar1.Name = "statusBar1"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.CheckFileExists = False
        Me.OpenFileDialog1.FileName = "SmoDemoBackup.bak"
        resources.ApplyResources(Me.OpenFileDialog1, "OpenFileDialog1")
        Me.OpenFileDialog1.InitialDirectory = "C:\"
        '
        'BackupRestore
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.ControlsPanel)
        Me.Controls.Add(Me.ResultsTextBox)
        Me.Controls.Add(Me.statusBar1)
        Me.Controls.Add(Me.label4)
        Me.Name = "BackupRestore"
        Me.ControlsPanel.ResumeLayout(False)
        Me.ControlsPanel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents ControlsPanel As System.Windows.Forms.Panel
    Friend WithEvents BackupButton As System.Windows.Forms.Button
    Friend WithEvents RestoreButton As System.Windows.Forms.Button
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents GetBackupFileButton As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents BackupFileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ResultsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents statusBar1 As System.Windows.Forms.StatusBar
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog

End Class
