# HowTo

## Update release
- Update release in `Directory.Build.props`
- Update release in `GitHubReleaseNotes.nuspec`
- Update release in this file

## Build
Build in release mode

## Generate VERIFICATION
Use LinqPad to generate VERIFICATION.txt

## Generate the release-notes

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
choco push githubreleasenotes.1.0.5.3.nupkg --source https://push.chocolatey.org/
```