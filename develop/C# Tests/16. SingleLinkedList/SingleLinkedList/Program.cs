using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingleLinkedList
{
    class Program
    {
        internal class Node
        {
            internal int data;
            internal Node next;
            public Node(int d)
            {
                data = d;
                next = null;
            }
        }

        internal class SingleLinkedList
        {
            internal Node head;

            internal void InsertFront(int new_data)
            {
                Node new_node = new Node(new_data);
                new_node.next = this.head;
                this.head = new_node;
            }

            internal void InsertLast(int new_data)
            {
                Node new_node = new Node(new_data);
                if (this.head == null)
                {
                    this.head = new_node;
                    return;
                }
                Node lastNode = GetLastNode();
                lastNode.next = new_node;
            }

            internal Node GetLastNode()
            {
                Node temp = this.head;
                while (temp.next != null)
                {
                    temp = temp.next;
                }
                return temp;
            }

            internal void InsertAfter(Node prev_node, int new_data)
            {
                if (prev_node == null)
                {
                    Console.WriteLine("The given previous node Cannot be null");
                    return;
                }
                Node new_node = new Node(new_data);
                new_node.next = prev_node.next;
                prev_node.next = new_node;
            }  

            internal void DeleteNodebyKey(int key)
            {
                Node temp = this.head;
                Node prev = null;
                if (temp != null && temp.data == key)
                {
                    this.head = temp.next;
                    return;
                }
                while (temp != null && temp.data != key)
                {
                    prev = temp;
                    temp = temp.next;
                }
                if (temp == null)
                {
                    return;
                }
                prev.next = temp.next;
            }

            public void ReverseLinkedList()
            {
                Node prev = null;
                Node current = this.head;
                Node temp = null;
                while (current != null)
                {
                    temp = current.next;
                    current.next = prev;
                    prev = current;
                    current = temp;
                }
                this.head = prev;
            }

            public void Print()
            {
                if (this.head == null) return;
                else
                {
                    Node current = this.head;
                    
                    while (current != null)
                    {
                        Console.WriteLine(current.data);
                        current = current.next;
                    }
                }
            }

            public void PrintLast()
            {
                if (this.head == null) return;
                else
                {
                    Node current = this.GetLastNode();

                    Console.WriteLine(current.data);
                }
            }
        }
        
        static void Main(string[] args)
        {
            SingleLinkedList singlyList = new SingleLinkedList();
            singlyList.head = new Node(1);
            singlyList.InsertLast(2);
            singlyList.InsertLast(3);
            singlyList.Print();
        }
    }
}
