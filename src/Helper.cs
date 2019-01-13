using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace midiGame
{
    class Helper
    {
        public static Random random = new Random();

        public static double DegreeToRad(double d)
        {
            return d * Math.PI / 180.0;
        }

        public static float randomFloatInRange(float min, float max)
        {
            if(max < min)
            {
                throw new Exception("Numbers are around the wrong way");
            }
            return (float)(random.NextDouble() * (max - min)) + min;
        }
    }
}
