# GitHubReleaseNotes
Generate Release Notes from GitHub using [Oktokit](https://github.com/octokit/octokit.net), [LibGit2Sharp](https://github.com/libgit2/libgit2sharp) and [Handlebars.Net](https://github.com/rexm/Handlebars.Net).

## Information
This project is based on [GitTools/GitReleaseNotes](https://github.com/GitTools/GitReleaseNotes).

## Usage
GitHubReleaseNotes can be run inside a git repository.
```
GitHubReleaseNotes.exe --path . --output ReleaseNotes.md
```

Arguments:
- `--path`: The path from the git repository. If not supplied, the current folder is used.
- `--output`: The path from the generated Release Notes. If not supplied, the output is written to the console.

## Output
The generated Release Notes ([Markdown](https://en.wikipedia.org/wiki/Markdown) formatted) will look like:
```
# 1.0.4.16 (11 September 2018)

 - [#202](https://github.com/StefH/GitHubReleaseNotes/pull/202) - Update logic PR contributed by [StefH](https://github.com/StefH)
 - [#201](https://github.com/StefH/GitHubReleaseNotes/issues/201) - Fix issue abc
```