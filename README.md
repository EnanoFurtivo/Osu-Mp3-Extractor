# Osu-Mp3-Extractor

Hi Everyone i was wondering if i could listen to some beats i had inside osu! in my phone, so i started to copy the files straight into it manually. Of course i got away with the awful Unicode Titles ex: (). and artists ex: (). So i made this program in C# wich allows you to select from your entire map collection. The maps you want will be correctly copied and exported with the original title and artist that are written in ASCII.

## Getting Started

If you are here for the simplified release version in .exe format you can download it here:
https://github.com/EnanoFurtivo/Osu-Mp3-Extractor/raw/master/Release.rar

- Just extract the Release folder and run the program.

- make sure you have installed .NET framework. If you dont have it you can find it over on Prerequisites below.

### Prerequisites

Have in mind that you NEED .net framework since this is C# code. You can find it here:
https://www.microsoft.com/net/download/dotnet-framework-runtime

Also you'll need any decompresion program like winRar or 7zip to decompress the proyect.

### Installing

If you want to use the portable version:

- Just extract the Release folder and run the program.

If your trying to use the full proyect:

- Just open the Osu-Mp3-Extractor.sln file in visual studio [preferably 2017]

- Then include TagLib-sharp library via nuget [or other ways] to the proyect

- Done!

## Built With

* [Visual Studio 2017](https://visualstudio.microsoft.com/)
* [TagLib Library](https://taglib.org/)

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details