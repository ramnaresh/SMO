Partial Public Class ServerInfo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerInfo))
        Me.ConnectionListView = New System.Windows.Forms.ListView
        Me.PropertyColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ValueColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ValueColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.PropertyColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.ServerListView = New System.Windows.Forms.ListView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'ConnectionListView
        '
        Me.ConnectionListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PropertyColumnHeader2, Me.ValueColumnHeader2})
        resources.ApplyResources(Me.ConnectionListView, "ConnectionListView")
        Me.ConnectionListView.FullRowSelect = True
        Me.ConnectionListView.Name = "ConnectionListView"
        Me.ConnectionListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ConnectionListView.View = System.Windows.Forms.View.Details
        '
        'PropertyColumnHeader2
        '
        resources.ApplyResources(Me.PropertyColumnHeader2, "PropertyColumnHeader2")
        '
        'ValueColumnHeader2
        '
        resources.ApplyResources(Me.ValueColumnHeader2, "ValueColumnHeader2")
        '
        'ValueColumnHeader
        '
        Me.ValueColumnHeader.Name = "ValueColumnHeader"
        resources.ApplyResources(Me.ValueColumnHeader, "ValueColumnHeader")
        '
        'PropertyColumnHeader
        '
        Me.PropertyColumnHeader.Name = "PropertyColumnHeader"
        resources.ApplyResources(Me.PropertyColumnHeader, "PropertyColumnHeader")
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.RefreshButton)
        resources.ApplyResources(Me.Panel1, "Panel1")
        Me.Panel1.Name = "Panel1"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'RefreshButton
        '
        resources.ApplyResources(Me.RefreshButton, "RefreshButton")
        Me.RefreshButton.Name = "RefreshButton"
        '
        'ServerListView
        '
        Me.ServerListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.PropertyColumnHeader, Me.ValueColumnHeader})
        resources.ApplyResources(Me.ServerListView, "ServerListView")
        Me.ServerListView.FullRowSelect = True
        Me.ServerListView.Name = "ServerListView"
        Me.ServerListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ServerListView.View = System.Windows.Forms.View.Details
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ServerListView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.ConnectionListView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        resources.ApplyResources(Me.Panel2, "Panel2")
        Me.Panel2.Name = "Panel2"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'ServerInfo
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "ServerInfo"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents ConnectionListView As System.Windows.Forms.ListView
    Friend WithEvents PropertyColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ValueColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ValueColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents PropertyColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents ServerListView As System.Windows.Forms.ListView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class
