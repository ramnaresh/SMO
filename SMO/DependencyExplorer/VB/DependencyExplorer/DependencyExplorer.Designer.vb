Partial Public Class DependencyExplorer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DependencyExplorer))
        Me.StatusBar = New System.Windows.Forms.TextBox
        Me.DatabasesComboBox = New System.Windows.Forms.ComboBox
        Me.TableListView = New System.Windows.Forms.ListView
        Me.Label1 = New System.Windows.Forms.Label
        Me.ShowDropDdlmenuItem = New System.Windows.Forms.MenuItem
        Me.ConnectMenuItem = New System.Windows.Forms.MenuItem
        Me.MenuItem2 = New System.Windows.Forms.MenuItem
        Me.WithoutDependenciesMenuItem = New System.Windows.Forms.MenuItem
        Me.WithDependenciesMenuItem = New System.Windows.Forms.MenuItem
        Me.ShowDependenciesmenuItem = New System.Windows.Forms.MenuItem
        Me.MainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
        Me.MenuItem1 = New System.Windows.Forms.MenuItem
        Me.MenuItem4 = New System.Windows.Forms.MenuItem
        Me.MenuItem5 = New System.Windows.Forms.MenuItem
        Me.MenuItem3 = New System.Windows.Forms.MenuItem
        Me.SuspendLayout()
        '
        'StatusBar
        '
        resources.ApplyResources(Me.StatusBar, "StatusBar")
        Me.StatusBar.Name = "StatusBar"
        Me.StatusBar.ReadOnly = True
        '
        'DatabasesComboBox
        '
        Me.DatabasesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        resources.ApplyResources(Me.DatabasesComboBox, "DatabasesComboBox")
        Me.DatabasesComboBox.FormattingEnabled = True
        Me.DatabasesComboBox.Name = "DatabasesComboBox"
        '
        'TableListView
        '
        resources.ApplyResources(Me.TableListView, "TableListView")
        Me.TableListView.Items.AddRange(New System.Windows.Forms.ListViewItem() {CType(resources.GetObject("TableListView.Items"), System.Windows.Forms.ListViewItem), CType(resources.GetObject("TableListView.Items1"), System.Windows.Forms.ListViewItem)})
        Me.TableListView.MultiSelect = False
        Me.TableListView.Name = "TableListView"
        Me.TableListView.UseCompatibleStateImageBehavior = False
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'ShowDropDdlmenuItem
        '
        Me.ShowDropDdlmenuItem.Index = 0
        resources.ApplyResources(Me.ShowDropDdlmenuItem, "ShowDropDdlmenuItem")
        '
        'ConnectMenuItem
        '
        Me.ConnectMenuItem.Index = 0
        resources.ApplyResources(Me.ConnectMenuItem, "ConnectMenuItem")
        '
        'MenuItem2
        '
        Me.MenuItem2.Index = 1
        Me.MenuItem2.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.WithoutDependenciesMenuItem, Me.WithDependenciesMenuItem, Me.ShowDependenciesmenuItem})
        resources.ApplyResources(Me.MenuItem2, "MenuItem2")
        '
        'WithoutDependenciesMenuItem
        '
        Me.WithoutDependenciesMenuItem.Index = 0
        resources.ApplyResources(Me.WithoutDependenciesMenuItem, "WithoutDependenciesMenuItem")
        '
        'WithDependenciesMenuItem
        '
        Me.WithDependenciesMenuItem.Index = 1
        resources.ApplyResources(Me.WithDependenciesMenuItem, "WithDependenciesMenuItem")
        '
        'ShowDependenciesmenuItem
        '
        Me.ShowDependenciesmenuItem.Index = 2
        resources.ApplyResources(Me.ShowDependenciesmenuItem, "ShowDependenciesmenuItem")
        '
        'MainMenu1
        '
        Me.MainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.MenuItem1, Me.MenuItem2, Me.MenuItem3})
        '
        'MenuItem1
        '
        Me.MenuItem1.Index = 0
        Me.MenuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ConnectMenuItem, Me.MenuItem4, Me.MenuItem5})
        resources.ApplyResources(Me.MenuItem1, "MenuItem1")
        '
        'MenuItem4
        '
        Me.MenuItem4.Index = 1
        resources.ApplyResources(Me.MenuItem4, "MenuItem4")
        '
        'MenuItem5
        '
        Me.MenuItem5.Index = 2
        resources.ApplyResources(Me.MenuItem5, "MenuItem5")
        '
        'MenuItem3
        '
        Me.MenuItem3.Index = 2
        Me.MenuItem3.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.ShowDropDdlmenuItem})
        resources.ApplyResources(Me.MenuItem3, "MenuItem3")
        '
        'DependencyExplorer
        '
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(Me.DatabasesComboBox)
        Me.Controls.Add(Me.TableListView)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.StatusBar)
        Me.Menu = Me.MainMenu1
        Me.Name = "DependencyExplorer"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusBar As System.Windows.Forms.TextBox
    Friend WithEvents DatabasesComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TableListView As System.Windows.Forms.ListView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ShowDropDdlmenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ConnectMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem2 As System.Windows.Forms.MenuItem
    Friend WithEvents WithoutDependenciesMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents WithDependenciesMenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents ShowDependenciesmenuItem As System.Windows.Forms.MenuItem
    Friend WithEvents MainMenu1 As System.Windows.Forms.MainMenu
    Friend WithEvents MenuItem1 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem3 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem4 As System.Windows.Forms.MenuItem
    Friend WithEvents MenuItem5 As System.Windows.Forms.MenuItem

End Class
