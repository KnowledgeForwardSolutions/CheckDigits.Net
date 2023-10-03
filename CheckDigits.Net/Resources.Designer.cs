﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CheckDigits.Net {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
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
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CheckDigits.Net.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Luhn algorithm - modulus 10 algorithm with weight 2 applied to every odd position character.
        /// </summary>
        internal static string LuhnAlgorithmDescription {
            get {
                return ResourceManager.GetString("LuhnAlgorithmDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Luhn.
        /// </summary>
        internal static string LuhnAlgorithmName {
            get {
                return ResourceManager.GetString("LuhnAlgorithmName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Modulus10_13 - modulus 10 algorithm with weights 1 and 3.
        /// </summary>
        internal static string Modulus10_13AlgorithmDescription {
            get {
                return ResourceManager.GetString("Modulus10_13AlgorithmDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Modulus10_13.
        /// </summary>
        internal static string Modulus10_13AlgorithmName {
            get {
                return ResourceManager.GetString("Modulus10_13AlgorithmName", resourceCulture);
            }
        }
    }
}