Public Partial Class ServerConnect
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerConnect))
        Me.SecurityPanel = New System.Windows.Forms.Panel
        Me.radioButton2 = New System.Windows.Forms.RadioButton
        Me.WindowsAuthenticationRadioButton = New System.Windows.Forms.RadioButton
        Me.SecondsLabel = New System.Windows.Forms.Label
        Me.ServerNamesComboBox = New System.Windows.Forms.ComboBox
        Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TimeoutUpDown = New System.Windows.Forms.NumericUpDown
        Me.AuthenticationLabel = New System.Windows.Forms.Label
        Me.DisplayEventsCheckBox = New System.Windows.Forms.CheckBox
        Me.ConnectTimeoutLabel = New System.Windows.Forms.Label
        Me.CancelCommandButton = New System.Windows.Forms.Button
        Me.ConnectCommandButton = New System.Windows.Forms.Button
        Me.PasswordTextBox = New System.Windows.Forms.TextBox
        Me.UserNameTextBox = New System.Windows.Forms.TextBox
        Me.PasswordLabel = New System.Windows.Forms.Label
        Me.UserNameLabel = New System.Windows.Forms.Label
        Me.ServerNameLabel = New System.Windows.Forms.Label
        Me.label1 = New System.Windows.Forms.Label
        Me.SecurityPanel.SuspendLayout()
        CType(Me.TimeoutUpDown, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SecurityPanel
        '
        Me.SecurityPanel.Controls.Add(Me.radioButton2)
        Me.SecurityPanel.Controls.Add(Me.WindowsAuthenticationRadioButton)
        resources.ApplyResources(Me.SecurityPanel, "SecurityPanel")
        Me.SecurityPanel.Name = "SecurityPanel"
        '
        'radioButton2
        '
        resources.ApplyResources(Me.radioButton2, "radioButton2")
        Me.radioButton2.Name = "radioButton2"
        '
        'WindowsAuthenticationRadioButton
        '
        Me.WindowsAuthenticationRadioButton.Checked = True
        resources.ApplyResources(Me.WindowsAuthenticationRadioButton, "WindowsAuthenticationRadioButton")
        Me.WindowsAuthenticationRadioButton.Name = "WindowsAuthenticationRadioButton"
        Me.WindowsAuthenticationRadioButton.TabStop = True
        '
        'SecondsLabel
        '
        resources.ApplyResources(Me.SecondsLabel, "SecondsLabel")
        Me.SecondsLabel.Name = "SecondsLabel"
        '
        'ServerNamesComboBox
        '
        Me.ServerNamesComboBox.FormattingEnabled = True
        resources.ApplyResources(Me.ServerNamesComboBox, "ServerNamesComboBox")
        Me.ServerNamesComboBox.Name = "ServerNamesComboBox"
        Me.ServerNamesComboBox.Sorted = True
        '
        'TimeoutUpDown
        '
        resources.ApplyResources(Me.TimeoutUpDown, "TimeoutUpDown")
        Me.TimeoutUpDown.Name = "TimeoutUpDown"
        Me.TimeoutUpDown.Value = New Decimal(New Integer() {15, 0, 0, 0})
        '
        'AuthenticationLabel
        '
        resources.ApplyResources(Me.AuthenticationLabel, "AuthenticationLabel")
        Me.AuthenticationLabel.Name = "AuthenticationLabel"
        '
        'DisplayEventsCheckBox
        '
        resources.ApplyResources(Me.DisplayEventsCheckBox, "DisplayEventsCheckBox")
        Me.DisplayEventsCheckBox.Name = "DisplayEventsCheckBox"
        '
        'ConnectTimeoutLabel
        '
        resources.ApplyResources(Me.ConnectTimeoutLabel, "ConnectTimeoutLabel")
        Me.ConnectTimeoutLabel.Name = "ConnectTimeoutLabel"
        '
        'CancelCommandButton
        '
        Me.CancelCommandButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        resources.ApplyResources(Me.CancelCommandButton, "CancelCommandButton")
        Me.CancelCommandButton.Name = "CancelCommandButton"
        '
        'ConnectCommandButton
        '
        Me.ConnectCommandButton.DialogResult = System.Windows.Forms.DialogResult.OK
        resources.ApplyResources(Me.ConnectCommandButton, "ConnectCommandButton")
        Me.ConnectCommandButton.Name = "ConnectCommandButton"
        '
        'PasswordTextBox
        '
        resources.ApplyResources(Me.PasswordTextBox, "PasswordTextBox")
        Me.PasswordTextBox.Name = "PasswordTextBox"
        '
        'UserNameTextBox
        '
        resources.ApplyResources(Me.UserNameTextBox, "UserNameTextBox")
        Me.UserNameTextBox.Name = "UserNameTextBox"
        '
        'PasswordLabel
        '
        resources.ApplyResources(Me.PasswordLabel, "PasswordLabel")
        Me.PasswordLabel.Name = "PasswordLabel"
        '
        'UserNameLabel
        '
        resources.ApplyResources(Me.UserNameLabel, "UserNameLabel")
        Me.UserNameLabel.Name = "UserNameLabel"
        '
        'ServerNameLabel
        '
        resources.ApplyResources(Me.ServerNameLabel, "ServerNameLabel")
        Me.ServerNameLabel.Name = "ServerNameLabel"
        '
        'label1
        '
        resources.ApplyResources(Me.label1, "label1")
        Me.label1.Name = "label1"
        '
        'ServerConnect
        '
        Me.AcceptButton = Me.ConnectCommandButton
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.CancelCommandButton
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.SecondsLabel)
        Me.Controls.Add(Me.ServerNamesComboBox)
        Me.Controls.Add(Me.TimeoutUpDown)
        Me.Controls.Add(Me.AuthenticationLabel)
        Me.Controls.Add(Me.DisplayEventsCheckBox)
        Me.Controls.Add(Me.ConnectTimeoutLabel)
        Me.Controls.Add(Me.CancelCommandButton)
        Me.Controls.Add(Me.ConnectCommandButton)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.Controls.Add(Me.UserNameTextBox)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UserNameLabel)
        Me.Controls.Add(Me.ServerNameLabel)
        Me.Controls.Add(Me.SecurityPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "ServerConnect"
        Me.ShowInTaskbar = False
        Me.SecurityPanel.ResumeLayout(False)
        CType(Me.TimeoutUpDown, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SecurityPanel As System.Windows.Forms.Panel
    Friend WithEvents radioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents WindowsAuthenticationRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents SecondsLabel As System.Windows.Forms.Label
    Friend WithEvents ServerNamesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents toolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TimeoutUpDown As System.Windows.Forms.NumericUpDown
    Friend WithEvents AuthenticationLabel As System.Windows.Forms.Label
    Friend WithEvents DisplayEventsCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents ConnectTimeoutLabel As System.Windows.Forms.Label
    Friend WithEvents CancelCommandButton As System.Windows.Forms.Button
    Friend WithEvents ConnectCommandButton As System.Windows.Forms.Button
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PasswordLabel As System.Windows.Forms.Label
    Friend WithEvents UserNameLabel As System.Windows.Forms.Label
    Friend WithEvents ServerNameLabel As System.Windows.Forms.Label
    Friend WithEvents label1 As System.Windows.Forms.Label
End Class
