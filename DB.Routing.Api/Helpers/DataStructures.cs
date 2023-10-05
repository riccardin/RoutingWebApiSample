using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{

    public class Node
    {
        public int Value { get; set; }
        public Node Next { get; set; }
        
    }

    public class LinkedListNode<T>
    {

        public LinkedListNode(T value) {

            Value = value;
        }

        public T Value { get; set; }

        private LinkedListNode<T> Next { get; set; }
        public LinkedListNode<T> Head { get; set; }
        public LinkedListNode<T> Tail { get; set; }

       

        private int Count = 0;

        public  void AddFirst(LinkedListNode<T> node)
        {

            //Save off the Head
            LinkedListNode<T> temp = Head;

            //Point head to the new node
            Head = node;

            //Insert the rest of the list behind the head
            Head.Next = temp;

            Count++;
            if (Count == 1) {
                //If the list was empty then the head and the tail should be both point to the new node
                Tail = Head;
            }

        }

        public void AddLast(LinkedListNode<T> node) {

            if (Count == 0)
            {

                Head = node;
            }
            else
            {
                Tail.Next = node;

            }

            Tail = node;

            Count++;


        }






    }

    public static class DataStructures
    {

            public static string createNodeList()
            {

                Node node1 = new Node { Value = 3 };
                Node node2 = new Node { Value = 7 };

                node1.Next = node2;

                Node node3 = new Node { Value = 10 };

                node2.Next = node3;


                return PrintList(node1);
            }

            public static string PrintList(Node list)
            {

                string listValues = string.Empty; ;
                while (list != null)
                {

                    listValues = listValues + list.Value + ",";

                    list = list.Next;
                }

                return listValues;


            }


        //function which returns the linkedList providade by .Net
        public static LinkedList<int> linedListFramwework() {

            LinkedList<int> linkedList = new LinkedList<int>();

            linkedList.AddFirst(2);

            return linkedList;
        }

        

    }


    public class GenericClass<T>
    {

        public T value  { get; set; }

        public GenericClass(T Value)
        {

            value = Value;


        }



    }



    public abstract class MyAbstractClass {

        private string _property;
        public void myFunction() { }



    }


    public class MyNonAbstractClass:MyAbstractClass {

        public void myNonAbstractFunction() {

            myFunction();

        }
    }






}
