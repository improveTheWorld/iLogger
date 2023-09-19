@echo off
setlocal enabledelayedexpansion

rem Get the version number from the file
set "version="
for /f "tokens=* delims=" %%F in (..\Version) do set "version=%%F"

rem Check if version is not empty
if not "!version!"=="" (
    rem Build the project and redirect only the error output
    dotnet build ../src --configuration Release /p:Version=%version% /p:Force=true 2> build_errors.txt

    rem Check if the error output is empty
    rem ...
) else (
    echo "Error: Failed to retrieve the version number from the file."
)

:end
