using System;
using System.IO;
using System.Linq;

namespace StackImplementation
{
    class Program
    {
        public class Queue<T> 
        {
            private class Node 
            {
                public Node(Node next, T value) 
                {
                    Next = next;
                    Value = value;
                }

                public Node Next { get; internal set; }
                public T Value { get; set;}
            }

            private Node m_Head;
            private Node m_Tail;

            public void Enqueue(T item) 
            {
                Node node = new Node(null, item);

                if (m_Head == null) 
                {
                    m_Head = node;
                    m_Tail = node;
                }
                else 
                {
                    m_Tail.Next = node;
                    m_Tail = node;
                }
            }

            public bool TryPeek(out T item) 
            {
                if (m_Head == null) 
                {
                    item = default(T);
                    return false;
                }

                item = m_Head.Value;

                return true;
            }

            public T Peek() 
            {
                if (m_Head == null)
                    throw new InvalidOperationException("Queue is empty.");

                return m_Head.Value;
            }

            public bool TryDequeue(out T item) 
            {
                if (m_Head == null) 
                {
                    item = default(T);
                    return false;
                }

                item = m_Head.Value;
                m_Head = m_Head.Next;

                return true;
            }

            public T Dequeue() 
            {
                if (m_Head == null)
                    throw new InvalidOperationException("Queue is empty.");

                T item = m_Head.Value;
                m_Head = m_Head.Next;

                return item;
            }

            public void Print()
            {
                if (m_Head == null) return;

                Console.WriteLine (m_Head.Value.ToString());
            }

            public void PrintStack()
            {
                if (m_Head == null) return;
                Node item = m_Head;

                while (item != null)
                {
                    Console.WriteLine(item.Value.ToString());
                    item = item.Next;
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
                string[] strings = new string[str.Length - 2];

                try
                {
                    int q = Convert.ToInt32(str[0]);
                    for (int i = 0; i < str.Length - 2; i++) strings[i] = str[i + 1];

                    Queue<int> myStack = new Queue<int>();

                    for (int i = 0; i < strings.Length; i++)
                    {
                        switch (Convert.ToInt32(strings[i].Substring(0, 1)))
                        {
                            case 1:
                                myStack.Enqueue(Convert.ToInt32(strings[i].Substring(2)));
                                break;
                            case 2:
                                myStack.Dequeue();
                                break;
                            case 3:
                                myStack.Print();
                                break;
                            case 4:
                                myStack.PrintStack();
                                break;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Nu ati introdus numere!");
                }
            }
        }
    }
}