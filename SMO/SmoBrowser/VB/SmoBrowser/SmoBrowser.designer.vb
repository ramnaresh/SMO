

Partial Class SmoBrowser 'The Partial modifier is only required on one class definition per project.
    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing
    
    
    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SmoBrowser))
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.objectTreeView = New System.Windows.Forms.TreeView
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer
        Me.propertyGrid1 = New System.Windows.Forms.PropertyGrid
        Me.textBox1 = New System.Windows.Forms.TextBox
        Me.menuStrip2 = New System.Windows.Forms.MenuStrip
        Me.fileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.exitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.menuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        resources.ApplyResources(Me.splitContainer1, "splitContainer1")
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.objectTreeView)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
        '
        'objectTreeView
        '
        resources.ApplyResources(Me.objectTreeView, "objectTreeView")
        Me.objectTreeView.HideSelection = False
        Me.objectTreeView.Name = "objectTreeView"
        Me.objectTreeView.Sorted = True
        '
        'splitContainer2
        '
        resources.ApplyResources(Me.splitContainer2, "splitContainer2")
        Me.splitContainer2.Name = "splitContainer2"
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.propertyGrid1)
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.textBox1)
        '
        'propertyGrid1
        '
        resources.ApplyResources(Me.propertyGrid1, "propertyGrid1")
        Me.propertyGrid1.Name = "propertyGrid1"
        '
        'textBox1
        '
        Me.textBox1.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.textBox1, "textBox1")
        Me.textBox1.Name = "textBox1"
        Me.textBox1.ReadOnly = True
        '
        'menuStrip2
        '
        Me.menuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fileToolStripMenuItem})
        resources.ApplyResources(Me.menuStrip2, "menuStrip2")
        Me.menuStrip2.Name = "menuStrip2"
        '
        'fileToolStripMenuItem
        '
        Me.fileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.exitToolStripMenuItem})
        Me.fileToolStripMenuItem.Name = "fileToolStripMenuItem"
        resources.ApplyResources(Me.fileToolStripMenuItem, "fileToolStripMenuItem")
        '
        'exitToolStripMenuItem
        '
        Me.exitToolStripMenuItem.Name = "exitToolStripMenuItem"
        resources.ApplyResources(Me.exitToolStripMenuItem, "exitToolStripMenuItem")
        '
        'SmoBrowser
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.splitContainer1)
        Me.Controls.Add(Me.menuStrip2)
        Me.MainMenuStrip = Me.menuStrip2
        Me.Name = "SmoBrowser"
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel2.ResumeLayout(False)
        Me.splitContainer2.Panel2.PerformLayout()
        Me.splitContainer2.ResumeLayout(False)
        Me.menuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    
    #End Region
    
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private WithEvents objectTreeView As System.Windows.Forms.TreeView
    Private propertyGrid1 As System.Windows.Forms.PropertyGrid
    Private splitContainer2 As System.Windows.Forms.SplitContainer
    Private textBox1 As System.Windows.Forms.TextBox
    Private menuStrip2 As System.Windows.Forms.MenuStrip
    Private fileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents exitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
