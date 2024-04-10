using DataStructuresCommon;
using System;


namespace BinarySearchTree;

public abstract class A_BST<T> : A_Collection<T>, I_BST<T>
    where T : IComparable<T>
{
    public abstract T Find(T data);
    public abstract int Height();
    public abstract void Iterate(ProcessData<T> pd, TRAVERSALORDER order);

    //All BSTs are going to need a reference to the root node of the tree
    protected Node<T> nRoot;

    //A counter to keep track of the number of data items in the tree
    protected int iCount = 0;

    public override int Count { get => iCount; }

}
