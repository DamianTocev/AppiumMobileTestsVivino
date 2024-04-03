using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;
using System.Globalization;

namespace AppiumMobileTestsVivino
{
    public class VivinoTests
    {
        private const string UriString = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppLocation = @"D:\RABOTNA\CODING\Projects\QA_Automation\APK-Fiels\vivino_8.18.11-8181203.apk";
        private AndroidDriver<AndroidElement> driver;
        private AppiumOptions options;

        [SetUp]
        public void PrepearApp()
        {
            this.options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("app", VivinoAppLocation);
            options.AddAdditionalCapability("platformName", "Android");
            options.AddAdditionalCapability("appPackage", "vivino.web.app");
            options.AddAdditionalCapability("appActivity", "com.sphinx_solution.activities.SplashActivity");
            //options.AddAdditionalCapability("udid", "emulator-5554");
            this.driver = new AndroidDriver<AndroidElement>(new Uri(UriString), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(40);
        }

        [TearDown]
        public void CloseApp()
        {
            driver.Dispose();
        }

        [Test]
        public void Test_Run_Vivino_On_Emulator()
        {
            Assert.Pass();
        }

        [Test]
        public void Test_Search_Wine_Verify_Name_And_Rating()
        {
            var linkAcount = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linkAcount.Click();
            var inputEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            inputEmail.SendKeys("vivinotests@gmail.som");
            var inputPass = driver.FindElementById("vivino.web.app:id/edtPassword");
            inputPass.SendKeys("12345678");
            var linkLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            linkLogin.Click();
            var buttonSearch = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            buttonSearch.Click();
            var imputFindWinesBUTTON = driver.FindElementById("vivino.web.app:id/search_header_text");
            imputFindWinesBUTTON.Click();
            var imputFindWines = driver.FindElementById("vivino.web.app:id/editText_input");
            imputFindWines.SendKeys("Katarzyna Reserve Red 2006");
            var linkKatarzyna = driver.FindElementById("vivino.web.app:id/wineryname_textView");
            linkKatarzyna.Click();

            var lableWineName = driver.FindElementById("vivino.web.app:id/wine_name");
            var lableRatingText = driver.FindElementById("vivino.web.app:id/rating").Text;
            var rating = double.Parse(lableRatingText, CultureInfo.InvariantCulture);


            Assert.That(lableWineName.Text, Is.EqualTo("Reserve Red 2006"));
            Assert.That(rating >= 1.00 && rating <= 5.00);

            //if the element is visible on the screen
            //var lableGrapes = driver.FindElementById("vivino.web.app:id/grapes_text_textview");

            //if the element is not visible on the screen
            var lableGrapes = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/grapes_text_textview\"))"
                );
            Assert.That(lableGrapes.Text, Is.EqualTo("Grapes"));

            var lableGrapesFact1 = driver.FindElementById("vivino.web.app:id/grapName_text");
            Assert.That(lableGrapesFact1.Text, Is.EqualTo("Cabernet Sauvignon"));

            //when I don't have an "id" I use the closest one and then I use "xpath" from it
            //var lableGrapesFact2 = driver.FindElementById("vivino.web.app:id/grapName_text");
            var lableGrapesFact2 = driver.FindElementByXPath("//android.widget.FrameLayout[2]/android.widget.LinearLayout/android.widget.TextView");
            Assert.That(lableGrapesFact2.Text, Is.EqualTo("Merlot"));

        }
    }
}