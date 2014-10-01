

Partial Class ScriptPanel 'The Partial modifier is only required on one class definition per project.
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
    ''' The contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent() 
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ScriptPanel))
        Me.runButton = New System.Windows.Forms.Button
        Me.panelTextBox = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'runButton
        '
        resources.ApplyResources(Me.runButton, "runButton")
        Me.runButton.Name = "runButton"
        '
        'panelTextBox
        '
        resources.ApplyResources(Me.panelTextBox, "panelTextBox")
        Me.panelTextBox.Name = "panelTextBox"
        Me.panelTextBox.ReadOnly = True
        '
        'ScriptPanel
        '
        Me.AcceptButton = Me.runButton
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.runButton)
        Me.Controls.Add(Me.panelTextBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "ScriptPanel"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)
    End Sub
    
    #End Region
    
    Private WithEvents runButton As System.Windows.Forms.Button
    Private panelTextBox As System.Windows.Forms.TextBox
End Class