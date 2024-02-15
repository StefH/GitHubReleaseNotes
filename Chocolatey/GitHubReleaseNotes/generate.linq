<Query Kind="Statements" />

// LinqPad script to generate the `VERIFICATION.txt` file

// Get the folder where this LinqPad script is located (https://stackoverflow.com/questions/3802779/linqpad-script-directory)
string chocolateyFolder = Path.GetDirectoryName(Util.CurrentQueryPath);
string projectFolder = Path.Combine(chocolateyFolder, "../", "../");

var doc = new XmlDocument(); doc.Load(Path.Combine(chocolateyFolder, "GitHubReleaseNotes.nuspec"));
string version = doc["package"]["metadata"]["version"].FirstChild.Value;
string exe = Path.Combine(projectFolder, "src", "GitHubReleaseNotes", "bin", "release", "net8.0", "win-x64", "publish", "GitHubReleaseNotes.exe");

string CreateSHA256()
{
	var crypt = new System.Security.Cryptography.SHA256Managed();
	var hash = new System.Text.StringBuilder();
	byte[] crypto = crypt.ComputeHash(File.ReadAllBytes(exe));
	foreach (byte theByte in crypto)
	{
		hash.Append(theByte.ToString("x2"));
	}
	return hash.ToString().ToUpperInvariant();
}

var fi = new FileInfo(exe);
long length = fi.Length;
double lengthInMb = Math.Floor(100.0 * length / (1024 * 1024)) / 100;
string dateModified = fi.LastWriteTime.ToString("yyyy-MM-dd HH:mm");

string text =
$@"VERIFICATION

To verify this package, follow these steps:

1] Right click the GitHubReleaseNotes.exe in Windows file explorer and go to the ""Details"" tab and check the following properties:
   - File description   GitHubReleaseNotes
   - Type               Application
   - File version       {version}
   - Product name       GitHubReleaseNotes
   - Product version    {version}
   - Copyright          Stef Heyenrath
   - Size               {lengthInMb} MB
   - Date modified      {dateModified}
   - Language           Language Neutral
   - Original filename  GitHubReleaseNotes.exe

2] Verify the SHA256 from the GitHubReleaseNotes.exe file (7zip can be used for this)
   - SHA256             {CreateSHA256()}

3] For the changes in this release, see ReleaseNotes.md

Note that this application is build as a selfcontained .NET 8 executable.";

File.WriteAllText(Path.Combine(chocolateyFolder, "tools", "VERIFICATION.txt"), text, Encoding.UTF8);

$"'VERIFICATION.txt' is generated for {version}".Dump();