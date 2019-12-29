# Tested in Windows and PowerShell Core Only

cd Release

7z a win-x64-nodependencies.7z win-x64-nodependencies\ -mx9
7z a win-x64-netcore31runtime.7z win-x64-netcore31runtime\ -mx9

7z a linux-x64-nodependencies.7z linux-x64-nodependencies\ -mx9
7z a linux-x64-netcore31runtime.7z linux-x64-netcore31runtime\ -mx9

7z a linux-arm-nodependencies.7z linux-arm-nodependencies\ -mx9
7z a linux-arm-netcore31runtime.7z linux-arm-netcore31runtime\ -mx9

7z a osx-x64-nodependencies.7z osx-x64-nodependencies\ -mx9
7z a osx-x64-netcore31runtime.7z osx-x64-netcore31runtime\ -mx9