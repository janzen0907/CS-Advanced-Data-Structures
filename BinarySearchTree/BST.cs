using Lists;
using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;



namespace BinarySearchTree;

public class BST<T> : A_BST<T>, ICloneable where T : IComparable<T>
{
    public BST()
    {
        //Initialize the root
        nRoot = null;
        //Set the count to 0
        //Counting the items in the tree can be time consuming so 
        //it is to our advantage to keep track of this
        iCount = 0;
    }

    /// <summary>
    /// This class will take a BST and convert it into a linkedlist
    /// This list will be sorted in descending order
    /// </summary>
    /// <returns>LinkedList with BST data</returns>
    public Lists.LinkedList<T> ToLinkedList()
    {
        //I could probably use the RecFindLargest method we wrote earlier
        
        //Create a list to store the BST in
        Lists.LinkedList<T> returnList = new Lists.LinkedList<T>();
        
        //If nRoot is empty just return the empty list
        if(nRoot == null)
        {
            return returnList;
        }

        //Otherwise call the recursive helper method to sort the list
        recFindLargestForList(nRoot, returnList);
        
        //return the created list
        return returnList;
    }

    
    /// <summary>
    /// Recusive method to find the largest element in the BST and add it to the LL
    /// I used the recFindLargest method from class and slightly altered it to
    /// add the data to the LL as we recursively find the largest element
    /// </summary>
    /// <param name="current"></param>
    /// <param name="list"></param>
    private void recFindLargestForList(Node<T> current, Lists.LinkedList<T> list)
    {
        //If its null just return, it will return an empty LL
        if(current == null)
        {
            return; //the list is empty
        }

        //Visit the right subtree first, as it will have the largest elements
        recFindLargestForList(current.Right, list);

        //Add the current nodes data to the linked list
        list.Add(current.Data);

        //Visit the left subtree
        recFindLargestForList(current.Left, list);  
      
    }



    //Adds an item to the tree.
    //Where? Whereever it belongs
    public override void Add(T data)
    {
        //When the tree is empty
        if(nRoot == null)
        {
            nRoot = new Node<T>(data);
        }
        else
        {
            //Otherwise well use recursion to solve this
            recAdd(data, nRoot);
            //TODO: Add balancing
            nRoot = Balance(nRoot);
        }

        iCount++;
    }

    private void recAdd(T data, Node<T> current)
    {
        //Compare the data we are adding with that stored in teh current node
        int iResult = data.CompareTo(current.Data);
        if(iResult < 0) 
        {
            //We know the data belongs to the left
            //Base case: The left child is null
            if(current.Left == null) 
            {
                current.Left = new Node<T>(data);
            }
            else 
            {
                //Recursive case - left node is not empty, add it to the left subtree
                recAdd(data, current.Left);
                current.Left = Balance(current.Left);
            }
        }
        else 
        {
            //Base case: The Right child is null
            if (current.Right == null)
            {
                current.Right = new Node<T>(data);
            }
            else
            {
                //Recursive case - right node is not empty,
                //add it to the right subtree
                recAdd(data, current.Right);
                current.Right = Balance(current.Right);
            }
        }
    }

    //Virtual balance method that might be overriden in a child class
    //This tree isn't self-balancing, so it does nothing
    internal virtual Node<T> Balance(Node<T> current)
    {
        return current;
    }

    public override void Clear()
    {
        nRoot = null;
        iCount = 0; 
    }

    //Clone is supposed to return a deep copy of the object
    //Required for IClonable
    public object Clone()
    {
        BST<T> clone = new BST<T>();
        clone.iCount = this.iCount;
        clone.nRoot = recClone(nRoot);

        return (object)clone;
    }

    //Recusively clone a node and its children
    private Node<T> recClone(Node<T> nCurrent)
    {
        //Create a new node 
        Node<T> nNew = null;

        if (nCurrent != null) 
        {
            //This isn't a true deep copy, because we're just doing a reference to the Data
            //instead of cloning the data
            //If we wanted a true deep copy, we'd want to make sure type T was also cloneable,
            //and then Clone the data too
            //But this works for basic types 
            nNew = new Node<T>(nCurrent.Data);
            //Then we'll clone the children
            nNew.Left = recClone(nCurrent.Left);
            nNew.Right = recClone(nCurrent.Right);
        }
        return nNew;
    }

    public override T Find(T data)
    {
        return recFind(data, nRoot);
    }

    protected T recFind(T data, Node<T> nCurrent) 
    {
        
        T tReturn;

        //The root is 
        if (nCurrent != null)
        {
            int iResult = data.CompareTo(nCurrent.Data);

            if(iResult == 0)
            {
                //We've found our object
                tReturn = nCurrent.Data;
            }
            else if (iResult < 0)
            {
                tReturn = recFind(data, nCurrent.Left);
            }
            else
            {
                tReturn = recFind(data, nCurrent.Right);
            }
        }
        else
        {
            throw new ApplicationException(data + " was not found in tree");
        }
        return tReturn;

        //maybe see if i can get my solution to work now. Was close but the recurssion was wrong

        //if (nRoot.Data.Equals(data))
        //{
        //    tReturn = data;
        //}

        ////If its less than the current node, search the left child
        //if (nCurrent.Data.CompareTo(data) < 0)
        //{
        //    //Less than 0 so search the left tree
        //    if (nCurrent.Left.Equals(data))
        //    {
        //        tReturn = data;
        //    }
        //    else
        //    {
        //        //Throw an exception as its not in the tree
        //        throw new ApplicationException("The node is not in the tree");
        //    }
        //}


        ////If its greater than the current node, search the right child
        //if (nCurrent.Data.CompareTo(data) > 0)
        //{
        //    if ((nCurrent.Right.Equals(data)))
        //    {
        //        tReturn = data;
        //    }
        //    else
        //    {
        //        //Throw an exception
        //        throw new ApplicationException("The node is not in the tree");
        //    }
        //}

        //return tReturn = data;


        //If we get null then its not in the tree, keep going until we hit a null
        //If its not in the tree we will throw an exception

    }

    public T FindSmallest()
    {
        if(nRoot == null)
        {
            throw new ApplicationException("Root is null");
        }
        else
        {
            

            return recFindSmallest(nRoot);
        }
    }

    //For me i was confused about the return type. But since we are only looking
    //for the largest value we only needed to take in the Node and return the node
    private T recFindSmallest(Node<T> current)
    {
        T tReturn;
        //Find the smallest
        //Algorithm:
        
        //Is my left child null?
        //Base case The left node is null
        if (current.Left == null)
        {
            //If it is, I am the smallest, return my data
            tReturn = current.Data;
            
        }
        //Its not null so lets check its child
        //If my left child is not null, return my left childs smallest
        else
        {
            tReturn = recFindSmallest(current.Left);
        }
        return tReturn;

    }

    public T FindLargest()
    {
        if (nRoot == null)
        {
            throw new ApplicationException("Root is null");
        }
        else
        {
            return recFindLargest(nRoot);
        }
    }

    //For me i was confused about the return type. But since we are only 
    private T recFindLargest(Node<T> current)
    {
        T tReturn;
 
        if (current.Right == null)
        {
            //If it is, I am the smallest, return my data
            tReturn = current.Data;
        }

        else
        {
            tReturn = recFindLargest(current.Right);
        }
        return tReturn;

    }



    public override IEnumerator<T> GetEnumerator()
    {
        return new DepthFirstEnumerator(this);
    }
    public  IEnumerator<T> GetBreadthFirstEnumerator()
    {
        return new BredthFirstEnumerator(this);
    }

    public override int Height()
    {
        //Which is the greater height, left or right?
        //We will have to check this on every node
        //Need some sort of counter that is incremented each time

        //Remeber the heigh is all the edges - if theres just one node
        //the height is 0
        //What if there are no nodes? We will say minus 1
        int iHeight = -1;
        if (nRoot != null)
        {
            iHeight = recHeight(nRoot);
            
        }
        return iHeight;

    }

    protected int recHeight(Node<T> nCurrent)
    {
        int heightLeft = 0;
        int heightRight = 0;
        //Figure out the height on the left
        
        if(nCurrent.Left != null)
        {
            heightLeft = recHeight(nCurrent.Left) + 1;
            
        }
        //Figure out the height on the right
        if (nCurrent.Right != null) 
        {
            heightRight = recHeight(nCurrent.Right) + 1;
            
        }
        return heightLeft > heightRight ? heightLeft : heightRight;
        //Add 1 to your height (because the edge between you and your childre)
        
        
        
        //return the height 
    }
    //There will be a midterm question about how the traversals work
    public override void Iterate(ProcessData<T> pd, TRAVERSALORDER order)
    {
        if(nRoot != null)
        {
            recIterate(nRoot, pd, order);
        }
        
    }

    /// <summary>
    /// This will iterate through the tree recursively, it will check the 
    /// 
    /// </summary>
    /// <param name="nCurrent"></param>
    /// <param name="pd"></param>
    /// <param name="order"></param>
    private void recIterate(Node<T> nCurrent, ProcessData<T> pd, TRAVERSALORDER order)
    {
        //Pre:order
        //First visit the root node
        if (order == TRAVERSALORDER.PRE_ORDER)
        {
            pd(nCurrent.Data);

        }

        //Then visist the left tree
        if(nCurrent.Left != null)
        {
            recIterate(nCurrent.Left, pd, order);
        }
        

        if(order == TRAVERSALORDER.IN_ORDER)
        {
            pd(nCurrent.Data);
        }
        
        //Then visit the right tree
        if(nCurrent.Right != null) 
        {
            recIterate(nCurrent.Right, pd, order);
        }

        if(order == TRAVERSALORDER.POST_ORDER)
        {
            pd(nCurrent.Data);  
        }        
    }

    /// <summary>
    /// Know how to write a iterative version of this
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public override bool Remove(T data)
    {
        //We need to keep track of wheter we removed the node or not
        bool bRemoved = false;
        //We need to alter the tree
        //We need a reference to bRemoved so it changes if it needs to
        nRoot = recRemove(nRoot, data, ref bRemoved);
        return bRemoved;

    }
    //There will be a question about remove on the midterm
        //With that I would also have to know how to do the findLargest, and findSmallet methods
    private Node<T> recRemove(Node<T> nCurrent,  T data, ref bool bRemoved)
    {
        //Base Case 
        //Remove a leaf node
        //Remove a node with 2 children
        //Remove the head node
        int iCompare = 0;

        if(nCurrent != null)
        {
            //If the current node is not null, we need to check to see if
            // it is the nofe to remove
            //If its 0, its the node we need
            //If its less than zero its to the left
            //If its greater than zero its to the right
            iCompare = data.CompareTo(nCurrent.Data);

            if(iCompare < 0) //the node we need to remove is in our left child
            {
                nCurrent.Left = recRemove(nCurrent.Left, data, ref bRemoved);
            }
            else if (iCompare > 0) //the node we need to remove is to the right 
            {
                nCurrent.Right = recRemove(nCurrent.Right, data, ref bRemoved); 
            }
            else //This is the node we need to remove!
            {
                bRemoved = true;
                //If we're removing a leaf node, it is pretty easy
                if(nCurrent.IsLeaf())
                {
                    iCount--; //I don't understand / remember where iCount comes from A_BST is the answer
                    nCurrent = null;
                }
                else if(nCurrent.Left != null &&  nCurrent.Right != null) 
                {
                    //This means we have children on both sides
                    //We will need to pull up a node from beneath us to take our place
                    //What nodes are viable candidates ? That is, what nodes will always be okay to replace
                    //this one with?? Either the biggest node on the left, or the smallest of the right
                    //This is removing and replacing the head
                    T tReplacement = recFindLargest(nCurrent.Left);
                    nCurrent.Data = tReplacement;
                    nCurrent.Left = recRemove(nCurrent.Left, tReplacement, ref bRemoved);
                }
                else if (nCurrent.Left != null)
                {
                    //We only have a left child
                    nCurrent = nCurrent.Left;
                    iCount--;
                }
                else
                {
                    //We only have a right child
                    nCurrent = nCurrent.Right;
                    iCount--;
                }
            }
        }
        return nCurrent;
        
    }
    //Need to keep track of what nodes we still need to visit
    //For this we will be using a Stack. 
    private class DepthFirstEnumerator : IEnumerator<T>
    {
        protected BST<T> parent = null;
        protected Node<T> nCurrent = null;
        //We need to keep track of nodes that we want to go back to
        //we want to do this in a last in, first out matter, which means we need a stack
        //Note: If we were using recursion (which we really can't in an enumerator)
        //we could jsut use the call stack as our stack instead
        protected Stack<Node<T>> sNodes;

        public DepthFirstEnumerator(BST<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current => nCurrent.Data;

        object IEnumerator.Current => nCurrent.Data;

        public void Dispose()
        {
            parent = null;
            nCurrent = null;
            sNodes = null;
        }

        public bool MoveNext()
        {
            //Keep track of whether we were actually able to move
            bool bMoved = false;
            //If there are any nodes in the stack, we've got something to do
            if(sNodes.Count > 0)
            {
                bMoved = true;
                //Get the next node from the stack
                nCurrent = sNodes.Pop();
                
                if (nCurrent.Right != null)
                {
                    sNodes.Push(nCurrent.Right);
                }
                //Add on any children, if they exist
                if (nCurrent.Left != null)
                {
                    sNodes.Push(nCurrent.Left);
                }
                

            }

            return bMoved;
        }

        private void pushAllLeft(Node<T> nLeft)
        {
            if(nLeft != null)
            {
                sNodes.Push(nLeft);
                pushAllLeft(nLeft.Left);
            }
        }

        public void Reset()
        {
            //Set up our stack
            sNodes = new Stack<Node<T>>();
            //Remember: upon reset, an enumerator should be 
            //*before* the first item, this means we'l put the head of our tree on the stack
            if(parent.nRoot != null)
            {
                sNodes.Push(parent.nRoot);
            }
        }
    }

    //this on uses a Que we must visit the children, left first then right
    private class BredthFirstEnumerator : IEnumerator<T>
    {
        protected BST<T> parent = null;
        protected Node<T> nCurrent = null;
        //Breadth first enumeration will visit all nodes on the same level
        //before going onto the next. That means we want a que instead of a stack
        protected Queue<Node<T>> qNodes;

        public BredthFirstEnumerator(BST<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current => nCurrent.Data;

        object IEnumerator.Current => nCurrent.Data;

        public void Dispose()
        {
            parent = null;
            nCurrent = null;
            qNodes = null;
        }

        public bool MoveNext()
        {
            //Keep track of whether we were actually able to move
            bool bMoved = false;
            //If there are any nodes in the stack, we've got something to do
            if (qNodes.Count > 0)
            {
                bMoved = true;
                //Get the next node from the stack
                nCurrent = qNodes.Dequeue();


                if (nCurrent.Left != null)
                {
                    qNodes.Enqueue(nCurrent.Left);
                }
                //Add on any children, if they exist
                if (nCurrent.Right != null)
                {
                    qNodes.Enqueue(nCurrent.Right);
                }
                

            }

            return bMoved;
        }

        private void pushAllLeft(Node<T> nLeft)
        {
            if (nLeft != null)
            {
                qNodes.Enqueue(nLeft);
                pushAllLeft(nLeft.Left);
            }
        }

        public void Reset()
        {
            //Set up our stack
            qNodes = new Queue<Node<T>>();
            //Remember: upon reset, an enumerator should be 
            //*before* the first item, this means we'l put the head of our tree on the stack
            if (parent.nRoot != null)
            {
                qNodes.Enqueue(parent.nRoot);
            }
        }
    }

}
