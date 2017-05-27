using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using PublicToilet.Common;
using PublicToilet.Services.interfaces;

namespace PublicToilet.ViewModels
{
    public class ToiletsViewModel : MvxViewModel
    {
        private ObservableCollection<Toilet> toilets;

        public ObservableCollection<Toilet> Toilets
        {
            get { return toilets; }
            set { SetProperty(ref toilets, value); }
        }

        private readonly IFeedService FeedService;

        public ToiletsViewModel(IFeedService feedService)
        {
            FeedService = feedService;
        }

        public async Task RetrieveToilets()
        {
            var results = await FeedService.RetrieveToilets();
            if (results != null)
            {
                Toilets = new ObservableCollection<Toilet>(results);
                Debug.WriteLine($"toilets {Toilets.Count}");
            }
        }

        public override async void Start()
        {
            base.Start();
            await RetrieveToilets();
        }
    }
}
