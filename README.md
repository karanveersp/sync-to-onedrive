# SyncToOneDrive

This cli app simply copies the given file paths to the following directory (created if not found) - `C:\users\<uname>\OneDrive\Synced`.

In addition, it assumes `Chocolatey` is installed on the host, and runs `choco export` in the user's home folder, copying the `packages.config` file as well.
This file is copied so that I always maintain a list of all apps installed via chocolatey in OneDrive. Whenever setting myself up on a new Windows installation, I can just invoke `choco install packages.config -y` to install the latest versions of all packages!

The purpose of this application is to deposit files to a certain folder on OneDrive.

For example, a powershell profile path can be passed as an argument to the program to have it be copied to the synced folder.