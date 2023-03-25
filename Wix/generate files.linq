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

string installerText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.installer.1.4.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
MinimumOSVersion: 10.0.0.0
InstallerType: wix
Installers:
- Architecture: x64
  InstallerUrl: https://github.com/StefH/GitHubReleaseNotes/releases/download/{version}/Setup.msi
  InstallerSha256: {CreateSHA256()}
  ProductCode: '{{1D840FA8-29CC-47FB-B192-A932DE13480F}}'
ManifestType: installer
ManifestVersion: 1.4.0";

string defaultLocaleText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.defaultLocale.1.4.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
PackageLocale: en-US
Publisher: Stef Heyenrath
PublisherUrl: https://github.com/StefH/GitHubReleaseNotes
PublisherSupportUrl: https://github.com/StefH/GitHubReleaseNotes
# PrivacyUrl: 
Author: Stef Heyenrath
PackageName: GitHubReleaseNotes
PackageUrl: https://github.com/StefH/GitHubReleaseNotes
License: MIT
LicenseUrl: https://github.com/StefH/GitHubReleaseNotes/blob/master/LICENSE
# Copyright: 
# CopyrightUrl: 
ShortDescription: Generate Release Notes in MarkDown format from a GitHub project.
# Description: 
Moniker: githubreleasenotes
Tags:
- changelog
- generate
- github
- markdown
- md
- releasenotes
- tags
# Agreements: 
# ReleaseNotes: 
ReleaseNotesUrl: https://github.com/StefH/GitHubReleaseNotes/blob/master/ReleaseNotes.md
ManifestType: defaultLocale
ManifestVersion: 1.4.0";

string versionText =
$@"# yaml-language-server: $schema=https://aka.ms/winget-manifest.version.1.4.0.schema.json

PackageIdentifier: StefHeyenrath.GitHubReleaseNotes
PackageVersion: {version}
DefaultLocale: en-US
ManifestType: version
ManifestVersion: 1.4.0";

File.WriteAllText(Path.Combine(folder, @"StefHeyenrath.GitHubReleaseNotes.installer.yaml"), installerText, Encoding.UTF8);
File.WriteAllText(Path.Combine(folder, @"StefHeyenrath.GitHubReleaseNotes.locale.en-US.yaml"), defaultLocaleText, Encoding.UTF8);
File.WriteAllText(Path.Combine(folder, @"StefHeyenrath.GitHubReleaseNotes.yaml"), versionText, Encoding.UTF8);

$"Manifest yamls are generated for {version}".Dump();