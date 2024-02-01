<Query Kind="Statements" />

// LINQPad script to generate the `product guid`.

// Get the folder where this LinqPad script is located (https://stackoverflow.com/questions/3802779/linqpad-script-directory)
string wixFolder = Path.GetDirectoryName(Util.CurrentQueryPath);
string setupFolder = Path.Combine(wixFolder, "Setup");
string productWxsFile = Path.Combine(setupFolder, "Product.wxs");

string productCode = "32249977-C6C1-40ED-B6A8-7084E69A0E7A";
string upgradeCode = Guid.NewGuid().ToString().ToUpperInvariant();
// string productCode = $"{{{productGuid}}}";

// Get version
string chocolateyFolder = Path.Combine(wixFolder, "../", "Chocolatey", "GithubReleaseNotes");
var specDoc = new XmlDocument();
specDoc.Load(Path.Combine(chocolateyFolder, "GitHubReleaseNotes.nuspec"));
string version = specDoc["package"]["metadata"]["version"].FirstChild.Value;

// Define the Wix namespace
XNamespace wixNamespace = "http://schemas.microsoft.com/wix/2006/wi";

// Load the XML file
var doc = XDocument.Load(productWxsFile);

// Find the Product element
var productElement = doc.Descendants(wixNamespace + "Product").Single();
productElement.Attribute("Id").Value = productCode;
productElement.Attribute("Version").Value = version;
productElement.Attribute("UpgradeCode").Value = upgradeCode;

// Save the modified XML file
doc.Save(productWxsFile);

File.WriteAllText(Path.Combine(wixFolder, "productGuid.txt"), productCode);
File.WriteAllText(Path.Combine(wixFolder, "upgradeGuid.txt"), upgradeCode);

$"ProductGuid and UpgradeGuid generated and saved to folder".Dump();