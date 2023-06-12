﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.TeamFoundation.Common.Internal.NativeMethods;

namespace ThreadApp
{
    class Plane : GeometricObject
    {
        /// <summary>Punkt przez który płaszczyzna przechodzi</summary>
        Vector3 point;
        /// <summary>Normalna do płaszczyzny</summary>
        Vector3 normal;
        public Plane(Vector3 point, Vector3 normal, IMaterial material)
        {
            this.point = point;
            this.normal = normal.Normalised;
            base.Material = material;
        }
        public override bool HitTest(Ray ray, ref double distance, ref Vector3 outNormal)
        {
            double t = (point - ray.Origin).Dot(normal) / ray.Direction.Dot(normal);
            if (t > Ray.Epsilon)
            {
                distance = t;
                outNormal = normal;
                return true;
            }
            return false;
        }
    }


}
