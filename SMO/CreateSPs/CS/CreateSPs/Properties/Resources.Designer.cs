﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.26
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.SqlServer.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Microsoft.Samples.SqlServer.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Create Stored Procedures - .
        /// </summary>
        internal static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Completed
        ///.
        /// </summary>
        internal static string Completed {
            get {
                return ResourceManager.GetString("Completed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating: {0}.
        /// </summary>
        internal static string Creating {
            get {
                return ResourceManager.GetString("Creating", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Creating stored procedure - {0}.{1}.
        /// </summary>
        internal static string CreatingStoredProcedure {
            get {
                return ResourceManager.GetString("CreatingStoredProcedure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Dropping stored procedure - {0}.{1}.
        /// </summary>
        internal static string DroppingStoredProcedure {
            get {
                return ResourceManager.GetString("DroppingStoredProcedure", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.{1,4:D4} seconds.
        /// </summary>
        internal static string ElapsedTime {
            get {
                return ResourceManager.GetString("ElapsedTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ready.
        /// </summary>
        internal static string Ready {
            get {
                return ResourceManager.GetString("Ready", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to create stored procedures?
        ///*** Note: This will overwrite the existing stored procedures. ***.
        /// </summary>
        internal static string ReallyCreate {
            get {
                return ResourceManager.GetString("ReallyCreate", resourceCulture);
            }
        }
    }
}
