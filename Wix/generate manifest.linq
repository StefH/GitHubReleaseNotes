<Query Kind="Statements" />

// LinqPad script to generate the `manifest.yaml` file

// Get the folder where this LinqPad script is located (https://stackoverflow.com/questions/3802779/linqpad-script-directory)
string folder = Path.GetDirectoryName(Util.CurrentQueryPath);
string chocolateyFolder = Path.Combine(folder, "../", "Chocolatey", "GithubReleaseNotes");

var doc = new XmlDocument(); doc.Load(Path.Combine(chocolateyFolder, "GitHubReleaseNotes.nuspec"));
string version = doc["package"]["metadata"]["version"].FirstChild.Value;
string msi = Path.Combine(folder, "Setup", "bin", "release", "Setup.msi");

string CreateSHA256()
{
	var crypt = new System.Security.Cryptography.SHA256Managed();
	var hash = new System.Text.StringBuilder();
	byte[] crypto = crypt.ComputeHash(File.ReadAllBytes(msi));
	foreach (byte theByte in crypto)
	{
		hash.Append(theByte.ToString("x2"));
	}
	return hash.ToString().ToUpperInvariant();
}

string text =
$@"PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
PackageName: GitHubReleaseNotes
Moniker: GitHubReleaseNotes
Publisher: Stef Heyenrath
License: MIT
LicenseUrl: https://github.com/StefH/GitHubReleaseNotes/blob/master/LICENSE
PackageUrl: https://github.com/StefH/GitHubReleaseNotes
ShortDescription: Generate Release Notes in MarkDown format from a GitHub project.
Tags:
- github
- markdown
- releasenotes
- changelog
- tags
- generate
- md
InstallerType: msi
Installers:
- Architecture: x64
  InstallerUrl: https://github.com/StefH/GitHubReleaseNotes/releases/download/{version}/Setup.msi
  InstallerSha256: {CreateSHA256()}
PackageLocale: en-US
ManifestType: singleton
ManifestVersion: 1.0.0
";

File.WriteAllText(Path.Combine(folder, $"{version}.yaml"), text, Encoding.UTF8);

$"Manifest yaml is generated for {version}".Dump();