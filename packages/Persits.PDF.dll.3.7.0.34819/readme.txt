There's a 2.0 already? I did get it working with 1.0, using a nuget package. I am pretty sure you would need to use nuget for 2.0 too. Here are the instructions I gave to the production team, if it helps. Note it's a little specific to visual studio but the overall concept should be the same if packaging for production. Also I'll send the spec and nupkg file.



1.    Download/install nuget command line tools.
2.    In some temporary folder, run `nuget spec` against the DLL, e.g. `nuget spec -a the_dll.dll`
3.    Edit the generated .spec file to remove the sample dependency, or make other changes as desired.
4.    Create a subfolder lib\netstandard1.1 and put the dll in there.
5.    Run `nuget pack the_dll.spec` to create the package.
6.    Put the package and spec file into the 3rdParty folder in the code tree (under the very top root - it's a subfolder in the folder where the Visual Studio .sln file is).
7.    Right-click on the project/solution and choose Manage NuGet Packages.
8.    Click on the settings icon near the top right beside Package Source.
9.    Add a repo that points to the 3rdParty folder in the root of the project.
10.  Install the nuget package from that repo.



nuget pack the_dll.spec