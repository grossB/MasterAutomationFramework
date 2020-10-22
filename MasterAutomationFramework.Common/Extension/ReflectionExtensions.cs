using System;
using System.Reflection;

namespace MasterAutomationFramework.Common.Extension
{
    public static class ReflectionExtensions
    {
        /// <summary>
        /// Determines whether the specified member has the specified attribute
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute to search for</typeparam>
        /// <param name="member">The member to inspect</param>
        /// <returns><b>true</b> if the member has the specified attribute, otherwise <b>false</b></returns>
        public static bool HasAttribute<TAttribute>(this MemberInfo member)
            where TAttribute : Attribute
        {
            return member.GetCustomAttribute<TAttribute>(false) != null;
        }
    }
}
