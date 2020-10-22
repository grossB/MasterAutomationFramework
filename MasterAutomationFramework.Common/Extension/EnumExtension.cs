using System;
using System.ComponentModel;
using System.Reflection;

namespace MasterAutomationFramework.Common.Extension
{
    public static class EnumExtension
    {
        /// <summary>
        /// Returns the description of an enum member, according to its DescriptionAttribute if present
        /// </summary>
        /// <param name="value">The value of the enum</param>
        /// <typeparam name="TEnum">The typeof the enum</typeparam>
        /// <returns>The description of the enum member according to the DescriptionAttribute if present, or the member's name</returns>
        /// <exception cref="InvalidOperationException">TEnum is not an enum type</exception>
        /// <example>
        /// enum Countries
        /// {
        ///     [Description("United States")]   
        ///     US,
        ///     [Description("United Kingdom")]
        ///     UK,
        ///     Israel
        /// }
        /// 
        /// Console.WriteLine(Countries.US.GetDescription()); // United States
        /// Console.WriteLine(Countries.UK.GetDescription()); // United Kingdom
        /// COnsole.WriteLine(Countries.Israel.GetDescription()); // Israel
        /// </example>
        public static string GetDescription<TEnum>(this TEnum value)
            where TEnum : struct
        {
            var type = typeof(TEnum);
            if (!type.IsEnum)
                throw new ArgumentException(string.Format("{0} is not an enum", type.Name));

            var field = type.GetField(value.ToString());
            var descriptionAttribute = field.GetCustomAttribute<DescriptionAttribute>();
            if (descriptionAttribute == null)
                return value.ToString();

            return descriptionAttribute.Description;
        }
    }
}
