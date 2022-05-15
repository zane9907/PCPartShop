using Microsoft.Toolkit.Mvvm.Messaging;
using Newtonsoft.Json;
using OW21BB.Models;
using OW21BB.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB.Logic
{
    public class ShopLogic : IShopLogic
    {
        IList<Part> Shop;
        IList<Part> Cart;

        IReciept reciept;
        IMessenger messenger;
        public double AllCost
        {
            get
            {
                return Cart.Count == 0 ? 0 : Cart.Sum(x => x.Price);
            }
        }

        public void Load()
        {
            if (File.Exists("shop.txt"))
            {
                var input = File.ReadAllLines("shop.txt").ToList();

                foreach (var item in input)
                {
                    var oneLine = item.Split(',');
                    this.Shop.Add(new Part()
                    {
                        Name = oneLine[0],
                        Type = (EnumTypes)Enum.Parse(typeof(EnumTypes), oneLine[1].ToUpper()),
                        Price = double.Parse(oneLine[2]),
                        Akcios = false
                    });
                }
            }
            messenger.Send("Loaded", "ShopInfo");
        }

        public void AddToCart(Part selected)
        {
            int countCPU = 0;
            int countMOTHERBOARD = 0;


            foreach (var item in Cart.ToList())
            {
                if (item.Type.Equals(EnumTypes.CPU))
                {
                    countCPU++;
                }
                else if (item.Type.Equals(EnumTypes.MOTHERBOARD))
                {
                    countMOTHERBOARD++;
                }
            }
            if (selected.Type.Equals(EnumTypes.CPU))
            {
                if (countCPU < 1)
                {
                    var asd = selected.GetCopy();
                    if (asd.Akcios)
                    {
                        asd.Price *= 0.9;
                    }
                    this.Cart.Add(asd);
                    selected.Akcios = false;
                    messenger.Send("Added", "ShopInfo");
                }
                
            }
            else if (selected.Type.Equals(EnumTypes.MOTHERBOARD))
            {
                if (countMOTHERBOARD < 1)
                {
                    var asd = selected.GetCopy();
                    if (asd.Akcios)
                    {
                        asd.Price *= 0.9;
                    }
                    this.Cart.Add(asd);
                    selected.Akcios = false;
                    messenger.Send("Added", "ShopInfo"); 
                }
            }
            else
            {
                var asd = selected.GetCopy();
                if (asd.Akcios)
                {
                    asd.Price *= 0.9;
                }
                this.Cart.Add(asd);
                selected.Akcios = false;
                messenger.Send("Added", "ShopInfo");
            }


        }

        public void Remove(Part selected)
        {
            this.Cart.Remove(selected);
            messenger.Send("Removed", "ShopInfo");
        }

        public void Akcio(Part selected)
        {
            selected.Akcios = true;
            messenger.Send("Akcio", "ShopInfo");
        }

        public void Finish()
        {
            reciept.Finish(this.Cart.ToList());
        }

        public ShopLogic(IMessenger messenger, IReciept reciept)
        {
            this.messenger = messenger;
            this.reciept = reciept;
        }
        public void SetupCollections(IList<Part> Shop, IList<Part> Cart)
        {
            this.Shop = Shop;
            this.Cart = Cart;
        }
    }
}
