#include "cSnotify.h"

/*Method Name: swap
  Takes:	   cSong*, cSong*
  Returns:	   void
  Description: swap the values
*/
void swap(cSong* a, cSong* b)
{
	cSong t;
	t = *a;
	*a = *b;
	*b = t;
}
/*Method Name: swap
  Takes:	   cPerson*, cPerson*
  Returns:	   void
  Description: swap the values
*/
void swap(cPerson* a, cPerson* b)
{
	cPerson t;
	t = *a;
	*a = *b;
	*b = t;
}

/*Method Name: partitionName
  Takes:	   cSmartArray<cSong>, int, int
  Returns:	   int
  Description: Selects the last element as a pivot and places it correctly in the array
*/
int partitionName(cSmartArray<cSong> arr[], int low, int high)
{
	cSong pivot = arr->getAt(high);
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr->getAt(j).name < pivot.name)
		{
			i++;
			swap(&arr->getAt(i), &arr->getAt(j));
		}
	}
	swap(&arr->getAt(i + 1), &arr->getAt(high));
	return (i + 1);
}

/*Method Name: sortName
  Takes:	   cSmartArray<cSong>, int, int
  Returns:	   int
  Description: Sorts the array by song name.
*/
void sortName(cSmartArray<cSong> arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partitionName(arr, low, high);
		sortName(arr, low, pi - 1);
		sortName(arr, pi + 1, high);
	}
}

/*Method Name: partitionID
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:	   int
  Description: Selects the last element as a pivot and places it correctly in the array
*/
int partitionID(cSmartArray <cPerson> arr[], int low, int high)
{
	cPerson pivot = arr->getAt(high);
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr->getAt(j).getSnotifyUniqueUserID() < pivot.getSnotifyUniqueUserID())
		{
			i++;
			swap(&arr->getAt(i), &arr->getAt(j));
		}
	}
	swap(&arr->getAt(i + 1), &arr->getAt(high));
	return (i + 1);
}

/*Method Name: sortID
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:	   int
  Description: Sorts the array by id.
*/
void sortID(cSmartArray <cPerson> arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partitionID(arr, low, high);
		sortID(arr, low, pi - 1);
		sortID(arr, pi + 1, high);
	}
}

/*Method Name: partitionFirst
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:	   int
  Description: Selects the last element as a pivot and places it correctly in the array
*/
int partitionFirst(cSmartArray <cPerson> arr[], int low, int high)
{
	cPerson pivot = arr->getAt(high);
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr->getAt(j).first < pivot.first)
		{
			i++;
			swap(&arr->getAt(i), &arr->getAt(j));
		}
	}
	swap(&arr->getAt(i + 1), &arr->getAt(high));
	return (i + 1);
}

/*Method Name: sortFirst
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:	   int
  Description: Sorts the array by firstname.
*/
void sortFirst(cSmartArray <cPerson> arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partitionFirst(arr, low, high);
		sortFirst(arr, low, pi - 1);
		sortFirst(arr, pi + 1, high);
	}
}

/*Method Name: partitionLast
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:	   void
  Description: Selects the last element as a pivot and places it correctly in the array
*/
int partitionLast(cSmartArray <cPerson> arr[], int low, int high)
{
	cPerson pivot = arr->getAt(high);
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr->getAt(j).last < pivot.last)
		{
			i++;
			swap(&arr->getAt(i), &arr->getAt(j));
		}
	}
	swap(&arr->getAt(i + 1), &arr->getAt(high));
	return (i + 1);
}

/*Method Name: sortLast
  Takes:	   cSmartArray<cPerson>, int, int
  Returns:     int
  Description: Sorts the array by lastname.
*/
void sortLast(cSmartArray <cPerson> arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partitionLast(arr, low, high);
		sortLast(arr, low, pi - 1);
		sortLast(arr, pi + 1, high);
	}
}

/*Method Name: partitionArtist
  Takes:	   cSmartArray<cSong>, int, int
  Returns:     void
  Description: Selects the last element as a pivot and places it correctly in the array
*/
int partitionArtist(cSmartArray <cSong> arr[], int low, int high)
{
	cSong pivot = arr->getAt(high);
	int i = (low - 1);

	for (int j = low; j <= high - 1; j++)
	{
		if (arr->getAt(j).artist < pivot.artist)
		{
			i++;
			swap(&arr->getAt(i), &arr->getAt(j));
		}
	}
	swap(&arr->getAt(i + 1), &arr->getAt(high));
	return (i + 1);
}

/*Method Name: sortArtist
  Takes:	   cSmartArray<cSong>, int, int
  Returns:	   int
  Description: Sorts the array by artist name.
*/
void sortArtist(cSmartArray <cSong> arr[], int low, int high)
{
	if (low < high)
	{
		int pi = partitionArtist(arr, low, high);
		sortArtist(arr, low, pi - 1);
		sortArtist(arr, pi + 1, high);
	}
}

/*Method Name: AddUser
  Takes:	   cPerson*, string&
  Returns:	   bool
  Description: Adds user to snotify people container.
*/
bool cSnotify::AddUser(cPerson* pPerson, std::string& errorString)
{
	people.addAtEnd(*pPerson);
	return true;
}

/*Method Name: UpdateUser
  Takes:	   cPerson*, string&
  Returns:     bool
  Description: Overwrites data for user if SIN and ID matched, returns false if does not match.
*/
bool cSnotify::UpdateUser(cPerson* pPerson, std::string& errorString)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).getSnotifyUniqueUserID() == pPerson->getSnotifyUniqueUserID() &&
			people.getAt(i).SIN == pPerson->SIN)
		{
			people.getAt(i) = *pPerson;
			return true;
		}
	}
	errorString = "Error. Person was not found.";
	return false;
}

/*Method Name: DeleteUser
  Takes:	   unsigned int, string&
  Returns:     bool
  Description: Converts smart array to dll and deletes the chosen element,
			   then convert back to smart array.

  DLL implemented.
*/
bool cSnotify::DeleteUser(unsigned int SnotifyUserID, std::string& errorString)
 {
	linkedList <cPerson>* pplList = new linkedList <cPerson>;
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		pplList->insertBeforeCurrent(people.getAt(i));
	}
	node<cPerson>* pTempNode = new node<cPerson>();
	pTempNode = pplList->pCurrentNode;
	pplList->moveToFirst();
	while (nullptr != pplList->pCurrentNode) {
		if (pplList->pCurrentNode->pPerson.getSnotifyUniqueUserID() == SnotifyUserID) {
			pplList->deleteAtCurrent();
			people.clear();
			if (pplList->pCurrentNode != pplList->pFirstNode)
			{
				pplList->moveToFirst();
			}
			while (nullptr != pplList->pCurrentNode)
			{
				people.addAtEnd(pplList->pCurrentNode->pPerson);
				pplList->moveNext();
			}
			return true;
		}
		pplList->moveNext();
	}
	pplList->pCurrentNode = pTempNode;
	errorString = "Couldn't find your person to delete";
	return false;
}

/*Method Name: AddSong
  Takes:	   cSong*, string&
  Returns:	   bool
  Description: Adds song to the snotify music container.
*/
bool cSnotify::AddSong(cSong* pSong, std::string& errorString)
{
	music.addAtEnd(*pSong);
	return true;
}

/*Method Name: UpdateSong
  Takes:	   cSong*, string&
  Returns:     bool
  Description: Updates song if unique ID matched with the passed one from pSong.
*/
bool cSnotify::UpdateSong(cSong* pSong, std::string& errorString)
{
	for (unsigned int i = 0; i < music.getSize(); i++)
	{
		if (music.getAt(i).getUniqueID() == pSong->getUniqueID())
		{
			music.getAt(i) = *pSong;
			return true;
		}
	}
	errorString = "Error. Song was not found.";
	return false;
}

/*Method Name: DeleteSong
  Takes:	   unsigned int, string&
  Returns:     bool
  Description: Converts smart array to dll similiar to deleteUser method,
			   then deletes chose song and convert back.

  DLL implemented.
*/
bool cSnotify::DeleteSong(unsigned int UniqueSongID, std::string& errorString)
{
	for (unsigned int i = 0; i < music.getSize(); i++)
	{
		musicdll.insertBeforeCurrent(music.getAt(i));
	}
	node<cSong>* pTempNode = new node<cSong>();
	pTempNode = musicdll.pCurrentNode;
	musicdll.moveToFirst();

	while (nullptr != musicdll.pCurrentNode)
	{
		if (musicdll.pCurrentNode->pPerson.getUniqueID() == UniqueSongID)
		{
			musicdll.deleteAtCurrent();
			music.clear();
			musicdll.moveToFirst();
			while (nullptr != musicdll.pCurrentNode)
			{
				music.addAtEnd(musicdll.pCurrentNode->pPerson);
				musicdll.moveNext();
			}
			return true;
		}
		musicdll.moveNext();
	}
	musicdll.pCurrentNode = pTempNode;
	errorString = "Couldn't find your song to delete";
	return false;
}

/*Method Name: AddSongToUserLibrary
  Takes:	   unsigned int, cSong*, string&
  Returns:	   bool
  Description: Validates ID and adds a song to person's library.
*/
bool cSnotify::AddSongToUserLibrary(unsigned int snotifyUserID, cSong* pNewSong, std::string& errorString)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).getSnotifyUniqueUserID() == snotifyUserID)
		{
			people.getAt(i).userLibrary.addAtEnd(*pNewSong);
			return true;
		}
	}
	errorString = "Error. Invalid user ID or no name song with this name.";
	return false;
}

/*Method Name: deleteElement
  Takes:	   cSmartArray <cSong>, int, string
  Returns:	   int
  Description: Deletes chosen element. 
			   Created for only one use in next method.
*/
int deleteElement(cSmartArray <cSong>arr, int n, std::string songName)
{
	int i;
	for (i = 0; i < n; i++)
		if (arr.getAt(i).name == songName)
			break;
	if (i < n)
	{
		n = n - 1;
		for (int j = i; j < n; j++)
			arr.getAt(j) = arr.getAt(j + 1);
	}
	return n;
}
/*Method Name: RemoveSongFromUserLibrary
  Takes:	   unsigned int, unsigned int, string&
  Returns:	   bool
  Description: Convert smart array to dll and convert back after delete.
			   Looks for user from whose library song is going to be removed.

  DLL implemented.
*/
bool cSnotify::RemoveSongFromUserLibrary(unsigned int snotifyUserID, unsigned int SnotifySongID, std::string& errorString)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).getSnotifyUniqueUserID() == snotifyUserID)
		{
			for (unsigned int k = 0; k < people.getAt(i).userLibrary.getSize(); k++)
			{
				if (people.getAt(i).userLibrary.getAt(k).getUniqueID() == SnotifySongID)

				{
					deleteElement(people.getAt(i).userLibrary,people.getAt(i).userLibrary.getSize(),
						people.getAt(i).userLibrary.getAt(k).name);
					return true;
				}
			}
			errorString = "Error. Song was not found.";
		}
	}
	errorString = "Error. Person was not found.";
	return false;
}

/*Method Name: UpdateRatingOnSong
  Takes:	   unsigned int, unsigned int, unsigned int
  Returns:	   bool
  Description: Looks for the chosen user and song by IDs, then updates the rating with the passed one.
*/
bool cSnotify::UpdateRatingOnSong(unsigned int SnotifyUserID, unsigned int songUniqueID, unsigned int newRating)
{
	for (unsigned int k = 0; k < people.getSize(); k++)
	{
		if (people.getAt(k).getSnotifyUniqueUserID() == SnotifyUserID)
		{
			for (unsigned int i = 0; i < people.getAt(k).userLibrary.getSize(); i++)
			{
				if (people.getAt(k).userLibrary.getAt(i).getUniqueID() == songUniqueID)
				{
					people.getAt(k).userLibrary.getAt(i).rating = newRating;
					return true;
				}
			}
			std:: cout << "Error. Can't find the song." << std::endl;
			return false;
		}
	}
	std::cout << "Error. Can't find the user." << std::endl;
	return false;
}

/*Method Name: GetSong
  Takes:	   unsigned int, unsigned int, string
  Returns:     cSong*
  Description: Looks for the chosen song by IDs and returns pointer to the song.
			   Also updates numberOfTimesPlayed.
*/
cSong* cSnotify::GetSong(unsigned int SnotifyUserID, unsigned int songUniqueID, std::string& errorString)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).getSnotifyUniqueUserID() == SnotifyUserID)
		{
			for (unsigned int k = 0; k < people.getAt(i).userLibrary.getSize(); k++)
			{
				if (people.getAt(i).userLibrary.getAt(k).getUniqueID() == songUniqueID)
				{
					people.getAt(i).userLibrary.getAt(k).numberOfTimesPlayed++;
					return &people.getAt(i).userLibrary.getAt(k);
				}
			}
			errorString = "Song was not found.";
			return 0;
		}
	}
	errorString = "Person was not found.";
	return 0;
}

/*Method Name: GetCurrentSongRating
  Takes:	   unsigned int, unsigned int, unsigned int&
  Returns:	   bool
  Description: Returns the current song rating into the passed value(songRating).
			   Returns false if was not found.
*/
bool cSnotify::GetCurrentSongRating(unsigned int snotifyUserID, unsigned int songUniqueID, unsigned int& songRating)
{
	std::string errorString;
	cSong* tempSong = GetSong(snotifyUserID, songUniqueID, errorString);
	if (tempSong != NULL)
	{
		songRating = tempSong->rating;
		return true;
	}
	std::cout << errorString << std::endl;
	return false;
}

/*Method Name: GetCurrentSongNumberOfPlays
  Takes:	   unsigned int, unsigned int, unsigned int&
  Returns:	   bool
  Description: Returns the current song numberOfPlays into the passed value(numberOfPlays).
			   Returns false if was not found.
*/
bool cSnotify::GetCurrentSongNumberOfPlays(unsigned int snotifyUserID, unsigned int songUniqueID, unsigned int& numberOfPlays)
{
	std::string errorString;
	cSong* tempSong = GetSong(snotifyUserID, songUniqueID, errorString);
	if (tempSong != NULL)
	{
numberOfPlays = tempSong->numberOfTimesPlayed;
return true;
	}
	std::cout << errorString << std::endl;
	return false;
}

/*Method Name: FindUserBySIN
  Takes:	   unsigned int
  Returns:     cPerson*
  Description: Looks for person by SIN and returns a pointer to it.
*/
cPerson* cSnotify::FindUserBySIN(unsigned int SIN)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).SIN == SIN)
		{
			return &people.getAt(i);
		}
	}
	std::cout << "Error. Can't find the user." << std::endl;
	return 0;
}

/*Method Name: FindUserBySnotifyID
  Takes:	   unsigned int
  Returns:     cPerson*
  Description: Looks for user by SnotifyID and returns a pointer to it.
*/
cPerson* cSnotify::FindUserBySnotifyID(unsigned int SnotifyID)
{
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		if (people.getAt(i).getSnotifyUniqueUserID() == SnotifyID)
		{
			return &people.getAt(i);
		}
	}
	std::cout << "Error. Can't find the user." << std::endl;
	return 0;
}

/*Method Name: FindSong
  Takes:       string, string
  Returns:	   cSong*
  Description: Looks for song by name and artist and returns a pointer to it.
*/
cSong* cSnotify::FindSong(std::string title, std::string artist)
{
	for (unsigned int i = 0; i < music.getSize(); i++)
	{
		if (music.getAt(i).name == title && music.getAt(i).artist == artist)
		{
			return &music.getAt(i);
		}
	}
	std::cout << "Error. Can't find the song." << std::endl;
	return 0;
}

/*Method Name: FindSong
  Takes:       unsigned int
  Returns:	   cSong*
  Description: Looks for song by uniqueID and returns a pointer to it.
*/
cSong* cSnotify::FindSong(unsigned int uniqueID)
{
	for (unsigned int i = 0; i < music.getSize(); i++)
	{
		if (music.getAt(i).getUniqueID() == uniqueID)
		{
			return &music.getAt(i);
		}
	}
	std::cout << "Error. Can't find the song." << std::endl;
	return 0;
}

/*Method Name: GetUsersSongLibrary
  Takes:	   unsigned int, cSong*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users library, in the form of a regular dynamic array.
*/
bool cSnotify::GetUsersSongLibrary(unsigned int snotifyUserID, cSong*& pLibraryArray, unsigned int& sizeOfLibary)
{
	for (unsigned int j = 0; j < people.getSize(); j++)
	{
		if (people.getAt(j).getSnotifyUniqueUserID() == snotifyUserID)
		{
			for (unsigned int k = 0; k < people.getAt(j).userLibrary.getSize(); k++)
			{
				// TODO: Copy all the songs over
				pLibraryArray[k] = people.getAt(j).userLibrary.getAt(k);
			}
			sizeOfLibary = people.getAt(j).userLibrary.getSize();
			return true;
		}
	}
	std::cout << "Error. Can't find the user." << std::endl;
	return false;
}

/*Method Name: GetUsersSongLibraryAscendingByTitle
  Takes:	   unsigned int, cSong*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users library, in the form of a regular dynamic array sorted by title.
*/
bool cSnotify::GetUsersSongLibraryAscendingByTitle(unsigned int snotifyUserID, cSong*& pLibraryArray, unsigned int& sizeOfLibary)
{
	if (GetUsersSongLibrary(snotifyUserID, pLibraryArray, sizeOfLibary))
	{
		cSmartArray<cSong>* pArray = new cSmartArray<cSong>;
		for (unsigned i = 0; i < sizeOfLibary; i++)
		{
			pArray->addAtEnd(pLibraryArray[i]);
		}
		sortName(pArray, 0, pArray->getSize());
		for (unsigned i = 0; i < pArray->getSize(); i++)
		{
			pLibraryArray[i] = pArray->getAt(i);
		}
		sizeOfLibary = pArray->getSize();
		return true;
	}
	else {
		std::cout << "Error. Library was not found." << std::endl;
		return false;
	}
}

/*Method Name: GetUsersSongLibraryAscendingByArtist
  Takes:	   unsigned int, cSong*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users library, in the form of a regular dynamic array sorted by artist.
*/
bool cSnotify::GetUsersSongLibraryAscendingByArtist(unsigned int snotifyUserID, cSong*& pLibraryArray, unsigned int& sizeOfLibary)
{
	if (GetUsersSongLibrary(snotifyUserID, pLibraryArray, sizeOfLibary))
	{
		cSmartArray<cSong>* pArray = new cSmartArray<cSong>;
		for (unsigned i = 0; i < sizeOfLibary; i++)
		{
			pArray->addAtEnd(pLibraryArray[i]);
		}
		sortArtist(pArray, 0, pArray->getSize());
		for (unsigned i = 0; i < pArray->getSize(); i++)
		{
			pLibraryArray[i] = pArray->getAt(i);
		}
		sizeOfLibary = pArray->getSize();
		return true;
	}
	else
	{
		std::cout << "Error. Library was not found." << std::endl;
		return false;
	}
}

/*Method Name: GetUsers
  Takes:	   cPerson*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users in the form of a regular dynamic array.
*/
bool cSnotify::GetUsers(cPerson*& pAllTheUsers, unsigned int& sizeOfUserArray)
{
	sizeOfUserArray = people.getSize();
	for (unsigned int i = 0; i < people.getSize(); i++)
	{
		pAllTheUsers[i] = people.getAt(i);
	}
	return true;
}

/*Method Name: GetUsersByID
  Takes:	   cPerson*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users in the form of a regular dynamic array sorted by ID.
*/
bool cSnotify::GetUsersByID(cPerson*& pAllTheUsers, unsigned int& sizeOfUserArray)
{
	if (GetUsers(pAllTheUsers, sizeOfUserArray))
	{
		cSmartArray<cPerson>* pArray = new cSmartArray<cPerson>;
		for (unsigned i = 0; i < sizeOfUserArray; i++)
		{
			pArray->addAtEnd(pAllTheUsers[i]);
		}
		sortID(pArray, 0, pArray->getSize());
		for (unsigned i = 0; i < pArray->getSize(); i++)
		{
			pAllTheUsers[i] = pArray->getAt(i);
		}
		sizeOfUserArray = pArray->getSize();
		return true;
	}
	else
	{
		std::cout << "Error. Library was not found." << std::endl;
		return false;
	}
}

/*Method Name: FindUsersFirstName
  Takes:	   cPerson*&, unsigned int&
  Returns:     bool
  Description: Returns a COPY of the users in the form of a regular dynamic array sorted by first name.
*/
bool cSnotify::FindUsersFirstName(std::string firstName, cPerson*& pAllTheUsers, unsigned int& sizeOfUserArray)
{
	if (GetUsers(pAllTheUsers, sizeOfUserArray))
	{
		cSmartArray<cPerson>* pArray = new cSmartArray<cPerson>;
		for (unsigned i = 0; i < sizeOfUserArray; i++)
		{
			pArray->addAtEnd(pAllTheUsers[i]);
		}
		sortFirst(pArray, 0, pArray->getSize());
		for (unsigned i = 0; i < pArray->getSize(); i++)
		{
			pAllTheUsers[i] = pArray->getAt(i);
		}
		sizeOfUserArray = pArray->getSize();
		return true;
	}
	else
	{
		std::cout << "Error. Library was not found." << std::endl;
		return false;
	}
}

/*Method Name: FindUsersLastName
  Takes:	   cPerson*&, unsigned int&
  Returns:	   bool
  Description: Returns a COPY of the users in the form of a regular dynamic array sorted by last name.
*/
bool cSnotify::FindUsersLastName(std::string lastName, cPerson*& pAllTheUsers, unsigned int& sizeOfUserArray)
{
	if (GetUsers(pAllTheUsers, sizeOfUserArray))
	{
		cSmartArray<cPerson>* pArray = new cSmartArray<cPerson>;
		for (unsigned i = 0; i < sizeOfUserArray; i++)
		{
			pArray->addAtEnd(pAllTheUsers[i]); 
		}
		sortLast(pArray, 0, pArray->getSize());
		for (unsigned i = 0; i < pArray->getSize(); i++)
		{
			pAllTheUsers[i] = pArray->getAt(i);
		}
		sizeOfUserArray = pArray->getSize();
		return true;
	}
	else
	{
		std::cout << "Error. Library was not found." << std::endl;
		return false;
	}
}