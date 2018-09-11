using System;
using System.Net.Http;
using Microsoft.Owin.Testing;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Ez.Tests
{
    public class WebApiTestBaseClass
    {
        private static readonly Destructor Finalise = new Destructor();
        public static string PXD_NETWORK_WARNING = "App Config has InPXDNetwork=false or is missing 'InPXDNetwork'. Set this to true to run this test (and make sure you are on the pioneer network to get to Unifier)";
        protected static TestServer TestServer;
        protected static HttpClient HttpClient;
        protected static IDisposable _app;
        //thanks! http://stackoverflow.com/questions/4364665/static-destructor
        static WebApiTestBaseClass()
        {
            TestServer = TestServer.Create<Ez.Web.Owin.Startup>();
            HttpClient = new HttpClient(TestServer.Handler);
            HttpClient.Timeout = new TimeSpan(0, 60, 00);
        }

        public string ResolvePathVars(string PathToResolve)
        {
            string assemblyBasePath = Path.GetDirectoryName(System.IO.Path.GetDirectoryName(
 System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)).Replace("file:\\", "");
            assemblyBasePath += (assemblyBasePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" : Path.DirectorySeparatorChar.ToString());
            return PathToResolve.Replace("{SOLUTION_PATH}", Path.GetFullPath(assemblyBasePath + @"\..\..\..") + Path.DirectorySeparatorChar.ToString()).Replace("{ASSEMBLY_PATH}", assemblyBasePath);
        }

        public static string Serialize<T>(T obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream ms = new MemoryStream();
            serializer.WriteObject(ms, obj);
            string retVal = Encoding.UTF8.GetString(ms.ToArray());
            return retVal;
        }

        public static T Deserialize<T>(string json)
        {
            T obj = Activator.CreateInstance<T>();
            MemoryStream ms = new MemoryStream(Encoding.Unicode.GetBytes(json));
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            obj = (T)serializer.ReadObject(ms);
            ms.Close();
            return obj;
        }
        private sealed class Destructor
        {
            ~Destructor()
            {
                //HttpClient.Dispose();
                TestServer.Dispose();
            }
        }
    }
}