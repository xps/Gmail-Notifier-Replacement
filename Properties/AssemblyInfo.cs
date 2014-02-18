using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Gmail Notifier Replacement")]
[assembly: AssemblyDescription("A wannabe replacement for the defunct Gmail Notifier")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Xavier Poinas")]
[assembly: AssemblyProduct("GmailNotifierReplacement")]
[assembly: AssemblyCopyright("Copyright © Xavier Poinas 2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("22d51b97-adbb-4db0-b197-bbf06f910b41")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// Initialize log4net
[assembly: log4net.Config.XmlConfigurator(Watch = false)]