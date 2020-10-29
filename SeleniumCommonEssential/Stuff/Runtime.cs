using NunitTest.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NunitTest.Stuff
{
    /// <summary>
    /// Utility to access runtime
    /// </summary>
    public static class Runtime
    {
        /// <summary>
        /// Get executing assembly
        /// </summary>
        public static Assembly ExecutingAssembly => Assembly.GetExecutingAssembly();

        /// <summary>
        /// Get executing folder
        /// </summary>
        public static string ExecutingFolder => Path.GetDirectoryName(ExecutingAssembly.Location);

        /// <summary>
        /// Get logs storage folder (executing folder + Logs)
        /// </summary>
        public static string LogOutputFolder { get; set; } = Path.Combine(ExecutingFolder, "Logs");

        /// <summary>
        /// Get the assembly based on the name provided
        /// The assembly is fetched from the stacktrace information and not by reading the assembly folder
        /// </summary>
        /// <param name="name">name of the assembly to fetch</param>
        /// <returns>the assembly </returns>
        public static Assembly GetAssembly(string name) => new StackTrace().GetFrames()
            .Select(x => x?.GetMethod().ReflectedType?.Assembly).Distinct()
            .FirstOrDefault(x => x != null && (x.ManifestModule.Name.EqualsIgnoreCase(name) ||
                                Path.GetFileNameWithoutExtension(x.ManifestModule.Name).EqualsIgnoreCase(name)));

        /// <summary>
        /// Get the calling assembly of the current invoking method from the stack trace
        /// </summary>
        public static Assembly CallingAssembly => new StackTrace().GetFrames()
            .Select(x => x?.GetMethod().ReflectedType?.Assembly).Distinct()
            .LastOrDefault(x => x != null && x.GetReferencedAssemblies().Any(y => y.FullName == ExecutingAssembly.FullName));

        /// <summary>
        /// Get the caller methodbase of the current inoking method from the stack trace
        /// </summary>
        public static MethodBase CallerMethod => new StackTrace().GetFrames()
            .Where(x => x?.GetMethod().ReflectedType?.Assembly.FullName == CallingAssembly.FullName)
            .Select(x => x.GetMethod())
            .Distinct()
            .LastOrDefault();

        /// <summary>
        /// Get the caller method name of the current invoking method from the stack trace
        /// </summary>
        /// <returns></returns>
        public static string GetCallerMethodName() => CallerMethod.Name;

        /// <summary>
        /// Get the caller type of the current invoking method from the stack trace
        /// </summary>
        public static Type CallerType => CallerMethod.DeclaringType;

        /// <summary>
        /// Get the assembly full path of the current invoking assembly from the stack trace
        /// </summary>
        public static string CallingAssemblyFullPath => CallingAssembly?.Location;

        /// <summary>
        /// Get the assembly folder of the current invoking assembly from the stack trace
        /// </summary>
        public static string CallingAssemblyFolder => Path.GetDirectoryName(CallingAssemblyFullPath);

        /// <summary>
        /// Get the assembly name with no extention of the current invoking assembly from the stack trace
        /// </summary>
        public static string CallingAssemblyNameWithNoExtension => Path.GetFileNameWithoutExtension(CallingAssemblyFullPath);

        /// <summary>
        /// Determine if the current execution is in debug mode based on debugger attached property
        /// </summary>
        public static bool IsInDebugMode => System.Diagnostics.Debugger.IsAttached;
    }

}
