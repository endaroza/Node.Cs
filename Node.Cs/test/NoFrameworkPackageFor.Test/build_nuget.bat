@echo off
echo %CD%
cd ..
cd .. 
REM Solution dir
cd build_utils
call dobuild_env.bat TRUE
call dobuild_single NoFrameworkPackageFor.Test 4.0 net40 test\NoFrameworkPackageFor.Test
call dobuild_single NoFrameworkPackageFor.Test 4.5 net45 test\NoFrameworkPackageFor.Test
call dobuild_nuget NoFrameworkPackageFor.Test test\NoFrameworkPackageFor.Test

rem call dobuild_clean
