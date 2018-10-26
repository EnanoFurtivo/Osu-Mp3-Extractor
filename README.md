# Osu-Mp3-Extractor

Hi Everyone i was wondering if i could listen to some beats i had inside osu! in my phone, so i started to copy the files straight into it manually. So i made this program in C# wich allows you to select from your entire map collection. The maps you want will be correctly copied and exported with the original title and artist that are written in ASCII.

## Getting Started

If you are here for the simplified release version in .exe format you can download it here:
https://github.com/EnanoFurtivo/Osu-Mp3-Extractor/raw/master/Release.rar

- Just extract the Release folder and run the program.

- Make sure you have installed .NET framework. If you dont have it you can find it over on Prerequisites below.

### Prerequisites

Have in mind that you NEED .net framework since this is C# code. You can find it here:
https://www.microsoft.com/net/download/dotnet-framework-runtime

Also you'll need any decompresion program like winRar or 7zip to decompress the proyect.

### Installing

If you want to use the portable version:

- Just extract the Release folder and run the program.

If your trying to use the full proyect:

- Just open the Osu-Mp3-Extractor.sln file in visual studio [preferably 2017]

- Then include TagLib-sharp and Osu-Database-Reader library's via nuget [or other ways] to the proyect

- Done!

## Features

- Song Title and artist are embeed into the mp3 files as a Tag so that you can easly find the songs by filtering.

- Thumbnails of from the beatmaps are also embeed into the mp3 exports so you can more easly recognize your maps when using a player that supports it.

- You can filter songs by Title, Artist, or Map creator inside the program so you can choos map by map or if you so wish from a collection. 

- Also the duration of the song is displayed so that you dont choose that damn 15 minute version you had laying arround.

- And if your not sure enough its the beatmap you want in case you have 3 Million versions of the same map, the thumbnails are displayed in real time.

- After v2.5 maps wont get duplicated meaning if you export MAP_A, and some other day you want to export MAP_B but also accidentally select MAP_A, MAP_A wont be exported again, only MAP_B.

## Built With

* [Visual Studio 2017](https://visualstudio.microsoft.com/)
* [Osu Database Reader](https://github.com/HoLLy-HaCKeR/osu-database-reader)
* [TagLib Library](https://taglib.org/)

## License

This project is licensed under the GPL-3.0 License - see the [LICENSE.md](LICENSE.md) file for details