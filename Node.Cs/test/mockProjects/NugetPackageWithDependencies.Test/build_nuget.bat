@echo off
echo %CD%
cd ..
cd .. 
cd ..
REM Solution dir
cd build_utils
call dobuild_env.bat TRUE
call dobuild_single NugetPackageWithDependencies.Test 4.0 net40 test\mockProjects\NugetPackageWithDependencies.Test
call dobuild_single NugetPackageWithDependencies.Test 4.5 net45 test\mockProjects\NugetPackageWithDependencies.Test
call dobuild_nuget NugetPackageWithDependencies.Test test\mockProjects\NugetPackageWithDependencies.Test

rem call dobuild_clean
