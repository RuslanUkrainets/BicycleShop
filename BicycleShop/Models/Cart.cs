using System.Collections.Generic;
using System.Linq;

namespace BicycleShop.Models
{
    public class Cart
    {
        private List<CartLine> _lineCollection = new();

        public IEnumerable<CartLine> Lines => _lineCollection;

        public void AddItem(Bicycle bicycle, int quantity)
        {
            CartLine line = _lineCollection.FirstOrDefault(x => x.Bicycle.Id == bicycle.Id);
            if(line == null)
            {
                _lineCollection.Add(new CartLine
                {
                    Bicycle = bicycle,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Bicycle bicycle) => 
            _lineCollection.RemoveAll(x => x.Bicycle.Id == bicycle.Id);

        public void Clear() => 
            _lineCollection.Clear();

        public int? Calculate() => _lineCollection.Sum(x => x.Bicycle.Price * x.Quantity);
    }
}
