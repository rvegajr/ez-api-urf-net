using Ez.Common.Extentions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Ez.Common
{
    public class HostList : List<string>
    {
    }
    public class EnvironmentSettings
    {
        public string Environment { get; set; }
        public HostList Host { get; set; } = new HostList();
        public Dictionary<string, object> Settings = new Dictionary<string, object>();
    }
    public static class ConfigurationExtentions
    {
        public static string ToJson(this Configuration This)
        {
            return JsonConvert.SerializeObject(This, Newtonsoft.Json.Formatting.Indented);
        }
        public static string ToFile(this Configuration This)
        {
            return JsonConvert.SerializeObject(This, Newtonsoft.Json.Formatting.Indented);
        }

    }
    public class Configuration
    {
        public object this[string settingName]
        {
            get
            {
                return GetValue(settingName);
            }
            set
            {
                SetValue(settingName, value);
            }
        }
        public EnvironmentSettings DefaultSettings { get; set; } = new EnvironmentSettings();
        public Dictionary<string, EnvironmentSettings> Environments { get; set; } = new Dictionary<string, EnvironmentSettings>();
        public static Configuration FromJson(string json)
        {
            return JsonConvert.DeserializeObject<Configuration>(json);
        }
        public static Configuration FromFile(string fileName)
        {
            return FromJson(File.ReadAllText(fileName));
        }


        public bool Exists(string SettingName, out string val)
        {
            var obj = GetValue(SettingName);
            val = "";
            if (obj != null) val = obj.ToString();
            return (obj != null);
        }

        public bool Exists(string SettingName)
        {
            return (GetValue(SettingName) != null);
        }

        public string Environment
        {
            get {
                try
                {
                    string retValue = "Default";
                    foreach (var envKey in Environments)
                    {
                        var env = envKey.Value;
                        foreach (var HostToCheck in env.Host)
                        {
                            if ((Url.ToLower().isWildCardMatch(HostToCheck.ToLower())) || (HostName.ToLower().isWildCardMatch(HostToCheck.ToLower())))
                            {
                                retValue = envKey.Key;
                                break;
                            }
                        }
                    }
                    return retValue;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public object GetValue(string SettingName)
        {
            try
            {
                object retValue = null;
                if (this.DefaultSettings.Settings.ContainsKey(SettingName))
                    retValue = this.DefaultSettings.Settings[SettingName];
                foreach(var env in Environments.Values)
                {
                    foreach(var HostToCheck in env.Host)
                    {
                        if ((Url.ToLower().isWildCardMatch(HostToCheck.ToLower())) || (HostName.ToLower().isWildCardMatch(HostToCheck.ToLower())))
                        {
                            if (env.Settings.ContainsKey(SettingName))
                                retValue = env.Settings[SettingName];
                            break;
                        }
                    }
                }
                return retValue;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void SetValue(string SettingName, object Val)
        {
            try
            {
                var isSet = false;
                foreach (var env in Environments.Values)
                {
                    foreach (var HostToCheck in env.Host)
                    {
                        if ((Url.ToLower().isWildCardMatch(HostToCheck.ToLower())) || (HostName.ToLower().isWildCardMatch(HostToCheck.ToLower())))
                        {
                            if (env.Settings.ContainsKey(SettingName))
                                env.Settings[SettingName] = Val;
                            isSet = true;
                            break;
                        }
                    }
                }
                if ((!isSet) && (this.DefaultSettings.Settings.ContainsKey(SettingName)))
                {
                    this.DefaultSettings.Settings[SettingName] = Val;
                }
            }
            catch (Exception)
            {
            }
        }

        private string url = "";
        public string Url
        {
            get
            {
                if (!string.IsNullOrEmpty(url))
                {
                    return url;
                }
                else
                {
                    var httpCtx = HttpContext.Current;
                    if ((httpCtx != null)
                        && (httpCtx.Handler != null)
                        && (httpCtx.Request != null)
                        && (httpCtx.Request.Url != null))
                    {
                        return httpCtx.Request.Url.DnsSafeHost.ToLower();
                    }
                    return "";
                }
            }
            set
            {
                url = value;
            }
        }
        private string hostName = "";
        public string HostName
        {
            get
            {
                if (!string.IsNullOrEmpty(hostName)) return hostName;
                return System.Net.Dns.GetHostName().ToLower();
            }
            set
            {
                hostName = value;
            }
        }
    }

    public enum TraceType
    {
        NoTracing,
        TraceWriter,
        DelegationHandler,
        NoSwaggerDelegationHandler,
    }
    public class AppSettings
    {
        private Configuration cfg = new Configuration();
        /// <summary></summary>
        public bool VerboseMessages { get => cfg["VerboseMessages"].AsBoolean(); set=> cfg["VerboseMessages"] = value; }
        /// <summary></summary>
        public string ConnectionString { get => cfg["ConnectionString"].ToString(); set => cfg["ConnectionString"] = value; }
        /// <summary></summary>
        public string Version { get => cfg["Version"].ToString(); set => cfg["Version"] = value; }
        /// <summary></summary>
        public TraceType TraceType { get => (TraceType)cfg["TraceType"].AsInt(0); set => cfg["TraceType"] = value; }
        /// <summary></summary>
        private static AppSettings instance;

        private AppSettings()
        {
        }

        private AppSettings(string configurationFileName)
        {
            cfg = Configuration.FromFile(configurationFileName);
        }

        public static AppSettings LoadFrom(string configurationFileName)
        {
            var instance = new AppSettings(configurationFileName);
            return instance;
        }
        private static string ConfigFileName {get; set;}
        public static AppSettings Instance
        {
            get
            {
                if (instance == null)
                {
                    ConfigFileName = "{ASSEMBLY_PATH}appsettings.json".ResolvePathVars();
                    try
                    {
                        if (File.Exists(ConfigFileName))
                        {
                            instance = AppSettings.LoadFrom(ConfigFileName);
                        }
                        else
                        {
                            instance = new AppSettings();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(string.Format("Error while parsing {0}. {1}", ConfigFileName, ex.Message), ex);
                    }
                }
                return instance;
            }
        }

        public string Value(string SettingName)
        {

            string sValue = null;
            if (Exists(SettingName, out sValue))
            {
                return sValue;
            }
            else
            {
                return "";
            }
        }

        public string Environment
        {
            get
            {
                return cfg.Environment;
            }
        }

        public string HostName
        {
            get
            {
                return cfg.HostName;
            }
        }

        public bool Exists(string SettingName, out string value)
        {
            return cfg.Exists(SettingName, out value);
        }
    }
}
