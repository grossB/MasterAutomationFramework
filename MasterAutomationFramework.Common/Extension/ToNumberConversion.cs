using Newtonsoft.Json;
using NunitTest.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterAutomationFramework.Common.Extension
{
    public static  class ToNumberConversion
    {
        /// <summary>
        /// Converts the string to integer. Returns 0 if the conversion fails and does not throw any exception
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static int ToInteger(this string text)
        {
            try
            {
                int.TryParse(text, out int result);
                return result;
            }
            catch (Exception)
            {
                //Log the exception here
                return 0;
            }
        }

        /// <summary>
        /// Converts the string to integer. Returns 0 if the conversion fails and does not throw any exception
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static long ToLong(this string text)
        {
            try
            {
                long.TryParse(text, out long result);
                return result;
            }
            catch (Exception)
            {
                //Log the exception here
                return 0;
            }
        }

        /// <summary>
        /// Converts the string to decimal. Returns 0 if the conversion fails and does not throw any exception
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this string text)
        {
            try
            {
                decimal.TryParse(text, out decimal result);
                return result;
            }
            catch (Exception)
            {
                //Log the exception here
                return 0;
            }
        }

        /// <summary>
        /// Returns string to a bool value. Conditions for true values are 'yes', 'true', 'enable', 'enabled'
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool ToBool(this string text)
        {
            if (text.IsEmpty())
                return false;

            return text.EqualsIgnoreCase("yes") ||
                   text.EqualsIgnoreCase("true") ||
                   text.EqualsIgnoreCase("Enable") ||
                   text.EqualsIgnoreCase("Enabled") ||
                   text.Equals("1");
        }

        /// <summary>
        /// Converts the string to double. Returns 0 if the conversion fails and does not throw any exception
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this string value)
        {
            try
            {
                double.TryParse(value, out double result);
                return result;
            }
            catch (Exception)
            {
                //Log the exception here
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string value)
        {
            try
            {
                if (value.IsEmpty())
                    return DateTime.MinValue;

                DateTime.TryParse(value, out DateTime result);
                return result;
            }
            catch (Exception)
            {
                //Log the exception here
                return DateTime.MinValue;
            }

        }

        /// <summary>
        /// Returns a dynamic object in the JSON format
        /// </summary>
        /// <param name="value">The value which is to be converted to JSON</param>
        /// <param name="exposeError">True if you want to throw the conversion error</param>
        /// <returns></returns>
        public static dynamic ToJson(this string value, bool exposeError = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<dynamic>(value);
            }
            catch (Exception)
            {
                if (exposeError)
                {
                    throw;
                }
                else
                {
                    return null;
                }
            }
        }

        public static T ToJson<T>(this string value, bool exposeError = false)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception)
            {
                if (exposeError)
                {
                    throw;
                }
                else
                {
                    return default(T);
                }
            }
        }
    }
}
