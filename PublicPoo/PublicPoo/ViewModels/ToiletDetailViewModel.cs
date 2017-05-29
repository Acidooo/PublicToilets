using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmCross.Core.ViewModels;
using Newtonsoft.Json;
using PublicPoo.Common.NavParams;
using PublicToilet.Common;

namespace PublicPoo.ViewModels
{
    public class ToiletDetailViewModel : MvxViewModel
    {
        private Toilet selectedToilet;
        public Toilet SelectedToilet
        {
            get { return selectedToilet;}
            set
            {
                SetProperty(ref selectedToilet, value);
            }
        }

        public string Address => SelectedToilet != null ? $"{SelectedToilet.Address1}, {SelectedToilet.Town} {SelectedToilet.State} {SelectedToilet.Postcode}" : "";

        public void Init(ToiletDetailNavParams navParam)
        {
            var serialisedToilet = navParam?.SerialisedToilet;
            if (!string.IsNullOrEmpty(serialisedToilet))
            {
                SelectedToilet = JsonConvert.DeserializeObject<Toilet>(serialisedToilet);
            }
        }
    }
}
