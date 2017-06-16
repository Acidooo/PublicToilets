using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.DownloadManager.Abstractions;

namespace PublicPoo.Services.interfaces
{
    public interface IDownloadService
    {
        void Download();
        void CancelDownload();
    }
}
