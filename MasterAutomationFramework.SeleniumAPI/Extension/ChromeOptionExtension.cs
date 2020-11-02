namespace MasterAutomationFramework.SeleniumAPI.Extension
{
    using MasterAutomationFramework.SeleniumAPI.Enums;
    using OpenQA.Selenium.Chrome;

    public static class ChromeOptionExtension
    {
        // -------------------------------------------------------------------------------------
        // For more options goto => https://peter.sh/experiments/chromium-command-line-switches/
        // -------------------------------------------------------------------------------------
        public static ChromeOptions SetOptions(this ChromeOptions options, ChromeDriverOptions option, string additionalAttribute = null)
        {
            // not sure if actual
            // --ignore-certificate-errors.
            switch (option)
            {
                case ChromeDriverOptions.disablesHarmFileWhenDownloading:
                    options.AddUserProfilePreference("safebrowsing", true);
                    break;
                case ChromeDriverOptions.downloadDirectory:
                    options.AddUserProfilePreference("download.default_directory", additionalAttribute);
                    break;
                case ChromeDriverOptions.newWindow:
                    //Launches URL in new browser window.
                    options.AddArguments($"--new-window");
                    break;
                case ChromeDriverOptions.maximized:
                    // Opens Chrome in maximize mode
                    options.AddArguments("--start-maximized");
                    break;
                case ChromeDriverOptions.fullscreen:
                    // Run chrome in fullscreen mode
                    options.AddArguments("--start-fullscreen");
                    break;
                case ChromeDriverOptions.incognito:
                    // starts in incognito mode
                    options.AddArguments("--incognito");
                    break;
                case ChromeDriverOptions.disableInforbars:
                    // Disable info-bar
                    options.AddExcludedArgument("enable-automation");
                    options.AddAdditionalCapability("useAutomationExtension", false);
                    break;
                case ChromeDriverOptions.disableExtensions:
                    // disable extension
                    options.AddArguments("--disable-extensions");
                    break;
                case ChromeDriverOptions.headless:
                    // headless execution
                    options.AddArguments("--headless");
                    options.AddArguments("--disable-gpu");
                    break;
                case ChromeDriverOptions.addExtension:
                    // https://crxextractor.com/  => get crx extension
                    options.AddExtension(additionalAttribute);
                    break;
                default:
                    break;
            }

            // Safe browsing disables "harm file" warning when downloading file.
            // options.AddUserProfilePreference("safebrowsing", true);

            // Other 
            // options.AddArguments("--disable-notifications");
            // --enable - logging--dump - dom https://www.google.pl/  
            // --headless--print - to - pdf https://www.google.pl/  
            // --headless--screenshot https://www.google.pl/ 
            return options;
        }
    }
}
