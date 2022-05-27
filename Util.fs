namespace SyncFiles

open System.Threading.Tasks
open CliWrap
open System.IO

module Util =
    /// <summary>Runs choco export command in the given directory path.</summary>
    /// <param name="workDir">The directory path to invoke the choco export command in.</param>
    /// <returns>Task that results in export file path</returns>
    let generateChocolateyExport (workDir: string) : Task<string> =

        task {
            let! _ =
                Cli
                    .Wrap("choco")
                    .WithArguments("export")
                    .WithWorkingDirectory(workDir)
                    .WithValidation(CommandResultValidation.ZeroExitCode)
                    .ExecuteAsync()

            return $"{workDir}\\packages.config"
        }

    /// <summary>Copies a file to the destination directory, overwriting the
    /// existing file if exists.</summary>
    /// <param name="destDir">Target directory path</param>
    /// <param name="fpath">Source file path</param>
    /// <returns>Destination file path</param>
    let copyFileToDir (destDir: string) (fpath: string) : string =
        let destPath = Path.Join(destDir, Path.GetFileName(fpath))
        File.Copy(fpath, destPath, overwrite = true)
        destPath
