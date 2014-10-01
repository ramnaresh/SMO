

Partial Class DatabaseDefrag 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DatabaseDefrag))
        Me.richTextBox1 = New System.Windows.Forms.RichTextBox
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.defragmentButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'richTextBox1
        '
        resources.ApplyResources(Me.richTextBox1, "richTextBox1")
        Me.richTextBox1.Name = "richTextBox1"
        Me.richTextBox1.ReadOnly = True
        '
        'DatabasesComboBox
        '
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Name = "Label1"
        '
        'defragmentButton
        '
        resources.ApplyResources(Me.defragmentButton, "defragmentButton")
        Me.defragmentButton.Name = "defragmentButton"
        Me.defragmentButton.UseVisualStyleBackColor = True
        '
        'DatabaseDefrag
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.defragmentButton)
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.richTextBox1)
        Me.Name = "DatabaseDefrag"
        Me.ResumeLayout(False)

    End Sub
    
    #End Region
    
    Private richTextBox1 As System.Windows.Forms.RichTextBox
    Private DatabasesComboBox As System.Windows.Forms.ComboBox
    Private Label1 As System.Windows.Forms.Label
    Private WithEvents defragmentButton As System.Windows.Forms.Button
End Class
