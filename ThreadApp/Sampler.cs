using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadApp
{
    class Sampler
    {
        private object locklock = new object();
        Random r;
        List<Vector2[]> sets;
        int sampleNdx;
        int setNdx;
        public Sampler(ISampleGenerator sampler,
        ISampleDistributor mapper,
        int sampleCt,
        int setCt)
        {
            this.sets = new List<Vector2[]>(setCt);
            this.r = new Random(0);
            this.SampleCount = sampleCt;
            for (int i = 0; i < setCt; i++)
            {
                var samples = sampler.Sample(sampleCt);
                var mappedSamples = samples.Select((x) => mapper.MapSample(x)).ToArray();
                sets.Add(mappedSamples);
            }
        }
        public Vector2 Single()
        {
            //Console.WriteLine($"setNdx: {setNdx}");
            //Console.WriteLine($"sampleNdx {sampleNdx}");
            Vector2 sample;
            lock (locklock)
            {
                sample = sets[setNdx][sampleNdx];
                sampleNdx++;
                if (sampleNdx >= sets[setNdx].Length)
                { sampleNdx = 0; setNdx = r.Next(sets.Count); }
            }
            return sample;
        }
        public int SampleCount { get; private set; }
        public int SetCount { get { return sets.Count; } }
    }
}
