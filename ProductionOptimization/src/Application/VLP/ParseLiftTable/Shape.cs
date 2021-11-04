using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class Shape
    {
        private Tuple shape;
        private int ndim;

        public int Ndim { get { return ndim; } }
        public Shape(params int[] shape)
        {
            this.shape = new Tuple(shape);
            this.ndim = shape.Length;
        }

        public int ElementsProduct
        {
            get
            {
                return this.shape.ElementsProduct;
            }
        }

        public int this[int i]
        {
            get
            {
                return this.shape[i];
            }

        }
        public override string ToString()
        {
            return this.shape.ToString();
        }
    }
}
