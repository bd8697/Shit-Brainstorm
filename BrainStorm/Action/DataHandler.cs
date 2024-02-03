using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BrainStorm.Action
{
    public static class DataHandler
    {
        public static Chilkat.Csv termsCsv;
        public static Chilkat.Csv branchesCsv;
        private static string path = "Terms/";
        private static string termsPath = "Terms.csv";

        public static void Init()
        {
            termsCsv = new Chilkat.Csv();
            branchesCsv = new Chilkat.Csv();

            termsCsv.LoadFile(termsPath);
            termsCsv.HasColumnNames = true;
            termsCsv.EscapeBackslash = false;

            branchesCsv.HasColumnNames = true;
        }

        public static void PrintCSV(Chilkat.Csv csv)
        {
            string csvDoc;
            csvDoc = csv.SaveToString();
            Console.WriteLine(csvDoc);
        }

        public static void CreateTerm(Term term)
        {
            string newLine = term.Id + "," + term.Name + "," + term.Visits + "\n";
            File.AppendAllText(termsPath, newLine);
            termsCsv.LoadFile(termsPath);

            CreateNewTermCSV(term.Name);
        }

        private static void CreateNewTermCSV(string newTermName) {
            string branchesPath = path + newTermName + ".csv";
            new Chilkat.Csv().SaveFile(branchesPath);
            File.AppendAllText(branchesPath, Utility.GetString("branchHeader") + "\n");
        }

        public static void CreateBranch(Branch branch, string termName)
        {
            string branchesPath = path + termName + ".csv";
            string newLine = branch.Id + "," + branch.Name + "," + branch.Score + "," + branch.Upvotes + "," + branch.Downvotes + "," + branch.Reports + "\n";
            File.AppendAllText(branchesPath, newLine);
            branchesCsv.LoadFile(branchesPath);
        }

        public static string ReadTerm(int row, int col)
        {
            return termsCsv.GetCell(row, col);
        }

        public static string ReadTermByName(int row, string col)
        {
            return termsCsv.GetCellByName(row, col);
        }

        public static string ReadBranch(string termName, int row, int col)
        {
            string branchesPath = path + termName + ".csv";
            branchesCsv.LoadFile(branchesPath);
            return branchesCsv.GetCell(row, col);
        }

        public static string ReadBranchByName(string termName, int row, string col)
        {
            string branchesPath = path + termName + ".csv";
            branchesCsv.LoadFile(branchesPath);
            return branchesCsv.GetCellByName(row, col);
        }

        public static void UpdateTerm(int row, int col, string replaceWith)
        {
            termsCsv.LoadFile(termsPath);
            termsCsv.SetCell(row, col, replaceWith);
            SaveTerms();
        }

        public static void UpdateTermByName(int row, string col, string replaceWith)
        {
            termsCsv.LoadFile(termsPath);
            termsCsv.SetCellByName(row, col, replaceWith);
            SaveTerms();
        }

        public static void UpdateBranch(string termName, int row, int col, string replaceWith)
        {
            string branchesPath = path + termName + ".csv";
            branchesCsv.LoadFile(branchesPath);
            branchesCsv.SetCell(row, col, replaceWith);
            SaveBranches(branchesCsv, branchesPath);
        }

        public static void UpdateBranchByName(string termName, int row, Dictionary<string, string> keyValue)
        {
            string branchesPath = path + termName + ".csv";
            branchesCsv.LoadFile(branchesPath);
            foreach(KeyValuePair<string, string> entry in keyValue)
            {
                branchesCsv.SetCellByName(row, entry.Key, entry.Value);
            }
            SaveBranches(branchesCsv, branchesPath);
        }

        public static void DeleteTermByRow(int idx)
        {
            termsCsv.DeleteRow(idx);
            SaveTerms();
        }

        public static void DeleteBranchesByRow(string termName, int idx)
        {
            string branchesPath = path + termName + ".csv";
            branchesCsv.LoadFile(branchesPath);
            branchesCsv.DeleteRow(idx);
            SaveBranches(branchesCsv, branchesPath);
        }

        public static int SearchTerm(string termName)
        {
            var terms = File.ReadLines(termsPath);
            foreach (var line in terms)
            {
                if (line.Split(',')[1].Equals(termName))
                {
                    try
                    {
                        return int.Parse(line.Split(',')[0]);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return -1;
        }

        public static int SearchBranch(string termName, string branchName)
        {
            string branchesPath = path + termName + ".csv";
            var branches = File.ReadLines(branchesPath);
            foreach (var line in branches)
            {
                if (line.Split(',')[1].Equals(branchName))
                {
                    try
                    {
                        return int.Parse(line.Split(',')[0]);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
            }
            return -1;
        }

        public static List<Term> GetAllTermsAsList()
        {
            List<Term> termList = new List<Term>();
            var terms = File.ReadLines(termsPath).Skip(1);
            foreach (var line in terms)
            {
                Term newTerm = new Term();
                newTerm.Id = int.Parse(line.Split(',')[0]);
                newTerm.Name = line.Split(',')[1];
                newTerm.Visits = int.Parse(line.Split(',')[2]);
                newTerm.Branches = GetBrachesInTerm(newTerm.Name);
                
                termList.Add(newTerm);
            }
            return termList;
        }

        public static List<Branch> GetBrachesInTerm(string termName)
        {
            List<Branch> branchesInTerm = new List<Branch>();

            string branchesPath = path + termName + ".csv";
            var branches = File.ReadLines(branchesPath).Skip(1);

            foreach (var line in branches)
            {
                Branch newBranch = new Branch();
                newBranch.Id = int.Parse(line.Split(',')[0]);
                newBranch.Name = line.Split(',')[1];
                newBranch.Score = int.Parse(line.Split(',')[2]);
                newBranch.Upvotes = int.Parse(line.Split(',')[3]);
                newBranch.Downvotes = int.Parse(line.Split(',')[4]);
                newBranch.Reports = int.Parse(line.Split(',')[5]);

                branchesInTerm.Add(newBranch);
            }
            return branchesInTerm;
        }

        private static bool SaveTerms()
        {
            bool success = termsCsv.SaveFile(termsPath);
            if (success != true)
            {
                Console.WriteLine(termsCsv.LastErrorText);
            }
            return success;
        }

        private static bool SaveBranches(Chilkat.Csv csv, string savePath)
        {
            bool success = csv.SaveFile(savePath);
            if (success != true)
            {
                Console.WriteLine(termsCsv.LastErrorText);
            }
            return success;
        }

        public static int GetIdForNewTerm()
        {
            return File.ReadLines(termsPath).Count() - 1;
        }
    }
}
