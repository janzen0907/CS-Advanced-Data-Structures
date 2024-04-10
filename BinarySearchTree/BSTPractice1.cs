using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Lists;


namespace BinarySearchTree;

public class BSTPractice1<T> : A_BST<T>, ICloneable where T : IComparable<T>
{
    public BSTPractice1()
    {
        //Intialize the root
        nRoot = null;
        iCount = 0;
    }


    public override void Add(T data)
    {
        //Add an item to a tree whereever it belongs
        //Base Case if the tree is null
        if(nRoot == null)
        {
            Node<T> newNode = new Node<T>(data);
        }
        else
        {
            //Call a recursive helper method to solve this
            recAdd(data, nRoot);
            //Call balance
            nRoot = Balance(nRoot);
        }
        iCount++;
    }

    private void recAdd(T data, Node<T> current)
    {
        //Compare the data we are adding to existing items in the tree
        //If the data is less than the root then we will have to find its home in the left
            //subtree
        //If the data is more it goes in the right subtree
        int iCompare = current.Data.CompareTo(data);
        if (iCompare < 0)
        {
            //The data belongs in the left
            if (current.Left == null)
            {
                //The left child is null so add the data
                current.Left = new Node<T>(data);
            }
            else
            {
                //Use recursion
                recAdd(data, current.Left);
                current.Left = Balance(current.Left);
            }
        }
        else
        {
            //data belongs on the right and the right child is null
            if(current.Right == null)
            {
                current.Right = new Node<T>(data);
            }
            else
            {
                //Use recursion
                recAdd(data, current.Right);
                current.Right = Balance(current.Right);
            }
        }
    }

    //Not a self balancing tree so this doesn't matter for this class
    internal virtual Node<T> Balance(Node<T> current)
    {
        return current;
    }

    public override void Clear()
    {
        //Clear the data from the tree
        nRoot = null;
        iCount = 0;
    }

    public object Clone()
    {
        BSTPractice1<T> clone = new BSTPractice1<T>();
        clone.iCount = this.iCount;
        clone.nRoot = this.nRoot;

        return (object)clone;
    }

    public override T Find(T data)
    {
        return recFind(data, nRoot);
    }

    private T recFind(T data, Node<T> current)
    {
        //The item we are looking for is the head
        T tReturn;
        if(current != null)
        {
            int iCompare = current.Data.CompareTo(data);
            if(iCompare == 0)
            {
                //We found the item
                tReturn = current.Data;
            }
            else if(iCompare < 0)
            {
                //Its somewhere on the left
                tReturn = recFind(data, current.Left);
            }
            else
            {
                //Must be somewhere on the right
                tReturn = recFind(data, current.Right);
            }

        }
        else
        {
            throw new ApplicationException(data + "was not found in tree ");
        }
        return tReturn;
    }

    public T FindSmallest()
    {
        if(nRoot == null)
        {
            throw new ApplicationException("The root cannot be null");
        }
        else
        {
            return recFindSmallest(nRoot);
        }
    }

    private T recFindSmallest(Node<T> current)
    {
        T tReturn;
        //Is my left child null?
        if(current.Left == null)
        {
            tReturn = current.Data;
        }
        //Is the left node null?
        //If smallest return data
        else
        {
            tReturn = recFindSmallest(current.Left);
        }
        //If not null use recurssion on the left child
        return tReturn;
    }

    public T FindLargest()
    {
        if(nRoot == null)
        {
            throw new ApplicationException("The tree is empty");
        }
        else
        {
            return recFindLargest(nRoot);
        }
    }

    private T recFindLargest(Node<T> current)
    {
        //If the right child is null add it
        //Otherwise use recursion
        T tReturn;

        if(current.Right != null)
        {
            tReturn = current.Data;
        }
        else
        {
            tReturn= recFindLargest(current.Right);
        }

        return tReturn;
    }


    public override IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    private class DepthFirstEnumerator : IEnumerator<T>
    {
        protected BSTPractice1<T> parent = null;
        protected Node<T> current = null;
        protected Stack<Node<T>> stack;

        public DepthFirstEnumerator(BSTPractice1<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current => current.Data;
        object IEnumerator.Current => current.Data;

        public void Dispose()
        {
            parent = null;
            current = null;
            stack = null;   
        }

        public bool MoveNext()
        {
            bool bMoved= false;
            if(stack.Count > 0)
            {
                bMoved = true;
                current = stack.Pop();
            }
            if(current.Right != null)
            {
                stack.Push(current.Right);
            }
            if(current.Left != null)
            {
                stack.Push(current.Left);
            }
            return bMoved;
        }

        public void Reset()
        {
           stack = new Stack<Node<T>>();
            if(parent.nRoot != null)
            {
                stack.Push(parent.nRoot);
            }
        }

    }

    private class BreathFirstEnumerator : IEnumerator<T>
    {
        protected BSTPractice1<T> parent = null;
        protected Node<T> current = null;
        protected Queue<Node<T>> queue;

        public BreathFirstEnumerator(BSTPractice1<T> parent)
        {
            this.parent = parent;
            Reset();
        }

        public T Current => current.Data;

        object IEnumerator.Current => current.Data;

        public void Dispose()
        {
            parent = null;
            queue = null;
            current = null;
        }

        public bool MoveNext()
        {
            bool bMoved = false;
            if(queue.Count < 0)
            {
                bMoved = true;
                current = queue.Dequeue();
            }
            if(current.Left != null)
            {
                queue.Enqueue(current.Left);
            }
            if(current.Right != null)
            {
                queue.Enqueue(current.Right);
            }
            return bMoved;
        }

        public void Reset()
        {
            queue = new Queue<Node<T>>();
            if(parent.nRoot != null)
            {
                queue.Enqueue(parent.nRoot);
            }
        }
    }

    public override int Height()
    {
        //Which height is greater?
        //Need a counter to keep track of the height on each side of the tree
        int iHeight = -1;
        if(nRoot != null)
        {
            iHeight = recHeight(nRoot);
        }
        return iHeight;
    }

    public int recHeight(Node<T> current)
    {
        int lHeight = 0;
        int rHeight = 0;
        if(current.Left != null) 
        {
            lHeight = recHeight(current.Left) + 1 ;
        }
        if(current.Right != null)
        {
            rHeight = recHeight(current.Right) + 1 ;
        }
        return lHeight > rHeight ? lHeight : rHeight;
    }

    public override void Iterate(ProcessData<T> pd, TRAVERSALORDER order)
    {
        if(nRoot != null)
        {
            recIterate(nRoot, pd, order);
        }
    }

    private void recIterate(Node<T> current, ProcessData<T> pd, TRAVERSALORDER order)
    {
        //PreOrder
        //root-->left-->right
        if(order == TRAVERSALORDER.PRE_ORDER)
        {
            pd(current.Data);
        }
        //then we go to the left tree
        if(current.Left != null)
        {
            recIterate(current.Left, pd, order);
        }
        if(order == TRAVERSALORDER.IN_ORDER)
        {
            pd(current.Data);
        }
        //Then we have to check the right tree
        if(current.Right != null)
        {
            recIterate(current.Right, pd, order);
        }
        if(order == TRAVERSALORDER.POST_ORDER)
        {
            pd(current.Data);
        }
    }

    public override bool Remove(T data)
    {
        //We will need to keep track of wheter we removed the node or nod
        bool bRemoved = false;
        nRoot = recRemove(nRoot, data, ref bRemoved);
        return bRemoved;
    }

    private Node<T> recRemove(Node<T> current, T data, ref bool bRemoved)
    {
        //Base case
        //remove a leaf node
        //remove a node with 2 chidldren
        //Remove the head node
        int iCompare = 0;
        if(current != null)
        {
            //If its 0 its the node we need
            //If its less its to the right
            //If its greater its to the left
            iCompare = data.CompareTo(current.Data);
            if(iCompare < 0)
            {
                current.Left = recRemove(current.Left, data, ref bRemoved);
            }
            if(iCompare > 0) 
            { 
                current.Right = recRemove(current.Right, data, ref bRemoved);
            }
            else //we found the node
            {
                bRemoved = true;
                //Check if its a leaf node
                if (current.IsLeaf())
                {
                    iCount--;
                    current = null;
                }
                else if (current.Left != null && current.Right != null)
                {
                    T tReplaceMe = recFindLargest(current.Left);
                    current.Data = tReplaceMe;
                    current.Left = recRemove(current.Left, tReplaceMe, ref bRemoved);
                }
                else if (current.Left != null)
                {
                    current = current.Left;
                    iCount--;
                }
                else
                {
                    current = current.Right;
                    iCount--;   
                }
            }
        }
        return current;
    }
}
