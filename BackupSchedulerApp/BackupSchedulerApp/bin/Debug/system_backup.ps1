
param(
    [string]$BackupTarget
)

try {
    Write-Output "Starting system image backup to $BackupTarget"

    # Run wbadmin system backup (requires admin rights)
    wbadmin start backup -backupTarget:$BackupTarget -include:C: -allCritical -quiet -vssFull

    Write-Output "System backup completed."
}
catch {
    Write-Error $_.Exception.Message
    exit 1
}
