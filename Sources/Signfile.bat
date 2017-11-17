@echo off

REM sign the file...
REM "C:\Program Files (x86)\Microsoft SDKs\Windows\v7.0A\bin\signtool.exe" sign /f "D:\Github\Signing\treeDiM.pfx" /p 2B3gmehk %1

REM if %errorlevel% neq 0 exit /b %errorlevel%

set timestamp_server=http://timestamp.comodoca.com/authenticode
set sign_tool="C:\Program Files (x86)\Microsoft SDKs\ClickOnce\SignTool\signtool.exe"
set cer_path="D:\Github\Signing\treeDiM.pfx"

REM check signtool exists
if not exist %sign_tool% (
echo signtool could not be found at %sign_tool%
GOTO failed
)
REM check cer_path exists
if not exist %cer_path% (
echo certificate could not be found at %cer_path%
GOTO failed
)

REM try to timestamp the file...
%sign_tool% sign /f %cer_path% /p 2B3gmehk /t %timestamp_server% %1

if %errorlevel% neq 0 GOTO failed

:succeeded
REM return a successful code...
echo Signfile.bat exit code is 0.
EXIT /B 0

:failed
REM return an error code...
echo Signfile.bat exit code is %errorlevel%.
set errorlevel=0
EXIT /B 0