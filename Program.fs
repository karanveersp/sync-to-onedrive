open System
open CliWrap
open System.Threading.Tasks
open System.IO

[<EntryPoint>]
let main args =
    let user = Environment.UserName

    let homeDir = $"C:\\Users\\{user}"
    let oneDriveSyncFolder = $"{homeDir}\\OneDrive\\Synced"

    if (Directory.Exists(oneDriveSyncFolder) <> true) then
        do
            Directory.CreateDirectory(oneDriveSyncFolder)
            |> ignore

    let generateChocolateyExport () : Task<string option> =
        task {
            let! res =
                Cli
                    .Wrap("choco")
                    .WithArguments("export")
                    .WithWorkingDirectory(homeDir)
                    .ExecuteAsync()

            match res.ExitCode with
            | 0 -> return Some $"{homeDir}\\packages.config"
            | _ -> return None
        }

    let copyFileToSyncDir (fpath: string) =
        let destPath = Path.Join(oneDriveSyncFolder, Path.GetFileName(fpath))
        File.Copy(fpath, destPath, overwrite = true)

    let chocoExportFile = generateChocolateyExport ()
    chocoExportFile.Wait()

    let paths =
        [ for p in args do
              yield p ]

    match chocoExportFile.Result with
    | Some exportFile ->
        for path in exportFile :: paths do
            copyFileToSyncDir path
            printfn $"Copied {Path.GetFileName(path)} to {oneDriveSyncFolder}"

        0
    | None ->
        printfn "Error running choco export"
        1
