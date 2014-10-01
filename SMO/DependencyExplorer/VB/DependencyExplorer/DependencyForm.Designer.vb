Partial Public Class DependencyForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DependencyForm))
        Me.NodeContextMenu = New System.Windows.Forms.ContextMenu
        Me.WhereUsedMenuItem = New System.Windows.Forms.MenuItem
        Me.DependenciesMenuItem = New System.Windows.Forms.MenuItem
        Me.PropertiesListView = New System.Windows.Forms.ListView
        Me.NameColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.ValueColumnHeader = New System.Windows.Forms.ColumnHeader
        Me.DependenciesTreeView = New System.Windows.Forms.TreeView
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'NodeContextMenu
        '
        Me.NodeContextMenu.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.WhereUsedMenuItem, Me.DependenciesMenuItem})
        '
        'WhereUsedMenuItem
        '
        Me.WhereUsedMenuItem.Index = 0
        resources.ApplyResources(Me.WhereUsedMenuItem, "WhereUsedMenuItem")
        '
        'DependenciesMenuItem
        '
        Me.DependenciesMenuItem.Index = 1
        resources.ApplyResources(Me.DependenciesMenuItem, "DependenciesMenuItem")
        '
        'PropertiesListView
        '
        Me.PropertiesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.NameColumnHeader, Me.ValueColumnHeader})
        resources.ApplyResources(Me.PropertiesListView, "PropertiesListView")
        Me.PropertiesListView.FullRowSelect = True
        Me.PropertiesListView.Name = "PropertiesListView"
        Me.PropertiesListView.View = System.Windows.Forms.View.Details
        '
        'NameColumnHeader
        '
        resources.ApplyResources(Me.NameColumnHeader, "NameColumnHeader")
        '
        'ValueColumnHeader
        '
        resources.ApplyResources(Me.ValueColumnHeader, "ValueColumnHeader")
        '
        'DependenciesTreeView
        '
        resources.ApplyResources(Me.DependenciesTreeView, "DependenciesTreeView")
        Me.DependenciesTreeView.Name = "DependenciesTreeView"
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.DependenciesTreeView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PropertiesListView)
        '
        'DependencyForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "DependencyForm"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    Friend WithEvents NodeContextMenu As System.Windows.Forms.ContextMenu
    Friend WithEvents WhereUsedMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents DependenciesMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents PropertiesListView As System.Windows.Forms.ListView
    Friend WithEvents DependenciesTreeView As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents NameColumnHeader As System.Windows.Forms.ColumnHeader
    Friend WithEvents ValueColumnHeader As System.Windows.Forms.ColumnHeader
End Class
