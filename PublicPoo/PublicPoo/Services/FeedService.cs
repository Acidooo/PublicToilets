using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BuildIt;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PublicToilet.Common;
using PublicToilet.Services.interfaces;

namespace PublicToilet.Services
{
    public class FeedService : IFeedService
    {
        public async Task<IEnumerable<Toilet>> RetrieveToilets()
        {
            var toilets = new List<Toilet>();
            try
            {
                //using (var client = new HttpClient())
                //{
                //    var response =
                //        await
                //            client.GetAsync(
                //                "http://data.gov.au/datastore/odata3.0/54566d76-a809-4959-8622-61dc30b3114d?$top=5&$format=json");
                //}
                // TODO: Replace the logic below to get the actual json from the url
                var json = "[{\"AccessibleUnisex\": \"False\", \"KeyRequired\": \"False\", \"DrinkingWater\": \"False\", \"Unisex\": \"False\", \"MLAK\": \"False\", \"State\": \"Western Australia\", \"AccessLimited\": \"False\", \"AccessibleParkingNote\": \"\", \"Latitude\": \" - 31.92183600\", \"Status\": \"Verified\", \"PaymentRequired\": \"False\", \"FacilityType\": \"Park or reserve\", \"Showers\": \"True\", \"RHTransfer\": \"False\", \"Address1\": \"West Road\", \"ParkingNote\": \"\", \"AccessibleNote\": \"\", \"DumpPoint\": \"False\", \"OpeningHoursNote\": \"\", \"Ambulant\": \"False\", \"OpeningHoursSchedule\": \"\", \"Town\": \"Bassendean\", \"SanitaryDisposal\": \"False\", \"Name\": \"Sandy Beach Reserve\", \"AccessibleFemale\": \"True\", \"URL\": \"https://toiletmap.gov.au/toilet/1\", \"Notes\": \"\", \"ParkingAccessible\": \"False\", \"Longitude\": \"115.95020622\", \"AccessNote\": \"\", \"AdultChange\": \"False\", \"BabyChange\": \"False\", \"LHTransfer\": \"False\", \"SharpsDisposal\": \"True\", \"Male\": \"True\", \"IconAltText\": \"Male and Female, or Unisex (Accessible)\", \"IsOpen\": \"AllHours\", \"AccessibleMale\": \"True\", \"AddressNote\": \"\", \"IconURL\": \"https://toiletmap.gov.au/images/icons/mfa.png\", \"Postcode\": \"6054\", \"Female\": \"True\", \"Parking\": \"False\", \"ToiletID\": \"1\", \"_id\": 1, \"ToiletType\": \"\"}, {\"AccessibleUnisex\": \"False\", \"KeyRequired\": \"False\", \"DrinkingWater\": \"False\", \"Unisex\": \"False\", \"MLAK\": \"False\", \"State\": \"Western Australia\", \"AccessLimited\": \"False\", \"AccessibleParkingNote\": \"\", \"Latitude\": \"-31.90441010\", \"Status\": \"Verified\", \"PaymentRequired\": \"False\", \"FacilityType\": \"Park or reserve\", \"Showers\": \"False\", \"RHTransfer\": \"False\", \"Address1\": \"North Road\", \"ParkingNote\": \"\", \"AccessibleNote\": \"\", \"DumpPoint\": \"False\", \"OpeningHoursNote\": \"\", \"Ambulant\": \"False\", \"OpeningHoursSchedule\": \"\", \"Town\": \"Bassendean\", \"SanitaryDisposal\": \"False\", \"Name\": \"Point Reserve\", \"AccessibleFemale\": \"True\", \"URL\": \"https://toiletmap.gov.au/toilet/2\", \"Notes\": \"\", \"ParkingAccessible\": \"False\", \"Longitude\": \"115.96099110\", \"AccessNote\": \"\", \"AdultChange\": \"False\", \"BabyChange\": \"False\", \"LHTransfer\": \"False\", \"SharpsDisposal\": \"False\", \"Male\": \"True\", \"IconAltText\": \"Male and Female, or Unisex (Accessible)\", \"IsOpen\": \"DaylightHours\", \"AccessibleMale\": \"True\", \"AddressNote\": \"\", \"IconURL\": \"https://toiletmap.gov.au/images/icons/mfa.png\", \"Postcode\": \"6054\", \"Female\": \"True\", \"Parking\": \"False\", \"ToiletID\": \"2\", \"_id\": 2, \"ToiletType\": \"\"}, {\"AccessibleUnisex\": \"False\", \"KeyRequired\": \"False\", \"DrinkingWater\": \"True\", \"Unisex\": \"False\", \"MLAK\": \"False\", \"State\": \"Western Australia\", \"AccessLimited\": \"False\", \"AccessibleParkingNote\": \"\", \"Latitude\": \"-31.89628865\", \"Status\": \"Verified\", \"PaymentRequired\": \"False\", \"FacilityType\": \"Park or reserve\", \"Showers\": \"True\", \"RHTransfer\": \"False\", \"Address1\": \"Off Seventh Avenue\", \"ParkingNote\": \"\", \"AccessibleNote\": \"\", \"DumpPoint\": \"False\", \"OpeningHoursNote\": \"\", \"Ambulant\": \"False\", \"OpeningHoursSchedule\": \"\", \"Town\": \"Bassendean\", \"SanitaryDisposal\": \"False\", \"Name\": \"Success Hill Reserve\", \"AccessibleFemale\": \"True\", \"URL\": \"https://toiletmap.gov.au/toilet/3\", \"Notes\": \"\", \"ParkingAccessible\": \"True\", \"Longitude\": \"115.95578101\", \"AccessNote\": \"\", \"AdultChange\": \"False\", \"BabyChange\": \"True\", \"LHTransfer\": \"False\", \"SharpsDisposal\": \"True\", \"Male\": \"True\", \"IconAltText\": \"Male and Female, or Unisex (Accessible)\", \"IsOpen\": \"DaylightHours\", \"AccessibleMale\": \"True\", \"AddressNote\": \"\", \"IconURL\": \"https://toiletmap.gov.au/images/icons/mfa.png\", \"Postcode\": \"6054\", \"Female\": \"True\", \"Parking\": \"True\", \"ToiletID\": \"3\", \"_id\": 3, \"ToiletType\": \"\"}, {\"AccessibleUnisex\": \"False\", \"KeyRequired\": \"False\", \"DrinkingWater\": \"False\", \"Unisex\": \"False\", \"MLAK\": \"False\", \"State\": \"Western Australia\", \"AccessLimited\": \"False\", \"AccessibleParkingNote\": \"\", \"Latitude\": \"-31.89147400\", \"Status\": \"Verified\", \"PaymentRequired\": \"False\", \"FacilityType\": \"Park or reserve\", \"Showers\": \"False\", \"RHTransfer\": \"False\", \"Address1\": \"Robinson Road\", \"ParkingNote\": \"\", \"AccessibleNote\": \"\", \"DumpPoint\": \"False\", \"OpeningHoursNote\": \"\", \"Ambulant\": \"False\", \"OpeningHoursSchedule\": \"\", \"Town\": \"Eden Hill\", \"SanitaryDisposal\": \"False\", \"Name\": \"Jubilee Reserve\", \"AccessibleFemale\": \"False\", \"URL\": \"https://toiletmap.gov.au/toilet/4\", \"Notes\": \"\", \"ParkingAccessible\": \"False\", \"Longitude\": \"115.94016370\", \"AccessNote\": \"\", \"AdultChange\": \"False\", \"BabyChange\": \"False\", \"LHTransfer\": \"False\", \"SharpsDisposal\": \"True\", \"Male\": \"True\", \"IconAltText\": \"Male and Female, or Unisex\", \"IsOpen\": \"DaylightHours\", \"AccessibleMale\": \"False\", \"AddressNote\": \"\", \"IconURL\": \"https://toiletmap.gov.au/images/icons/mf.png\", \"Postcode\": \"6054\", \"Female\": \"True\", \"Parking\": \"False\", \"ToiletID\": \"4\", \"_id\": 4, \"ToiletType\": \"Sewerage\"}, {\"AccessibleUnisex\": \"False\", \"KeyRequired\": \"False\", \"DrinkingWater\": \"False\", \"Unisex\": \"False\", \"MLAK\": \"False\", \"State\": \"Western Australia\", \"AccessLimited\": \"False\", \"AccessibleParkingNote\": \"\", \"Latitude\": \"-31.91343349\", \"Status\": \"Verified\", \"PaymentRequired\": \"False\", \"FacilityType\": \"Park or reserve\", \"Showers\": \"False\", \"RHTransfer\": \"False\", \"Address1\": \"Guildford Road\", \"ParkingNote\": \"\", \"AccessibleNote\": \"\", \"DumpPoint\": \"False\", \"OpeningHoursNote\": \"\", \"Ambulant\": \"False\", \"OpeningHoursSchedule\": \"\", \"Town\": \"Ashfield\", \"SanitaryDisposal\": \"False\", \"Name\": \"Ashfield Reserve\", \"AccessibleFemale\": \"False\", \"URL\": \"https://toiletmap.gov.au/toilet/5\", \"Notes\": \"\", \"ParkingAccessible\": \"True\", \"Longitude\": \"115.93647720\", \"AccessNote\": \"\", \"AdultChange\": \"False\", \"BabyChange\": \"False\", \"LHTransfer\": \"False\", \"SharpsDisposal\": \"True\", \"Male\": \"True\", \"IconAltText\": \"Male and Female, or Unisex\", \"IsOpen\": \"DaylightHours\", \"AccessibleMale\": \"False\", \"AddressNote\": \"\", \"IconURL\": \"https://toiletmap.gov.au/images/icons/mf.png\", \"Postcode\": \"6054\", \"Female\": \"True\", \"Parking\": \"True\", \"ToiletID\": \"5\", \"_id\": 5, \"ToiletType\": \"\"}]";
                var data = JsonConvert.DeserializeObject<List<Toilet>>(json);
                toilets.Fill(data);
            }
            catch (Exception ex)
            {
                ex.LogException();
            }
            return toilets;
        }
    }
}
