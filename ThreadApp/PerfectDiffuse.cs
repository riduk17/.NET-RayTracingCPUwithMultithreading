using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    class PerfectDiffuse : IMaterial
    {
        ColorRgb materialColor;
        public PerfectDiffuse(ColorRgb materialColor)
        {
            this.materialColor = materialColor;
        }
        public ColorRgb Shade(Raytracer tracer, HitInfo hit)
        {
            ColorRgb totalColor = ColorRgb.Black;
            foreach (var light in hit.World.Lights)
            {
                Vector3 inDirection = (light.Position - hit.HitPoint).Normalised;
                double diffuseFactor = inDirection.Dot(hit.Normal);
                if (diffuseFactor < 0) { continue; }
                if (hit.World.AnyObstacleBetween(hit.HitPoint, light.Position))
                { continue; }
                totalColor += light.Color * materialColor * diffuseFactor;
            }
            return totalColor;
        }
    }
}
