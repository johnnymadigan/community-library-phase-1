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

    // ▄▀█ █▀▄ █▀▄
    // █▀█ █▄▀ █▄▀ @author: Johnny Madigan
    // Add a new member to this member collection
    // Pre-condition: this member collection is not full
    // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
    // No duplicate will be added into this the member collection
    public void Add(IMember member)
    {
        if (!IsFull() && !Search(member))                   // Binary search to see if member exists to prevent adding duplicate
        {
            int pos = count - 1;

            while (pos >= 0)                                // If adding the first member (pos will be -1) skip loop to insert immediately
            {
                if (member.CompareTo(members[pos]) == -1)   // BASIC OP (most frequent/impactful in worst-case)
                {
                    members[pos + 1] = members[pos];        // New member comes before current, shift members up and loop
                    pos--;
                }
                else break;                                 // New member comes after current, break out of loop to insert into gap
            }

            members[pos + 1] = (Member)member;
            count++;
            Console.WriteLine($"✔ ADDED ({member.FirstName} {member.LastName})");
        }
        else if (IsFull()) Console.WriteLine($"✘ NOT ADDED ({member.FirstName} {member.LastName}) COLLECTION FULL");
        else Console.WriteLine($"✘ NOT ADDED ({member.FirstName} {member.LastName}) DUPLICATE");
    }


    // █▀▄ █▀▀ █░░ █▀▀ ▀█▀ █▀▀
    // █▄▀ ██▄ █▄▄ ██▄ ░█░ ██▄ @author: Johnny Madigan
    // Remove a given member out of this member collection
    // Pre-condition: nil
    // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
    public void Delete(IMember aMember)
    {
        // Don't bother calling SEARCH as inefficient doing a binary search twice...
        // (SEARCH returns a bool so will need to do another binary search for the position anyway)
        if (!IsEmpty())
        {
            int min = 0;
            int max = count - 1;

            while (min <= max)                          // Logarithmic time O(log N) for worst-case binary search
            {
                int mid = (max + min) / 2;              // No need for "floor" as terms are integers so C# auto truncates decimals
                int order = aMember.CompareTo(members[mid]);

                if (order == 0)                         // If found, shift members from queried member down by 1 to delete
                {
                    for (int i = mid; i < count - 1; i++)
                        members[i] = members[i + 1];    // BASIC OP (most frequent/impactful in worst-case)
                    members[count - 1] = null;          // Set the dangling member obj to null
                    count--;
                    Console.WriteLine($"✔ DELETED ({aMember.FirstName} {aMember.LastName})");
                    return;
                }
                else if (order == -1) max = mid - 1;    // If not found, adjust window if member is in lower half
                else min = mid + 1;                     // If not found, adjust window if member is in greater half
            }
            // Reach here if member to delete was never found
            Console.WriteLine($"✘ NOT DELETED ({aMember.FirstName} {aMember.LastName}) DOES NOT EXIST");
        }
        else Console.WriteLine($"✘ NOT DELETED ({aMember.FirstName} {aMember.LastName}) COLLECTION EMPTY");
    }


    // █▀ █▀▀ ▄▀█ █▀█ █▀▀ █░█
    // ▄█ ██▄ █▀█ █▀▄ █▄▄ █▀█ @author: Johnny Madigan
    // Search a given member in this member collection 
    // Pre-condition: nil
    // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
    public bool Search(IMember member)
    {
        if (!IsEmpty())
        {
            int min = 0;
            int max = count - 1;

            while (min <= max)                              // Logarithmic time O(log N) for worst-case binary search
            {
                int mid = (max + min) / 2;                  // No need for "floor" as terms are integers so C# auto truncates decimals
                int order = member.CompareTo(members[mid]);

                // BASIC OPS BELOW
                if (order == 0) return true;                // If found, return true
                else if (order == -1) max = mid - 1;        // If not found, adjust window if member is in lower half
                else min = mid + 1;                         // If not found, adjust window if member is in greater half
            }
        }
        return false;                                       // Reach here if collection was empty OR member was never found
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

