
Partial Class DatabaseSpace 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(DatabaseSpace))
        Dim dataGridViewCellStyle1 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle2 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle3 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle4 As New System.Windows.Forms.DataGridViewCellStyle()
        Dim dataGridViewCellStyle5 As New System.Windows.Forms.DataGridViewCellStyle()
        Me.dataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DatabaseName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DatabaseSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SpaceAvailable = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogSpaceSize = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LogSpaceUsed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        ' 
        ' dataGridView1
        ' 
        Me.dataGridView1.AllowUserToAddRows = False
        Me.dataGridView1.AllowUserToDeleteRows = False
        resources.ApplyResources(Me.dataGridView1, "dataGridView1")
        Me.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DatabaseName, Me.DatabaseSize, Me.SpaceAvailable, Me.LogSpaceSize, Me.LogSpaceUsed})
        Me.dataGridView1.MultiSelect = False
        Me.dataGridView1.Name = "dataGridView1"
        Me.dataGridView1.ReadOnly = True
        Me.dataGridView1.RowTemplate.ReadOnly = True
        Me.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        ' 
        ' DatabaseName
        ' 
        dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False
        Me.DatabaseName.DefaultCellStyle = dataGridViewCellStyle1
        resources.ApplyResources(Me.DatabaseName, "DatabaseName")
        Me.DatabaseName.Name = "DatabaseName"
        Me.DatabaseName.ReadOnly = True
        ' 
        ' DatabaseSize
        ' 
        dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        dataGridViewCellStyle2.Format = "N2"
        dataGridViewCellStyle2.NullValue = Nothing
        Me.DatabaseSize.DefaultCellStyle = dataGridViewCellStyle2
        resources.ApplyResources(Me.DatabaseSize, "DatabaseSize")
        Me.DatabaseSize.Name = "DatabaseSize"
        Me.DatabaseSize.ReadOnly = True
        ' 
        ' SpaceAvailable
        ' 
        dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        dataGridViewCellStyle3.Format = "N2"
        Me.SpaceAvailable.DefaultCellStyle = dataGridViewCellStyle3
        resources.ApplyResources(Me.SpaceAvailable, "SpaceAvailable")
        Me.SpaceAvailable.Name = "SpaceAvailable"
        Me.SpaceAvailable.ReadOnly = True
        ' 
        ' LogSpaceSize
        ' 
        dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        dataGridViewCellStyle4.Format = "N2"
        Me.LogSpaceSize.DefaultCellStyle = dataGridViewCellStyle4
        resources.ApplyResources(Me.LogSpaceSize, "LogSpaceSize")
        Me.LogSpaceSize.Name = "LogSpaceSize"
        Me.LogSpaceSize.ReadOnly = True
        ' 
        ' LogSpaceUsed
        ' 
        dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        dataGridViewCellStyle5.Format = "N2"
        Me.LogSpaceUsed.DefaultCellStyle = dataGridViewCellStyle5
        resources.ApplyResources(Me.LogSpaceUsed, "LogSpaceUsed")
        Me.LogSpaceUsed.Name = "LogSpaceUsed"
        Me.LogSpaceUsed.ReadOnly = True
        ' 
        ' DatabaseSpace
        ' 
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(dataGridView1)
        Me.Name = "DatabaseSpace"
        CType(Me.dataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
    End Sub

#End Region

    Private dataGridView1 As System.Windows.Forms.DataGridView
    Private DatabaseName As System.Windows.Forms.DataGridViewTextBoxColumn
    Private DatabaseSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Private SpaceAvailable As System.Windows.Forms.DataGridViewTextBoxColumn
    Private LogSpaceSize As System.Windows.Forms.DataGridViewTextBoxColumn
    Private LogSpaceUsed As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
