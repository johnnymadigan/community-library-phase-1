using System;

namespace MemberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("test");
            MemberCollection col = new MemberCollection(10);


            Member m1 = new Member("i go first", "a");
            //Member m2 = new Member("i go last", "z");
            //Member m3 = new Member("im in the middle", "madigan");
            //Member m4 = new Member("im in the middle", "madigan");
            //Member m5 = new Member("im in the middle i come after the others", "madigan");
            //Member m6 = new Member("i go 2nd", "b");
            //Member m7 = new Member("i go 3rd", "c");
            //Member m8 = new Member("i go 4th", "d");
            //Member m9 = new Member("he", "horndog");
            //Member m10 = new Member("he", "dude");
            //Member m11 = new Member("not", "added");


            col.Add(m1);
            Console.WriteLine(col.ToString());
            Console.WriteLine("end");

            //col.Add(m2);
            //col.Add(m3);
            //col.Add(m4);
            //col.Add(m5);
            //col.Add(m6);
            //col.Add(m7);
            //col.Add(m8);
            //col.Add(m9);
            //col.Add(m10);
            //col.Add(m11);


            //Console.WriteLine(col.ToString());

            //Console.WriteLine(col.Search(m1));
            //col.Delete(m1);

            //Console.WriteLine(col.Search(m1));
            //Console.WriteLine(col.ToString());

        }
    }
}
