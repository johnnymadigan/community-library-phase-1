//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;


class MemberCollection : IMemberCollection
{
    // Fields
    private int capacity;
    private int count;
    private Member[] members; //make sure members are sorted in dictionary order

    // Properties

    // get the capacity of this member colllection 
    // pre-condition: nil
    // post-condition: return the capacity of this member collection and this member collection remains unchanged
    public int Capacity { get { return capacity; } }

    // get the number of members in this member colllection 
    // pre-condition: nil
    // post-condition: return the number of members in this member collection and this member collection remains unchanged
    public int Number { get { return count; } }




    // Constructor - to create an object of member collection 
    // Pre-condition: capacity > 0
    // Post-condition: an object of this member collection class is created

    public MemberCollection(int capacity)
    {
        if (capacity > 0)
        {
            this.capacity = capacity;
            members = new Member[capacity];
            count = 0;
        }
    }

    // check if this member collection is full
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is full; otherwise return false.
    public bool IsFull()
    {
        return count == capacity;
    }

    // check if this member collection is empty
    // Pre-condition: nil
    // Post-condition: return ture if this member collection is empty; otherwise return false.
    public bool IsEmpty()
    {
        return count == 0;
    }

    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        // ▄▀█ █▀▄ █▀▄
        // █▀█ █▄▀ █▄▀ @author: Johnny Madigan

        if (!IsFull() && !Search(member)) // Call binary search to see if member exists to prevent adding duplicate
        {
            int pos = count - 1;

            while (pos >= 0) // If adding the first member (pos will be -1) skip loop to insert immediately
            {
                // If new member comes before current, shift current member up to make a gap then loop
                // Otherwise new member comes after current so break out of loop to insert into gap
                if (member.CompareTo(members[pos]) == -1) // BASIC OP (most impactful in worst-case)
                {
                    members[pos + 1] = members[pos];
                    pos--;
                }
                else break;
            }
            members[pos + 1] = (Member)member;
            count++;
            Console.WriteLine($"✔ ADDED ({member.FirstName} {member.LastName})");
        }
        else if (IsFull()) Console.WriteLine($"✘ NOT ADDED ({member.FirstName} {member.LastName}) COLLECTION FULL");
        else Console.WriteLine($"✘ NOT ADDED ({member.FirstName} {member.LastName}) DUPLICATE");
    }

    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given member was in the member collection
    public void Delete(IMember aMember)
    {
        // █▀▄ █▀▀ █░░ █▀▀ ▀█▀ █▀▀
        // █▄▀ ██▄ █▄▄ ██▄ ░█░ ██▄ @author: Johnny Madigan

        // Don't bother calling SEARCH as inefficient doing a binary search twice...
        // (SEARCH returns a bool so will need to do another binary search for the position anyway)
        if (!IsEmpty())
        {
            int min = 0;
            int max = count - 1;

            // Logarithmic time O(log N) for worst-case binary search
            while (min <= max)                                                  
            {
                int mid = (max + min) / 2; // No "floor" needed as terms are integers (C# auto truncates decimals)

                // Found? shift members down by 1 from queried member to delete, otherwise adjust search window (lower or greater half)
                if (aMember.CompareTo(members[mid]) == 0)
                {
                    for (int i = mid; i < count - 1; i++)
                        members[i] = members[i + 1]; // BASIC OP (most impactful in worst-case)
                    members[count - 1] = null; // Set the dangling member obj to null
                    count--;
                    Console.WriteLine($"✔ DELETED ({aMember.FirstName} {aMember.LastName})");
                    return;
                }
                else if (aMember.CompareTo(members[mid]) == -1) max = mid - 1;
                else min = mid + 1;
            }                                                                   
        }
        Console.WriteLine($"✘ DOES NOT EXIST ({aMember.FirstName} {aMember.LastName})");
    }

    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        // █▀ █▀▀ ▄▀█ █▀█ █▀▀ █░█
        // ▄█ ██▄ █▀█ █▀▄ █▄▄ █▀█ @author: Johnny Madigan

        if (!IsEmpty())
        {
            int min = 0;
            int max = count - 1;

            // Logarithmic time O(log N) for worst-case binary search
            while (min <= max) 
            {
                int mid = (max + min) / 2; // No "floor" needed as terms are integers (C# auto truncates decimals)

                // BASIC OPS BELOW
                // Found? return true, otherwise adjust search window (lower or greater half)
                if (member.CompareTo(members[mid]) == 0) return true;
                else if (member.CompareTo(members[mid]) == -1) max = mid - 1;
                else min = mid + 1;
            }
        }
        return false;
    }

    // Remove all the members in this member collection
    // Pre-condition: nil
    // Post-condition: no member in this member collection 
    public void Clear()
    {
        for (int i = 0; i < count; i++)
        {
            this.members[i] = null;
        }
        count = 0;
    }

    // Return a string containing the information about all the members in this member collection.
    // The information includes last name, first name and contact number in this order
    // Pre-condition: nil
    // Post-condition: a string containing the information about all the members in this member collection is returned
    public string ToString()
    {
        string s = "";
        for (int i = 0; i < count; i++)
            s = s + members[i].ToString() + "\n";
        return s;
    }


}

