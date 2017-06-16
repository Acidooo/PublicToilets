using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;
using PublicPoo.Common.NavParams;
using PublicToilet.Common;
using PublicToilet.Services.interfaces;
using Plugin.DownloadManager.Abstractions;
using PublicPoo.Services.interfaces;

namespace PublicPoo.ViewModels
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
        private readonly IDownloadService DownloadService;
        private readonly IDownloadManager DownloadManager;

        private ICommand downloadCommand;
        public ICommand DownloadCommand => downloadCommand ?? (downloadCommand = new MvxCommand(DownloadTask));

        private ICommand cancelCommand;
        public ICommand CancelCommand => cancelCommand ?? (cancelCommand = new MvxCommand(CancelTask));

        public ToiletsViewModel(IFeedService feedService, IDownloadService downloadService, IDownloadManager downloadManager)
        {
            FeedService = feedService;
            DownloadService = downloadService;
            DownloadManager = downloadManager;
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

        private void DownloadTask()
        {
            DownloadService.Download();
        }

        private void CancelTask()
        {
            DownloadService.CancelDownload();
        }

        public void NavigateToToiletDetailPage(Toilet selectedToilet)
        {
            if (selectedToilet == null) return;

            ShowViewModel<ToiletDetailViewModel>(new ToiletDetailNavParams() { SerialisedToilet = JsonConvert.SerializeObject(selectedToilet) });
        }
    }
}
