using BrainStorm.Action;
using System.Collections.Generic;
using System.Linq;

namespace BrainStorm
{
    public sealed class TermRepo
    {
        private List<Term> Terms { get; set; }
        private static readonly object myLock = new object();
        private static TermRepo instance = null;
        public static TermRepo Instance
        {
            get
            {
                lock(myLock)
                {
                    if(instance == null)
                    {
                        instance = new TermRepo();
                    }
                    return instance;
                }
            }
        }

        public TermRepo()
        {
            Terms = DataHandler.GetAllTermsAsList();
        }

        public List<Term> GetTerms()
        {
            return Terms;
        }

        public void AddTerm(Term term)
        {
            Terms.Add(term);
            DataHandler.CreateTerm(term);
        }

        public void AddBranch(string termName, Branch branch)
        {
            GetTerm(termName).AddBranch(branch);
            DataHandler.CreateBranch(branch, termName);
        }

        public void DeleteTerm(string termName)
        {
            Term term = GetTerm(termName);
            Terms.Remove(term);
            DataHandler.DeleteTermByRow(term.Id);
        }

        public void DeleteBranch(string termName, string branchName)
        {
            Branch branch = GetBranch(termName, branchName);
            GetTerm(termName).Branches.Remove(branch);
            DataHandler.DeleteBranchesByRow(termName, branch.Id);
        }

        public void UpdateVisitsInTerm(string termName)
        {
            // terms.Where(term => term.Name == termName).ToList().ForEach(term => term.VisitCount++);
            Term term = GetTerm(termName);
            term.Visits++;
            DataHandler.UpdateTermByName(term.Id, "visits", term.Visits.ToString());
        }

        public void UpdateBranch(string termName, Branch branch, List<string> updateFields)
        {
            Term term = GetTerm(termName);
            term.Branches.Where(b => b.Name == branch.Name).ToList().ForEach(b => b = branch);

            Dictionary<string, string> keyValue = new Dictionary<string, string>();
            foreach(string field in updateFields)
            {
                switch (field)
                {
                    case "score": { keyValue.Add("score", branch.Score.ToString()); break; }
                    case "upvotes": { keyValue.Add("upvotes", branch.Upvotes.ToString()); break; }
                    case "downvotes": { keyValue.Add("downvotes", branch.Downvotes.ToString()); break; }
                    case "reports": { keyValue.Add("reports", branch.Reports.ToString()); break; }
                }
            }
            DataHandler.UpdateBranchByName(termName, branch.Id, keyValue);
        }

        public Term GetTerm(string termName)
        {
            return Terms.FirstOrDefault(term => term.Name == termName);
        }

        public Branch GetBranch(string termName, string branchName) {

            return GetTerm(termName).Branches.FirstOrDefault(branch => branch.Name == branchName);
        }

        public int GetIdForNewTerm()
        {
            return Terms.Count;
        }
    }
}
