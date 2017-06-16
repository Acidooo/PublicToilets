using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Plugin.DownloadManager.Abstractions;
using PublicPoo.Services.interfaces;

namespace PublicPoo.Droid.Services
{
    public class AndroidDownloadService : IDownloadService
    {
        public IDownloadFile File;

        private IDownloadManager DownloadManager;
        public AndroidDownloadService(IDownloadManager downloadManager)
        {
            DownloadManager = downloadManager;
        }

        public void AbortDownloading()
        {
            DownloadManager.Abort(File);
        }

        public bool IsDownloading()
        {
            if (File == null) return false;

            switch (File.Status)
            {
                case DownloadFileStatus.INITIALIZED:
                case DownloadFileStatus.PAUSED:
                case DownloadFileStatus.PENDING:
                case DownloadFileStatus.RUNNING:
                    return true;

                case DownloadFileStatus.COMPLETED:
                case DownloadFileStatus.CANCELED:
                case DownloadFileStatus.FAILED:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public void Download()
        {
            if (IsDownloading())
            {
                AbortDownloading();
                return;
            }

            File = DownloadManager.CreateDownloadFile("http://ipv4.download.thinkbroadband.com/10MB.zip");
            File.PropertyChanged += OnPropertyChanged;

            DownloadManager.Start(File, false);
            Debug.WriteLine($"Download Queue: {DownloadManager.Queue.ToList().Count}");
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                Debug.Assert(e?.PropertyName != null, "e?.PropertyName != null");
                //System.Diagnostics.Debug.WriteLine("[Property changed] " + e.PropertyName + " -> " +
                //                                   sender.GetType()
                //                                       .GetProperty(e.PropertyName)
                //                                       .GetValue(sender, null)
                //                                       .ToString());

                // Update UI text-fields
                //var downloadFile = ((IDownloadFile)sender);
                switch (e.PropertyName)
                {
                    case nameof(IDownloadFile.Status):
                        break;
                    case nameof(IDownloadFile.StatusDetails):
                        break;
                    case nameof(IDownloadFile.TotalBytesExpected):
                        break;
                    case nameof(IDownloadFile.TotalBytesWritten):
                        break;
                }

                // Update UI if download-status changed.
                if (e.PropertyName == "Status")
                {
                    switch (((IDownloadFile)sender).Status)
                    {
                        case DownloadFileStatus.COMPLETED:
                        case DownloadFileStatus.FAILED:
                        case DownloadFileStatus.CANCELED:
                            File.PropertyChanged -= OnPropertyChanged;
                            // Get the path this file was saved to. When you didn't set a custom path, this will be some temporary directory.
                            //var nativeDownloadManager = (DownloadManager)ApplicationContext.GetSystemService(DownloadService);
                            //System.Diagnostics.Debug.WriteLine(nativeDownloadManager.GetUriForDownloadedFile(((DownloadFileImplementation)sender).Id));

                            // If you don't want your download to be listed in the native "Download" app after the download is finished
                            //nativeDownloadManager.Remove(((DownloadFileImplementation)sender).Id);
                            break;
                    }
                }

                // Update UI while donwloading.
                if (e.PropertyName == "TotalBytesWritten" || e.PropertyName == "TotalBytesExpected")
                {
                    var bytesExpected = ((IDownloadFile)sender).TotalBytesExpected;
                    var bytesWritten = ((IDownloadFile)sender).TotalBytesWritten;
                    Debug.WriteLine($"TotalBytesExpected: {bytesExpected}");
                    Debug.WriteLine($"TotalBytesWritten: {bytesWritten}");

                    if (bytesExpected > 0)
                    {
                        var percentage = Math.Round(bytesWritten / bytesExpected);
                        Debug.WriteLine($"Progress in percentage: {percentage * 100}%");

                        if (percentage >= 1)
                        {
                            File.PropertyChanged -= OnPropertyChanged;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void CancelDownload()
        {
            //if (downloader.IsDownloading())
            //{
            //    downloader.AbortDownloading();
            //}
        }
    }
}