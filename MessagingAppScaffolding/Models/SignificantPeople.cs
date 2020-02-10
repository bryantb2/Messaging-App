using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityWebsite.Models
{
    public class SignificantPeople
    {
        //CLASS FIELDS
        private string titleText = "Significant People";
        private string mainDescription = "Below is a short section on people who have had a major impact on our community.";
        //index synchronized arrays
        private string[] figureSourceImages = new string[] { "https://images2.minutemediacdn.com/image/upload/c_crop,h_1126,w_2000,x_0,y_0/f_auto,q_auto,w_1100/v1564085984/shape/mentalfloss/george-lucas-gettyimages-929360234.jpg", "https://lumiere-a.akamaihd.net/v1/images/Darth-Vader_6bda9114.jpeg?region=0%2C23%2C1400%2C785&width=960", "https://lumiere-a.akamaihd.net/v1/images/databank_jarjarbinks_01_169_c70767ab.jpeg?region=0%2C0%2C1560%2C878&width=960"};
        private string[] figureCaptions = new string[] { "George Lucas", "Darth Vader", "The Secret Dark Lord" };

        //PROPERTIES
        public string[,] FigureArray
        {
            get
            {
                string[,] mergedListItemArray = new string[3, 3];
                for (int i = 0; i < 3; i++)
                {
                    mergedListItemArray[0, i] = figureSourceImages[i];
                    mergedListItemArray[1, i] = figureCaptions[i];
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
