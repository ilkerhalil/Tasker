using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();
            var person2 = new Person();

            //person.Firstname = "saadasdas";
            var properties = person.GetType().GetProperties();
            foreach (var pi in properties)
            {
                var backingFieldName = string.Format("<{0}>k__BackingField", pi.Name);
                var backingField =  pi.DeclaringType.GetField(backingFieldName,BindingFlags.Instance | BindingFlags.NonPublic );
                backingField?.SetValue(person, "aaaa");
            }
        }
    }


    public class Person:PersonBase
    {
        public Person()
        {
      //      Age = new List<string>();

        }

        //public string Firstname { get; set; }
 
//        public List<string> Age { get; } 
    }

    public class PersonBase
    {
        public IDictionary<string, object> Lastname { get; }
    }

}
