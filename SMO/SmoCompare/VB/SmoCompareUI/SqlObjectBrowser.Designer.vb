

Partial Class SqlObjectBrowser 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SqlObjectBrowser))
        Me.cancelCommandButton = New System.Windows.Forms.Button
        Me.selectButton = New System.Windows.Forms.Button
        Me.objectBrowserTreeView = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'cancelCommandButton
        '
        resources.ApplyResources(Me.cancelCommandButton, "cancelCommandButton")
        Me.cancelCommandButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.cancelCommandButton.Name = "cancelCommandButton"
        '
        'selectButton
        '
        resources.ApplyResources(Me.selectButton, "selectButton")
        Me.selectButton.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.selectButton.Name = "selectButton"
        '
        'objectBrowserTreeView
        '
        resources.ApplyResources(Me.objectBrowserTreeView, "objectBrowserTreeView")
        Me.objectBrowserTreeView.Name = "objectBrowserTreeView"
        '
        'SqlObjectBrowser
        '
        Me.AcceptButton = Me.selectButton
        resources.ApplyResources(Me, "$this")
        Me.CancelButton = Me.cancelCommandButton
        Me.Controls.Add(Me.cancelCommandButton)
        Me.Controls.Add(Me.selectButton)
        Me.Controls.Add(Me.objectBrowserTreeView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "SqlObjectBrowser"
        Me.ShowInTaskbar = False
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private WithEvents cancelCommandButton As System.Windows.Forms.Button
    Private WithEvents selectButton As System.Windows.Forms.Button
    Private WithEvents objectBrowserTreeView As System.Windows.Forms.TreeView
End Class