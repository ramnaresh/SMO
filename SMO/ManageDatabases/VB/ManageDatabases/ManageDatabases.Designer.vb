Partial Public Class ManageDatabases
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManageDatabases))
        Me.NewDatabaseTextBox = New System.Windows.Forms.TextBox
        Me.NewDatabaseLabel = New System.Windows.Forms.Label
        Me.CreateButton = New System.Windows.Forms.Button
        Me.sbrStatus = New System.Windows.Forms.StatusBar
        Me.clhDatabaseName = New System.Windows.Forms.ColumnHeader
        Me.DisplayLabel = New System.Windows.Forms.Label
        Me.clhSize = New System.Windows.Forms.ColumnHeader
        Me.DatabasesListView = New System.Windows.Forms.ListView
        Me.clhCreateDate = New System.Windows.Forms.ColumnHeader
        Me.clhSpaceAvailable = New System.Windows.Forms.ColumnHeader
        Me.clhCompatibilityLevel = New System.Windows.Forms.ColumnHeader
        Me.ResultsLabel = New System.Windows.Forms.Label
        Me.ShowSqlStatementsCheckBox = New System.Windows.Forms.CheckBox
        Me.EventLogTextBox = New System.Windows.Forms.TextBox
        Me.ShowServerMessagesCheckBox = New System.Windows.Forms.CheckBox
        Me.DeleteButton = New System.Windows.Forms.Button
        Me.DatabaseLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'NewDatabaseTextBox
        '
        resources.ApplyResources(Me.NewDatabaseTextBox, "NewDatabaseTextBox")
        Me.NewDatabaseTextBox.Name = "NewDatabaseTextBox"
        '
        'NewDatabaseLabel
        '
        resources.ApplyResources(Me.NewDatabaseLabel, "NewDatabaseLabel")
        Me.NewDatabaseLabel.Name = "NewDatabaseLabel"
        '
        'CreateButton
        '
        Me.CreateButton.BackColor = System.Drawing.SystemColors.Control
        Me.CreateButton.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.CreateButton, "CreateButton")
        Me.CreateButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CreateButton.Name = "CreateButton"
        Me.CreateButton.UseVisualStyleBackColor = False
        '
        'sbrStatus
        '
        resources.ApplyResources(Me.sbrStatus, "sbrStatus")
        Me.sbrStatus.Name = "sbrStatus"
        '
        'clhDatabaseName
        '
        resources.ApplyResources(Me.clhDatabaseName, "clhDatabaseName")
        '
        'DisplayLabel
        '
        resources.ApplyResources(Me.DisplayLabel, "DisplayLabel")
        Me.DisplayLabel.Name = "DisplayLabel"
        '
        'clhSize
        '
        resources.ApplyResources(Me.clhSize, "clhSize")
        '
        'DatabasesListView
        '
        resources.ApplyResources(Me.DatabasesListView, "DatabasesListView")
        Me.DatabasesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clhDatabaseName, Me.clhCreateDate, Me.clhSize, Me.clhSpaceAvailable, Me.clhCompatibilityLevel})
        Me.DatabasesListView.FullRowSelect = True
        Me.DatabasesListView.HideSelection = False
        Me.DatabasesListView.MultiSelect = False
        Me.DatabasesListView.Name = "DatabasesListView"
        Me.DatabasesListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.DatabasesListView.UseCompatibleStateImageBehavior = False
        Me.DatabasesListView.View = System.Windows.Forms.View.Details
        '
        'clhCreateDate
        '
        resources.ApplyResources(Me.clhCreateDate, "clhCreateDate")
        '
        'clhSpaceAvailable
        '
        resources.ApplyResources(Me.clhSpaceAvailable, "clhSpaceAvailable")
        '
        'clhCompatibilityLevel
        '
        resources.ApplyResources(Me.clhCompatibilityLevel, "clhCompatibilityLevel")
        '
        'ResultsLabel
        '
        resources.ApplyResources(Me.ResultsLabel, "ResultsLabel")
        Me.ResultsLabel.Name = "ResultsLabel"
        '
        'ShowSqlStatementsCheckBox
        '
        resources.ApplyResources(Me.ShowSqlStatementsCheckBox, "ShowSqlStatementsCheckBox")
        Me.ShowSqlStatementsCheckBox.BackColor = System.Drawing.SystemColors.Control
        Me.ShowSqlStatementsCheckBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.ShowSqlStatementsCheckBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ShowSqlStatementsCheckBox.Name = "ShowSqlStatementsCheckBox"
        Me.ShowSqlStatementsCheckBox.UseVisualStyleBackColor = False
        '
        'EventLogTextBox
        '
        resources.ApplyResources(Me.EventLogTextBox, "EventLogTextBox")
        Me.EventLogTextBox.Name = "EventLogTextBox"
        Me.EventLogTextBox.ReadOnly = True
        '
        'ShowServerMessagesCheckBox
        '
        resources.ApplyResources(Me.ShowServerMessagesCheckBox, "ShowServerMessagesCheckBox")
        Me.ShowServerMessagesCheckBox.BackColor = System.Drawing.SystemColors.Control
        Me.ShowServerMessagesCheckBox.Cursor = System.Windows.Forms.Cursors.Default
        Me.ShowServerMessagesCheckBox.ForeColor = System.Drawing.SystemColors.ControlText
        Me.ShowServerMessagesCheckBox.Name = "ShowServerMessagesCheckBox"
        Me.ShowServerMessagesCheckBox.UseVisualStyleBackColor = False
        '
        'DeleteButton
        '
        Me.DeleteButton.BackColor = System.Drawing.SystemColors.Control
        Me.DeleteButton.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.DeleteButton, "DeleteButton")
        Me.DeleteButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DeleteButton.Name = "DeleteButton"
        Me.DeleteButton.UseVisualStyleBackColor = False
        '
        'DatabaseLabel
        '
        Me.DatabaseLabel.BackColor = System.Drawing.SystemColors.Control
        Me.DatabaseLabel.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.DatabaseLabel, "DatabaseLabel")
        Me.DatabaseLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DatabaseLabel.Name = "DatabaseLabel"
        '
        'ManageDatabases
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.NewDatabaseTextBox)
        Me.Controls.Add(Me.NewDatabaseLabel)
        Me.Controls.Add(Me.CreateButton)
        Me.Controls.Add(Me.sbrStatus)
        Me.Controls.Add(Me.DisplayLabel)
        Me.Controls.Add(Me.DatabasesListView)
        Me.Controls.Add(Me.ResultsLabel)
        Me.Controls.Add(Me.ShowSqlStatementsCheckBox)
        Me.Controls.Add(Me.EventLogTextBox)
        Me.Controls.Add(Me.ShowServerMessagesCheckBox)
        Me.Controls.Add(Me.DeleteButton)
        Me.Controls.Add(Me.DatabaseLabel)
        Me.Name = "ManageDatabases"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents NewDatabaseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NewDatabaseLabel As System.Windows.Forms.Label
    Friend WithEvents CreateButton As System.Windows.Forms.Button
    Friend WithEvents sbrStatus As System.Windows.Forms.StatusBar
    Friend WithEvents clhDatabaseName As System.Windows.Forms.ColumnHeader
    Friend WithEvents DisplayLabel As System.Windows.Forms.Label
    Friend WithEvents clhSize As System.Windows.Forms.ColumnHeader
    Friend WithEvents DatabasesListView As System.Windows.Forms.ListView
    Friend WithEvents clhCreateDate As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhCompatibilityLevel As System.Windows.Forms.ColumnHeader
    Friend WithEvents ResultsLabel As System.Windows.Forms.Label
    Friend WithEvents ShowSqlStatementsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents EventLogTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ShowServerMessagesCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents DeleteButton As System.Windows.Forms.Button
    Friend WithEvents DatabaseLabel As System.Windows.Forms.Label
    Friend WithEvents clhSpaceAvailable As System.Windows.Forms.ColumnHeader

End Class
