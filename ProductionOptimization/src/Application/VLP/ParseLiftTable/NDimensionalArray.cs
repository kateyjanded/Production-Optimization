using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.VLP.ParseLiftTable
{
    public class NDimensionalArray
    {
        private int ndim;
        //private int size;
        Shape shape;
        private double[] data;

        public int StorageLength
        {
            get
            {
                return data.Length;
            }
        }

        public Shape Shape { get { return shape; } }
        public int Ndim
        {
            get
            {
                return shape.Ndim;
            }
        }

        public NDimensionalArray(params int[] shape)
        {
            this.shape = new Shape(shape);
            this.ndim = this.shape.Ndim;
            int recordLength = this.shape.ElementsProduct;

            data = new double[recordLength];
        }

        public NDimensionalArray(List<double> items)
        {
            Initialize(items);
        }

        public NDimensionalArray(List<int> items)
        {
            var list = new List<double>();
            foreach (int el in items)
                list.Add((double)el);

            Initialize(list);
        }

        public NDimensionalArray(double[] items)
        {
            this.shape = new Shape(1);
            this.ndim = 1;
            this.data = items;
        }

        public NDimensionalArray Reshape(Shape newShape)
        {
            int storageLengthFromShape = newShape.ElementsProduct;
            if (storageLengthFromShape != StorageLength)
                throw new Exception("Shape not compatible with array size");
            this.shape = newShape;
            this.ndim = shape.Ndim;
            return this;
        }

        public NDimensionalArray Reshape(params int[] shapeArray)
        {
            var shape = new Shape(shapeArray);
            return Reshape(shape);
        }

        public void Initialize(List<double> items)
        {
            this.shape = new Shape(1);
            this.ndim = 1;
            this.data = items.ToArray();
        }

        public double At(Tuple indices)
        {
            int index = MapToStorageIndex(indices);

            return data[index];
        }

        public double this[Tuple indices]
        {
            get
            {
                int index = MapToStorageIndex(indices);
                return this.data[index];
            }
            set
            {
                int index = MapToStorageIndex(indices);
                this.data[index] = value;
            }
        }
        private int MapToStorageIndex(Tuple indices)
        {
            // row major ordering

            if (indices.Ndim != ndim)
                throw new Exception("input indices has the wrong dimension");

            // row major ordering
            int storage_index = 0;

            for (int i = 0; i < ndim; ++i)
            {
                int stride = 1;
                for (int j = i + 1; j < ndim; ++j)
                {
                    stride *= shape[j];
                }
                storage_index += indices[i] * stride; //bug, I initially forgot to multiply indices[i]
                                                      //by stride
            }

            return storage_index;
        }

    }
}
