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
        ///   Looks up a localized string similar to Backup and Restore Database - .
        /// </summary>
        internal static string AppTitle {
            get {
                return ResourceManager.GetString("AppTitle", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Backing up the database
        ///.
        /// </summary>
        internal static string BackingUp {
            get {
                return ResourceManager.GetString("BackingUp", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Backup complete!
        ///.
        /// </summary>
        internal static string BackupComplete {
            get {
                return ResourceManager.GetString("BackupComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0} Backup.
        /// </summary>
        internal static string BackupSetName {
            get {
                return ResourceManager.GetString("BackupSetName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Completed: {0}.
        /// </summary>
        internal static string CompletedPercent {
            get {
                return ResourceManager.GetString("CompletedPercent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Generated script
        ///.
        /// </summary>
        internal static string GeneratedScript {
            get {
                return ResourceManager.GetString("GeneratedScript", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample Backup Media Set # 1.
        /// </summary>
        internal static string MediaDescription {
            get {
                return ResourceManager.GetString("MediaDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Set #1.
        /// </summary>
        internal static string MediaName {
            get {
                return ResourceManager.GetString("MediaName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ..
        /// </summary>
        internal static string ProgressCharacter {
            get {
                return ResourceManager.GetString("ProgressCharacter", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Do you really want to restore the {0} database?
        ///*** Note: This will overwrite the existing database. ***.
        /// </summary>
        internal static string ReallyRestore {
            get {
                return ResourceManager.GetString("ReallyRestore", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to 
        ///Restore complete!
        ///.
        /// </summary>
        internal static string RestoreComplete {
            get {
                return ResourceManager.GetString("RestoreComplete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Restoring the database
        ///.
        /// </summary>
        internal static string Restoring {
            get {
                return ResourceManager.GetString("Restoring", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Sample Backup of {0}.
        /// </summary>
        internal static string SampleBackup {
            get {
                return ResourceManager.GetString("SampleBackup", resourceCulture);
            }
        }
    }
}