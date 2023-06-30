using BookStore;
using BookStore.Enums;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace BookStore.Classes.Sorting
{
    public class SortInfo : IKey<string>
    {
        public ENBookAttributes Attribute { get; set; }
        public ENSortingType SortType { get; set; }

        public string Key
        {
            get
            {
                return Attribute.ToString();
               // return GetKey(Attribute, SortType);
            }
        }

        public void SortDecision()
        {
            SortType = SortType == ENSortingType.Ascending? ENSortingType.Descending : ENSortingType.Ascending;
        }

        public static string GetKey(string attribute, string sortType)
        {
            ENBookAttributes enAttribute = ENBookAttributes.None;
            Enum.TryParse(attribute, out enAttribute);

            ENSortingType enSortType;
            Enum.TryParse(sortType, out enSortType);

            return string.Format("{0}_{1}", enAttribute, enSortType);
        }

        public static string GetKey(ENBookAttributes enAttribute, ENSortingType enSortType)
        {
            return string.Format("{0}_{1}", enAttribute, enSortType);
        }
    }
}
