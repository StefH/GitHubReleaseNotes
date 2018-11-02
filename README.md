# GitHubReleaseNotes
Generate Release Notes from GitHub.

## Information
[![Chocolatey downloads](https://img.shields.io/chocolatey/dt/githubreleasenotes.svg)]()
[![Chocolatey](https://img.shields.io/chocolatey/v/githubreleasenotes.svg)](https://chocolatey.org/packages/GitHubReleaseNotes)

## Install
```
choco install GitHubReleaseNotes
```
This will use [Chocolatey](http://chocolatey.org) to install **GitHubReleaseNotes** into your `%path%`, ready to be used for any project

## Usage
GitHubReleaseNotes can be run inside a git repository.
```
GitHubReleaseNotes.exe --path . --output ReleaseNotes.md --version 1.0.4.17 --language en --skip-empty-releases
```

Arguments:
- `--path`: The path from the git repository. If not supplied, the current folder is used.
- `--output`: The location from the generated Release Notes. If not supplied, the output is written to the console.
- `--version`: Define a custom version name for the latest release instead of the value "next".
- `--language`: Provide the language (two letter according to [ISO-639-1](https://en.wikipedia.org/wiki/ISO_639-1)) which is used to format the dates. If not provided, "en" is used. It's also possible to use a value like "system", which takes the current system ui language.
- `--skip-empty-releases`: Define this optional argument to skip writing releases which have no associated Issues or Pull Requests.

## Output
The generated Release Notes ([Markdown](https://en.wikipedia.org/wiki/Markdown) formatted) will look like:
```
# 1.0.4.16 (11 September 2018)

 - [#202](https://github.com/StefH/GitHubReleaseNotes/pull/202) - Update logic PR contributed by [StefH](https://github.com/StefH)
 - [#201](https://github.com/StefH/GitHubReleaseNotes/issues/201) - Fix issue abc
```

## Copyright

### Notes
This project is based on [GitTools/GitReleaseNotes](https://github.com/GitTools/GitReleaseNotes).

### Dependencies
-  [Oktokit](https://github.com/octokit/octokit.net)
-  [LibGit2Sharp](https://github.com/libgit2/libgit2sharp)
-  [Handlebars.Net](https://github.com/rexm/Handlebars.Net)
-  [Fody](https://github.com/Fody/Fody)
-  [Fody.Costura](https://github.com/Fody/Costura)

