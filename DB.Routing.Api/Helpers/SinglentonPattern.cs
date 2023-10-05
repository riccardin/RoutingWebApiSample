using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DB.Routing.Api.Helpers
{
    //Sealed makes the class not able to derived from
    public sealed class SinglentonPattern
    {
        //To make it threat safe. Statici is not enough
        private static readonly Lazy<SinglentonPattern> _instance = new Lazy<SinglentonPattern>(() => new SinglentonPattern());

        public static SinglentonPattern Current
        {
            get { return _instance.Value; }
        }
        private SinglentonPattern() { }

    }





    public sealed class MyClass
    {

        private MyClass() { }

        private static readonly Lazy<MyClass> _instance = new Lazy<MyClass>(() => new MyClass());

        public static MyClass Instance
        {

            get { return _instance.Value; }


        }


    }


   


    

}






