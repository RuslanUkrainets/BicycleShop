using System.Collections.Generic;
using System.Linq;

namespace BicycleShop.Extentions
{
    public static class Common
    {
        public static IEnumerable<T> GetItemsOfPage<T>(IEnumerable<T> items, ref int page, int take = 10)
        {
            page = page == 0 ? 1 : page;
            IEnumerable<T> tmpItems = new List<T>(items);

            var skip = page * take - take;

            items = items.Skip(skip).Take(take);

            if (!items.Any())
            {
                page = page - 1 > 1 ? page - 1 : 1;
                skip = page * take - take;
                items = tmpItems.Skip(skip).Take(take);
            }

            return items;
        }
    }
}
