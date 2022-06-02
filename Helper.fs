namespace SyncFiles

open System.Threading.Tasks
open CliWrap

module Helper =
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
