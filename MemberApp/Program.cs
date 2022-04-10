using System;

namespace MemberApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Phone tests passed? {TestPhone()}");
            Console.WriteLine($"Pin tests passed? {TestPin()}");

            IMemberCollection col = new MemberCollection(4);

            IMember m1 = new Member("i go last", "zzzz");
            IMember m2 = new Member("paul", "dano");
            IMember m3 = new Member("i go first", "aaaa");
            IMember m4 = new Member("bofa", "dem");

            Console.WriteLine(col.Search(m4)); // false (quick cus empty)
            col.Delete(m1); // empty
            col.Add(m1);
            col.Delete(m2); // does not exist
            Console.WriteLine(col.Search(m2)); // false (not empty but added yet)
            col.Add(m2);
            col.Add(m3);
            col.Add(m3);    // dupe
            col.Add(m4);
            col.Add(m4);    // full (before recognising dupe)
            Console.WriteLine(col.Search(m4)); // true
            col.Delete(m2);
            Console.WriteLine(col.Search(m2)); // false
            col.Delete(m2); // does not exist
            col.Add(m2);

            Console.WriteLine("________________________________\n" +
                col.ToString() + "________________________________\n");
        }

        static bool TestPhone()
        {
            if (!IMember.IsValidContactNumber("0469404666")) return false;
            if (!IMember.IsValidContactNumber("0444444444")) return false;

            if (IMember.IsValidContactNumber("0444444444 ")) return false;  // trailing space
            if (IMember.IsValidContactNumber(" 0444444444")) return false;  // leading space
            if (IMember.IsValidContactNumber("1444444444")) return false;   // first digit not 0
            if (IMember.IsValidContactNumber("04444x4444")) return false;   // not all digits
            if (IMember.IsValidContactNumber("xxxxxxxxxx")) return false;   // no digits
            if (IMember.IsValidContactNumber("")) return false;             // empty
            if (IMember.IsValidContactNumber("          ")) return false;   // blank

            return true;
        }

        static bool TestPin()
        {
            if (!IMember.IsValidPin("123456")) return false;
            if (!IMember.IsValidPin("12345")) return false;
            if (!IMember.IsValidPin("1234")) return false;

            if (IMember.IsValidPin("1234 ")) return false;      // trailing space
            if (IMember.IsValidPin(" 1234")) return false;      // leading space
            if (IMember.IsValidPin("1234567")) return false;    // too long
            if (IMember.IsValidPin("123")) return false;        // too short
            if (IMember.IsValidPin("12xx56")) return false;     // not all digits
            if (IMember.IsValidPin("xxxxxx")) return false;     // no digits
            if (IMember.IsValidPin("")) return false;           // empty
            if (IMember.IsValidPin("    ")) return false;       // blank lower bound
            if (IMember.IsValidPin("      ")) return false;     // blank higher bound

            return true;
        }
    }
}

