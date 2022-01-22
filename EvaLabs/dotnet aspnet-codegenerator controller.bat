@echo off

for /F "delims=" %%D in ('dir /B/S ".\Domain\Entities\*.cs"') do (

dotnet aspnet-codegenerator controller ^
--model %%~nD ^
--dataContext EvaLabs.Domain.Context.EvaContext ^
--useDefaultLayout ^
--referenceScriptLibraries ^
--relativeFolderPath Controllers ^
--useAsyncActions ^
--controllerName %%~nDController ^
--force

echo.----------------------------------------------------------

)

pause