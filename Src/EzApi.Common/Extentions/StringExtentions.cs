using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Ez.Common.Extentions
{
    public static class StringExtentions
    {
        /// <summary>
        /// Will make sure the string passed ends with a proper system directory seperator only if it is an existing directory
        /// </summary>
        /// <param name="PathToMakeSureEndsWithSystemDirectorySeperator"></param>
        /// 
        /// <returns></returns>
        public static string PathEnds(this string PathToMakeSureEndsWithSystemDirectorySeperator)
        {
            return PathToMakeSureEndsWithSystemDirectorySeperator + ((!PathToMakeSureEndsWithSystemDirectorySeperator.EndsWith(Path.DirectorySeparatorChar.ToString())) ? Path.DirectorySeparatorChar.ToString() : "");
        }
        /// <summary>
        /// Resolve the path variables
        /// </summary>
        /// <param name="PathToResolve"></param>
        /// <returns></returns>
        public static string ResolvePathVars(this string PathToResolve)
        {
            return PathToResolve.ResolvePathVars("EzApi");
        }
        /// <summary>
        /// Resolve the pah vatraibles
        /// </summary>
        /// <param name="PathToResolve"></param>
        /// <returns></returns>
        public static string ResolvePathVars(this string PathToResolve, string rootFolderName)
        {
            if ((PathToResolve.Contains("{SOLUTION_PATH}")) || (PathToResolve.Contains("{ASSEMBLY_PATH}")))
            {
                var AssemblyPath = AppContext.BaseDirectory;
                AssemblyPath += (AssemblyPath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" : Path.DirectorySeparatorChar.ToString());
                var SolutionPath = AssemblyPath;

                //File.AppendAllText(@"C:\Temp\DEBUG.txt", string.Format("   System.Diagnostics.Debugger.IsAttached={0}" + "\n", System.Diagnostics.Debugger.IsAttached));
                DirectoryInfo di = new DirectoryInfo(SolutionPath);
                while (di != null)
                {
                    if (di.Name == rootFolderName)
                    {
                        SolutionPath = di.FullName + Path.DirectorySeparatorChar.ToString();
                        break;
                    }
                    di = di.Parent;
                }
                PathToResolve = PathToResolve.Replace("{SOLUTION_PATH}", SolutionPath).Replace("{ASSEMBLY_PATH}", AssemblyPath);
            }

            return PathToResolve;
        }


        /// <summary>
        /// Extract the base URL from a much larger without the scheme, so http//localhost:4200/api/config would return localhost:4200
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string BaseUrlNoScheme(this string url)
        {
            if (url == null) return null;
            System.Uri uri = new System.Uri(url);
            int port = uri.Port;
            string host = uri.Host;
            string protocol = uri.Scheme;
            if (port == 80)
            {
                return uri.GetComponents(UriComponents.Host, UriFormat.UriEscaped);
            }
            else
            {
                return uri.GetComponents(UriComponents.HostAndPort, UriFormat.UriEscaped);
            }
        }

        public static bool isWildCardMatch(this string value, string patternToMatch)
        {
            return Regex.IsMatch(value, ("^" + Regex.Escape(patternToMatch).Replace("\\*", ".*") + "$"));
        }
    }
}
