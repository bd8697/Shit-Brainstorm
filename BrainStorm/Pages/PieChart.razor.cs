using ChartJs.Blazor.Common;
using ChartJs.Blazor.PieChart;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using ChartJs.Blazor.Common.Handlers;
using ChartJs.Blazor.Interop;
using Newtonsoft.Json.Linq;
using MatBlazor;
using BlazorAnimate;
using Microsoft.AspNetCore.Components.Web;
using System.Threading.Tasks;

namespace BrainStorm.Pages
{
    public partial class PieChart
    {
        private MatTheme greenTheme;
        private PieConfig _config;
        protected ElementReference globalKeyHandler;
        public string TermName { get; set; }
        public string SearchTextClass { get; set; }
        public string BranchToAdd { get; set; }     

        private List<Branch> branches = new List<Branch>();
        private List<Animate> fadeUpAnims = new List<Animate>();
        private List<Animate> fadeDownAnims = new List<Animate>();
        private List<Animate> fadeDownTriggers = new List<Animate>();
        private List<Animate> fadeUpTriggers = new List<Animate>();
        private List<string> branchColors = new List<string>();
        private List<string> btnAnchors = new List<string>();
        private List<bool> hideButtons = new List<bool>();

        private int branchesPerChartSlider;
        public int BranchesPerChartSlider
        {
            get => branchesPerChartSlider;
            set
            {
                branchesPerChartSlider = value;
                Utility.BranchesPerChart = value;
                Utility.NewColorSeed();
                UriHelper.NavigateTo("pieChart", true);
            }
        }

        private void ResetClassOnBlur()
        {
            SearchTextClass += " "; // todo: why do we need this?
        }

        private void OnUpvote(int idx)
        {
            if (btnAnchors[idx].Equals(Anchor.BottomBottom)) 
            {
                branches[idx].Upvote(TermName);
                OnVote(idx);
            }
        }

        private void OnDownvote(int idx)
        {
            if (btnAnchors[idx].Equals(Anchor.BottomBottom))
            {
                branches[idx].Downvote(TermName);
                OnVote(idx);
            }
        }

        private void OnReport(int idx)
        {
            if (btnAnchors[idx].Equals(Anchor.BottomBottom))
            {
                if(branches[idx].Report(TermName) == true)
                {
                    Utility.NewColorSeed();
                    UriHelper.NavigateTo("pieChart", true);
                }
                OnVote(idx);
            }
        }

        async Task OnVote(int idx)
        {
            btnAnchors[idx] = Anchor.CenterTop; // no more animations
            fadeDownTriggers[idx].Run();
            await Task.Delay(250);
            hideButtons[idx] = true;
            StateHasChanged();
        }

        private void TriggerFade(int idx)
        {
            if (btnAnchors[idx].Equals(Anchor.TopTop))
            {
                btnAnchors[idx] = Anchor.BottomBottom; // show
                fadeUpTriggers[idx].Run();
            }
        }

        private void Show(MatToastType type)
        {
            string title;
            string message;

            if (type == MatToastType.Success)
            {
                title = Utility.GetString("newBranchSuccessTitle");
                message = Utility.GetString("newBranchSuccessMessage");
            }
            else
            {
                title = Utility.GetString("newBranchErrorTitle");
                message = Utility.GetString("newBranchErrorMessage");
            }

            Toaster.Add(message, type, title, null, null);
        }

        public void AddBranchToTerm()
        {
            if (BranchToAdd != null)
            {
                BranchToAdd = Utility.FirstWordOf(BranchToAdd.ToLower());
                if (BranchToAdd != TermName && TermRepo.Instance.GetBranch(TermName, BranchToAdd) == null)
                {
                    Branch newBranch = new Branch(BranchToAdd, TermRepo.Instance.GetTerm(TermName).Branches.Count);
                    TermRepo.Instance.AddBranch(TermName, newBranch);
                    Utility.CurrentTerm.Branches.Add(newBranch);
                    Show(MatToastType.Success);
                    if(branches.Count < Utility.BranchesPerChart)
                    {
                        Utility.NewColorSeed();
                        UriHelper.NavigateTo("pieChart", true);
                    }
                    return;
                }
            }
            Show(MatToastType.Danger);
        }

        private void GoToTop()
        {
            Utility.CurrentBranchIdx = 0;
            Utility.NewColorSeed();
            UriHelper.NavigateTo("pieChart", true);
        }

        private void KeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "ArrowLeft")
            {
                Utility.CurrentBranchIdx -= Math.Min(Utility.CurrentBranchIdx, Utility.BranchesPerChart);
            }
            else if (e.Code == "ArrowRight")
            {
                Utility.CurrentBranchIdx += Utility.BranchesPerChart;
            }
            else
            {
                return;
            }

            Utility.NewColorSeed();
            UriHelper.NavigateTo("pieChart", true);
        }

        private void SearchKeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter")
            {
                AddBranchToTerm();
            }
        }

        public void OnClickHandler(JObject mouseEvent, JArray activeElements)
        {
            foreach (JObject elem in activeElements)
            {
                foreach (JProperty prop in elem.GetValue("_model"))
                {
                    if(prop.Name.Equals("label"))
                    {
                        string clickedOn = prop.Value.ToString();
                        if(clickedOn.Equals(TermName))
                        {
                            Utility.CurrentBranchIdx += Utility.BranchesPerChart;
                        }
                        else
                        {
                            Utility.TermForChart(clickedOn);
                        }
                        Utility.NewColorSeed();
                        UriHelper.NavigateTo("pieChart", true);
                    }
                }
            }
        }

        private void SearchNewTerm()
        {
            UriHelper.NavigateTo("", true);
        }

        protected override void OnInitialized() //note: when render-mode="ServerPrerendered" (in _Host"), this is run twice. This means random values are generated twice, so be careful with those.
        {
            TermName = Utility.CurrentTerm.Name;
            branchesPerChartSlider = Utility.BranchesPerChart;
            SearchTextClass = Utility.GetString("searchTextClass");

            greenTheme = new MatTheme()
            {
                Primary = Utility.GetString("myRed"),
                Secondary = Utility.GetString("myGreen")
            };

            _config = new PieConfig
            {
                Options = new PieOptions
                {
                    Responsive = true,
                    OnClick = new DelegateHandler<ChartMouseEvent>(OnClickHandler)
                }
            };

            for (int i = 0; i < Utility.BranchesPerChart; i++)
            {
                int idx = i + Utility.CurrentBranchIdx;
                if(idx < Utility.CurrentTerm.Branches.Count)
                {
                    branches.Add(Utility.CurrentTerm.Branches[idx]);
                } else
                {
                    if(i == 0 && Utility.CurrentTerm.Branches.Count > 0)
                    {
                        Utility.CurrentBranchIdx = 0;
                        i = -1;
                    } else
                    {
                        break;
                    }
                }
            }

            List<int> branchScores = new List<int>();
            branchScores.Add(0);
            branchColors = Utility.GenerateColors(branches.Count, Utility.ColorSeed);

            _config.Data.Labels.Add(TermName); // labels are shared between datasets, and we want the term.name label to be displayed in only one of them, so we make a fake branch and set it's value to 0, so it will be afected by changes to the label, but it will be invisible to the user

            foreach (Branch branch in branches)
            {
                _config.Data.Labels.Add(branch.Name);
                branchScores.Add(branch.Score);
                fadeUpAnims.Add(new Animate());
                fadeDownAnims.Add(new Animate());
                fadeDownTriggers.Add(new Animate());
                fadeUpTriggers.Add(new Animate());
                btnAnchors.Add(null);
                hideButtons.Add(false);
            }
            
            PieDataset<int> scoresData = new PieDataset<int>(branchScores)
            {
                BackgroundColor = new IndexableOption<string>(branchColors.ToArray())
            };

            PieDataset<int> termData = new PieDataset<int>(new[] { Utility.CurrentTerm.Visits })
            {
                BackgroundColor = new[] {Utility.GetString("myBrown")}
            };

            _config.Data.Datasets.Add(scoresData);
            _config.Data.Datasets.Add(termData);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if(firstRender)
            {
                await JSRuntime.InvokeVoidAsync("SetFocusToElement", globalKeyHandler);
                await Task.Delay(1); // *I think* chart is rendered at least 1 frame after canvas and buttons
                for (int idx = 0; idx < branches.Count; idx++)
                {
                    btnAnchors[idx] = Anchor.TopTop;
                    fadeUpAnims[idx].Run();
                    fadeDownAnims[idx].Run();
                }
                StateHasChanged();
            }
        }
    }
}
