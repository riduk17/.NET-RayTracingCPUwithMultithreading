using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    interface IMaterial
    {
        ColorRgb Shade(Raytracer tracer, HitInfo hit);
    }
}
