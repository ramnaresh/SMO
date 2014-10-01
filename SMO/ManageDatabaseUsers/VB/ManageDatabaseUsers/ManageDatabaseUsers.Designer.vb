Partial Public Class ManageDatabaseUsers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ManageDatabaseUsers))
        Me.LoginsComboBox = New System.Windows.Forms.ComboBox
        Me.label1 = New System.Windows.Forms.Label
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.UserNameTextBox = New System.Windows.Forms.TextBox
        Me.label2 = New System.Windows.Forms.Label
        Me.DeleteUserButton = New System.Windows.Forms.Button
        Me.AddUserButton = New System.Windows.Forms.Button
        Me.clhLoginName = New System.Windows.Forms.ColumnHeader
        Me.clhUserName = New System.Windows.Forms.ColumnHeader
        Me.UsersListView = New System.Windows.Forms.ListView
        Me.clhUserID = New System.Windows.Forms.ColumnHeader
        Me.DatabasesLabel = New System.Windows.Forms.Label
        Me.UsersLabel = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'LoginsComboBox
        '
        resources.ApplyResources(Me.LoginsComboBox, "LoginsComboBox")
        Me.LoginsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.LoginsComboBox.DropDownWidth = 250
        Me.LoginsComboBox.FormattingEnabled = True
        Me.LoginsComboBox.Name = "LoginsComboBox"
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'DatabasesComboBox
        '
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.DatabasesComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'UserNameTextBox
        '
        resources.ApplyResources(Me.UserNameTextBox, "UserNameTextBox")
        Me.UserNameTextBox.Name = "UserNameTextBox"
        '
        'label2
        '
        resources.ApplyResources(Me.label2, "label2")
        Me.label2.Name = "label2"
        '
        'DeleteUserButton
        '
        resources.ApplyResources(Me.DeleteUserButton, "DeleteUserButton")
        Me.DeleteUserButton.Name = "DeleteUserButton"
        '
        'AddUserButton
        '
        resources.ApplyResources(Me.AddUserButton, "AddUserButton")
        Me.AddUserButton.Name = "AddUserButton"
        '
        'clhLoginName
        '
        resources.ApplyResources(Me.clhLoginName, "clhLoginName")
        '
        'clhUserName
        '
        resources.ApplyResources(Me.clhUserName, "clhUserName")
        '
        'UsersListView
        '
        resources.ApplyResources(Me.UsersListView, "UsersListView")
        Me.UsersListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.clhUserName, Me.clhLoginName, Me.clhUserID})
        Me.UsersListView.FullRowSelect = True
        Me.UsersListView.HideSelection = False
        Me.UsersListView.MultiSelect = False
        Me.UsersListView.Name = "UsersListView"
        Me.UsersListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.UsersListView.View = System.Windows.Forms.View.Details
        '
        'clhUserID
        '
        resources.ApplyResources(Me.clhUserID, "clhUserID")
        '
        'DatabasesLabel
        '
        Me.DatabasesLabel.BackColor = System.Drawing.SystemColors.Control
        Me.DatabasesLabel.Cursor = System.Windows.Forms.Cursors.Default
        resources.ApplyResources(Me.DatabasesLabel, "DatabasesLabel")
        Me.DatabasesLabel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.DatabasesLabel.Name = "DatabasesLabel"
        '
        'UsersLabel
        '
        resources.ApplyResources(Me.UsersLabel, "UsersLabel")
        Me.UsersLabel.Name = "UsersLabel"
        '
        'ManageDatabaseUsers
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.LoginsComboBox)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Controls.Add(Me.UserNameTextBox)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.DeleteUserButton)
        Me.Controls.Add(Me.AddUserButton)
        Me.Controls.Add(Me.UsersListView)
        Me.Controls.Add(Me.DatabasesLabel)
        Me.Controls.Add(Me.UsersLabel)
        Me.Name = "ManageDatabaseUsers"
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub
    Friend WithEvents LoginsComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents label1 As System.Windows.Forms.Label
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents label2 As System.Windows.Forms.Label
    Friend WithEvents DeleteUserButton As System.Windows.Forms.Button
    Friend WithEvents AddUserButton As System.Windows.Forms.Button
    Friend WithEvents clhLoginName As System.Windows.Forms.ColumnHeader
    Friend WithEvents clhUserName As System.Windows.Forms.ColumnHeader
    Friend WithEvents UsersListView As System.Windows.Forms.ListView
    Friend WithEvents clhUserID As System.Windows.Forms.ColumnHeader
    Friend WithEvents DatabasesLabel As System.Windows.Forms.Label
    Friend WithEvents UsersLabel As System.Windows.Forms.Label

End Class
