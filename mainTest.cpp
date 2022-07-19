/* Name: mainTest.cpp
*  Purpose: Test Snotify methods as well as generators.
*  Date: 2020-11-08
*  Author: Bohdan Simakov
*/
#include "cSnotify.h"
#include "cPersonGenerator.h"
#include "cMusicGenerator.h"
#include <iostream>

int main()
{
	//std::cout << "Generate 2 random people: " << std::endl;
	//cPersonGenerator* pPersonGenerator = new cPersonGenerator();
	//cPerson* personA = pPersonGenerator->generateRandomPerson();
	//cPerson* personB = pPersonGenerator->generateRandomPerson();
	//personA->SIN = 123;
	//std::cout << "Person 1 First name and Last name: " << personA->first << " " << personA->last << std::endl;
	//std::cout << "Person 2 First name and Last name: " << personB->first << " " << personB->last << std::endl << std::endl;

	//std::cout << "Test adding 2 users: " << std::endl;
	//cSnotify* snotify = new cSnotify();
	//std::string errorString;
	//if (snotify->AddUser(personA, errorString))
	//{
	//	std::cout << snotify->people.getAt(0).first << " " << snotify->people.getAt(0).last << " was added! " << std::endl;
	//}
	//else
	//{
	//	std::cout << "Was not added." << std::endl << std::endl;
	//}
	//if (snotify->AddUser(personB, errorString))
	//{
	//	std::cout << snotify->people.getAt(1).first << " " << snotify->people.getAt(1).last << " was added! " << std::endl;
	//}
	//else
	//{
	//	std::cout << "Was not added." << std::endl << std::endl;
	//}

	//std::cout << "Test update method, change name to Bohdan and overwrite data:" << std::endl;
	//personA->first = "Bohdan";
	//if (snotify->UpdateUser(personA, errorString))
	//{
	//	std::cout << "Data, overwritten." << std::endl;
	//	std::cout << snotify->people.getAt(0).first << " " << snotify->people.getAt(0).last << std::endl;
	//}
	//else
	//{
	//	std::cout << "Was not overwritten." << std::endl << std::endl;
	//}

	////std::cout << "Test delete method:" << std::endl;

	//cMusicGenerator* songGenerator = new cMusicGenerator();
	//cSong* songA = songGenerator->getRandomSong();
	//cSong* songB = songGenerator->getRandomSong();
	//std::cout << "\nTesting getRandomSong:" << std::endl;
	//std::cout << songA->name << " by " << songA->artist << std::endl;
	//std::cout << songB->name << " by " << songB->artist << std::endl << std::endl;

	//std::cout << "\nTesting addSong method:" << std::endl;
	//if (snotify->AddSong(songA, errorString))
	//{
	//	std::cout << snotify->music.getAt(0).name << " " << snotify->music.getAt(0).artist << " was added! " << std::endl;
	//}
	//else
	//{
	//	std::cout << errorString << std::endl;
	//}
	//if (snotify->AddSong(songB, errorString))
	//{
	//	std::cout << snotify->music.getAt(1).name << " " << snotify->music.getAt(1).artist << " was added! " << std::endl;
	//}
	//else 
	//{
	//	std::cout << errorString << std::endl << std::endl;
	//}

	//std::cout << "Testing addToUserLibrary method:" << std::endl;
	//if (snotify->AddSongToUserLibrary(personB->getSnotifyUniqueUserID(), songB, errorString))
	//{
	//	std::cout << personB->userLibrary.getAt(0).name << " by " << personB->userLibrary.getAt(0).artist << " was added to library! " << std::endl << std::endl;
	//}
	//else
	//{
	//	std::cout << "nothing was added." << std::endl;
	//}
	//std::cout << "\nTesting deleteUser" << std::endl;
	//std::cout << "Create person and song to delete." << std::endl;
	//cPerson* personToDelete = pPersonGenerator->generateRandomPerson();
	//std::cout << "Person To Delete First name and Last name: " << personToDelete->first << " " << personToDelete->last << std::endl;
	///*cSong* songToDelete = songGenerator->getRandomSong();
	//std::cout << songToDelete->name << " by " << songToDelete->artist << std::endl;*/
	//if (snotify->AddUser(personToDelete, errorString))
	//{
	//	std::cout << snotify->people.getAt(2).first << " " << snotify->people.getAt(2).last << " was added! " << std::endl;
	//}
	//else
	//{
	//	std::cout << "Was not added." << std::endl << std::endl;
	//}
	//if (snotify->AddSongToUserLibrary(personToDelete->getSnotifyUniqueUserID(), songB, errorString))
	//{
	//	std::cout << personToDelete->userLibrary.getAt(0).name << " by " << personToDelete->userLibrary.getAt(0).artist << " was added to library! " << std::endl << std::endl;
	//}
	//else
	//{
	//	std::cout << "nothing was added." << std::endl;
	//}

	//std::cout << "Test updateRatingOnSong method:" << std::endl;
	//personToDelete->userLibrary.getAt(0).rating = 1;
	//std::cout << "Old rating : " << personToDelete->userLibrary.getAt(0).rating << std::endl;
	//if ((snotify->UpdateRatingOnSong(personToDelete->getSnotifyUniqueUserID(), songB->getUniqueID(), 2)))
	//{
	//	std::cout << "New rating : " << personToDelete->userLibrary.getAt(0).rating << std::endl;
	//}

	////unsigned int songRating = 0;
	////if (snotify->GetCurrentSongRating(personToDelete->getSnotifyUniqueUserID(), songB->getUniqueID(), songRating))
	////{
	////	std::cout << "Rating of" << songB->name << " by " << songB->artist << " is " << songRating << std::endl;
	////}
	////personToDelete->userLibrary.getAt(0).numberOfTimesPlayed = 300;

	////unsigned int numPlays = 0;
	////if (snotify->GetCurrentSongNumberOfPlays(personToDelete->getSnotifyUniqueUserID(), songB->getUniqueID(), numPlays))
	////{
	////	std::cout << songB->name << " by " << songB->artist << " was played " << numPlays << " times." << std::endl;
	////}
	//std::cout << "Find by SIN and ID test" << std::endl;
	//if (snotify->FindUserBySIN(123))
	//{
	//	std::cout << snotify->FindUserBySIN(123)->first << " was found by SIN" << std::endl;
	//}
	//if (snotify->FindUserBySnotifyID(personA->getSnotifyUniqueUserID()))
	//{
	//	std::cout << snotify->FindUserBySnotifyID(personA->getSnotifyUniqueUserID())->first << " was found by ID." << std::endl;
	//}

	//std::cout << "Find song: " << std::endl;
	//std::cout << snotify->FindSong(songB->getUniqueID())->name << " was found by ID." << std::endl;
	//std::cout << snotify->FindSong(songB->name, songB->artist)->name << " was found by name and artist." << std::endl;

	//std::cout << "Test GetSong: " << std::endl;
	//personToDelete->userLibrary.getAt(0).numberOfTimesPlayed = 100;
	//std::cout << snotify->GetSong(personToDelete->getSnotifyUniqueUserID(), songB->getUniqueID(), errorString)->numberOfTimesPlayed << " times was plated" << std::endl;

	cMusicGenerator* songGenerator = new cMusicGenerator();
	std::string errorString;
	cPersonGenerator* pPersonGenerator = new cPersonGenerator();
	cSnotify* snotify = new cSnotify();
	cPerson* person1 = pPersonGenerator->generateRandomPerson();
	cPerson* person2 = pPersonGenerator->generateRandomPerson();
	cPerson* person3 = pPersonGenerator->generateRandomPerson();
	std::cout << "New person's First name and Last name: " << person1->first << " " << person1->last << std::endl;
	cSong* song1 = songGenerator->getRandomSong();
	cSong* song2 = songGenerator->getRandomSong();
	cSong* song3 = songGenerator->getRandomSong();
	cSong* song4 = songGenerator->getRandomSong();
	cSong* song5 = songGenerator->getRandomSong();

	if (snotify->AddUser(person1, errorString))
	{
		std::cout << snotify->people.getAt(0).first << " " << snotify->people.getAt(0).last << " was added! " << std::endl;
	}
	if (snotify->AddUser(person2, errorString))
	{
		std::cout << snotify->people.getAt(1).first << " " << snotify->people.getAt(1).last << " was added! " << std::endl;
	}
	if (snotify->AddUser(person3, errorString))
	{
		std::cout << snotify->people.getAt(2).first << " " << snotify->people.getAt(2).last << " was added! " << std::endl;
	}
	if (snotify->AddSong(song1, errorString))
	{
		std::cout << snotify->music.getAt(0).name << " " << snotify->music.getAt(0).artist << " was added! " << std::endl;
	}
	if (snotify->AddSong(song2, errorString))
	{
		std::cout << snotify->music.getAt(1).name << " " << snotify->music.getAt(1).artist << " was added! " << std::endl;
	}
	if (snotify->AddSong(song3, errorString))
	{
		std::cout << snotify->music.getAt(2).name << " " << snotify->music.getAt(2).artist << " was added! " << std::endl;
	}
	if (snotify->AddSong(song4, errorString))
	{
		std::cout << snotify->music.getAt(3).name << " " << snotify->music.getAt(3).artist << " was added! " << std::endl;
	}
	if (snotify->AddSong(song5, errorString))
	{
		std::cout << snotify->music.getAt(4).name << " " << snotify->music.getAt(4).artist << " was added! " << std::endl;
	}

	if (snotify->AddSongToUserLibrary(person1->getSnotifyUniqueUserID(), song1, errorString))
	{
		std::cout << snotify->people.getAt(0).userLibrary.getAt(0).name << "was added to library." << std::endl;
	}
	if (snotify->AddSongToUserLibrary(person1->getSnotifyUniqueUserID(), song2, errorString))
	{
		std::cout << snotify->people.getAt(0).userLibrary.getAt(1).name << "was added to library." << std::endl;
	}
	if (snotify->AddSongToUserLibrary(person1->getSnotifyUniqueUserID(), song3, errorString))
	{
		std::cout << snotify->people.getAt(0).userLibrary.getAt(2).name << "was added to library." << std::endl;
	}
	if (snotify->AddSongToUserLibrary(person1->getSnotifyUniqueUserID(), song4, errorString))
	{
		std::cout << snotify->people.getAt(0).userLibrary.getAt(3).name << "was added to library." << std::endl;
	}
	if (snotify->AddSongToUserLibrary(person1->getSnotifyUniqueUserID(), song5, errorString))
	{
		std::cout << snotify->people.getAt(0).userLibrary.getAt(4).name << "was added to library." << std::endl;
	}
	unsigned int sizeOfLibary = 10;
	cSong* pLibraryArray = new cSong[sizeOfLibary];

	std::cout << std::endl << "Test GetUsersSongLibrary method:" << std::endl;
	if (snotify->GetUsersSongLibrary(person1->getSnotifyUniqueUserID(), pLibraryArray, sizeOfLibary))
	{
		for (unsigned i = 0; i < sizeOfLibary; i++)
		{
			std::cout << pLibraryArray[i].name << " by " << pLibraryArray[i].artist << std::endl;
		}
	}
	std::cout << std::endl;


	std::cout << std::endl << "Test GetUsersSongLibraryAscendingByTitle method:" << std::endl;
	unsigned int sizeOfLibary2 = 10;
	cSong* pLibraryArray2 = new cSong[sizeOfLibary2];
	if (snotify->GetUsersSongLibraryAscendingByTitle(person1->getSnotifyUniqueUserID(), pLibraryArray2, sizeOfLibary2))
	{
		for (unsigned i = 0; i < sizeOfLibary2; i++)
		{
			std::cout << pLibraryArray2[i].name << " by " << pLibraryArray2[i].artist << std::endl;
		}
	}
	std::cout << std::endl << "Test GetUsersSongLibraryAscendingByArtists method:" << std::endl;
	unsigned int sizeOfLibary3 = 10;
	cSong* pLibraryArray3 = new cSong[sizeOfLibary3];
	if (snotify->GetUsersSongLibraryAscendingByArtist(person1->getSnotifyUniqueUserID(), pLibraryArray3, sizeOfLibary3))
	{
		for (unsigned i = 0; i < sizeOfLibary3; i++) {
			std::cout << pLibraryArray3[i].name << " by " << pLibraryArray3[i].artist << std::endl;
		}
	}
	else
	{
		std::cout << errorString << std::endl;
	}

	std::cout << std::endl << "Test GetUsersbyId method:" << std::endl;
	unsigned int sizeOfUserLibary2 = 10;
	cPerson* pUserLibraryArray2 = new cPerson[sizeOfUserLibary2];
	if (snotify->GetUsersByID(pUserLibraryArray2, sizeOfUserLibary2))
	{
		for (unsigned i = 0; i < sizeOfUserLibary2; i++)
		{
			std::cout << pUserLibraryArray2[i].first << " " << pUserLibraryArray2[i].last << " " << pUserLibraryArray2[i].getSnotifyUniqueUserID() << std::endl;
		}
	}
	else 
	{
		std::cout << errorString << std::endl;
	}

	std::cout << std::endl << "Test GetUsers method: " << std::endl;
	unsigned int sizeOfUserLibary = 10;
	cPerson* pUserLibraryArray = new cPerson[sizeOfUserLibary];
	if (snotify->GetUsers(pUserLibraryArray, sizeOfUserLibary))
	{
		for (unsigned i = 0; i < sizeOfUserLibary; i++)
		{
			std::cout << pUserLibraryArray[i].first << " " << pUserLibraryArray[i].last << std::endl;
		}
	}
	else
	{
		std::cout << errorString << std::endl;
	}

	std::cout << std::endl << "Test FindUsersFirstName method: " << std::endl;
	unsigned int sizeOfUserLibary3 = 10;
	cPerson* pUserLibraryArray3 = new cPerson[sizeOfUserLibary3];
	if (snotify->FindUsersFirstName(person2->first, pUserLibraryArray3, sizeOfUserLibary3))
	{
		for (unsigned i = 0; i < sizeOfUserLibary3; i++)
		{
			std::cout << pUserLibraryArray3[i].first << " " << pUserLibraryArray3[i].last << std::endl;
		}
	}
	else
	{
		std::cout << errorString << std::endl;
	}

	std::cout << std::endl << "Test FindUsersLastName method: " << std::endl;
	unsigned int sizeOfUserLibary4 = 10;
	cPerson* pUserLibraryArray4 = new cPerson[sizeOfUserLibary4];
	if (snotify->FindUsersLastName(person2->last, pUserLibraryArray4, sizeOfUserLibary4))
	{
		for (unsigned i = 0; i < sizeOfUserLibary4; i++) {
			std::cout << pUserLibraryArray4[i].first << " " << pUserLibraryArray4[i].last << std::endl;
		}
	}
	else {
		std::cout << errorString << std::endl;
	}
	std::cout << "0:" << pUserLibraryArray4[0].first << " " << pUserLibraryArray4[0].last << std::endl;
	std::cout << "1:" << pUserLibraryArray4[1].first << " " << pUserLibraryArray4[1].last << std::endl;
	std::cout << "2:" << pUserLibraryArray4[2].first << " " << pUserLibraryArray4[2].last << std::endl;
	std::cout << "3:" << pUserLibraryArray4[3].first << " " << pUserLibraryArray4[3].last << std::endl;
	//if (snotify->DeleteUser(person1->getSnotifyUniqueUserID(), errorString))
	//{
	//	std::cout << "Person was deleted." << std::endl;
	//}
	//if (snotify->DeleteSong(song1->getUniqueID(),errorString))
	//{
	//	std::cout << "Song was deleted." << std::endl;
	//}
	if (snotify->RemoveSongFromUserLibrary(person1->getSnotifyUniqueUserID(), song1->getUniqueID(), errorString))
	{
		std::cout << "Song was removed from library." << std::endl;
	}
	if (snotify->RemoveSongFromUserLibrary(person1->getSnotifyUniqueUserID(), song2->getUniqueID(), errorString))
	{
		std::cout << "Song was removed from library." << std::endl;
	}
	std::cout << "\nTesting GetUsersSongLibrary" << std::endl;
	if (snotify->GetUsersSongLibrary(person1->getSnotifyUniqueUserID(), pLibraryArray, sizeOfLibary))
	{
		std::cout << "song 0: " << pLibraryArray[0].name << std::endl;
		std::cout << "song 1: " << pLibraryArray[1].name << std::endl;
		std::cout << "song 2: " << pLibraryArray[2].name << std::endl;
		std::cout << "song 3: " << pLibraryArray[3].name << std::endl;
		std::cout << "song 4: " << pLibraryArray[4].name << std::endl;
		std::cout << "song 5: " << pLibraryArray[5].name << std::endl;
		std::cout << "song 6: " << pLibraryArray[6].name << std::endl;
		std::cout << "song 7: " << pLibraryArray[7].name << std::endl;
	}
	std::cout << std::endl;

}
