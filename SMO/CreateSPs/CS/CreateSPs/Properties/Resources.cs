﻿//------------------------------------------------------------------------------
// <autogenerated>
//     This code was generated by a tool.
//     Runtime Version:2.0.40903.31
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </autogenerated>
//------------------------------------------------------------------------------

namespace Microsoft.Samples.SqlServer.Properties {
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the Strongly Typed Resource Builder
    // class via a tool like ResGen or Visual Studio.NET.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    class Resources {
        
        private static System.Resources.ResourceManager _resMgr;
        
        private static System.Globalization.CultureInfo _resCulture;
        
        /*FamANDAssem*/ internal Resources() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Resources.ResourceManager ResourceManager {
            get {
                if ((_resMgr == null)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Microsoft.Samples.SqlServer.Properties.Resources", typeof(Resources).Assembly);
                    _resMgr = temp;
                }
                return _resMgr;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        public static System.Globalization.CultureInfo Culture {
            get {
                return _resCulture;
            }
            set {
                _resCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Completed".
        /// </summary>
        public static string Completed {
            get {
                return ResourceManager.GetString("Completed", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Creating: {0}".
        /// </summary>
        public static string Creating {
            get {
                return ResourceManager.GetString("Creating", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Creating stored procedure - {0}.{1}".
        /// </summary>
        public static string CreatingStoredProcedure {
            get {
                return ResourceManager.GetString("CreatingStoredProcedure", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Dropping stored procedure - {0}.{1}".
        /// </summary>
        public static string DroppingStoredProcedure {
            get {
                return ResourceManager.GetString("DroppingStoredProcedure", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "{0}.{1,4:D4} seconds".
        /// </summary>
        public static string ElapsedTime {
            get {
                return ResourceManager.GetString("ElapsedTime", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Ready".
        /// </summary>
        public static string Ready {
            get {
                return ResourceManager.GetString("Ready", _resCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to "Do you really want to create stored procedures?
        ///*** Note: This will overwrite the existing stored procedures. ***".
        /// </summary>
        public static string ReallyCreate {
            get {
                return ResourceManager.GetString("ReallyCreate", _resCulture);
            }
        }
    }
}
