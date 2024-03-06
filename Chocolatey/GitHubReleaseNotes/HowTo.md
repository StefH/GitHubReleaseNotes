# HowTo

## Update release
- Update Version in `Directory.Build.props`
- Update version in `GitHubReleaseNotes.nuspec`
- Update Version in this file

## Build
Build in release mode

## Generate VERIFICATION
Use LinqPad on `.\Chocolatey\generate.linq` to generate VERIFICATION.txt

## Generate the release-notes
- Update the version in 'GenerateReleaseNotes.cmd'
- Run GenerateReleaseNotes.cmd

## Pack
Run as **Administrator**:
``` cmd
cd .\Chocolatey\GitHubReleaseNotes
choco pack
```

## Push
Set the api-key (only done once)
``` cmd
choco apikey --key {KEY} --source https://push.chocolatey.org/
```

Then push:
``` cmd
choco push githubreleasenotes.1.0.11.nupkg --source https://push.chocolatey.org/
```