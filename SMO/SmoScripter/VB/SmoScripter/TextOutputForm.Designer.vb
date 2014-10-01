

Partial Class TextOutputForm 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(TextOutputForm))
        Me.OutputTextBox = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        ' 
        ' OutputTextBox
        ' 
        resources.ApplyResources(Me.OutputTextBox, "OutputTextBox")
        Me.OutputTextBox.BackColor = System.Drawing.SystemColors.Window
        Me.OutputTextBox.Name = "OutputTextBox"
        Me.OutputTextBox.ReadOnly = True
        ' 
        ' TextOutputForm
        ' 
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(OutputTextBox)
        Me.Name = "TextOutputForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

#End Region

    Private OutputTextBox As System.Windows.Forms.TextBox
End Class