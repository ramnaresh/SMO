Partial Public Class ManageTables
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManageTables))
        Me.TablesComboBox = New System.Windows.Forms.ComboBox
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.TableNameTextBox = New System.Windows.Forms.TextBox
        Me.label4 = New System.Windows.Forms.Label
        Me.ColumnsListView = New System.Windows.Forms.ListView
        Me.clhColumnName = New System.Windows.Forms.ColumnHeader
        Me.clhDataType = New System.Windows.Forms.ColumnHeader
        Me.clhLength = New System.Windows.Forms.ColumnHeader
        Me.clhAllowNulls = New System.Windows.Forms.ColumnHeader
        Me.clhInPrimaryKey = New System.Windows.Forms.ColumnHeader
        Me.DeleteTableButton = New System.Windows.Forms.Button
        Me.AddTableButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'TablesComboBox
        '
        resources.ApplyResources(Me.TablesComboBox, "TablesComboBox")
        Me.TablesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.TablesComboBox.FormattingEnabled = True
        Me.TablesComboBox.Name = "TablesComboBox"
        '
        'DatabasesComboBox
        '
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'TableNameTextBox
        '
        resources.ApplyResources(Me.TableNameTextBox, "TableNameTextBox")
        Me.TableNameTextBox.Name = "TableNameTextBox"
        '
        'label4
        '
        resources.ApplyResources(Me.label4, "label4")
        Me.label4.Name = "label4"
        '
        'ColumnsListView
        '
        resources.ApplyResources(Me.ColumnsListView, "ColumnsListView")
        Me.ColumnsListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clhColumnName, Me.clhDataType, Me.clhLength, Me.clhAllowNulls, Me.clhInPrimaryKey})
        Me.ColumnsListView.FullRowSelect = True
        Me.ColumnsListView.HideSelection = False
        Me.ColumnsListView.MultiSelect = False
        Me.ColumnsListView.Name = "ColumnsListView"
        Me.ColumnsListView.View = System.Windows.Forms.View.Details
        '
        'clhColumnName
        '
        resources.ApplyResources(Me.clhColumnName, "clhColumnName")
        '
        'clhDataType
        '
        resources.ApplyResources(Me.clhDataType, "clhDataType")
        '
        'clhLength
        '
        resources.ApplyResources(Me.clhLength, "clhLength")
        '
        'clhAllowNulls
        '
        resources.ApplyResources(Me.clhAllowNulls, "clhAllowNulls")
        '
        'clhInPrimaryKey
        '
        resources.ApplyResources(Me.clhInPrimaryKey, "clhInPrimaryKey")
        '
        'DeleteTableButton
        '
        resources.ApplyResources(Me.DeleteTableButton, "DeleteTableButton")
        Me.DeleteTableButton.BackColor = System.Drawing.SystemColors.Control
        Me.DeleteTableButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.DeleteTableButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DeleteTableButton.Name = "DeleteTableButton"
        Me.DeleteTableButton.UseVisualStyleBackColor = False
        '
        'AddTableButton
        '
        resources.ApplyResources(Me.AddTableButton, "AddTableButton")
        Me.AddTableButton.BackColor = System.Drawing.SystemColors.Control
        Me.AddTableButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.AddTableButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.AddTableButton.Name = "AddTableButton"
        Me.AddTableButton.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.SystemColors.Control
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Name = "Label3"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Control
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Name = "Label2"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.Control
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Name = "Label1"
        '
        'ManageTables
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.TablesComboBox)
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Controls.Add(Me.TableNameTextBox)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.ColumnsListView)
        Me.Controls.Add(Me.DeleteTableButton)
        Me.Controls.Add(Me.AddTableButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ManageTables"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Friend WithEvents TablesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TableNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents label4 As System.Windows.Forms.Label
    Friend WithEvents ColumnsListView As System.Windows.Forms.ListView
    Friend WithEvents clhColumnName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhDataType As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhLength As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhAllowNulls As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhInPrimaryKey As System.Windows.Forms.ColumnHeader
    Friend WithEvents DeleteTableButton As System.Windows.Forms.Button
    Friend WithEvents AddTableButton As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
