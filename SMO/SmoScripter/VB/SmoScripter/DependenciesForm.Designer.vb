

Partial Class DependenciesForm 'The Partial modifier is only required on one class definition per project.
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing
    
    
    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    ''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean) 
        If disposing AndAlso Not (components Is Nothing) Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    
    End Sub
    
    #Region "Windows Form Designer generated code"
    
    
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent() 
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DependenciesForm))
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.DependenciesTreeView = New System.Windows.Forms.TreeView
        Me.WhereUsedContextMenuStrip = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.WhereUsedToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ScriptTextBox = New System.Windows.Forms.TextBox
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.WhereUsedContextMenuStrip.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        resources.ApplyResources(Me.splitContainer1, "splitContainer1")
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.DependenciesTreeView)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.ScriptTextBox)
        '
        'DependenciesTreeView
        '
        Me.DependenciesTreeView.ContextMenuStrip = Me.WhereUsedContextMenuStrip
        resources.ApplyResources(Me.DependenciesTreeView, "DependenciesTreeView")
        Me.DependenciesTreeView.Name = "DependenciesTreeView"
        '
        'WhereUsedContextMenuStrip
        '
        Me.WhereUsedContextMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.WhereUsedToolStripMenuItem})
        Me.WhereUsedContextMenuStrip.Name = "WhereUsedContextMenuStrip"
        resources.ApplyResources(Me.WhereUsedContextMenuStrip, "WhereUsedContextMenuStrip")
        '
        'WhereUsedToolStripMenuItem
        '
        Me.WhereUsedToolStripMenuItem.Name = "WhereUsedToolStripMenuItem"
        resources.ApplyResources(Me.WhereUsedToolStripMenuItem, "WhereUsedToolStripMenuItem")
        '
        'ScriptTextBox
        '
        resources.ApplyResources(Me.ScriptTextBox, "ScriptTextBox")
        Me.ScriptTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.ScriptTextBox.Name = "ScriptTextBox"
        Me.ScriptTextBox.ReadOnly = True
        '
        'DependenciesForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splitContainer1)
        Me.Name = "DependenciesForm"
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.Panel2.PerformLayout()
        Me.splitContainer1.ResumeLayout(False)
        Me.WhereUsedContextMenuStrip.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    
    #End Region
    
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents DependenciesTreeView As System.Windows.Forms.TreeView
    Private ScriptTextBox As System.Windows.Forms.TextBox
    Private WhereUsedContextMenuStrip As System.Windows.Forms.ContextMenuStrip
    Private WithEvents WhereUsedToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class