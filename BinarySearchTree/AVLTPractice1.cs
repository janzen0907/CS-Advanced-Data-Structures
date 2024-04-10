using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinarySearchTree;

public class AVLTPractice1<T> : BSTPractice1<T> where T :IComparable<T>
{
    internal override Node<T> Balance(Node<T> current)
    {
        Node<T> nNewRoot = current;
        if(current != null)
        {
            //Get the balance factor
            int iHeightDiff = getHeightDifference(current);

            if(iHeightDiff > 1)
            {
                int iRightHeightDiff = getHeightDifference(nNewRoot.Right);
                if (iRightHeightDiff < 0)
                {
                    nNewRoot = DoubleLeft(current);
                }
                else
                {
                    nNewRoot = SingleLeft(current);
                }
            }
           else if(iHeightDiff < -1 ) 
            {
                int iLeftHeightDiff = getHeightDifference(nNewRoot.Left);
                if (iLeftHeightDiff > 0)
                {
                    nNewRoot = DoubleRight(current);
                }
                else
                {
                    nNewRoot = SingleRight(current);
                }
            }
            //Then the tree is left heavy

        }
            return nNewRoot;
        
    }

    private int getHeightDifference(Node<T> current) 
    {
        int iHeightDiff = 0;
        int lHeight = -1;
        int rHeight = -1;

        if(current != null)
        {
            //Check if the right child is null
            if(current.Right != null)
            {
                rHeight = recHeight(current.Right);
            }
            //Then check the left and do the same
            if(current.Left != null) 
            {
                lHeight = recHeight(current.Left);
            }
            iHeightDiff = rHeight - lHeight;
        }
        return iHeightDiff;
    }
    public static Node<T> SingleLeft(Node<T> nOldRoot)
    {
        //Root points right
        Node<T> nNewRoot = nOldRoot.Right;
        //Make the old roots right child equal to the new roots left child
        nOldRoot.Right = nNewRoot.Left;
        //The new roots left child will be set to the old root
        nNewRoot.Left = nOldRoot;
        return nNewRoot;
    }
    public static Node<T> SingleRight(Node<T> nOldRoot)
    {
        //Make the root of the tree point to the left child of the original
        Node<T> newRoot = nOldRoot.Left;
        //Make the old roots left child eqal ot the new roots right child
        nOldRoot.Left = newRoot.Right;
        //The new roots right child will be set to the old root. 
        newRoot.Right = nOldRoot;
        return newRoot;
    }
    public static Node<T> DoubleLeft(Node<T> nOldRoot)
    {
        //Perform single right rotation, right child of the original node
        nOldRoot.Right = SingleRight(nOldRoot.Right);
        //Then perform single left on original root
        return SingleLeft(nOldRoot);
    }
    public static Node<T> DoubleRight(Node<T> nOldRoot)
    {
        nOldRoot.Left = SingleLeft(nOldRoot.Left);
        return SingleRight(nOldRoot);
    }

}
