using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheCoreBanking.Finance.Data.Helpers;

namespace TheCoreBanking.Finance.Products.Helpers
{
    public class EnumerationTypes
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
    public class EnumToList
    {

        public static List<EnumerationTypes> ListStates()
        {

            var enumTypes = from States type in Enum.GetValues(typeof(States))
                            select new { Id = (int)type, Type = type.ToString() };

            List<EnumerationTypes> enumTypeList = new List<EnumerationTypes>();
            foreach (var items in enumTypes)
            {
                EnumerationTypes enumType = new EnumerationTypes();
                enumType.Id = items.Id;
                enumType.Type = items.Type;
                enumTypeList.Add(enumType);
            }
            return enumTypeList;
        }
        

    }
}
