#include "cMusicGenerator.h"

// Warning C26812 : Prefer 'enum class' over 'enum' (Enum.3)
#pragma warning( disable : 26812 )

#include <iostream>
#include <fstream>
#include <sstream>		// String Stream
#include <string>

/*Method Name: LoadMusicInformation
  Takes:	   string, string&
  Returns:	   bool
  Description: Load music info from the file.
*/
bool cMusicGenerator::LoadMusicInformation(std::string musicFileName, std::string& errorString)
{
	std::ifstream musicFile(musicFileName);
	if (!musicFile.is_open())
	{
		errorString = "Error. musicFile file was not opened.";
		return false;
	}
	else
	return true;
}

/*Method Name: getNumberOfSongsLoaded
  Takes:	   void
  Returns:	   unsigned int
  Description: Returns number of songs loaded from file.
*/
unsigned int getNumberOfSongsLoaded()
{
	std::ifstream musicFile("hot_stuff_2.csv");
	if (!musicFile.is_open())
	{
		std::cout << "Error. musicFile file was not opened.";
		return 0;
	}

	std::string line;
	unsigned int numberOfLines = 0;

	while (std::getline(musicFile, line))
	{
		numberOfLines++;
	}

	return numberOfLines;
}

/*Method Name: findSong
  Takes:	   string, string
  Returns:	   cSong*
  Description: Returns a song object with UNIQUE id.
*/
cSong* cMusicGenerator::findSong(std::string songName, std::string artist)
{
	cSong* song = new cSong;
	std::string error;
	std::string rating = "";
	bool wasArtist = false;
	bool wasName = false;
	unsigned int randomSong = rand() % getNumberOfSongsLoaded();
	std::ifstream musicFile("hot_stuff_2.csv");
	if (!musicFile.is_open())
	{
		std::cout << "Error. musicFile file was not opened.";
		return 0;
	}
	std::string line;
	unsigned int lineCount = 0;
	while (std::getline(musicFile, line))
	{
		lineCount++;
		std::stringstream ssLine(line);
		std::string token;
		unsigned int tokenCount = 0;
		while (std::getline(ssLine, token, ','))
		{
			if (token == songName && tokenCount == 3)
			{
				wasName = true;
			}
			else if (token == artist && tokenCount == 4 && wasName == true)
			{
				wasArtist = true;
				song->name = token;
				song->artist = token;
			}
			else if (wasArtist = true && wasName == true && tokenCount == 8)
			{
				song->rating = stoi(token);
			}
			tokenCount++;
		}
	}
	song->uniqueID = randomSong;
	return song;
}

/*Method Name: getRandomSong
  Takes:	   void
  Returns:     cSong*
  Description: Returns a song object with properties assigned to properties of song in file respectivelly.
*/
cSong* cMusicGenerator::getRandomSong()
{
	cSong* song = new cSong;
	std::string name;
	std::string singer;
	std::string rating = "";
	std::string error;
	unsigned int randomSong = rand() % getNumberOfSongsLoaded();

	std::ifstream musicFile("hot_stuff_2.csv");
	if (!musicFile.is_open())
	{
		std::cout << "Error. musicFile file was not opened.";
		return 0;
	}
	
	std::string line;
	unsigned int lineCount = 0;

	while (std::getline(musicFile, line) && lineCount != randomSong)
	{
		lineCount++;
		std::stringstream ssLine(line);
		std::string token;
		unsigned int tokenCount = 0;
		while (std::getline(ssLine, token, ','))
		{
			// Get song name from the 4th column.
			if (tokenCount == 3)
			{
				name = token;
			}
			// Get the singer name.
			else if (tokenCount == 4)
			{
				singer = token;
			}
			// Get the week position.
			else if (tokenCount == 8)
			{
				rating = token;
			}
			// Skip other columns.
			tokenCount++;
		}
	}
	song->name = name;
	song->artist = singer;
	song->rating = stoi(rating);
	song->uniqueID = randomSong;
	return song;
}


