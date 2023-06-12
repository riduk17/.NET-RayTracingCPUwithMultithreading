﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    class HitInfo
    {
        /// <summary>Trafiony obiekt lub null jeśli promień w nic nie trafił</summary>
        public GeometricObject HitObject { get; set; }
        /// <summary>Referencja do świata który renderujemy</summary>
        public World World { get; set; }
        /// <summary>Normalna do punktu trafienia</summary>
        public Vector3 Normal { get; set; }
        /// <summary>Punkt trafienia (w koordynatach świata)</summary>
        public Vector3 HitPoint { get; set; }
        /// <summary>Promień który trafił obiekt</summary>
        public Ray Ray { get; set; }
        /// <summary>Zwiększana przy śledzeniu odbitego lub załamanego promienia</summary>
        public int Depth { get; set; }
    }
}
