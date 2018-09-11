#tool nuget:?package=vswhere
#addin "Cake.WebDeploy"

/*
Example Usage
1. The following will generate a STAGE deployment (the default will be STAGE)
.\build.ps1 -ScriptArgs '-envversion="STAGE"'


2. The following will generate a LIVE deployment 
.\build.ps1 -ScriptArgs '-envversion="LIVE"'

3. the following will generate a LIVE deployment tagged to a version of 1.5.0.5
.\build.ps1 -ScriptArgs '-envversion="LIVE-1.5.0.5"'
*/

DirectoryPath vsLatest  = VSWhereLatest();
FilePath msBuildPathX64 = (vsLatest==null)
                            ? null
                            : vsLatest.CombineWithFilePath("./MSBuild/15.0/Bin/amd64/MSBuild.exe");
var target = Argument("target", "Finish");
var env = Argument("env", "STAGE");
var version = Argument("version", "1.0.1.0003");
//var version = Argument("version", "??");
var envVersion = Argument("envversion", "??");
//ScripArgs Does not support multiple arguments,  so you can pass env and version with a hyphen
if (envVersion!="??") {
	var envVersionParse = envVersion.Split('-');
	env = envVersionParse[0];
	if (envVersionParse.Length>1) version = envVersionParse[1];	
}
var configuration = Argument("configuration", "Debug");
var thisDir = System.IO.Path.GetFullPath(".") + System.IO.Path.DirectorySeparatorChar;
var binDir = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.Web/bin/"));
var webPackageDir = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.Web/obj/Debug/Package/MARSApi.Web.zip"));
var projectFile = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.Web/MARSApi.Web.csproj"));
var solutionFile = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.sln"));
var buildDir = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.Web/bin/") + Directory(configuration));
var publishDir = System.IO.Path.GetFullPath(Directory("./Src/MARSApi.Web/obj/Debug/Package/PackageTmp/")) + System.IO.Path.DirectorySeparatorChar;
var tempPath = System.IO.Path.GetTempPath();
var deployPath = (env=="STAGE" ? @"\\Sim-svr05\c$\inetpub\wwwroot\marsapi\" : @"\\Sim-svr07\c$\inetpub\wwwroot\marsapi\");
public int MAJOR = 0; public int MINOR = 1; public int REVISION = 2; public int BUILD = 3; //Version Segments

Information(string.Format("target={0}", target));
Information(string.Format("env={0}", env));
Information(string.Format("envVersion={0}", envVersion));
Information(string.Format("version={0}", version));
Information(string.Format("deployPath={0}", deployPath));
Information(string.Format("thisDir={0},  Exists?={1}", thisDir, System.IO.Directory.Exists(thisDir)));
Information(string.Format("binDir={0},  Exists?={1}", binDir, System.IO.Directory.Exists(binDir)));
Information(string.Format("buildDir={0},  Exists?={1}", buildDir, System.IO.Directory.Exists(buildDir)));
Information(string.Format("projectFile={0},  Exists?={1}", projectFile, System.IO.File.Exists(projectFile)));
Information(string.Format("solutionFile={0},  Exists?={1}", solutionFile, System.IO.File.Exists(solutionFile)));
Information(string.Format("tempPath={0},  tempPath?={1}", tempPath, System.IO.Directory.Exists(tempPath)));
if (!((env=="STAGE") || (env=="LIVE"))) 
	throw new Exception(String.Format("Ennvironment was incorrectly set,  it can only be STAGE or LIVE... it was {0}", env));   
//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() =>
{
    CleanDirectory(buildDir);
});

Task("RestoreNuGetPackages")
    .IsDependentOn("Clean")
    .Does(() =>
{

	var settings = new NuGetRestoreSettings()
	{
		// VSTS has old version of Nuget.exe and Automapper restore fails because of that
		ToolPath = "./nuget/nuget.exe",
		Verbosity = NuGetVerbosity.Detailed,
	};
	NuGetRestore(solutionFile, settings);
});

Task("UpdateVersionFiles")
  .IsDependentOn("RestoreNuGetPackages")
  .Does(() =>
{
	var WebApiAssemblyFileName = thisDir + @"Src\MARSApi.Web\Properties\AssemblyInfo.cs";
	var potentialVersion = GetVersionInAssembly("AssemblyVersion", WebApiAssemblyFileName);
	if (version!="??") {
		potentialVersion = version;
	}
	Information("{0}: Old Version={1}", WebApiAssemblyFileName, potentialVersion);
	potentialVersion = VersionStringIncrement(potentialVersion, REVISION);
	potentialVersion = VersionStringParts(potentialVersion, MAJOR, MINOR, REVISION) + ".0000";
	Information("{0}: New Potential Version={1}", WebApiAssemblyFileName, potentialVersion);

	Information("Incrementing Version Files");
	WriteVersionInAssembly("AssemblyVersion", WebApiAssemblyFileName, potentialVersion);
	WriteVersionInAssembly("AssemblyFileVersion", WebApiAssemblyFileName, potentialVersion);
		Information("thisDir={0}", thisDir);
	var lst = DirSearch(thisDir, "AssemblyInfo.cs");
	foreach (var file in lst)
	{
		Information("{0}: New Potential Version={1}", file, potentialVersion);
		WriteVersionInAssembly("AssemblyVersion", file, potentialVersion);
		WriteVersionInAssembly("AssemblyFileVersion", file, potentialVersion);
	}
});

Task("Build")
  .IsDependentOn("UpdateVersionFiles")
  .Does(() =>
{
	MSBuild(solutionFile, new MSBuildSettings { ToolPath = msBuildPathX64 }
	  .WithProperty("DeployOnBuild", "true")
	);
	
	MSBuild(projectFile, new MSBuildSettings { ToolPath = msBuildPathX64 }
        .UseToolVersion(MSBuildToolVersion.Default)
		.WithTarget("publish")
		.WithProperty("DeployOnBuild", "true")
		.WithProperty("PublishDirectory", deployPath)
	);	
});

Task("Finish")
  .IsDependentOn("Build")
  .Does(() =>
{
	EnsureDirectoryExists(deployPath);
	if (!DirectoryExists(deployPath))
		throw new Exception(String.Format("Deploy Path {0} does not exist :(", deployPath));   
	Information(string.Format("Clearing {0}", deployPath));
	//CleanDirectories(deployPath);
	System.IO.DirectoryInfo di = new DirectoryInfo(deployPath);
	foreach (System.IO.FileInfo file in di.GetFiles())
		file.Delete(); 
	foreach (System.IO.DirectoryInfo dir in di.GetDirectories())
		dir.Delete(true); 	
	if (!DirectoryExists(publishDir + "EzDbCodeGen")) System.IO.Directory.Delete(publishDir + "EzDbCodeGen");
	if (!DirectoryExists(publishDir))
		throw new Exception(String.Format("Publish Path {0} does not exist :(", publishDir));   
	Information(string.Format("Copying {0} to {1}", publishDir, deployPath ));
	CopyDirectory(publishDir, deployPath);
	Information("Build Script has completed... Version that was deployed was {0}", version);
});

RunTarget(target);

public bool IsValidVersionString(string versionString) {
	var v = versionString.Replace(".", "");
	int test;
    return int.TryParse(v, out test);
}

//segmentToIncrement can equal Major, Minor, Revision, Build in format Major.Minor.Revision.Build
public string VersionStringIncrement(string versionString, int segmentToIncrement) {
	var vArr = versionString.Split('.');
	var valAsStr = vArr[segmentToIncrement];
	int valAsInt = 0;
    int.TryParse(valAsStr, out valAsInt);	
	vArr[segmentToIncrement] = (valAsInt + 1).ToString();
	return String.Join(".", vArr);
}

//versionSegments can equal Major, Minor, Revision, Build in format Major.Minor.Revision.Build
public string VersionStringParts(string versionString, params int[] versionSegments) {
	var vArr = versionString.Split('.');
	string newVersion = "";
	foreach ( var versionSegment in versionSegments ) {
		newVersion += (newVersion.Length>0 ? "." : "") + vArr[versionSegment].ToString();
	}
	return newVersion;
}

public string Pluck(string str, string leftString, string rightString)
{
	try
	{
//Information(@"{0}: str={1}, leftString='{2}', rightString='{3}'", "Pluck", str, leftString, rightString);
		var lpos = str.IndexOf(leftString);
//Information(@"{0}: lpos={1}", "Pluck", lpos);
		if (lpos > 0)
		{
			var rpos = str.IndexOf(rightString, lpos);
//Information(@"{0}: rpos={1}", "Pluck", rpos);
			if ((rpos > 0) && (rpos > lpos))
			{
				return str.Substring(lpos + leftString.Length, (rpos - lpos) - leftString.Length);
			}
		}
	}
	catch (Exception)
	{
		return "";
	}
	return "";
}

public string GetVersionInAssembly(string VarName, string AssemblyFileName) {
	var version = "";
	try
	{
		var manifestText = System.IO.File.ReadAllText(AssemblyFileName);
	//Information("{0}-{1}: Text={2}", AssemblyFileName, VarName, manifestText);

		var potentialVersion = Pluck(manifestText, VarName + "(\"", "\")]");
	//Information("{0}-{1}: potentialVersion={2}", AssemblyFileName, VarName, potentialVersion);
		if (IsValidVersionString(potentialVersion)) {
			version = potentialVersion;
		} else {
			throw new Exception(String.Format("{0}-{1}: Version {2} format is invalid.", AssemblyFileName, VarName, potentialVersion));   
		}
	}
	catch (Exception ex)
	{
		throw ex;
	}
	return version;
}


public bool WriteVersionInAssembly(string VarName, string AssemblyFileName, string VersionSet) {
//Information("WriteVersionInAssembly->{0}-{1}: \n\nVersionSet={2}\n\n", AssemblyFileName, VarName, VersionSet);
	try
	{
		var manifestText = System.IO.File.ReadAllText(AssemblyFileName);
		var potentialVersion = Pluck(manifestText, VarName + "(\"", "\")]");
		if (IsValidVersionString(VersionSet)) {

			var fromVersionString = VarName + "(\"" + potentialVersion + "\")]";
			var toVersionString = VarName + "(\"" + VersionSet + "\")]";
			//Information("WriteVersionInAssembly->{0}-{1}: \n\nfromVersionString={2}, toVersionString={3}\n\n", AssemblyFileName, VarName, fromVersionString, toVersionString);
			
			manifestText = manifestText.Replace(fromVersionString, toVersionString);

			//Information("WriteVersionInAssembly->{0}-{1}: Version={2}, Next Version={3}", AssemblyFileName, VarName, potentialVersion, VersionSet);

			potentialVersion = VersionStringIncrement(potentialVersion, BUILD);
			potentialVersion = VersionStringParts(potentialVersion, MAJOR, MINOR, BUILD) + ".0000";
			System.IO.File.WriteAllText(AssemblyFileName, manifestText);
			//Information("{0}: Version={1}", AssemblyFileName, manifestText);
		} else {
			throw new Exception(String.Format("{0}: Version {1} format is invalid.", AssemblyFileName, potentialVersion));   
		}
		return true;
	}
	catch (Exception ex)
	{
		throw ex;
	}
}

public List<string> DirSearch(string sDir, string FileToSearchFor) 
{
	List<string> lstFilesFound = new List<string>();
	try
	{
		foreach (string d in System.IO.Directory.GetDirectories(sDir)) 
		{
			foreach (string f in System.IO.Directory.GetFiles(d, FileToSearchFor)) 
			{
				lstFilesFound.Add(f);
			}
			lstFilesFound.AddRange(DirSearch(d, FileToSearchFor));
		}
		return lstFilesFound;
	}
	catch (System.Exception) 
	{
		throw;
	}
}

public IEnumerable<FileInfo> TraverseDirectory(string rootPath, Func<FileInfo, bool> Pattern)
{
	var directoryStack = new Stack<DirectoryInfo>();
	directoryStack.Push(new DirectoryInfo(rootPath));
	while (directoryStack.Count > 0)
	{
		var dir = directoryStack.Pop();
		try
		{
			foreach (var i in dir.GetDirectories())
				directoryStack.Push(i);
		}
		catch (UnauthorizedAccessException) {
			continue; // We don't have access to this directory, so skip it
		}
		foreach (var f in dir.GetFiles().Where(Pattern)) // "Pattern" is a function
			yield return f;
	}
}