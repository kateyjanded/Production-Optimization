using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{public class Tuple : IEnumerable<int>
    {
        //private int[] elements;
        private List<int> elements;
        public Tuple(params int[] elements)
        {
            this.elements = elements.ToList();
        }

        public Tuple(List<int> elements)
        {
            this.elements = elements;
        }

        public IEnumerator<int> GetEnumerator()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                // Yield each day of the week.
                yield return elements[i];
            }
        }

        public int ElementsProduct
        {
            get
            {
                int product = 1;
                foreach (int el in elements)
                {
                    product *= el;
                }

                return product;
            }
        }

        public int this[int i]
        {
            get
            {
                return this.elements[i];
            }
        }

        public void Append(int item)
        {
            elements.Add(item);
        }

        public int Ndim
        {
            get
            {
                return elements.Count;
            }
        }

        public override string ToString()
        {
            string repr = "(";
            for (int i = 0; i < elements.Count; ++i)
            {
                repr += elements[i].ToString();
                if ((i + 1 != elements.Count))
                    repr += ", ";
            }
            repr += ")";
            return repr;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Tuple Copy()
        {
            var list = new List<int>(elements);
            var res = new Tuple(list);
            return res;
        }

        public List<Tuple> DecomposeToSingleElementTuples()
        {
            var result = new List<Tuple>();
            foreach (var e in elements)
            {
                var temp = new Tuple(e);
                result.Add(temp);
            }
            return result;
        }
    }
}
