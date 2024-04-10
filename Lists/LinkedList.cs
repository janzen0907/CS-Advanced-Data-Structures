using System;
using System.Collections;
using System.Collections.Generic;

using DataStructuresCommon;

namespace Lists;
//An implementation of a list using arrays
//Know how to code the methods recurssivly and non recursivly


public class LinkedList<T> : A_List<T> where T: IComparable<T>
{
    //Interior member to hold our values. Initialize to empty. 
    private Node head;
    
    public override void Add(T data)
    {
        //Recursive version
        //head = recAdd(head, data);
        
        //If the list is empty just add it in
        if(head == null)
        {
            head = new Node(data);
        }
        else //the list is not empty
        {
            //find the last node in the list
            Node current = head;
            //Loop to the end of the list
            while(current.next != null)
            {
                current = current.next;
            }
            //Add the new item at the end of the list
            current.next = new Node(data);
        }

    }
    //Recursive helper for the Add method
    //Takes in a nodem and a data item to add to the end of the chain
    //Returns the node
    private Node recAdd(Node current, T data)
    {
        //Base Case is that we're at the end of the chain, 
        //and current is equal to null
        if(current == null)
        {
            //Add in the new data
            current = new Node(data);
        }
        else{ //We'reare ntot at the end of the chain
            current.next = recAdd(current.next, data);
        }
        return current;
    }

    public override void Clear()
    {
        //Set head to null will clear the list
        head = null;
    }

    //Another recursive version of insert - doesn't look foward
    //It just goes to the location we nedd to work on
    public override void Insert(int index, T data)
    {
        if(index > this.Count || index < 0)
        {
            throw new IndexOutOfRangeException("Index out of bounds");
        }
        //change the head of the list
        head = recInsert(index, head, data);
    }

    //Helper method for our second version of the insert
    //Node is a class thats marked private
    //The method signiture must be private for the recursive helper
    private Node recInsert(int index, Node current, T data)
    {
        //Base Case
        if(index == 0)
        {
            //Return a new node that has the data were adding, next property is current
            current = new Node(data, current);
        }
        else //recursive case
        {
            current.next = recInsert(--index, current.next, data);
        }

        return current;
    }

    /*
    //This implementation recurses to th enode previous to where we want
    //to do the work of adding in a new node. We might say it looks forward
    public override void Insert(int index, T data)
    {
        
        if(index > this.Count || index < 0)
        {
            throw new IndexOutOfRangeException("Index out of bound");
        }
        //If we're inserting at the head
            if(index == 0)
            {
                head = new Node(data, head);
            }
            else
            {
                recInsert(index, head, data);
            }

    }
     private void recInsert(int index, Node current, T data)
        {
            //if where we want to insert is in teh next position
            if(index == 1)
            {
                current.next = new Node(data, current.next);
            }
            else
            {
                recInsert(--index, current.next, data);
            }
        }
        */

    /*
    public override bool Remove(T data)
    {
        bool bRemoved = false;
        if(head == null)
        {
            //if the list is empty remove will always fail to find the item
            return bRemoved;
        }
        //To remove the first entry we would just have to move head to the 
            //next element
        //Check wheter the data we're looking for is head
        if(this.head.data.CompareTo(data) == 0)
        {
            //If it is, cut it out
            this.head = this.head.next; //skip the first item
            bRemoved = true;
        }
        else{
            //Otherwise we need to start looking ahead. Because we're
            //singlely-linked we have no way to get back to our parent/previous
            //node, so we need to look ahead to see if we want ot "link around" a node
            //we want to delete
            //This would be easier with a doubly-linked list.
            //Start off looking at the head
            Node currentNode = this.head;
            Node scoutNode; //Use this to look ahead
            //While we havent removed the item, and we're not at the end of the list
            while(!bRemoved && currentNode != null)
            {
                //Look ahead to the current Node's neighbor
                scoutNode = currentNode.next;
                //Check if the scout node is the data were looking to remove
                if(scoutNode != null && scoutNode.data.CompareTo(data) == 0)
                {
                    //cut it out
                    //We are setting the currentNode next to scoutNode next this will skip over the item thus removing it
                    currentNode.next = scoutNode.next;
                    bRemoved = true;
                }
                else{
                    currentNode = scoutNode;
                }
            }
        }
        //Remove the data from the linked list/
        //Return a boolean if it actually was removed
        return bRemoved;
    }
    */
    //Recursive version of the remove method
    public override bool Remove(T data)
    {
       // bool bRemoved = false;
       return recRemove(ref head, data);
        
        // if(index > this.Count || index < 0)
        // {
        //     throw new IndexOutOfRangeException("Index out of bounds");
        // }
        //If we're removing the head
       // if(this.head.data.CompareTo(data) == 0)
       // {
           // this.head = this.head.next;
           // bRemoved = true;
       // }
       // else{
            //call the recursive method
            //current doesnt exist, does this need to be head???
            //I wasnt sure what to put as the first parameter in this?
            
       // }
        //return bRemoved;

    }
    
    private bool recRemove(ref Node current, T data)
    {
        bool bRemoved = false;
        //Do we also need a scout node here?? Im not to sure
        //I did not need  a scout nod
        //Node scoutNode = current.next;
        //Base case
        //Already dealt with if it is the first item in the list
        //Need to deal with if it is a middle item in the lsit
        //The base case is current is null in which we dont do anything


        //A recursive cae may occure if current is not null
        //In my solution i missed that their could be 2 base cases
        //We dont care if its the head
        if(current != null)
        {
            //The other base case is if we've found the item
            if(current.data.CompareTo(data) == 0) 
            {
                bRemoved = true;
                current = current.next;
            }
            else
            {
            //Also not sure what needed to be put as a parameter for this one
            //I was correct
                bRemoved = recRemove(ref current.next, data);
            }
        }
        
        return bRemoved;
    }
    //We skipped this, so it will be on a exam or an assignment
    //Write them in both a recursive way and an itereative approch

    /// <summary>
    /// Remove an element of a linked list at a specified index
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    public override T RemoveAt(int index)
    {
        //Check if the index is out of bounds first
        if(index < 0)
        {
            throw new IndexOutOfRangeException("The index does not match the size of the LL");
        }
        
        //What if the index is the head? Deal with this
        if (index == 0)
        {
            T removed = head.data;
            head = head.next;
            return removed;
            
        }

        //If the item to be removed is not the head then call a 
        //Little recursive helper method
        return recRemoveAt(ref head, index);
    }

    /// <summary>
    /// Recursive helper method to remove an item at a specific index in a linked
    /// list
    /// </summary>
    /// <param name="nCurrent"> Reference to the current node</param>
    /// <param name="index">index in which to remove the item</param>
    /// <returns></returns>
    private T recRemoveAt(ref Node nCurrent, int index)
    {

        //If we found the node then we can remove it
        if(index == 0)
        {
            
            //We found the item
            T removed = nCurrent.data;
            //Point the linked list at the item after what we removed
            nCurrent = nCurrent.next;
            return removed;
        }
        //If thats not th case then we will call the recursive method again
        //Decrement the index until we reach 0
        return recRemoveAt(ref nCurrent.next, --index);
    }

    /// <summary>
    /// Replace node at a specified index with another node
    /// </summary>
    /// <param name="index"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <exception cref="InvalidOperationException"></exception>
    public override T ReplaceAt(int index, T data)
    {
        //The index is not within the size of our linked list
        if(index < 0)
        {
            throw new IndexOutOfRangeException("The index is not within the size of the LL");

        }
        //If the head is null then the list is empty
        if (head == null)
        {
            throw new InvalidOperationException("The list is empty");
        }
        //Call the recursive method
        return recReplaceAt(head, index, data);
        
    }
    /// <summary>
    /// Recursive helper method to replace a node at a specified inddex
    /// </summary>
    /// <param name="nCurrent"></param>
    /// <param name="index"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    private T recReplaceAt(Node nCurrent, int index, T data)
    {
        //If we find the node
        if(index == 0)
        {
            //replace the current nodes data with the new data
            nCurrent.data = data;
        }
        //We haven't found the item yet so use recursion
        else
        {
            //Call the recursive method and decrement the index till we find the item
            return recReplaceAt(nCurrent.next, --index, data);
        }
        return nCurrent.data;
    }

    public override IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
    }

    private class Enumerator: IEnumerator<T>
    {
        //reference to the parent linked list
        private LinkedList<T> parent;
        //Reference to the node we just moved to
        private Node lastVisited;
        //Reference to the next node we want to visit
        private Node scout;
        public Enumerator (LinkedList<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current
        {
            get
            {
                return lastVisited.data;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return lastVisited.data;
            }
        }

        public void Dispose()
        {
            parent = null;
            scout = null;
            lastVisited = null;
        }

        public void Reset()
        {
            //If we're reset we arent currently visiting any nodes
            lastVisited = null;
            //The next node -  the scout - is going to be the head of the list
            scout = parent.head;
        }
        //Move next moves the internal pointr to the next node if possible
        //Returns true if it can, false otherwise
        public bool MoveNext()
        {
            bool wasAbleToMove = false;
            //If the scout is not null then there is another node to move to
            if(scout != null)
            {
                //We can move
                wasAbleToMove = true;
                //Move the current node pointer to the scout
                lastVisited = scout;
                //move the scout to the next node
                scout = scout.next;
            }
            
            return wasAbleToMove;
        }
    }
    //A class that represents data and a reference to the node's neighbour
    private class Node
    {
        public T data;
        public Node next;
        //You can build one constructor off of another constructor
        //The colo : indicates to run another constructor first
        //often you'll specify running the base constructor of this class
        //but ut could be another class, like your parent
        public Node(T data) : this(data, null) { }
        public Node(T data, Node next)
        {
            this.data = data;
            this.next = next;
        }
    }

}