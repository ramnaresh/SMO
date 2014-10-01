Partial Public Class VerifyBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(VerifyBackup))
        Me.BackupDeviceComboBox = New System.Windows.Forms.ComboBox
        Me.BackupContentsListView = New System.Windows.Forms.ListView
        Me.DeviceTypeLabel = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ReadHeaderButton = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Frame1 = New System.Windows.Forms.GroupBox
        Me.LocationLabel = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.StatusLabel = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.VerifyButton = New System.Windows.Forms.Button
        Me.Frame1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BackupDeviceComboBox
        '
        Me.BackupDeviceComboBox.BackColor = System.Drawing.SystemColors.Window
        Me.BackupDeviceComboBox.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.BackupDeviceComboBox, "BackupDeviceComboBox")
        Me.BackupDeviceComboBox.ForeColor = System.Drawing.SystemColors.WindowText
        Me.BackupDeviceComboBox.FormattingEnabled = True
        Me.BackupDeviceComboBox.Name = "BackupDeviceComboBox"
        '
        'BackupContentsListView
        '
        resources.ApplyResources(Me.BackupContentsListView, "BackupContentsListView")
        Me.BackupContentsListView.FullRowSelect = True
        Me.BackupContentsListView.HideSelection = False
        Me.BackupContentsListView.MultiSelect = False
        Me.BackupContentsListView.Name = "BackupContentsListView"
        Me.BackupContentsListView.View = System.Windows.Forms.View.Details
        '
        'DeviceTypeLabel
        '
        resources.ApplyResources(Me.DeviceTypeLabel, "DeviceTypeLabel")
        Me.DeviceTypeLabel.BackColor = System.Drawing.SystemColors.Control
        Me.DeviceTypeLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.DeviceTypeLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DeviceTypeLabel.Name = "DeviceTypeLabel"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Name = "Label2"
        '
        'ReadHeaderButton
        '
        resources.ApplyResources(Me.ReadHeaderButton, "ReadHeaderButton")
        Me.ReadHeaderButton.Name = "ReadHeaderButton"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Name = "Label1"
        '
        'Frame1
        '
        resources.ApplyResources(Me.Frame1, "Frame1")
        Me.Frame1.BackColor = System.Drawing.SystemColors.Control
        Me.Frame1.Controls.Add(Me.BackupDeviceComboBox)
        Me.Frame1.Controls.Add(Me.DeviceTypeLabel)
        Me.Frame1.Controls.Add(Me.Label2)
        Me.Frame1.Controls.Add(Me.Label1)
        Me.Frame1.Controls.Add(Me.LocationLabel)
        Me.Frame1.Controls.Add(Me.Label7)
        Me.Frame1.Controls.Add(Me.StatusLabel)
        Me.Frame1.Controls.Add(Me.Label5)
        Me.Frame1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Frame1.Name = "Frame1"
        Me.Frame1.TabStop = False
        '
        'LocationLabel
        '
        resources.ApplyResources(Me.LocationLabel, "LocationLabel")
        Me.LocationLabel.AutoEllipsis = True
        Me.LocationLabel.BackColor = System.Drawing.SystemColors.Control
        Me.LocationLabel.Cursor = System.Windows.Forms.Cursors.Default
        Me.LocationLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LocationLabel.Name = "LocationLabel"
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.SystemColors.Control
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Name = "Label7"
        '
        'StatusLabel
        '
        Me.StatusLabel.BackColor = System.Drawing.SystemColors.Control
        Me.StatusLabel.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.StatusLabel, "StatusLabel")
        Me.StatusLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.StatusLabel.Name = "StatusLabel"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Control
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Name = "Label5"
        '
        'VerifyButton
        '
        resources.ApplyResources(Me.VerifyButton, "VerifyButton")
        Me.VerifyButton.BackColor = System.Drawing.SystemColors.Control
        Me.VerifyButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.VerifyButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.VerifyButton.Name = "VerifyButton"
        Me.VerifyButton.UseVisualStyleBackColor = False
        '
        'VerifyBackup
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.BackupContentsListView)
        Me.Controls.Add(Me.ReadHeaderButton)
        Me.Controls.Add(Me.Frame1)
        Me.Controls.Add(Me.VerifyButton)
        Me.Name = "VerifyBackup"
        Me.Frame1.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Public WithEvents BackupDeviceComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents BackupContentsListView As System.Windows.Forms.ListView
    Public WithEvents DeviceTypeLabel As System.Windows.Forms.Label
    Public WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ReadHeaderButton As System.Windows.Forms.Button
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Frame1 As System.Windows.Forms.GroupBox
    Public WithEvents LocationLabel As System.Windows.Forms.Label
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents StatusLabel As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents VerifyButton As System.Windows.Forms.Button
End Class
