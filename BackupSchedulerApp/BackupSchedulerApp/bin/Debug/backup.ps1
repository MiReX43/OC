
param(
    [string]$SourceFolder,
    [string]$DestinationFolder,
    [switch]$CreateRestorePoint
)

function Create-RestorePoint {
    Write-Output 'Creating system restore point...'
    # Create restore point, requires admin rights!
    Checkpoint-Computer -Description 'Scheduled Backup Restore Point' -RestorePointType 'MODIFY_SETTINGS'
}

try {
    if ($CreateRestorePoint) {
        Create-RestorePoint
    }

    Write-Output "Starting backup from $SourceFolder to $DestinationFolder"
    # Use robocopy for mirroring backup
    robocopy $SourceFolder $DestinationFolder /MIR /Z /XA:H /W:5 /R:3

    if ($LASTEXITCODE -le 3) {
        Write-Output 'Backup completed successfully.'
        exit 0
    }
    else {
        Write-Output "Backup failed with code $LASTEXITCODE"
        exit 1
    }
}
catch {
    Write-Error $_.Exception.Message
    exit 1
}
