# HowTo - WinGet

## Update release
Do Chocolatey first

## Update version
Use LinqPad on `generate guid.linq` to generate the product guid and + update the upgrade guid.

## Generate yaml
Use LinqPad on `generate files.linq` to generate the 3 yaml files.

## Add yaml
Add the 3 new yaml files to the soluton in the "winget-pkgs" folder

## Verify : Validate
Do a `winget validate --manifest <path>`

## Upload
Create the new version (tag) and upload 'zip' to https://github.com/StefH/GitHubReleaseNotes/releases

## Verify Install + Uninstall
Do a `winget install --manifest <path>`

Do a `winget uninstall --manifest <path>`

## winget-pkgs
Create a PR with the new {version}.yaml in https://github.com/StefH/winget-pkgs

Use the "." key on GitHub StefH/winget-pkgs to start VS Code and create a folder + add 3 yamls.


:memo: `<path>` is the name of the directory containing the manifest you're submitting.

--------------------------------------------------------------------------------------------------------------------------------

# HowTo - WinGet

## Build setup
Build setup project

## Upload
Upload the wix setup to https://github.com/StefH/GitHubReleaseNotes/releases