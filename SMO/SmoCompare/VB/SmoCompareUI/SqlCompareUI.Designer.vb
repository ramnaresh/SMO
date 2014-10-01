

Partial Class SqlCompareUI 'The Partial modifier is only required on one class definition per project.
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
        Dim resources As New System.ComponentModel.ComponentResourceManager(GetType(SqlCompareUI))
        Me.value1ColumnHeader = New System.Windows.Forms.ColumnHeader()
        Me.urn2ColumnHeader = New System.Windows.Forms.ColumnHeader()
        Me.serverLabel1 = New System.Windows.Forms.Label()
        Me.value2ColumnHeader = New System.Windows.Forms.ColumnHeader()
        Me.propertyNameColumnHeader = New System.Windows.Forms.ColumnHeader()
        Me.differencesListView = New System.Windows.Forms.ListView()
        Me.urn1ColumnHeader = New System.Windows.Forms.ColumnHeader()
        Me.shallowCompareButton = New System.Windows.Forms.Button()
        Me.serverTextBox1 = New System.Windows.Forms.TextBox()
        Me.urnCompareGroupBox = New System.Windows.Forms.GroupBox()
        Me.urnTextBox1 = New System.Windows.Forms.TextBox()
        Me.object1URNLabel = New System.Windows.Forms.Label()
        Me.object2URNLabel = New System.Windows.Forms.Label()
        Me.urnTextBox2 = New System.Windows.Forms.TextBox()
        Me.objectBrowse2Button = New System.Windows.Forms.Button()
        Me.objectBrowse1Button = New System.Windows.Forms.Button()
        Me.serverLoginGroupBox = New System.Windows.Forms.GroupBox()
        Me.serverLabel2 = New System.Windows.Forms.Label()
        Me.serverTextBox2 = New System.Windows.Forms.TextBox()
        Me.inDepthCompareButton = New System.Windows.Forms.Button()
        Me.passwordTextBox2 = New System.Windows.Forms.TextBox()
        Me.loginTextBox2 = New System.Windows.Forms.TextBox()
        Me.loginLabel2 = New System.Windows.Forms.Label()
        Me.passwordLabel2 = New System.Windows.Forms.Label()
        Me.passwordTextBox1 = New System.Windows.Forms.TextBox()
        Me.loginTextBox1 = New System.Windows.Forms.TextBox()
        Me.loginLabel1 = New System.Windows.Forms.Label()
        Me.passwordLabel1 = New System.Windows.Forms.Label()
        Me.genScript2to1Button = New System.Windows.Forms.Button()
        Me.genScript1to2Button = New System.Windows.Forms.Button()
        Me.differencesLabel = New System.Windows.Forms.Label()
        Me.col1 = New System.Windows.Forms.ColumnHeader()
        Me.objectListView2 = New System.Windows.Forms.ListView()
        Me.col2 = New System.Windows.Forms.ColumnHeader()
        Me.object2OnlyLabel = New System.Windows.Forms.Label()
        Me.object1OnlyLabel = New System.Windows.Forms.Label()
        Me.objectListView1 = New System.Windows.Forms.ListView()
        Me.urnCompareGroupBox.SuspendLayout()
        Me.serverLoginGroupBox.SuspendLayout()
        Me.SuspendLayout()
        ' 
        ' value1ColumnHeader
        ' 
        Me.value1ColumnHeader.Name = "value1ColumnHeader"
        resources.ApplyResources(Me.value1ColumnHeader, "value1ColumnHeader")
        ' 
        ' urn2ColumnHeader
        ' 
        Me.urn2ColumnHeader.Name = "urn2ColumnHeader"
        resources.ApplyResources(Me.urn2ColumnHeader, "urn2ColumnHeader")
        ' 
        ' serverLabel1
        ' 
        resources.ApplyResources(Me.serverLabel1, "serverLabel1")
        Me.serverLabel1.Name = "serverLabel1"
        ' 
        ' value2ColumnHeader
        ' 
        Me.value2ColumnHeader.Name = "value2ColumnHeader"
        resources.ApplyResources(Me.value2ColumnHeader, "value2ColumnHeader")
        ' 
        ' propertyNameColumnHeader
        ' 
        Me.propertyNameColumnHeader.Name = "propertyNameColumnHeader"
        resources.ApplyResources(Me.propertyNameColumnHeader, "propertyNameColumnHeader")
        ' 
        ' differencesListView
        ' 
        resources.ApplyResources(Me.differencesListView, "differencesListView")
        Me.differencesListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.propertyNameColumnHeader, Me.urn1ColumnHeader, Me.urn2ColumnHeader, Me.value1ColumnHeader, Me.value2ColumnHeader})
        Me.differencesListView.FullRowSelect = True
        Me.differencesListView.GridLines = True
        Me.differencesListView.Name = "differencesListView"
        Me.differencesListView.View = System.Windows.Forms.View.Details
        ' 
        ' urn1ColumnHeader
        ' 
        resources.ApplyResources(Me.urn1ColumnHeader, "urn1ColumnHeader")
        ' 
        ' shallowCompareButton
        ' 
        resources.ApplyResources(Me.shallowCompareButton, "shallowCompareButton")
        Me.shallowCompareButton.Name = "shallowCompareButton"
        ' 
        ' serverTextBox1
        ' 
        resources.ApplyResources(Me.serverTextBox1, "serverTextBox1")
        Me.serverTextBox1.Name = "serverTextBox1"
        ' 
        ' urnCompareGroupBox
        ' 
        resources.ApplyResources(Me.urnCompareGroupBox, "urnCompareGroupBox")
        Me.urnCompareGroupBox.Controls.Add(Me.urnTextBox1)
        Me.urnCompareGroupBox.Controls.Add(Me.object1URNLabel)
        Me.urnCompareGroupBox.Controls.Add(Me.object2URNLabel)
        Me.urnCompareGroupBox.Controls.Add(Me.urnTextBox2)
        Me.urnCompareGroupBox.Controls.Add(Me.objectBrowse2Button)
        Me.urnCompareGroupBox.Controls.Add(Me.objectBrowse1Button)
        Me.urnCompareGroupBox.Name = "urnCompareGroupBox"
        Me.urnCompareGroupBox.TabStop = False
        ' 
        ' urnTextBox1
        ' 
        resources.ApplyResources(Me.urnTextBox1, "urnTextBox1")
        Me.urnTextBox1.Name = "urnTextBox1"
        Me.urnTextBox1.ReadOnly = True
        ' 
        ' object1URNLabel
        ' 
        resources.ApplyResources(Me.object1URNLabel, "object1URNLabel")
        Me.object1URNLabel.Name = "object1URNLabel"
        ' 
        ' object2URNLabel
        ' 
        resources.ApplyResources(Me.object2URNLabel, "object2URNLabel")
        Me.object2URNLabel.Name = "object2URNLabel"
        ' 
        ' urnTextBox2
        ' 
        resources.ApplyResources(Me.urnTextBox2, "urnTextBox2")
        Me.urnTextBox2.Name = "urnTextBox2"
        Me.urnTextBox2.ReadOnly = True
        ' 
        ' objectBrowse2Button
        ' 
        resources.ApplyResources(Me.objectBrowse2Button, "objectBrowse2Button")
        Me.objectBrowse2Button.Name = "objectBrowse2Button"
        ' 
        ' objectBrowse1Button
        ' 
        resources.ApplyResources(Me.objectBrowse1Button, "objectBrowse1Button")
        Me.objectBrowse1Button.Name = "objectBrowse1Button"
        ' 
        ' serverLoginGroupBox
        ' 
        resources.ApplyResources(Me.serverLoginGroupBox, "serverLoginGroupBox")
        Me.serverLoginGroupBox.Controls.Add(Me.serverLabel1)
        Me.serverLoginGroupBox.Controls.Add(Me.shallowCompareButton)
        Me.serverLoginGroupBox.Controls.Add(Me.serverTextBox1)
        Me.serverLoginGroupBox.Controls.Add(Me.serverLabel2)
        Me.serverLoginGroupBox.Controls.Add(Me.serverTextBox2)
        Me.serverLoginGroupBox.Controls.Add(Me.inDepthCompareButton)
        Me.serverLoginGroupBox.Controls.Add(Me.passwordTextBox2)
        Me.serverLoginGroupBox.Controls.Add(Me.loginTextBox2)
        Me.serverLoginGroupBox.Controls.Add(Me.loginLabel2)
        Me.serverLoginGroupBox.Controls.Add(Me.passwordLabel2)
        Me.serverLoginGroupBox.Controls.Add(Me.passwordTextBox1)
        Me.serverLoginGroupBox.Controls.Add(Me.loginTextBox1)
        Me.serverLoginGroupBox.Controls.Add(Me.loginLabel1)
        Me.serverLoginGroupBox.Controls.Add(Me.passwordLabel1)
        Me.serverLoginGroupBox.Name = "serverLoginGroupBox"
        Me.serverLoginGroupBox.TabStop = False
        ' 
        ' serverLabel2
        ' 
        resources.ApplyResources(Me.serverLabel2, "serverLabel2")
        Me.serverLabel2.Name = "serverLabel2"
        ' 
        ' serverTextBox2
        ' 
        resources.ApplyResources(Me.serverTextBox2, "serverTextBox2")
        Me.serverTextBox2.Name = "serverTextBox2"
        ' 
        ' inDepthCompareButton
        ' 
        resources.ApplyResources(Me.inDepthCompareButton, "inDepthCompareButton")
        Me.inDepthCompareButton.Name = "inDepthCompareButton"
        ' 
        ' passwordTextBox2
        ' 
        resources.ApplyResources(Me.passwordTextBox2, "passwordTextBox2")
        Me.passwordTextBox2.Name = "passwordTextBox2"
        ' 
        ' loginTextBox2
        ' 
        resources.ApplyResources(Me.loginTextBox2, "loginTextBox2")
        Me.loginTextBox2.Name = "loginTextBox2"
        ' 
        ' loginLabel2
        ' 
        resources.ApplyResources(Me.loginLabel2, "loginLabel2")
        Me.loginLabel2.Name = "loginLabel2"
        ' 
        ' passwordLabel2
        ' 
        resources.ApplyResources(Me.passwordLabel2, "passwordLabel2")
        Me.passwordLabel2.Name = "passwordLabel2"
        ' 
        ' passwordTextBox1
        ' 
        resources.ApplyResources(Me.passwordTextBox1, "passwordTextBox1")
        Me.passwordTextBox1.Name = "passwordTextBox1"
        ' 
        ' loginTextBox1
        ' 
        resources.ApplyResources(Me.loginTextBox1, "loginTextBox1")
        Me.loginTextBox1.Name = "loginTextBox1"
        ' 
        ' loginLabel1
        ' 
        resources.ApplyResources(Me.loginLabel1, "loginLabel1")
        Me.loginLabel1.Name = "loginLabel1"
        ' 
        ' passwordLabel1
        ' 
        resources.ApplyResources(Me.passwordLabel1, "passwordLabel1")
        Me.passwordLabel1.Name = "passwordLabel1"
        ' 
        ' genScript2to1Button
        ' 
        resources.ApplyResources(Me.genScript2to1Button, "genScript2to1Button")
        Me.genScript2to1Button.Name = "genScript2to1Button"
        ' 
        ' genScript1to2Button
        ' 
        resources.ApplyResources(Me.genScript1to2Button, "genScript1to2Button")
        Me.genScript1to2Button.Name = "genScript1to2Button"
        ' 
        ' differencesLabel
        ' 
        resources.ApplyResources(Me.differencesLabel, "differencesLabel")
        Me.differencesLabel.Name = "differencesLabel"
        ' 
        ' col1
        ' 
        Me.col1.Name = "col1"
        resources.ApplyResources(Me.col1, "col1")
        ' 
        ' objectListView2
        ' 
        resources.ApplyResources(Me.objectListView2, "objectListView2")
        Me.objectListView2.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col2})
        Me.objectListView2.FullRowSelect = True
        Me.objectListView2.GridLines = True
        Me.objectListView2.Name = "objectListView2"
        Me.objectListView2.View = System.Windows.Forms.View.Details
        ' 
        ' col2
        ' 
        resources.ApplyResources(Me.col2, "col2")
        ' 
        ' object2OnlyLabel
        ' 
        resources.ApplyResources(Me.object2OnlyLabel, "object2OnlyLabel")
        Me.object2OnlyLabel.Name = "object2OnlyLabel"
        ' 
        ' object1OnlyLabel
        ' 
        resources.ApplyResources(Me.object1OnlyLabel, "object1OnlyLabel")
        Me.object1OnlyLabel.Name = "object1OnlyLabel"
        ' 
        ' objectListView1
        ' 
        resources.ApplyResources(Me.objectListView1, "objectListView1")
        Me.objectListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.col1})
        Me.objectListView1.FullRowSelect = True
        Me.objectListView1.GridLines = True
        Me.objectListView1.Name = "objectListView1"
        Me.objectListView1.View = System.Windows.Forms.View.Details
        ' 
        ' SqlCompareUI
        ' 
        resources.ApplyResources(Me, "$this")
        Me.Controls.Add(differencesListView)
        Me.Controls.Add(urnCompareGroupBox)
        Me.Controls.Add(serverLoginGroupBox)
        Me.Controls.Add(genScript2to1Button)
        Me.Controls.Add(genScript1to2Button)
        Me.Controls.Add(differencesLabel)
        Me.Controls.Add(objectListView2)
        Me.Controls.Add(object2OnlyLabel)
        Me.Controls.Add(object1OnlyLabel)
        Me.Controls.Add(objectListView1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.Name = "SqlCompareUI"
        Me.urnCompareGroupBox.ResumeLayout(False)
        Me.urnCompareGroupBox.PerformLayout()
        Me.serverLoginGroupBox.ResumeLayout(False)
        Me.serverLoginGroupBox.PerformLayout()
        Me.ResumeLayout(False)
    
    End Sub
    
    #End Region
    
    Private value1ColumnHeader As System.Windows.Forms.ColumnHeader
    Private urn2ColumnHeader As System.Windows.Forms.ColumnHeader
    Private serverLabel1 As System.Windows.Forms.Label
    Private value2ColumnHeader As System.Windows.Forms.ColumnHeader
    Private propertyNameColumnHeader As System.Windows.Forms.ColumnHeader
    Private differencesListView As System.Windows.Forms.ListView
    Private urn1ColumnHeader As System.Windows.Forms.ColumnHeader
    Private WithEvents shallowCompareButton As System.Windows.Forms.Button
    Private WithEvents serverTextBox1 As System.Windows.Forms.TextBox
    Private urnCompareGroupBox As System.Windows.Forms.GroupBox
    Private urnTextBox1 As System.Windows.Forms.TextBox
    Private object1URNLabel As System.Windows.Forms.Label
    Private object2URNLabel As System.Windows.Forms.Label
    Private urnTextBox2 As System.Windows.Forms.TextBox
    Private WithEvents objectBrowse2Button As System.Windows.Forms.Button
    Private WithEvents objectBrowse1Button As System.Windows.Forms.Button
    Private serverLoginGroupBox As System.Windows.Forms.GroupBox
    Private serverLabel2 As System.Windows.Forms.Label
    Private WithEvents serverTextBox2 As System.Windows.Forms.TextBox
    Private WithEvents inDepthCompareButton As System.Windows.Forms.Button
    Private WithEvents passwordTextBox2 As System.Windows.Forms.TextBox
    Private WithEvents loginTextBox2 As System.Windows.Forms.TextBox
    Private loginLabel2 As System.Windows.Forms.Label
    Private passwordLabel2 As System.Windows.Forms.Label
    Private WithEvents passwordTextBox1 As System.Windows.Forms.TextBox
    Private WithEvents loginTextBox1 As System.Windows.Forms.TextBox
    Private loginLabel1 As System.Windows.Forms.Label
    Private passwordLabel1 As System.Windows.Forms.Label
    Private WithEvents genScript2to1Button As System.Windows.Forms.Button
    Private WithEvents genScript1to2Button As System.Windows.Forms.Button
    Private differencesLabel As System.Windows.Forms.Label
    Private col1 As System.Windows.Forms.ColumnHeader
    Private objectListView2 As System.Windows.Forms.ListView
    Private col2 As System.Windows.Forms.ColumnHeader
    Private object2OnlyLabel As System.Windows.Forms.Label
    Private object1OnlyLabel As System.Windows.Forms.Label
    Private objectListView1 As System.Windows.Forms.ListView
End Class
