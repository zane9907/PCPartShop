using OW21BB.Models;
using System.Collections.Generic;

namespace OW21BB.Logic
{
    public interface IShopLogic
    {
        double AllCost { get; }

        void AddToCart(Part selected);
        void Finish();
        void Load();
        void Remove(Part selected);

        public void Akcio(Part selected);

        void SetupCollections(IList<Part> Shop, IList<Part> Cart);
    }
}