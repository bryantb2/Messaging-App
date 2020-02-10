using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MessagingApp.Models
{
    public class Locations
    {
        //CLASS FIELDS
        private string titleText = "Locations and Resources";
        private string mainDescription = "Real life meetups happen in LCC's computer lab on Tuesday's and Thursdays, so wear a SW shirt! Below is an interactable" +
                                    "list of important resources and locations related to our club meetups:";
        //index synchronized arrays
        private string[] listItemTitles = new string[] { "LCC Location (Google Maps)", "Star Wars Shirts (Amazon)", "Weekly Fan Wiki Read", "The History of Hyperdrives", "Star Wars in Pop Culture" };
        private string[] listItemHrefs = new string[] { "https://www.google.com/maps/d/embed?mid=18FqAfCw5eOOrFrBbQaTk1zGIvK8&hl", "https://www.amazon.com/Star-Wars-Tropical-Stormtrooper-Graphic/dp/B07KVJJJV7/ref=lp_16698506011_1_7?s=apparel&ie=UTF8&qid=1570434439&sr=1-7&nodeID=16698506011&psd=1", "https://www.starwars.com/news/d23-expo-2019-star-wars-the-rise-of-skywalker-poster-revealed", "https://starwars.fandom.com/wiki/Hyperdrive", "https://en.wikipedia.org/wiki/Cultural_impact_of_Star_Wars" };

        //PROPERTIES
        public string[,] LocationsDataList
        {
            get
            {
                string[,] mergedListItemArray = new string[5,5];
                for (int i = 0; i < 5; i++)
                {
                    mergedListItemArray[0,i] = listItemTitles[i];
                    mergedListItemArray[1, i] = listItemHrefs[i];
                }
                return mergedListItemArray;
            }
        }

        public string TitleText
        {
            get { return this.titleText; }
        }

        public string MainDescription
        {
            get { return this.mainDescription; }
        }
    }
}
