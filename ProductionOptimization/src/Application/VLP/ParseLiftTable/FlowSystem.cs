using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class FlowSystem
    {
        protected List<IComponent> components;

        public FlowSystem()
        {
            components = new List<IComponent>();
        }
    }
}
