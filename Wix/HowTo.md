# HowTo

## Update release
Do Chocolatey first

## Update version
Use LinqPad on `generate guid.linq` to generate + update the product guid and the upgrade guid.

## Build setup
Build setup project

## Generate yaml
Use LinqPad on `generate files.linq` to generate the 3 yaml files.

## Add yaml
Add the 3 new yaml files to the soluton in the "Wix and winget-pkgs" folder

## Verify : Validate
Do a `winget validate --manifest {version}.yaml` 

## Upload
Create the new version (tag) and upload setup to https://github.com/StefH/GitHubReleaseNotes/releases

### Verify Install + Uninstall
Do a `winget install --manifest {version}.yaml`
Do a `winget uninstall --manifest {version}.yaml`

## winget-pkgs
Create a PR with the new {version}.yaml in https://github.com/StefH/winget-pkgs

Use the "." key on GitHub StefH/winget-pkgs to start VS Code and create a folder + add yaml.