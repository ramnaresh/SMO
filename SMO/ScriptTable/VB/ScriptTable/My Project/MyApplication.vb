'------------------------------------------------------------------------------
' <autogenerated>
'     This code was generated by a tool.
'     Runtime Version:2.0.40603.0
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </autogenerated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Namespace My
    
    Partial Class MyApplication

        <Global.System.Diagnostics.DebuggerStepThrough()> _
        Public Sub New()
            MyBase.New(ApplicationServices.AuthenticationMode.Windows)
            Me.IsSingleInstance = False
            Me.EnableVisualStyles = True
            Me.ShutdownStyle = ApplicationServices.ShutdownMode.AfterMainFormCloses
        End Sub

        <Global.System.Diagnostics.DebuggerStepThrough()> _
        Protected Overrides Sub OnCreateMainForm()
            Me.MainForm = My.Forms.ScriptTable
        End Sub
    End Class
End Namespace
