using System;

namespace MemberApp
{
    class Program
    {
        public static IMemberCollection col = new MemberCollection(4);

        public static IMember m1 = new Member("im last", "z");
        public static IMember m2 = new Member("paul", "dano");
        public static IMember m3 = new Member("im first", "a");
        public static IMember m4 = new Member("bofa", "dem");

        static void Main(string[] args)
        {
            // TESTS
            Console.WriteLine($"✔ = Phone tests are {TestPhone()}");
            Console.WriteLine($"✔ = Pin tests are {TestPin()}");
            TestCollection();

            // PRINT COLLECTION
            Console.WriteLine("\n━━┅━━━┅━━┅━━╾╮\n" +
                col.ToString() + "━━┅━━━┅━━┅━━╾╯\n");
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

        static void TestCollection()
        {
            string s;

            s = col.Search(m4) ? $"✔ {col.Search(m4)}" : $"✘ {col.Search(m4)}";
            Console.WriteLine($"✘ = {s}");

            Console.Write("✘ = "); col.Delete(m1);
            Console.Write("✔ = "); col.Add(m1);
            Console.Write("✘ = "); col.Delete(m2);

            s = col.Search(m2) ? $"✔ {col.Search(m2)}" : $"✘ {col.Search(m2)}";
            Console.WriteLine($"✘ = {s}");

            Console.Write("✔ = "); col.Add(m2);
            Console.Write("✔ = "); col.Add(m3);
            Console.Write("✘ = "); col.Add(m3);
            Console.Write("✔ = "); col.Add(m4);
            Console.Write("✘ = "); col.Add(m4);

            s = col.Search(m4) ? $"✔ {col.Search(m4)}" : $"✘ {col.Search(m4)}";
            Console.WriteLine($"✔ = {s}");

            Console.Write("✔ = "); col.Delete(m2);

            s = col.Search(m2) ? $"✔ {col.Search(m2)}" : $"✘ {col.Search(m2)}";
            Console.WriteLine($"✘ = {s}");
            Console.Write("✘ = "); col.Delete(m2);
            Console.Write("✔ = "); col.Add(m2);
        }
    }
}

