using System;
using Ez.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace UnitTests
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void ConfigRenderTests()
        {
            var Config = new Configuration();
            var DefaultSettings = new EnvironmentSettings();
            DefaultSettings.Environment = "Default";
            DefaultSettings.Settings.Add("Key1", "Value1");
            DefaultSettings.Settings.Add("Key2", 2);
            DefaultSettings.Settings.Add("Key3", 3.1);
            DefaultSettings.Settings.Add("Key4", false);
            DefaultSettings.Settings.Add("Key5", DateTime.Now);
            Config.DefaultSettings = DefaultSettings;
            var DevSettings = new EnvironmentSettings();
            DevSettings.Environment = "Local";
            DevSettings.Host.Add("NSWIN10VM");
            DevSettings.Host.Add("*localhost:*");
            DevSettings.Settings.Add("Key1", "LOCALValue1");
            DevSettings.Settings.Add("Key2", 102);
            Config.Environments.Add(DevSettings.Environment, DevSettings);
            var ProdSettings = new EnvironmentSettings();
            ProdSettings.Environment = "Prod";
            ProdSettings.Host.Add("Ezprod.com");
            ProdSettings.Settings.Add("Key1", "PRODValue1");
            ProdSettings.Settings.Add("Key2", 902);
            ProdSettings.Settings.Add("Key5", DateTime.Now.AddDays(10));
            Config.Environments.Add(ProdSettings.Environment, ProdSettings);
            var json =  JsonConvert.SerializeObject(
                Config
                , Newtonsoft.Json.Formatting.Indented);
            var newConfig = JsonConvert.DeserializeObject<Configuration>(json);

            Assert.IsTrue(Config["Key1"].Equals("Value1"), "Key should have equaled Val1");
            Config.Url = "http://localhost:50000";
            Assert.IsTrue(Config["Key1"].Equals("LOCALValue1"), "Key should have equaled LOCALValue1");
            Config.HostName = "SIM-SVR03";
            Assert.IsTrue(Config["Key1"].Equals("PRODValue1"), "Key should have equaled PRODValue1");
            Config.HostName = "NSWIN10VM";
            Assert.IsTrue(Config["Key1"].Equals("LOCALValue1"), "Key should have equaled LOCALValue1");


        }
    }
}
