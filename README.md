This repo is a simple C# script for generating `Template:Color_Skins`, `Template:Color_Weapon_Skins` and `Template:Color_Companions` for the official brawlhalla wiki.

The only things that would require it to be modified is new items with duplicate names. So it is pretty hacked together.

### Running

Clone the repo: `git clone --recurse-submodules https://github.com/eyalzus12/BrawlhallaColorPageGenerator.git`

Then inside the "BrawlhallaColorPageGenerator" folder: `dotnet run --project BrawlhallaColorPageGenerator`

Requires .NET 9