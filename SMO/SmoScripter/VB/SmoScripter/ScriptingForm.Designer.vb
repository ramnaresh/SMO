

Partial Class ScriptingForm 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptingForm))
        Me.splitContainer1 = New System.Windows.Forms.SplitContainer
        Me.Phase1TreeView = New System.Windows.Forms.TreeView
        Me.label1 = New System.Windows.Forms.Label
        Me.splitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Phase2ListBox = New System.Windows.Forms.ListBox
        Me.label2 = New System.Windows.Forms.Label
        Me.Phase3TextBox = New System.Windows.Forms.TextBox
        Me.label3 = New System.Windows.Forms.Label
        Me.splitContainer1.Panel1.SuspendLayout()
        Me.splitContainer1.Panel2.SuspendLayout()
        Me.splitContainer1.SuspendLayout()
        Me.splitContainer2.Panel1.SuspendLayout()
        Me.splitContainer2.Panel2.SuspendLayout()
        Me.splitContainer2.SuspendLayout()
        Me.SuspendLayout()
        '
        'splitContainer1
        '
        resources.ApplyResources(Me.splitContainer1, "splitContainer1")
        Me.splitContainer1.Name = "splitContainer1"
        '
        'splitContainer1.Panel1
        '
        Me.splitContainer1.Panel1.Controls.Add(Me.Phase1TreeView)
        Me.splitContainer1.Panel1.Controls.Add(Me.label1)
        '
        'splitContainer1.Panel2
        '
        Me.splitContainer1.Panel2.Controls.Add(Me.splitContainer2)
        '
        'Phase1TreeView
        '
        resources.ApplyResources(Me.Phase1TreeView, "Phase1TreeView")
        Me.Phase1TreeView.Name = "Phase1TreeView"
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'splitContainer2
        '
        resources.ApplyResources(Me.splitContainer2, "splitContainer2")
        Me.splitContainer2.Name = "splitContainer2"
        '
        'splitContainer2.Panel1
        '
        Me.splitContainer2.Panel1.Controls.Add(Me.Phase2ListBox)
        Me.splitContainer2.Panel1.Controls.Add(Me.label2)
        '
        'splitContainer2.Panel2
        '
        Me.splitContainer2.Panel2.Controls.Add(Me.Phase3TextBox)
        Me.splitContainer2.Panel2.Controls.Add(Me.label3)
        '
        'Phase2ListBox
        '
        resources.ApplyResources(Me.Phase2ListBox, "Phase2ListBox")
        Me.Phase2ListBox.FormattingEnabled = True
        Me.Phase2ListBox.Name = "Phase2ListBox"
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'Phase3TextBox
        '
        Me.Phase3TextBox.BackColor = System.Drawing.SystemColors.Window
        resources.ApplyResources(Me.Phase3TextBox, "Phase3TextBox")
        Me.Phase3TextBox.Name = "Phase3TextBox"
        Me.Phase3TextBox.ReadOnly = True
        '
        'label3
        '
        resources.ApplyResources(Me.label3, "label3")
        Me.label3.Name = "label3"
        '
        'ScriptingForm
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.splitContainer1)
        Me.Name = "ScriptingForm"
        Me.splitContainer1.Panel1.ResumeLayout(False)
        Me.splitContainer1.Panel1.PerformLayout()
        Me.splitContainer1.Panel2.ResumeLayout(False)
        Me.splitContainer1.ResumeLayout(False)
        Me.splitContainer2.Panel1.ResumeLayout(False)
        Me.splitContainer2.Panel1.PerformLayout()
        Me.splitContainer2.Panel2.ResumeLayout(False)
        Me.splitContainer2.Panel2.PerformLayout()
        Me.splitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub
    
    #End Region
    
    Private splitContainer1 As System.Windows.Forms.SplitContainer
    Private splitContainer2 As System.Windows.Forms.SplitContainer
    Private label3 As System.Windows.Forms.Label
    Private Phase3TextBox As System.Windows.Forms.TextBox
    Private label2 As System.Windows.Forms.Label
    Private Phase2ListBox As System.Windows.Forms.ListBox
    Private Phase1TreeView As System.Windows.Forms.TreeView
    Private label1 As System.Windows.Forms.Label
End Class