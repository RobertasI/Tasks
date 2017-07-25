using System;

namespace BinaryTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binarytree = new BinaryTree();
            var root = binarytree.rootNode = new Node(2);
            binarytree.rootNode.left = new Node(7);
            binarytree.rootNode.left.left = new Node(2);
            binarytree.rootNode.left.right = new Node(6);
            binarytree.rootNode.left.right.left = new Node(5);
            binarytree.rootNode.left.right.right = new Node(11);

            binarytree.rootNode.right = new Node(5);
            binarytree.rootNode.right.right = new Node(9);
            binarytree.rootNode.right.right.left = new Node(4);


            int sum = binarytree.findSumOfBinaryTree(root);

            Console.WriteLine("sum of tree: " + sum);

        }
    }
}
