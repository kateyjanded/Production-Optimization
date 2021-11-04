using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public interface IComponent
    {
        double BHP(double rate);
        double Rate(double pressure);
    }
}
