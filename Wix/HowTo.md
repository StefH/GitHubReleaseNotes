# HowTo

## Update release
Do Chocolatey first

## Build tool
Build in release mode

## Update version
Update the version in `Product.wxs`

## Build setup
Build setup project

## Generate yaml
Use LinqPad on `generate manifest.linq` to generate the yaml file

## Add yaml
Add the new {version}.yaml to the soluton in the "Wix and winget-pkgs" folder

## Verify : Validate
Do a `winget validate --manifest {version}.yaml` 

## Upload
Create the new version and upload setup to https://github.com/StefH/GitHubReleaseNotes/releases

### Verify Install + Uninstall
Do a `winget install --manifest {version}.yaml`
Do a `winget uninstall --manifest {version}.yaml`

## winget-pkgs
Create a PR with the new {version}.yaml in https://github.com/StefH/winget-pkgs