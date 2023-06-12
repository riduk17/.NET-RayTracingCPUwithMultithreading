using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    abstract class GeometricObject
    {
        public IMaterial Material { get; set; }
        public abstract bool HitTest(Ray ray, ref double distance, ref Vector3 normal);
    }
}
