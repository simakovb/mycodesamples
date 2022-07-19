// Author: Bohdan Simakov
// Date: 2020-10-24
// Purpose: doubly linked list class
#pragma once
#include <iostream>

template <class T>

class node
{
public:
	node* pNextNode;
	node* pPreviousNode;
	T pPerson;

	// c'tor
	node() {
		this->pNextNode = nullptr;
		this->pPreviousNode = nullptr;
	}
	// d'tor
	~node()
	{

	}
};

template <class lT>

class linkedList
{
public:
	node <lT>* pCurrentNode;
	node <lT>* pFirstNode;
	node <lT>* pLastNode;


	// c'tor
	linkedList() {
		this->pCurrentNode = nullptr;
		this->pFirstNode = nullptr;
		this->pLastNode = nullptr;
	}

	// d'tor
	~linkedList()
	{

	}

	// Methods.

	/*Method Name: insertBeforeCurrent
	  Takes: template <T>
	  Returns: void
	  Description: inserts an element before current node
	*/
	void insertBeforeCurrent(lT pToInsert)
	{
		if (this->pCurrentNode == nullptr)
		{
			this->pCurrentNode = new node<lT>;
			this->pFirstNode = this->pCurrentNode;
			this->pLastNode = this->pCurrentNode;
			this->pCurrentNode->pPerson = pToInsert;
			return;
		}
		node<lT>* pNewNode = new node<lT>();
		pNewNode->pPerson = pToInsert;
		pNewNode->pNextNode = this->pCurrentNode;
		pNewNode->pPreviousNode = this->pCurrentNode->pPreviousNode;

		if (this->pCurrentNode == this->pFirstNode)
		{
			this->pFirstNode = pNewNode;
		}
		this->pCurrentNode->pPreviousNode = pNewNode;
		this->pCurrentNode = pNewNode;

	}

	/*Method Name: insertBeforeCurrent
	  Takes: template <T>*
	  Returns: void
	  Description: inserts an element before current node
	*/
	void insertBeforeCurrent(lT* pToInsert)
	{
		if (this->pCurrentNode == nullptr)
		{
			this->pCurrentNode = new node<lT>();
			this->pFirstNode = this->pCurrentNode;
			this->pLastNode = this->pCurrentNode;
			this->pCurrentNode->pPerson = *pToInsert;
			return;
		}
		node <lT>* pNewNode = new node<lT>();
		pNewNode->pPerson = *pToInsert;
		pNewNode->pNextNode = this->pCurrentNode;
		pNewNode->pPreviousNode = this->pCurrentNode->pPreviousNode;
		this->pCurrentNode->pPreviousNode = pNewNode;
		this->pCurrentNode = pNewNode;
		this->pFirstNode = this->pCurrentNode;
	}

	/*Method Name: deleteAtCurrent
	  Takes: template <T>
	  Returns: void
	  Description: Deletes and element at the current node
	*/
	void deleteAtCurrent()
	{
		if (this->pCurrentNode->pPreviousNode != nullptr && this->pCurrentNode->pNextNode != nullptr) {
			node<lT>* pTemp = this->pCurrentNode->pNextNode;
			delete(pCurrentNode);
			this->pCurrentNode = pTemp;
			return;
		}
		else if (this->pCurrentNode->pPreviousNode == nullptr && this->pCurrentNode->pNextNode != nullptr) {
			if (this->pCurrentNode == this->pFirstNode) {
				this->pFirstNode = this->pCurrentNode->pNextNode;
			}
			node<lT>* pTemp = this->pCurrentNode->pNextNode;
			delete(pCurrentNode);
			this->pCurrentNode = pTemp;
			this->pCurrentNode->pPreviousNode = nullptr;
			return;
		}
		else if (this->pCurrentNode->pNextNode == nullptr && this->pCurrentNode->pPreviousNode != nullptr) {
			if (this->pCurrentNode == this->pLastNode) {
				this->pLastNode = this->pCurrentNode->pPreviousNode;
			}
			node<lT>* pTemp = this->pCurrentNode->pPreviousNode;
			delete(pCurrentNode);
			this->pCurrentNode = pTemp;
			this->pCurrentNode->pNextNode = nullptr;
			return;
		}
		else {
			delete(pCurrentNode);
			pCurrentNode = nullptr;
		}
	}

	/*Method Name: moveNext
	  Takes: void
	  Returns: void
	  Description: moves the current node a steap forward
	*/
	void moveNext()
	{

		if(this->pCurrentNode->pNextNode != nullptr)
		{
			this->pCurrentNode = this->pCurrentNode->pNextNode;
		}
		else
		{
			this->pCurrentNode = nullptr;
		}
	}

	/*Method Name: movePrevious
	  Takes: template <T>
	  Returns: void
	  Description: Moves current node one step back
	*/
	void movePrevious()
	{
		if (this->pCurrentNode->pPreviousNode != nullptr)
		{
			this->pCurrentNode = this->pCurrentNode->pPreviousNode;
		}
		else
		{
			this->pCurrentNode = nullptr;
		}
	}

	/*Method Name: moveToFirst
	  Takes: template <T>
	  Returns: void
	  Description: moves current node to first
	*/
	void moveToFirst()
	{
		while (this->pCurrentNode != this->pFirstNode)
		{
			movePrevious();
		}
	}

	/*Method Name: moveToLast
	  Takes: template <T>
	  Returns: void
	  Description: Moves current node to the last element
	*/
	void moveToLast()
	{
		while (this->pCurrentNode != this->pLastNode)
		{
			moveNext();
		}
	}

	/*Method Name: getCurrent
	  Takes: template <T>
	  Returns: lT
	  Description: returns element at the current node
	*/
	lT getCurrent()
	{
		return this->pCurrentNode->pPerson;
	}

	/*Method Name: isEmpty
	  Takes: template <T>
	  Returns: void
	  Description: Checks if the list is empty
	*/
	bool isEmpty()
	{
		if (this->pCurrentNode == nullptr)
		{
			return true;
		}
		else
			return false;
	}

	/*Method Name: clear
	  Takes: template <T>
	  Returns: void
	  Description: Clears the list
	*/
	void clear()
	{
		moveToFirst();
		while (this->pCurrentNode != nullptr)
		{
			deleteAtCurrent();
		}
	}


};

