using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace BinaryTree
{
    class Program
    {
        class Node
        {
            public int key;
            public Node left, right;

            public Node(int item)
            {
                key = item;
                left = right = null;
            }
        }

        class BinaryTree
        {
            Node root;

            BinaryTree() { root = null; }

            void printPostorder(Node node)
            {
                if (node == null)
                    return;

                printPostorder(node.left);
                printPostorder(node.right);

                Console.Write(node.key + " ");
            }

            void printInorder(Node node)
            {
                if (node == null)
                    return;

                printInorder(node.left);
                Console.Write(node.key + " ");
                printInorder(node.right);
            }

            void printPreorder(Node node)
            {
                if (node == null)
                    return;

                Console.Write(node.key + " ");
                printPreorder(node.left);
                printPreorder(node.right);
            }

            void printPostorder() { printPostorder(root); }
            void printInorder() { printInorder(root); }
            void printPreorder() { printPreorder(root); }

            static public void Main(String[] args)
            {
                BinaryTree tree = new BinaryTree();
                tree.root = new Node(1);
                tree.root.right = new Node(2);
                tree.root.right.right = new Node(5);
                tree.root.right.right.left = new Node(3);
                tree.root.right.right.right = new Node(6);
                tree.root.right.right.left.right = new Node(4);
                Console.WriteLine("Preorder traversal "
                                  + "of binary tree is ");
                tree.printPreorder();

                Console.WriteLine("\nInorder traversal "
                                  + "of binary tree is ");
                tree.printInorder();

                Console.WriteLine("\nPostorder traversal "
                                  + "of binary tree is ");
                tree.printPostorder();
            }
        }
    }
}
