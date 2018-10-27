Quick start guide

Generate new package:
    choco new -h will get you started seeing options available to you.
    Once you figured out all of your options, you should move forward with generating your template.
Edit template using common sense
    cd package-name
    Edit the package-name.nuspec configuration file.
    Edit the ./tools/chocolateyInstall.ps1 install script.
        Make sure you figure out the installer's silent mode. Use Universal Silent Switch Finder, which is available as a Choco package: choco install ussf
    You must save your files with UTFñ8 character encoding without BOM. (Details)
Build the package
    Still in package directory
    choco pack
    * "Successfully created package-name.1.1.0.nupkg"ù
Test the package
    Testing should probably be done on a Virtual Machine
    In your package directory, use:
    * choco install package-name -s . (package-name is the id element in the nuspec)
Push the package to the Chocolatey community package repository:
    Get a Chocolatey account:
    * Register
    Copy the API key from your Chocolatey account.
    choco apikey -k [API_KEY_HERE] -source https://push.chocolatey.org/
    choco push package-name.1.1.0.nupkg -s https://push.chocolatey.org/ - nupkg file can be ommitted if it is the only one in the directory.
