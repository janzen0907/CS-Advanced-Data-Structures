using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Intrinsics.X86;
using System.Threading.Tasks;

namespace BinarySearchTree;


public class AVLT<T> : BST<T> where T : IComparable<T>
{
    public override void Iterate(ProcessData<T> pd, TRAVERSALORDER order)
    {
        //All traversals have tricks to them
        //Pre-Order is the easiest, process root node then each of the children
        //Loop on If the stack has items in it or not
        //Process the root node
            //Put the right child onto the stack, then the left child of the stack
            //Pop a node off of the stack and process it.
            //Evertime after we process the data, we  should put the right and left child on the stack

        //In-Order
            //First we have to go as far left as possible
            //Keep traversing left until null is hit
            //Once we hit a null we can do processing this means pop it off and process
                //After processing for everthing on the right, traverse the left side
                    //Then we process the right after left, is completly done
                    
        //Post-Order
            //Need a way to keep track of if we still need to deal with the right hand children of the node
            //Boolean, to check would have been a good way
            //Add a double of every value to the stack
                //This way we can peek at the stack, if theirs 2 of the same value
                //Then we can pop a node off the stack, but instead of processing it head down the right side
                //When theirs only one of a value in the Stack then we can pop it off and process it
            //Could have also used a useless class like tombstone 
            //Many ways to do this, its important to know what data we need to traverse through these
        Stack<Node <T>> stack = new Stack<Node <T>>();
        Node<T> nCurrent = nRoot;

        //In order Traversal
        if(order == TRAVERSALORDER.IN_ORDER)
        {
            //Why would i use a while(true)? Doofus move
            while (true)
            {
                //if not null put it in the stack
                if (nCurrent != null)
                {
                    stack.Push(nCurrent);
                    //Process the data
                    //pd(nCurrent.Data);
                    Console.WriteLine(nCurrent.Data + "Traversing left side of tree");
                    //Check the left child
                    nCurrent = nCurrent.Left;
                }
                else
                {
                    //if the stack is empty break as there are no more nodes to traval
                    if (stack.Count == 0)
                    {
                        break;
                    }

                    //make nCurrent the value we popped off the stack
                    nCurrent = stack.Pop();
                    Console.WriteLine(nCurrent.Data + "Traversing right side of tree");
                    //process the data
                    pd(nCurrent.Data);
                    //Move to the right child
                    nCurrent = nCurrent.Right;
                }
            }
        }
        if(order == TRAVERSALORDER.PRE_ORDER)
        {
            //Push the root onto the stack
            stack.Push(nCurrent);
            
            //Loop until our stack is empty
            while(stack.Count != 0) 
            {
                //remove whatevers at the top of the stack
                nCurrent = stack.Pop();
                pd(nCurrent.Data);
                //Add the right side of the node to the stack
                if(nCurrent.Right != null)
                {
                    //We can put right in first as the stack is last in first out
                    stack.Push(nCurrent.Right);
                }
                //If the left is not empty add it to the stack
                if (nCurrent.Left != null)
                {
                    stack.Push(nCurrent.Left);
                }
            }    
        }
        if(order == TRAVERSALORDER.POST_ORDER)
        {
            Stack<Node<T>> stack2 = new Stack<Node<T>>();
            //First push the root onto stack 1
            stack.Push(nCurrent);
            while(stack.Count > 0) 
            {
                nCurrent = stack.Pop();
                stack2.Push(nCurrent);
                
                //Push the left child onto the stack
                if(nCurrent.Left != null)
                {
                    stack.Push(nCurrent.Left);
                }
                //Push right onto the stack
                if (nCurrent.Right != null) 
                { 
                    stack.Push(nCurrent.Right);
                }

            }
            //Deal with the nodes in stack 2, which should be in post order
            while(stack2.Count != 0)
            {
                //Process the data
                pd(nCurrent.Data);
                //Remove the processed node
                stack2.Pop();
            }
        }
           
    }

    /// <summary>
    /// Iterative approach to adding values to ths BST
    /// </summary>
    /// <param name="data"></param>
    public override void Add(T data)
    {
        //Got this wrong.
        //First check the root
            //Check if you have a left
            //Figure out if it belongs on the left or right
            //Most of the above was right but 
                //Needed to balance from the bottom up.
                //Keep track of all the nodes we went through
              
        //If the tree is empty
        if(nRoot == null)
        {
            nRoot = new Node<T>(data);
        }
        else
        {
            //We need to compare the data to the other nodes in the tree
            //Variable to hold the current data
            Node<T> nCurrent = nRoot;
            //Need something to hold the tree values so we can create the new 
            //Structure for our tree and balance it easily. For this I will use a stack
            Stack<Node<T>> values = new Stack<Node<T>>();
            //Variable to compare the data to be added against current data
            int iResult = data.CompareTo(nCurrent.Data);

            //Use a while loop to go through all the elements in the tree
            while(nCurrent != null)
            {
                //Push the values into the stack
                values.Push(nCurrent);
                //if iResult is less than 0 we know that the data belongs 
                //to the left child
                if(iResult < 0)
                {
                    //check if nCurrents left child is null
                    if(nCurrent == null)
                    {
                        //Add the data as nCurrents left child
                        nCurrent.Left = new Node<T>(data);
                        //We added the element so we can break
                        break;
                    }
                    else
                    {
                        //go furhter down the left subtree
                        nCurrent = nCurrent.Left;
                    }
                }
                else
                {
                    //If it doesnt belong on the left then we will traverse the right subtree
                    //check if nCurrents right child is null
                    if(nCurrent == null)
                    {
                        //Add the data as nCurrents right child
                        nCurrent.Right = new Node<T>(data);
                        //Added the element, break
                        break;
                    }
                    else
                    {
                        //keep traversing the right subtree
                        {
                            nCurrent = nCurrent.Right;
                        }
                    }
                }
            }

            //Now we need to balance the tree. I will use another loop and i will pop
            //the elements off the stack and balance them
            while(values.Count > 0)
            {
                nCurrent = values.Pop();
                //Balance the elements using the balance method we created in class
                nCurrent = Balance(nCurrent);   
            }

            //Update the root if needed
            nRoot = nCurrent;
        }

        //increment the count of items
        iCount++;
    }

    /**
     * Right positive, left Negative
     * Algorithim for balancing AVLTs
     * First, go down to the bottom and insert the node -- Add already does this
     * Then we balance bottom up, making sure at each step the tree is balanced
     * Again, add does this by calling balance at each step up the tree
     * 
     * nNewNode <-- current Node
     * if the current node is not null
     *  calculate the difference in hiehgh between teh right and left subtree
     *  (this is the balance factor!)
     *  if the tree is unbalanced to the right
     *      Compute the balance factor of the right child
     *          nNewRoot <--- double left rotation on our current node
     *      else
     *          nNewRoot <--- single left rotation on our current node
     *  else if were unbalanced to the left
     *      Compute the balance factor of the left child
     *      If the left child is right heavy
     *          nNewRoot <--- double right roation on our current node
     *      else
     *          nNewRoot <--- single right rotation on our current node
     *      ekse
     *          Were already balanced we dont have to do anything
     *      Return the new root
     *Note: use the recHeight method to find the height
     */
    internal override Node<T> Balance(Node<T> nCurrent)
    {
        //Go down to the bottom and insert the node

        Node<T> nNewRoot = nCurrent;
        //Balance bottom up making sure at each step the tree is balanced
 
        //if the current node is not null
        if(nCurrent != null)
        {
            //balance factor
            int iHeightDiff = getHeightDifference(nCurrent); 
           //if its greater 1 we need to do a rotation to the right
            if(iHeightDiff > 1)
            {
                //Get balance factor of right child
                int iRightChildHeightDiff = getHeightDifference(nNewRoot.Right);

                if(iRightChildHeightDiff < 0)
                {
                    //double left on current node
                    nNewRoot = DoubleLeft(nCurrent);
                }
                else
                {
                    //Single left
                    nNewRoot = SingleLeft(nCurrent);
                }
            }
            
            //unbalanced to the left
            else if(iHeightDiff < -1)
            {
                int iLeftChildHeightDiff = getHeightDifference(nNewRoot.Left);
                if(iLeftChildHeightDiff > 0)
                {
                    //our left child is right heavy
                    nNewRoot = DoubleRight(nCurrent);
                }
                else
                {
                    nNewRoot = SingleRight(nCurrent);
                }
            }
       
        }
        
        return nNewRoot;
        
    }

    //Determine the height difference
    //We'll return this as right height minus left height
    //(keep in minf, some people write it the opposite way)
    //But for us, postive means we're right heavy
    //negative means we're left heavy
    private int getHeightDifference(Node<T> nCurrent) 
    {
        int iHeightDiff = 0;
        //variable for height of left subtree
        int heightLeft = -1;
        //right subtree
        int heightRight = -1;

        if (nCurrent != null)
        {

            //check if the right child is null
            if (nCurrent.Right != null)
            {
                //if not add to the height of the right
                heightRight = recHeight(nCurrent.Right);
            }
            //check if the left child is null
            if (nCurrent.Left != null)
            {
                //if not add to the height of the left
                heightLeft = recHeight(nCurrent.Left);
            }
            iHeightDiff = heightRight - heightLeft;
        }
        
        //Return the height difference of our right subtree and left subtree
        return iHeightDiff;
    }

    //Know the algorithm made coding this super easy
    /// <summary>
    /// Perform a single left rotation
    /// Givene an original root node
    /// We will make the root of the tree point to the right child of the orignal
    /// We will make the old roots' right child equal to the new root's left child
    /// The new root's left child will be set to the old root
    /// </summary>
    /// <param name="nOldRoot"></param>
    /// <returns></returns>
    public static Node<T> SingleLeft(Node<T> nOldRoot)
    {
        // Make the root of the tree point to the right child of the original
        Node<T> nNewRoot = nOldRoot.Right;
        //Make the old roots right child equal to the new roots left child
        nOldRoot.Right = nNewRoot.Left;
        //The new roots left child will be set to the old root
        nNewRoot.Left = nOldRoot;
        

        return nNewRoot;
    }

    /// <summary>
    /// Performs a single right rotation, like the single left but the 
    /// oppisite direction
    /// </summary>
    /// <param name="nOldRoot"></param>
    /// <returns></returns>
    public static Node<T> SingleRight(Node<T> nOldRoot)
    {
        //Make the root of the tree point to the left child of the original
        Node<T> nNewRoot = nOldRoot.Left;
        //Make the old roots left child eqal ot the new roots right child
        nOldRoot.Left = nNewRoot.Right;
        //The new roots right child will be set to the old root. 
        nNewRoot.Right = nOldRoot;

        return nNewRoot;
    }

    /// <summary>
    /// Performs a double left rotation
    /// First it performs a single right rotation on the right child of the original root
    /// Then it performs a single Left rotation on the original root
    /// </summary>
    /// <param name="nOldRoot"></param>
    /// <returns></returns>
    public static Node<T> DoubleLeft(Node<T> nOldRoot)
    {
        nOldRoot.Right = SingleRight(nOldRoot.Right);
        return SingleLeft(nOldRoot);
        
    }

    public static Node<T> DoubleRight(Node<T> nOldRoot)
    {
        nOldRoot.Left = SingleLeft(nOldRoot.Left);
        return SingleRight(nOldRoot);
    }
}
