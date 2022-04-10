using System;

namespace MemberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IMemberCollection col = new MemberCollection(5);

            IMember m1 = new Member("i go last", "z");
            IMember m2 = new Member("nutz", "deez");
            IMember m3 = new Member("i go first", "a");
            IMember m4 = new Member("dem", "bofa");
            IMember m5 = new Member("im in the middle", "middle");

            m1.IsValidPhoneNumber(m1);

            col.Delete(m1); // empty

            col.Add(m1);
            col.Add(m2);
            col.Add(m3);
            col.Add(m3);    // dupe
            col.Add(m4);
            col.Add(m5);
            col.Add(m5);    // full (before recognising dupe)

            Console.WriteLine(col.Search(m5)); // true

            col.Delete(m1);
            col.Delete(m2);

            Console.WriteLine(col.Search(m2)); // false

            col.Delete(m3);
            col.Delete(m4);
            col.Delete(m5);
            col.Delete(m5); // does not exist

            Console.WriteLine(col.Search(m5)); // false (quick cus empty)

            col.Add(m1);
            col.Add(m2);
            Console.WriteLine(col.Search(m3)); // false (not empty but added yet)
            col.Add(m3);
            col.Add(m4);
            col.Add(m5);

            Console.WriteLine("________________________________\n" +
                col.ToString() + "________________________________\n");
        }
    }
}

