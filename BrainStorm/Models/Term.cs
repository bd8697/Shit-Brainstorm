using System;
using System.Collections.Generic;
using System.Linq;

namespace BrainStorm
{
    public class Term
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Branch> Branches { get; set; }
        public int Visits { get; set; }

        public Term(string name, int id)
        {
            Name = name;
            Branches = new List<Branch>();
            Visits = 1; // chart doesn't work with 0 here
            Id = id;
        }

        public Term()
        {
            Branches = new List<Branch>();
        }

        public Term(Term toCopy)
        {
            Name = toCopy.Name;
            Branches = new List<Branch>(toCopy.Branches);
            Visits = toCopy.Visits;
            Id = toCopy.Id;
        }

        public void AddBranch(String branchName)
        {
            Branches.Add(new Branch(branchName, Branches.Count));
        }
        public void AddBranch(Branch branch)
        {
            Branches.Add(branch);
        }

        public void Visited()
        {
            Visits++;
        }

        public void RemoveBannedBranches()
        {
            foreach (Branch branch in Branches.ToList())
            {
                if(branch.Score < 0)
                {
                    Branches.Remove(branch);
                }
            }
        }
    }
}
