using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    class Raytracer
    {
        int maxDepth;
        private object balanceLock = new object();
        public Raytracer(int maxDepth)
        {
            this.maxDepth = maxDepth;
        }
        public Bitmap Raytrace(World world, ICamera camera, Size imageSize)
        {
            Sampler singleSample = new Sampler(
            new Regular(), new SquareDistributor(), 1, 1);
            return this.Raytrace(world, camera, imageSize, singleSample);
        }
        public Bitmap Raytrace(World world,
        ICamera camera,
        Size imageSize,
        Sampler sampler)
        {
            Bitmap bmp = new Bitmap(imageSize.Width, imageSize.Height);
            //for (int y = 0; y < imageSize.Height; y++)
            var result = Parallel.For(0, imageSize.Height,(y, state) =>
            {
                //Console.WriteLine($"Beginning iteration {y}");
                for (int x = 0; x < imageSize.Width; x++)
                {
                    ColorRgb totalColor = ColorRgb.Black;
                        for (int i = 0; i < sampler.SampleCount; i++)
                        {
                            Vector2 sample = sampler.Single();
                            Vector2 pictureCoordinates = new Vector2(
                            ((x + sample.X) / (double)imageSize.Width) * 2 - 1,
                            ((y + sample.Y) / (double)imageSize.Height) * 2 - 1);
                            Ray ray = camera.GetRayTo(pictureCoordinates);
                            totalColor += ShadeRay(world, ray, 0)
                            / (double)sampler.SampleCount;
                        }
                        lock(balanceLock)
                            bmp.SetPixel(x, y, StripColor(totalColor));
                }
                //Console.WriteLine($"Ending iteration {y}");
            });
            return bmp;
        }

        public ColorRgb ShadeRay(World world, Ray ray, int currentDepth)
        {
            if (currentDepth > maxDepth) { return ColorRgb.Black; }
            HitInfo info = world.TraceRay(ray);
            info.Depth = currentDepth + 1;
            if (info.HitObject == null) { return world.BackgroundColor; }
            IMaterial material = info.HitObject.Material;
            return material.Shade(this, info);
        }

        Color StripColor(ColorRgb colorInfo)
        {
            colorInfo.R = colorInfo.R < 0 ? 0 : colorInfo.R > 1 ? 1 : colorInfo.R;
            colorInfo.G = colorInfo.G < 0 ? 0 : colorInfo.G > 1 ? 1 : colorInfo.G;
            colorInfo.B = colorInfo.B < 0 ? 0 : colorInfo.B > 1 ? 1 : colorInfo.B;
            return Color.FromArgb((int)(colorInfo.R * 255),
            (int)(colorInfo.G * 255),
            (int)(colorInfo.B * 255));
        }
    }
}
