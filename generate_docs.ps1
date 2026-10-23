param(
    [Parameter(ValueFromRemainingArguments = $true)]
    [string[]] $CommandArgs
)

$root = $PSScriptRoot

if (-not $CommandArgs) {
    $CommandArgs = @("PluginServicesRip", (Join-Path $root "docs/Plugins/Services"))
}

dotnet run --project (Join-Path $root "BaroAutoDoc") -- @CommandArgs
exit $LASTEXITCODE
