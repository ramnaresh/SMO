Partial Public Class LoadRegAssembly
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(LoadRegAssembly))
        Me.clhCreateDate = New System.Windows.Forms.ColumnHeader
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.DropAssemblyButton = New System.Windows.Forms.Button
        Me.AddAssemblyButton = New System.Windows.Forms.Button
        Me.label3 = New System.Windows.Forms.Label
        Me.label2 = New System.Windows.Forms.Label
        Me.clhAssemblyVersion = New System.Windows.Forms.ColumnHeader
        Me.AssembliesListView = New System.Windows.Forms.ListView
        Me.clhAssemblyName = New System.Windows.Forms.ColumnHeader
        Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.AssemblyFileTextBox = New System.Windows.Forms.TextBox
        Me.label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'clhCreateDate
        '
        resources.ApplyResources(Me.clhCreateDate, "clhCreateDate")
        '
        'DatabasesComboBox
        '
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'DropAssemblyButton
        '
        resources.ApplyResources(Me.DropAssemblyButton, "DropAssemblyButton")
        Me.DropAssemblyButton.Name = "DropAssemblyButton"
        '
        'AddAssemblyButton
        '
        resources.ApplyResources(Me.AddAssemblyButton, "AddAssemblyButton")
        Me.AddAssemblyButton.Margin = New System.Windows.Forms.Padding(1, 3, 3, 3)
        Me.AddAssemblyButton.Name = "AddAssemblyButton"
        '
        'label3
        '
        Me.label3.BackColor = System.Drawing.SystemColors.Control
        Me.label3.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.label3, "label3")
        Me.label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label3.Name = "label3"
        '
        'label2
        '
        Me.label2.BackColor = System.Drawing.SystemColors.Control
        Me.label2.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.label2.Name = "label2"
        '
        'clhAssemblyVersion
        '
        resources.ApplyResources(Me.clhAssemblyVersion, "clhAssemblyVersion")
        '
        'AssembliesListView
        '
        resources.ApplyResources(Me.AssembliesListView, "AssembliesListView")
        Me.AssembliesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clhAssemblyName, Me.clhAssemblyVersion, Me.clhCreateDate})
        Me.AssembliesListView.FullRowSelect = True
        Me.AssembliesListView.HideSelection = False
        Me.AssembliesListView.MultiSelect = False
        Me.AssembliesListView.Name = "AssembliesListView"
        Me.AssembliesListView.View = System.Windows.Forms.View.Details
        '
        'clhAssemblyName
        '
        resources.ApplyResources(Me.clhAssemblyName, "clhAssemblyName")
        '
        'openFileDialog1
        '
        resources.ApplyResources(Me.openFileDialog1, "openFileDialog1")
        '
        'AssemblyFileTextBox
        '
        resources.ApplyResources(Me.AssemblyFileTextBox, "AssemblyFileTextBox")
        Me.AssemblyFileTextBox.Name = "AssemblyFileTextBox"
        Me.AssemblyFileTextBox.ReadOnly = True
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'LoadRegAssembly
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.AssembliesListView)
        Me.Controls.Add(Me.DropAssemblyButton)
        Me.Controls.Add(Me.AddAssemblyButton)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.AssemblyFileTextBox)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Name = "LoadRegAssembly"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Friend WithEvents clhCreateDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DropAssemblyButton As System.Windows.Forms.Button
    Friend WithEvents AddAssemblyButton As System.Windows.Forms.Button
    Friend WithEvents label3 As System.Windows.Forms.Label
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents clhAssemblyVersion As System.Windows.Forms.ColumnHeader
    Friend WithEvents AssembliesListView As System.Windows.Forms.ListView
    Friend WithEvents clhAssemblyName As System.Windows.Forms.ColumnHeader
    Friend WithEvents openFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents AssemblyFileTextBox As System.Windows.Forms.TextBox
    Friend WithEvents label1 As System.Windows.Forms.Label

End Class
