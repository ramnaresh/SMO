'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version:2.0.40903.31
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    Partial Class MyApplication
        
        <System.Diagnostics.DebuggerStepThrough()>  _
        Public Sub New()
            MyBase.New(Microsoft.VisualBasic.ApplicationServices.AuthenticationMode.Windows)
            Me.IsSingleInstance = false
            Me.EnableVisualStyles = true
            Me.ShutDownStyle = Microsoft.VisualBasic.ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub
        
        <System.Diagnostics.DebuggerStepThrough()>  _
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = My.Forms.SqlServerList
        End Sub
    End Class
End Namespace
