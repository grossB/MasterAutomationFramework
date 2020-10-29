namespace MasterAutomationFramework.Common.ConfigLoader
{
    /// <summary>
    /// Simple config for example
    /// </summary>
    public class SimpleConfig
    {
        public string StringValue { get; set; }

        public int IntValue { get; set; }

        /// <summary>
        /// For setup default value we simply use property default value initializer
        /// </summary>
        public int IntValueWithDefault { get; set; } = 5;
    }
}
