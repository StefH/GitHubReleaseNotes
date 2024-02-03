<Query Kind="Statements" />

// LINQPad script to generate the `manifest.yaml` files.

// Get the folder where this LinqPad script is located (https://stackoverflow.com/questions/3802779/linqpad-script-directory)
string folder = Path.GetDirectoryName(Util.CurrentQueryPath);
string chocolateyFolder = Path.Combine(folder, "../", "Chocolatey", "GithubReleaseNotes");
string projectFolder = Path.Combine(chocolateyFolder, "../", "../");

string outputFolder = Path.Combine(projectFolder, "src", "GitHubReleaseNotes", "bin", "release", "net8.0", "win-x64", "publish");
string exe = Path.Combine(outputFolder, "GitHubReleaseNotes.exe");
string zip = Path.Combine(outputFolder, "GitHubReleaseNotes.zip");

var doc = new XmlDocument(); 
doc.Load(Path.Combine(chocolateyFolder, "GitHubReleaseNotes.nuspec"));

string version = doc["package"]["metadata"]["version"].FirstChild.Value;
// string msi = Path.Combine(folder, "Setup", "bin", "release", "Setup.msi");

var fi = new FileInfo(exe);
string dateModified = fi.LastWriteTime.ToString("yyyy-MM-dd");

string CreateSHA256()
{
	var crypt = new System.Security.Cryptography.SHA256Managed();
	var hash = new System.Text.StringBuilder();
	byte[] crypto = crypt.ComputeHash(File.ReadAllBytes(zip));
	foreach (byte theByte in crypto)
	{
		hash.Append(theByte.ToString("x2"));
	}
	return hash.ToString().ToUpperInvariant();
}

string productCode = $"{{{File.ReadAllText(Path.Combine(folder, "productGuid.txt"))}}}";
// string upgradeCode = $"{{{File.ReadAllText(Path.Combine(folder, "upgradeGuid.txt"))}}}";

// StefHeyenrath.GitHubReleaseNotes.installer.yaml
string installerText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.installer.1.5.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
InstallerType: zip
NestedInstallerType: portable
NestedInstallerFiles:
- RelativeFilePath: GitHubReleaseNotes.exe
ProductCode: '{productCode}'
ReleaseDate: {dateModified}
Installers:
- Architecture: x64
  InstallerUrl: https://github.com/StefH/GitHubReleaseNotes/releases/download/{version}/GitHubReleaseNotes.zip
  InstallerSha256: {CreateSHA256()}
ManifestType: installer
ManifestVersion: 1.5.0";


// StefHeyenrath.GitHubReleaseNotes.locale.en-US.yaml
string defaultLocaleText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.defaultLocale.1.5.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
PackageLocale: en-US
Publisher: Stef Heyenrath
PublisherUrl: https://github.com/StefH
PublisherSupportUrl: https://github.com/StefH/GitHubReleaseNotes
PackageName: GitHubReleaseNotes
PackageUrl: https://github.com/StefH/GitHubReleaseNotes
License: MIT
LicenseUrl: https://github.com/StefH/GitHubReleaseNotes/blob/master/LICENSE
ShortDescription: Generate Release Notes in MarkDown format from a GitHub project.
Moniker: githubreleasenotes
Tags:
- changelog
- generate
- github
- markdown
- md
- releasenotes
- tags
ReleaseNotesUrl: https://github.com/StefH/GitHubReleaseNotes/blob/master/ReleaseNotes.md
ManifestType: defaultLocale
ManifestVersion: 1.5.0";


// StefHeyenrath.GitHubReleaseNotes
string versionText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.version.1.5.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
DefaultLocale: en-US
ManifestType: version
ManifestVersion: 1.5.0";

var versionFolder = Path.Combine(folder, version);
Directory.CreateDirectory(versionFolder);

File.WriteAllText(Path.Combine(versionFolder, @"StefHeyenrath.GitHubReleaseNotes.installer.yaml"), installerText, Encoding.UTF8);
File.WriteAllText(Path.Combine(versionFolder, @"StefHeyenrath.GitHubReleaseNotes.locale.en-US.yaml"), defaultLocaleText, Encoding.UTF8);
File.WriteAllText(Path.Combine(versionFolder, @"StefHeyenrath.GitHubReleaseNotes.yaml"), versionText, Encoding.UTF8);

$"Manifest yamls are generated for {version}".Dump();