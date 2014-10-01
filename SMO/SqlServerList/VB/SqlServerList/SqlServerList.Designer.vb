Partial Public Class SqlServerList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SqlServerList))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.serverListBox1 = New System.Windows.Forms.ListBox
        Me.enumAvailableSqlServersLabel = New System.Windows.Forms.Label
        Me.serverListBox2 = New System.Windows.Forms.ListBox
        Me.getDataSourcesLabel = New System.Windows.Forms.Label
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        resources.ApplyResources(Me.SplitContainer1, "SplitContainer1")
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.serverListBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.enumAvailableSqlServersLabel)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.serverListBox2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.getDataSourcesLabel)
        '
        'serverListBox1
        '
        resources.ApplyResources(Me.serverListBox1, "serverListBox1")
        Me.serverListBox1.FormattingEnabled = True
        Me.serverListBox1.Name = "serverListBox1"
        Me.serverListBox1.Sorted = True
        '
        'enumAvailableSqlServersLabel
        '
        resources.ApplyResources(Me.enumAvailableSqlServersLabel, "enumAvailableSqlServersLabel")
        Me.enumAvailableSqlServersLabel.Name = "enumAvailableSqlServersLabel"
        '
        'serverListBox2
        '
        resources.ApplyResources(Me.serverListBox2, "serverListBox2")
        Me.serverListBox2.FormattingEnabled = True
        Me.serverListBox2.Name = "serverListBox2"
        Me.serverListBox2.Sorted = True
        '
        'getDataSourcesLabel
        '
        resources.ApplyResources(Me.getDataSourcesLabel, "getDataSourcesLabel")
        Me.getDataSourcesLabel.Name = "getDataSourcesLabel"
        '
        'SqlServerList
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "SqlServerList"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents serverListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents serverListBox2 As System.Windows.Forms.ListBox
    Friend WithEvents enumAvailableSqlServersLabel As System.Windows.Forms.Label
    Friend WithEvents getDataSourcesLabel As System.Windows.Forms.Label

End Class
