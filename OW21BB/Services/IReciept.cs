using OW21BB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB.Services
{
    public interface IReciept
    {
        void Finish(List<Part> parts);
    }
}
