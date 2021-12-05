using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MergeSortedLinkedLists
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
            using (var sr = new StreamReader(Console.OpenStandardInput()))
            using (Stream stdout = Console.OpenStandardOutput())
            {
                int bufSize = 1024;
                Stream inStream = Console.OpenStandardInput(bufSize);
                Console.SetIn(new StreamReader(inStream, Console.InputEncoding, false, bufSize));

                var lines = sr.ReadToEnd().Split('\n');
                string[] str = lines.ToArray();
                try
                {
                    int t = Convert.ToInt32(str[0]);
                    int[] n = new int[t + 1];
                    n[0] = Convert.ToInt32(str[1]);
                    int cursor = 1;
                    SingleLinkedList mergedLinkedLists = new SingleLinkedList();
                    SingleLinkedList[] singlyList = new SingleLinkedList[2];

                    for (int z = 0; z < t + 1; z++)
                    {
                        if (z > 0)
                        {
                            cursor += n[z - 1] + 1;
                            n[z] += Convert.ToInt32(str[cursor]);
                        }

                        int[] ints = new int[n[z]];

                        for (int i = 2; i <= n[z] + 1; i++)
                        {
                            ints[i - 2] = Convert.ToInt32(str[i + cursor - 1]);
                        }

                        singlyList[z] = new SingleLinkedList();
                        singlyList[z].head = new Node(ints[0]);
                        for (int j = 1; j < ints.Length; j++) singlyList[z].InsertLast(ints[j]);
                    }

                    mergedLinkedLists = new SingleLinkedList();
                    mergedLinkedLists.head = singlyList[0].head;
              
                    Node current = singlyList[0].GetLastNode();

                    Node current1 = singlyList[1].head;

                    while (current1 != null)
                    {
                        mergedLinkedLists.InsertLast(current1.data);
                        current1 = current1.next;
                    }

                    mergedLinkedLists.Print();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
