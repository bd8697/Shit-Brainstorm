using System.Collections.Generic;

namespace BrainStorm
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }
        public int Reports { get; set; }
        public int Score { get; set; }

        public Branch(string name, int id)
        {
            Name = name;
            Upvotes = 0;
            Downvotes = 0;
            Reports = 0;
            Score = 1;
            // Score = Utility.RandomInt(1, 100);
            Id = id;
        }

        public Branch(int id, string name, int score, int upvotes, int downvotes, int reports)
        {
            Name = name;
            Id = id;
            Upvotes = upvotes;
            Downvotes = downvotes;
            Reports = reports;
            Score = score;
        }

        public Branch()
        {

        }

        public void Upvote(string termName)
        {
            Upvotes++;
            Score++;
            TermRepo.Instance.UpdateBranch(termName, this, new List<string> { "upvotes", "score" });
        }
        public void Downvote(string termName)
        {
            Downvotes++;
            Score--;
            if(Score == 0)
            {
                Score++;
            }
            TermRepo.Instance.UpdateBranch(termName, this, new List<string> { "downvotes", "score"});
        }
        public bool Report(string termName)
        {
            bool removed = false;
            List<string> toUpdate = new List<string>();
            Reports++;
            if (Reports > Score && Reports > Utility.ReportsThreshold)
            {
                Score = -1;
                Utility.CurrentTerm.Branches.Remove(this);
                toUpdate.Add("score");
                removed = true;
            }
            toUpdate.Add("reports");
            TermRepo.Instance.UpdateBranch(termName, this, toUpdate);
            return removed;
        }
    }
}
