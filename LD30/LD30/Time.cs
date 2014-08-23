using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    static class Time
    {
        public static float Delta { get; private set; }

        public static void Update(float elapsed)
        {
            Delta = elapsed;
        }
    }
}
