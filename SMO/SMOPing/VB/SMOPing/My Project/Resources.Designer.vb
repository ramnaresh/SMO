﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:2.0.50215.44
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

<Assembly: Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope:="member", Target:="My.Resources.Resources.get_ResourceManager():System.Resources.ResourceManager"),  _
 Assembly: Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope:="member", Target:="My.Resources.Resources.get_Culture():System.Globalization.CultureInfo"),  _
 Assembly: Global.System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Scope:="member", Target:="My.Resources.Resources.set_Culture(System.Globalization.CultureInfo):Void")> 

Namespace My.Resources
    
    'This class was auto-generated by the StronglyTypedResourceBuilder
    'class via a tool like ResGen or Visual Studio.
    'To add or remove a member, edit your .ResX file then rerun ResGen
    'with the /str option, or rebuild your VS project.
    '''<summary>
    '''  A strongly-typed resource class, for looking up localized strings, etc.
    '''</summary>
    <Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleName()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Returns the cached ResourceManager instance used by this class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If (resourceMan Is Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("Microsoft.Samples.SqlServer.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Overrides the current thread's CurrentUICulture property for all
        '''  resource lookups using this strongly typed resource class.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error in SMOPing argument ({0}).
        '''</summary>
        Friend ReadOnly Property ArgumentError() As String
            Get
                Return ResourceManager.GetString("ArgumentError", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Connection String: {0}.
        '''</summary>
        Friend ReadOnly Property ConnectionString() As String
            Get
                Return ResourceManager.GetString("ConnectionString", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Connection Timeout: {0}.
        '''</summary>
        Friend ReadOnly Property ConnectTimeout() As String
            Get
                Return ResourceManager.GetString("ConnectTimeout", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Error: {0}.
        '''</summary>
        Friend ReadOnly Property ErrorMessage() As String
            Get
                Return ResourceManager.GetString("ErrorMessage", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Execution Mode: {0}.
        '''</summary>
        Friend ReadOnly Property ExecutionMode() As String
            Get
                Return ResourceManager.GetString("ExecutionMode", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SMOPing [-S] [-U -P | -E] [-T] [-V] [-v] [-?]
        '''  -S server name
        '''  -U user name
        '''  -P password
        '''  -E integrated security, defaults to true, overrides -U option
        '''  -T connection timeout (seconds)
        '''  -V verbose mode, defaults to false
        '''  -v display SQL Server version
        '''  -? help and usage information
        '''.
        '''</summary>
        Friend ReadOnly Property Help() As String
            Get
                Return ResourceManager.GetString("Help", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to *** Using integrated security ***.
        '''</summary>
        Friend ReadOnly Property IntegratedSecurity() As String
            Get
                Return ResourceManager.GetString("IntegratedSecurity", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Max Pool Size: {0}.
        '''</summary>
        Friend ReadOnly Property MaxPoolSize() As String
            Get
                Return ResourceManager.GetString("MaxPoolSize", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Min Pool Size: {0}.
        '''</summary>
        Friend ReadOnly Property MinPoolSize() As String
            Get
                Return ResourceManager.GetString("MinPoolSize", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Network Protocol: {0}.
        '''</summary>
        Friend ReadOnly Property NetworkProtocol() As String
            Get
                Return ResourceManager.GetString("NetworkProtocol", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to PacketSize: {0}.
        '''</summary>
        Friend ReadOnly Property PacketSize() As String
            Get
                Return ResourceManager.GetString("PacketSize", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Pooled Connection Lifetime: {0}.
        '''</summary>
        Friend ReadOnly Property PooledConnectionLifetime() As String
            Get
                Return ResourceManager.GetString("PooledConnectionLifetime", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Press &lt;Enter&gt; to continue.
        '''</summary>
        Friend ReadOnly Property PressEnter() As String
            Get
                Return ResourceManager.GetString("PressEnter", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to *** Using SQLServer security ***.
        '''</summary>
        Friend ReadOnly Property SQLSecurity() As String
            Get
                Return ResourceManager.GetString("SQLSecurity", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SQL Server login: {0}.
        '''</summary>
        Friend ReadOnly Property SqlServerLogin() As String
            Get
                Return ResourceManager.GetString("SqlServerLogin", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SQL Server name: {0}.
        '''</summary>
        Friend ReadOnly Property SqlServerName() As String
            Get
                Return ResourceManager.GetString("SqlServerName", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to SQL Server version: {0}.{1}.{2}.
        '''</summary>
        Friend ReadOnly Property SqlServerVersion() As String
            Get
                Return ResourceManager.GetString("SqlServerVersion", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Looks up a localized string similar to Statement Timeout: {0}.
        '''</summary>
        Friend ReadOnly Property StatementTimeout() As String
            Get
                Return ResourceManager.GetString("StatementTimeout", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
