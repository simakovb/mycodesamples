#include "cPersonGenerator.h"

// Warning C26812 : Prefer 'enum class' over 'enum' (Enum.3)
#pragma warning( disable : 26812 )

#include <iostream>
#include <fstream>
#include <sstream>		// String Stream
#include <string>

/*Method Name: LoadCensusFiles
  Takes:	   string, string, string, string&
  Returns:	   bool
  Description: Load people info from the file.
*/
bool cPersonGenerator::LoadCensusFiles(
	std::string babyNameFile,
	std::string surnameFile,
	std::string streetNameFile,
	std::string& errorString)
{
	std::ifstream babyName(babyNameFile);
	if (!babyName.is_open())
	{
		errorString = "Error. babyName file was not opened.";
			return false;
	}
	std::ifstream surname(surnameFile);
	if (!surname.is_open())
	{
		errorString = "Error. surname file was not opened.";
		return false;
	}
	std::ifstream streetName(streetNameFile);
	if (!streetName.is_open())
	{
		errorString = "Error. streetName file was not opened.";
		return false;
	}
	else
	return true;
}

/*Method Name: readCSVFile
  Takes:	   void
  Returns:	   bool
  Description: Read csv file.
*/

bool readCSVFile(void)
{
	// Open the file
	std::ifstream namesFile("Names_2010Census.csv");
	if (!namesFile.is_open())
	{
		std::cout << "Didn't open file" << std::endl;
		return false;
	}

	std::string theLine;

	unsigned int lineCount = 0;
	while (std::getline(namesFile, theLine))
	{
		lineCount++;
		std::stringstream ssLine(theLine);

		std::string token;
		unsigned int tokenCount = 0;
		while (std::getline(ssLine, token, ','))
		{
			if (tokenCount == 0)
			{
				std::cout << token << std::endl;
			}
			// Ignore the other parts of the line
			tokenCount++;
		}
	}

	std::cout << "Lines read = " << lineCount << std::endl;

	return true;
}

/*Method Name: getNumberOfNamesLoaded
  Takes:	   void
  Returns:	   unsigned int
  Description: Returns number of names loaded from file.
*/
unsigned int cPersonGenerator::getNumberOfNamesLoaded()
{
	std::ifstream namesFile("yob1991.txt");
	if (!namesFile.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return false;
	}
	std::string line;
	unsigned int numberOfLines = 0;
	while (std::getline(namesFile, line))
	{
		numberOfLines++;
	}
	return numberOfLines;
}

/*Method Name: getNumberOfSurnamesLoaded
  Takes:	   void
  Returns:	   unsigned int
  Description: Returns number of surnames loaded from file.
*/
unsigned int cPersonGenerator::getNumberOfSurnamesLoaded()
{
	std::ifstream surnamesFile("Names_2010Census.csv");
	if (!surnamesFile.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return false;
	}
	std::string line;
	unsigned int numberOfLines = 0;
	while (std::getline(surnamesFile, line))
	{
		numberOfLines++;
	}
	return numberOfLines;
}

/*Method Name: getNumberOfStreetsLoaded
  Takes:	   void
  Returns:	   unsigned int
  Description: Returns number of streets loaded from file.
*/
unsigned int cPersonGenerator::getNumberOfStreetsLoaded()
{
	std::ifstream streetsFile("Street_Names.csv");
	if (!streetsFile.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return false;
	}
	std::string line;
	unsigned int numberOfLines = 0;
	while (std::getline(streetsFile, line))
	{
		numberOfLines++;
	}
	return numberOfLines;
}

/*Method Name: generateRandomPerson
  Takes:	   void
  Returns:	   cPerson*
  Description: Returns a person object with properties assigned to properties of person in files respectivelly.
*/
cPerson* cPersonGenerator::generateRandomPerson()
{
	cPerson* person = new cPerson;
	std::string first; 
	std::string middle; 
	std::string last; 
	std::string gender; 
	std::string streetName; 
	std::string streetType; 
	std::string streetDirection;

	unsigned int randomName = rand() % getNumberOfNamesLoaded();
	unsigned int randomSurname = rand() % getNumberOfSurnamesLoaded();
	unsigned int randomStreetName = rand() % getNumberOfStreetsLoaded();

	// Get random name.
	std::ifstream babyNames("yob1991.txt");
	if (!babyNames.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return 0;
	}
	std::string nameLine;
	unsigned int nameLineCount = 0;
	
	while (std::getline(babyNames, nameLine) && nameLineCount!=randomName)
	{
		nameLineCount++;
		if (nameLineCount == randomName)
		{
			std::stringstream ssLine(nameLine);
			std::string token;
			unsigned int tokenCount = 0;
			while (std::getline(ssLine, token, ','))
			{
				// First part of field is name.
				if (tokenCount == 0)
				{
					first = token;
				}
				// Second part is gender.
				else if (tokenCount == 1)
				{
					gender = token;
				}
				// We don't need the third one.
					tokenCount++;
			}
		}
	}

	// Get random surname.
	std::ifstream surnames("Names_2010Census.csv");
	if (!surnames.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return 0;
	}
	std::string surnameLine;
	unsigned int surnameCount = 0;

	while (std::getline(surnames, surnameLine) && surnameCount != randomSurname)
	{
		surnameCount++;
		if (surnameCount == randomSurname)
		{
			std::stringstream ssLine(surnameLine);
			std::string token;
			unsigned int tokeCount = 0;
			while (std::getline(ssLine, token, ','))
			{
				// Only first part needed.
				if (tokeCount == 0)
				{
					last = token;
				}
					tokeCount++;
			}
		}
	}
	
	// Get random street name.
	std::ifstream streetNames("Names_2010Census.csv");
	if (!streetNames.is_open())
	{
		std::cout << "Error. File was not open." << std::endl;
		return 0;
	}
	std::string streetLine;
	unsigned int streetCount = 0;

	while (std::getline(streetNames, streetLine) && streetCount != randomStreetName)
	{
		streetCount++;
		if (streetCount == randomStreetName)
		{
			std::stringstream ssLine(streetLine);
			std::string token;
			unsigned int tokenCount = 0;
			while (std::getline(ssLine, token, ','))
			{
				// Take first part as full street name.
				if (tokenCount == 0)
				{
					streetName = token;
				}
				// Take second part as street type.
				else if (tokenCount == 2)
				{
					streetType = token;
				}
				// Taker third as direction.
				else if (tokenCount == 3)
				{
					streetDirection = token;
				}
					tokenCount++;
			}
		}
	}

	// Assign received properties.
	person->first = first;
	person->last = last;
	if (gender == "M")
	{
		person->gender = cPerson::eGenderType(0);
	}
	else if (gender == "F")
	{
		person->gender = cPerson::eGenderType(1);
	}
	else
	{
		person->gender = cPerson::eGenderType(2);
	}
	person->streetName = streetName;
	person->streetType = streetType;
	person->streetDirection = streetDirection;

	return person;

}