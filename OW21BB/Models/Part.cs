using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB.Models
{
    public class Part : ObservableObject
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        private EnumTypes type;

        public EnumTypes Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }


        private double price;

        public double Price
        {
            get { return price; }
            set { SetProperty(ref price, value); }
        }

        private bool akcios;

        public bool Akcios
        {
            get { return akcios; }
            set { SetProperty(ref akcios, value); }
        }



        public Part GetCopy()
        {
            return new Part()
            {
                Name = Name,
                Type = Type,
                Price = Price,
                Akcios = Akcios
            };
        }

    }
}
