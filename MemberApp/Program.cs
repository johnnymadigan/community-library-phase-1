using System;

namespace MemberApp
{
    class Program
    {
        static void Main(string[] args)
        {

            MemberCollection col = new MemberCollection(10);

            Member m1 = new Member("i go first", "a");
            Member m2 = new Member("i go last", "z");
            Member m3 = new Member("im in the middle", "madigan");
            

            col.Add(m1);
            col.Add(m2);
            col.Add(m3);
            Console.WriteLine(col.ToString());

            col.Add(m3);
            Console.WriteLine(col.ToString());
        }
    }
}

