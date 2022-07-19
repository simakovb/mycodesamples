// Author:  Bohdan Simakov
// Date:    2020-10-24
// Purpose: Smart array class

#pragma once
#include <iostream>

template <class T>

class cSmartArray {
private:

	// attributes
	unsigned int nextElementIndex = 0;
	static const unsigned int INITIALSIZE = 10;
	unsigned int currentSize = INITIALSIZE;
	T* peopleArrayP = new T[INITIALSIZE];


public:
	// c'tor
	cSmartArray()
	{
		this->nextElementIndex = 0;
	}
	// d'tor
	~cSmartArray()
	{

	}

	/*Method Name: addAtEnd
	  Takes:	   template <T>
	  Returns:	   void
	  Description: adds data to the "end" of the container
	  */
	void addAtEnd(T elementToInsert)
	{
		if (this->nextElementIndex == this->currentSize)
		{
			this->currentSize *= 2;
			T* pEnhancedArray = new T[this->currentSize];

			for (unsigned int i = 0; i != this->nextElementIndex; i++)
			{
				pEnhancedArray[i] = this->peopleArrayP[i];
			}

			T* pOldArrayPointer = this->peopleArrayP;
			this->peopleArrayP = pEnhancedArray;
			delete[] pOldArrayPointer;
		}
		this->peopleArrayP[this->nextElementIndex] = elementToInsert;
		this->nextElementIndex++;
		return;
	}


	/*Method Name: getSize
	  Takes:	   void
	  Returns:     unsigned int
	  Description: return the number of values in the container
	  */
	unsigned int getSize(void)
	{
		return this->nextElementIndex;
	}

	/*Method Name: isEmpty
	  Takes:	   void
	  Returns:     bool
	  Description: checks if the container empty or not
	  */
	bool isEmpty(void)
	{
		if (this->nextElementIndex > 0)
		{
			return false;
		}
		else
			return true;
	}

	/*Method Name: getAt
	  Takes:	   unsigned int
	  Returns:	   template <T>
	  Description: returns the value at particular index
	  */
	T &getAt(unsigned int index)
	{
		return this->peopleArrayP[index];
	}

	/*Method Name: clear
	  Takes:	   void
	  Returns:	   void
	  Description: deletes all the data from the container
	  */
	void clear(void)
	{
		T* clearedArray = new T[this->currentSize];
		delete[] peopleArrayP;
		T* pOldArrayPointer = this->peopleArrayP;
		this->peopleArrayP = clearedArray;
		this->nextElementIndex = 0;
	}

};


