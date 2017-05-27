using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PublicToilet.Common;

namespace PublicToilet.Services.interfaces
{
    public interface IFeedService
    {
        Task<IEnumerable<Toilet>> RetrieveToilets();
    }
}
