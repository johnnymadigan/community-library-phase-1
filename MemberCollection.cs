//CAB301 assessment 1 - 2022
//The implementation of MemberCollection ADT
using System;
using System.Linq;

namespace MemberApp
{
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
        // @author: Johnny Madigan
        // Pre-condition: this member collection is not full
        // Post-condition: a new member is added to the member collection and the members are sorted in ascending order by their full names;
        // No duplicate will be added into this the member collection
        public void Add(IMember member)
        {
            // To be implemented by students in Phase 1

            // if first member
            if (!IsFull())
            {
                int pos = count - 1; // position of the last member in collection

                // SORT
                // Linear time O(n) sorting
                while (pos >= 0)
                {
                    // Check if new member's last name comes before, after, or identical to current member's
                    int compResult = Compare(member.LastName, members[pos].LastName);

                    // If last names are identical, check first names
                    if (compResult == 0) compResult = Compare(member.FirstName, members[pos].FirstName);

                    // If new member should come before second, shift down, otherwise break out of loop to insert
                    if (compResult == -1)
                    {
                        members[pos + 1] = members[pos];
                        pos--;
                    } 
                    else
                    {
                        break;
                    }
                }

                // Insert member in the empty slot, keep track of count
                members[pos + 1] = (Member)member;
                count++;
                Console.WriteLine($"Successfully added '{member.FirstName} {member.LastName}'");
            }
            else
            {
                // Since this is a public method, let the user know if member was not added
                Console.WriteLine($"Failed to add '{member.FirstName} {member.LastName}' as collection is full");
            }
        }

        // CUSTOM PRIVATE HELPER: Compare two strings lexicographically
        // @author: Johnny Madigan
        // Pre-condition: nil
        // Post-condition: return 0 if equal, -1 if first string comes before the second, 1 if it comes after
        private int Compare(string name1, string name2)
        {
            // BY CHAR
            // Linear time O(n) comparison for each char's integer value
            int i = 0;

            while (i < name1.Length && i < name2.Length)
            {
                if (name1[i] < name2[i])
                {
                    return -1;
                } 
                else if (name1[i] > name2[i])
                {
                    return 1;
                } 
                else
                {
                    // Continue as current chars are equal
                    i++;
                }
            }

            // BY LENGTH
            // Constant time O(1) checking...
            // If all chars are equal at the same positions, compare by lengths...
            // Smaller length comes before, 1 for after, 0 for equal
            if (name1.Length < name2.Length)
            {
                return -1;
            }
            else if (name1.Length > name2.Length)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // Remove a given member out of this member collection
        // @author: Johnny Madigan
        // Pre-condition: nil
        // Post-condition: the given member has been removed from this member collection, if the given meber was in the member collection
        public void Delete(IMember aMember)
        {
            // To be implemented by students in Phase 1

            int pos = Position(aMember);

            if (pos >= 0)
            {
                // SORT
                // Shift all members down from position of deleted member 
                for (int i = pos; i < count - 1; i++)
                {
                    members[i] = members[i + 1];
                }

                // Keep track of count
                count--;
                Console.WriteLine($"Successfully deleted '{aMember.FirstName} {aMember.LastName}'");
            }
            else
            {
                // Since this is a public method, let the user know if member was not deleted
                Console.WriteLine($"Failed to delete '{aMember.FirstName} {aMember.LastName}' as not in collection");
            }
                
        }

        // Search a given member in this member collection 
        // @author: Johnny Madigan
        // Pre-condition: nil
        // Post-condition: return true if this memeber is in the member collection; return false otherwise; member collection remains unchanged
        public bool Search(IMember member)
        {
            // To be implemented by students in Phase 1

            return Position(member) >= 0;
        }

        // CUSTOM PRIVATE HELPER: Find the index of a member in this collection using BINARY search
        // @author: Johnny Madigan
        // Pre-condition: nil
        // Post-condition: return the index if member is in collection, otherwise -1
        private int Position(IMember member)
        {
            // Logarithmic time O(log N) for worst-case binary search
            int min = 0;
            int max = members.Length - 1;

            // If not in collection, we will reach a point where...
            // mid and Max will converge and the member should be in the greater half...
            // causing mid to be greater than max, stopping the loop as the collection has been checked
            while (min <= max)
            {
                // No need for floor method...
                // as C# division will always result in an int if all numbers are int
                int mid = (max + min) / 2;

                if (members[mid].FirstName == member.FirstName && members[mid].LastName == member.LastName)
                {
                    return mid;
                }
                else
                {
                    // Check if new member's last name comes before, after, or identical to current midpoint
                    int compResult = Compare(member.LastName, members[mid].LastName);

                    // If last names are identical, check first names
                    if (compResult == 0) compResult = Compare(member.FirstName, members[mid].FirstName);

                    // If queried member comes before midpoint, set to search lower half, otherwise greater half
                    if (compResult == -1)
                    {
                        max = mid - 1;
                    }
                    else
                    {
                        min = mid + 1;
                    }
                }

            }

            return -1; // if not found
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

}