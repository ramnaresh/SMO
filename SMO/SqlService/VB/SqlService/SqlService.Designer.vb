Partial Public Class SqlService
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SqlService))
        Me.TimeoutLabel = New System.Windows.Forms.Label
        Me.TimeoutUpDown = New System.Windows.Forms.NumericUpDown
        Me.sbrStatus = New System.Windows.Forms.StatusBar
        Me.ResumeButton = New System.Windows.Forms.Button
        Me.PauseButton = New System.Windows.Forms.Button
        Me.StopButton = New System.Windows.Forms.Button
        Me.StartButton = New System.Windows.Forms.Button
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.ServicesListView = New System.Windows.Forms.ListView
        Me.clhService = New System.Windows.Forms.ColumnHeader
        Me.clhName = New System.Windows.Forms.ColumnHeader
        Me.clhStatus = New System.Windows.Forms.ColumnHeader
        Me.clhStartupType = New System.Windows.Forms.ColumnHeader
        Me.clhLogOnAs = New System.Windows.Forms.ColumnHeader
        Me.clhState = New System.Windows.Forms.ColumnHeader
        Me.label2 = New System.Windows.Forms.Label
        CType(Me.TimeoutUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TimeoutLabel
        '
        resources.ApplyResources(Me.TimeoutLabel, "TimeoutLabel")
        Me.TimeoutLabel.Name = "TimeoutLabel"
        '
        'TimeoutUpDown
        '
        resources.ApplyResources(Me.TimeoutUpDown, "TimeoutUpDown")
        Me.TimeoutUpDown.Maximum = New Decimal(New Integer() {300, 0, 0, 0})
        Me.TimeoutUpDown.Minimum = New Decimal(New Integer() {30, 0, 0, 0})
        Me.TimeoutUpDown.Name = "TimeoutUpDown"
        Me.TimeoutUpDown.Value = New Decimal(New Integer() {30, 0, 0, 0})
        '
        'sbrStatus
        '
        resources.ApplyResources(Me.sbrStatus, "sbrStatus")
        Me.sbrStatus.Name = "sbrStatus"
        '
        'ResumeButton
        '
        resources.ApplyResources(Me.ResumeButton, "ResumeButton")
        Me.ResumeButton.Name = "ResumeButton"
        '
        'PauseButton
        '
        resources.ApplyResources(Me.PauseButton, "PauseButton")
        Me.PauseButton.Name = "PauseButton"
        '
        'StopButton
        '
        resources.ApplyResources(Me.StopButton, "StopButton")
        Me.StopButton.Name = "StopButton"
        '
        'StartButton
        '
        resources.ApplyResources(Me.StartButton, "StartButton")
        Me.StartButton.Name = "StartButton"
        '
        'RefreshButton
        '
        Me.RefreshButton.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.RefreshButton, "RefreshButton")
        Me.RefreshButton.Name = "RefreshButton"
        '
        'ServicesListView
        '
        resources.ApplyResources(Me.ServicesListView, "ServicesListView")
        Me.ServicesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clhService, Me.clhName, Me.clhStatus, Me.clhStartupType, Me.clhLogOnAs, Me.clhState})
        Me.ServicesListView.FullRowSelect = True
        Me.ServicesListView.HideSelection = False
        Me.ServicesListView.MultiSelect = False
        Me.ServicesListView.Name = "ServicesListView"
        Me.ServicesListView.UseCompatibleStateImageBehavior = False
        Me.ServicesListView.View = System.Windows.Forms.View.Details
        '
        'clhService
        '
        resources.ApplyResources(Me.clhService, "clhService")
        '
        'clhName
        '
        resources.ApplyResources(Me.clhName, "clhName")
        '
        'clhStatus
        '
        resources.ApplyResources(Me.clhStatus, "clhStatus")
        '
        'clhStartupType
        '
        resources.ApplyResources(Me.clhStartupType, "clhStartupType")
        '
        'clhLogOnAs
        '
        resources.ApplyResources(Me.clhLogOnAs, "clhLogOnAs")
        '
        'clhState
        '
        resources.ApplyResources(Me.clhState, "clhState")
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'SqlService
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TimeoutLabel)
        Me.Controls.Add(Me.TimeoutUpDown)
        Me.Controls.Add(Me.sbrStatus)
        Me.Controls.Add(Me.ResumeButton)
        Me.Controls.Add(Me.PauseButton)
        Me.Controls.Add(Me.StopButton)
        Me.Controls.Add(Me.StartButton)
        Me.Controls.Add(Me.RefreshButton)
        Me.Controls.Add(Me.ServicesListView)
        Me.Controls.Add(Me.label2)
        Me.Name = "SqlService"
        CType(Me.TimeoutUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TimeoutLabel As System.Windows.Forms.Label
    Friend WithEvents TimeoutUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents sbrStatus As System.Windows.Forms.StatusBar
    Friend WithEvents ResumeButton As System.Windows.Forms.Button
    Friend WithEvents PauseButton As System.Windows.Forms.Button
    Friend WithEvents StopButton As System.Windows.Forms.Button
    Friend WithEvents StartButton As System.Windows.Forms.Button
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents ServicesListView As System.Windows.Forms.ListView
    Friend WithEvents clhService As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhStatus As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhStartupType As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhLogOnAs As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhState As System.Windows.Forms.ColumnHeader
    Friend WithEvents label2 As System.Windows.Forms.Label

End Class
