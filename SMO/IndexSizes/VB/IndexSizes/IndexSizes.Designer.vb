

Partial Class IndexSizes 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(IndexSizes))
        Dim dataGridViewCellStyle1 As New System.Windows.Forms.DataGridViewCellStyle()
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dataGridView1 = New System.Windows.Forms.DataGridView()
        Me.TableName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IndexName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.IndexSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' DatabasesComboBox
        ' 
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        ' 
        ' Label1
        ' 
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Name = "Label1"
        ' 
        ' dataGridView1
        ' 
        Me.dataGridView1.AllowUserToAddRows = False
        Me.dataGridView1.AllowUserToDeleteRows = False
        resources.ApplyResources(Me.dataGridView1, "dataGridView1")
        Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.TableName, Me.IndexName, Me.IndexSize})
        Me.dataGridView1.Name = "dataGridView1"
        Me.dataGridView1.ReadOnly = True
        ' 
        ' TableName
        ' 
        resources.ApplyResources(Me.TableName, "TableName")
        Me.TableName.Name = "TableName"
        Me.TableName.ReadOnly = True
        ' 
        ' IndexName
        ' 
        resources.ApplyResources(Me.IndexName, "IndexName")
        Me.IndexName.Name = "IndexName"
        Me.IndexName.ReadOnly = True
        ' 
        ' IndexSize
        ' 
        dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        dataGridViewCellStyle1.Format = "N0"
        dataGridViewCellStyle1.NullValue = Nothing
        Me.IndexSize.DefaultCellStyle = dataGridViewCellStyle1
        resources.ApplyResources(Me.IndexSize, "IndexSize")
        Me.IndexSize.Name = "IndexSize"
        Me.IndexSize.ReadOnly = True
        ' 
        ' IndexSizes
        ' 
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(dataGridView1)
        Me.Controls.Add(DatabasesComboBox)
        Me.Controls.Add(Label1)
        Me.Name = "IndexSizes"
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    
    End Sub
    
    #End Region
    
    Private WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Private Label1 As System.Windows.Forms.Label
    Private dataGridView1 As System.Windows.Forms.DataGridView
    Private TableName As System.Windows.Forms.DataGridViewTextBoxColumn
    Private IndexName As System.Windows.Forms.DataGridViewTextBoxColumn
    Private IndexSize As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
