
Partial Public Class MainForm 'The Partial modifier is only required on one class definition per project.
    Inherits System.Windows.Forms.Form
    
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent() 
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainForm))
        Me.statusStrip1 = New System.Windows.Forms.StatusStrip
        Me.serverVersionToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.speedToolStripStatusLabel = New System.Windows.Forms.ToolStripStatusLabel
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SqlServerTreeView = New System.Windows.Forms.TreeView
        Me.ListView = New System.Windows.Forms.ListView
        Me.ListViewContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.scriptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.scriptwithDependenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.dependenciesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.statusStrip1.SuspendLayout()
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.ListViewContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'statusStrip1
        '
        Me.statusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.serverVersionToolStripStatusLabel, Me.speedToolStripStatusLabel})
        resources.ApplyResources(Me.statusStrip1, "statusStrip1")
        Me.statusStrip1.Name = "statusStrip1"
        '
        'serverVersionToolStripStatusLabel
        '
        Me.serverVersionToolStripStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.serverVersionToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.serverVersionToolStripStatusLabel.Name = "serverVersionToolStripStatusLabel"
        resources.ApplyResources(Me.serverVersionToolStripStatusLabel, "serverVersionToolStripStatusLabel")
        '
        'speedToolStripStatusLabel
        '
        Me.speedToolStripStatusLabel.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
                    Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.speedToolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Sunken
        Me.speedToolStripStatusLabel.Name = "speedToolStripStatusLabel"
        resources.ApplyResources(Me.speedToolStripStatusLabel, "speedToolStripStatusLabel")
        '
        'splitContainer1
        '
        resources.ApplyResources(Me.splitContainer1, "splitContainer1")
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.SqlServerTreeView)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.ListView)
        '
        'SqlServerTreeView
        '
        resources.ApplyResources(Me.SqlServerTreeView, "SqlServerTreeView")
        Me.SqlServerTreeView.Name = "SqlServerTreeView"
        '
        'ListView
        '
        Me.ListView.ContextMenuStrip = Me.ListViewContextMenuStrip
        resources.ApplyResources(Me.ListView, "ListView")
        Me.ListView.FullRowSelect = True
        Me.ListView.GridLines = True
        Me.ListView.MultiSelect = False
        Me.ListView.Name = "ListView"
        Me.ListView.UseCompatibleStateImageBehavior = False
        Me.ListView.View = System.Windows.Forms.View.Details
        '
        'ListViewContextMenuStrip
        '
        Me.ListViewContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.scriptToolStripMenuItem, Me.scriptwithDependenciesToolStripMenuItem, Me.dependenciesToolStripMenuItem})
        Me.ListViewContextMenuStrip.Name = "contextMenuStrip1"
        resources.ApplyResources(Me.ListViewContextMenuStrip, "ListViewContextMenuStrip")
        '
        'scriptToolStripMenuItem
        '
        Me.scriptToolStripMenuItem.Name = "scriptToolStripMenuItem"
        resources.ApplyResources(Me.scriptToolStripMenuItem, "scriptToolStripMenuItem")
        '
        'scriptwithDependenciesToolStripMenuItem
        '
        Me.scriptwithDependenciesToolStripMenuItem.Name = "scriptwithDependenciesToolStripMenuItem"
        resources.ApplyResources(Me.scriptwithDependenciesToolStripMenuItem, "scriptwithDependenciesToolStripMenuItem")
        '
        'dependenciesToolStripMenuItem
        '
        Me.dependenciesToolStripMenuItem.Name = "dependenciesToolStripMenuItem"
        resources.ApplyResources(Me.dependenciesToolStripMenuItem, "dependenciesToolStripMenuItem")
        '
        'MainForm
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.splitContainer1)
        Me.Controls.Add(Me.statusStrip1)
        Me.Name = "MainForm"
        Me.statusStrip1.ResumeLayout(False)
        Me.statusStrip1.PerformLayout()
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.ListViewContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    
    
    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean) 
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    
    End Sub
    Private statusStrip1 As System.Windows.Forms.StatusStrip
    Private serverVersionToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Private speedToolStripStatusLabel As System.Windows.Forms.ToolStripStatusLabel
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents ListView As System.Windows.Forms.ListView
    Private WithEvents ListViewContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents scriptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents scriptwithDependenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents dependenciesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents SqlServerTreeView As System.Windows.Forms.TreeView
End Class
