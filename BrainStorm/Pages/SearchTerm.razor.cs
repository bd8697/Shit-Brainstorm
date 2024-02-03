using BrainStorm.Action;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainStorm.Pages
{
    public partial class SearchTerm
    {
        MatTheme greenTheme;
        public string SearchTextClass { get; set; }
        public string SearchedTerm { get; set; }
        private bool OpenGuide { get; set; }

        protected override void OnInitialized()
        {
            SearchTextClass = Utility.GetString("searchTextClass");

            greenTheme = new MatTheme()
            {
                Primary = Utility.GetString("myRed"),
                Secondary = Utility.GetString("myGreen")
            };
        }
        private void ResetClassOnBlur()
        {
            SearchTextClass += " ";
        }

        private void CloseGuide()
        {
            OpenGuide = false;
        }

        private void ShowGuide()
        {
            OpenGuide = true;
        }

        public void KeyUp(KeyboardEventArgs e)
        {
            if (e.Code == "Enter")
            {
                GoToChart();
            }
        }

        public void GoToChart()
        {
            if (SearchedTerm == null)
            {
                ToastError(MatToastType.Danger);
            } 
            else
            {
                Utility.TermForChart(Utility.FirstWordOf(SearchedTerm.ToLower()));
                Utility.NewColorSeed();
                UriHelper.NavigateTo("pieChart", true);
            }
        }

        private void ToastError(MatToastType type)
        {
            Toaster.Add(Utility.GetString("searchToast"), type, null, null, null);
        }
    }
}
