# HowTo

## Pack

Run as **Administrator**:
``` cmd
cd .\Chocolatey\GitHubReleaseNotes
choco pack
```

## Push

First set the api-key:
``` cmd
choco apikey --key {KEY} --source https://push.chocolatey.org/
```

Then push:
``` cmd
choco push githubreleasenotes.1.0.5.0.nupkg --source https://push.chocolatey.org/
```