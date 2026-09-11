$ErrorActionPreference = 'Stop'

# The .NET Framework compiler uses the installed v4 runtime assemblies, so it
# does not require the discontinued .NET Framework 4.0 developer pack.
$csc = Join-Path $env:WINDIR 'Microsoft.NET\Framework\v4.0.30319\csc.exe'
$sdkBuild = Join-Path $PSScriptRoot '..\Mono_Cs_NET - copia\Mono_Cs_NET\obj\x86\Release'
$interopBuild = Join-Path $PSScriptRoot '..\Mono_Cs_NET - copia\Mono_Cs_NET\bin\Release'
$outDir = Join-Path $PSScriptRoot 'bin'
New-Item -ItemType Directory -Force -Path $outDir | Out-Null

& $csc /nologo /target:exe /platform:x86 /out:"$outDir\HoribaBridge.exe" `
  /reference:"$sdkBuild\Interop.JYCONFIGBROWSERCOMPONENTLib.dll" `
  /reference:"$interopBuild\Interop.JYMONOLib.dll" `
  /reference:"$interopBuild\Interop.JYSYSTEMLIBLib.dll" `
  /reference:System.Windows.Forms.dll /reference:System.Web.Extensions.dll `
  "$PSScriptRoot\Program.cs"
if ($LASTEXITCODE -ne 0) { throw "C# compiler failed with exit code $LASTEXITCODE." }

# The vendor's working Mono_Cs_NET.exe is linked with /NXCOMPAT:NO.  The
# installed SDK has no editbin.exe, so clear IMAGE_DLLCHARACTERISTICS_NX_COMPAT
# (0x0100) directly in the PE32 optional header of this x86 build.
$exe = "$outDir\HoribaBridge.exe"
$bytes = [System.IO.File]::ReadAllBytes($exe)
$peOffset = [System.BitConverter]::ToInt32($bytes, 0x3C)
$optionalHeader = $peOffset + 24
if ([System.BitConverter]::ToUInt16($bytes, $optionalHeader) -ne 0x10B) {
  throw 'HoribaBridge.exe is not a PE32 executable; refusing to modify its NXCOMPAT flag.'
}
$dllCharacteristicsOffset = $optionalHeader + 0x46
$dllCharacteristics = [System.BitConverter]::ToUInt16($bytes, $dllCharacteristicsOffset)
$withoutNxCompat = [uint16]($dllCharacteristics -band 0xFEFF)
[System.BitConverter]::GetBytes($withoutNxCompat).CopyTo($bytes, $dllCharacteristicsOffset)
[System.IO.File]::WriteAllBytes($exe, $bytes)

Copy-Item "$interopBuild\Interop.JYMONOLib.dll" -Destination "$outDir\Interop.JYMONOLib.dll" -Force
Copy-Item "$interopBuild\Interop.JYSYSTEMLIBLib.dll" -Destination "$outDir\Interop.JYSYSTEMLIBLib.dll" -Force
Copy-Item "$sdkBuild\Interop.JYCONFIGBROWSERCOMPONENTLib.dll" -Destination "$outDir\Interop.JYCONFIGBROWSERCOMPONENTLib.dll" -Force
Write-Host "Built $outDir\HoribaBridge.exe (NXCOMPAT disabled)."
