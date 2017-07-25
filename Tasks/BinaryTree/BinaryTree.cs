
namespace BinaryTree
{
    public class BinaryTree
    {
        public Node rootNode;

        public int findSumOfBinaryTree(Node node)
        {           
            if (node == null)
            {
                return 0;
            }

            return node.value + findSumOfBinaryTree(node.left) + findSumOfBinaryTree(node.right); 
        }
    }
}
