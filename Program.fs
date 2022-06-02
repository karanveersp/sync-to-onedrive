open System
open System.IO
open SyncFiles.Helper
open Utils.Fs.File

[<EntryPoint>]
let main args =
    let user = Environment.UserName

    let homeDir = $"C:\\Users\\{user}"
    let oneDriveSyncFolder = $"{homeDir}\\OneDrive\\Synced"

    if not (Directory.Exists(oneDriveSyncFolder)) then
        Directory.CreateDirectory(oneDriveSyncFolder)
        |> ignore

    let chocoExportFile = generateChocolateyExport (homeDir)
    chocoExportFile.Wait()

    let paths =
        [ for p in args do
              yield p ]

    let allPaths = chocoExportFile.Result :: paths

    for path in allPaths do
        let destPath = copyFileToDir oneDriveSyncFolder path
        printfn $"Copied {path} to {destPath}"

    0
