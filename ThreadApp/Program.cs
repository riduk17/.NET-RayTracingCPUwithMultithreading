using System.Diagnostics;
using System.Drawing;
using ThreadApp;

class Program
{
    static void Main(string[] args)
    {
        var watch = Stopwatch.StartNew();
        // Stworzenie świata (kolor tła = łagodny niebieski)
        World world = new World(Color.PowderBlue);

        // Trzy różnokolorowe kule (patrz obrazek)
        // Materiały
        IMaterial redMat = new Reflective(Color.LightCoral, 0.4, 1, 300, 0.6);
        IMaterial greenMat = new Reflective(Color.LightGreen, 0.4, 1, 300, 0.6);
        IMaterial blueMat = new Reflective(Color.LightBlue, 0.4, 1, 300, 0.6);
        IMaterial grayMat = new Reflective(Color.Gray, 0.4, 1, 300, 0.6);
        IMaterial magentaMat = new Reflective(Color.Magenta, 0.4, 1, 300, 0.6);
        IMaterial wheatMat = new Reflective(Color.Wheat, 0.4, 1, 300, 0.6);
        IMaterial goldMat = new Phong(Color.Gold, 0.8, 1, 30);
        IMaterial blackMat = new Reflective(Color.DarkGray, 0.4, 1, 300, 0.6);
        // Trzy różnokolorowe kule

        world.Add(new Sphere(new Vector3(0, 8, 16), 8, wheatMat));
        world.Add(new Sphere(new Vector3(64, 32, 16), 5, goldMat));
        world.Add(new Sphere(new Vector3(-4, 0, 0), 2, redMat));
        world.Add(new Sphere(new Vector3(4, 0, 0), 2, greenMat));
        world.Add(new Sphere(new Vector3(0, 0, 3), 2, blueMat));
        world.Add(new Sphere(new Vector3(-6, 0.5, 3), 1, magentaMat));
        world.Add(new Plane(new Vector3(0, -2, 0), new Vector3(0, 1, 0), blackMat));
        world.AddLight(new PointLight(new Vector3(0, 5, -5), Color.White));
        /*
        world.Add(new Sphere(new Vector3(0, 4, 0), 2, redMat));
        world.Add(new Sphere(new Vector3(4, 4, 0), 2, greenMat));
        world.Add(new Sphere(new Vector3(2, 0.55, 0), 2, blueMat));
        world.Add(new Sphere(new Vector3(2, 2, 2), 2, wheatMat));
        world.AddLight(new PointLight(new Vector3(2, 5, 1), Color.White));*/

        ICamera camera = new PinholeCamera(new Vector3(0, 1, -8), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 1);
        //ICamera camera = new PinholeCamera(new Vector3(2, 3, -2), new Vector3(2, 3, 0), new Vector3(0, -1, 0), 1);

        Raytracer tracer = new Raytracer(50);

        const int SampleCt = 64;
        // Raytracing!
        Sampler antiAlias = new Sampler(new Regular(), new SquareDistributor(), SampleCt, 1); // na razie wystarczy jeden set
        Bitmap image = tracer.Raytrace(world, camera, new Size(1024, 1024), antiAlias);

        watch.Stop();
        // Zapisanie obrazka w jakimś miłym miejscu na dysku.
        image.Save("raytraced.png");
        string Location_ToOpen = @"C:\Users\riduk\Desktop\.NET_copy\ThreadApp\bin\Debug\net7.0\raytraced.png";
        if (!File.Exists(Location_ToOpen))
        {
            return;
        }

        string argument = "/open, \"" + Location_ToOpen + "\"";

        System.Diagnostics.Process.Start("explorer.exe", argument);

        Console.WriteLine(
          $"The Execution time of the program is {(watch.ElapsedMilliseconds)/1000}s");
    }
}