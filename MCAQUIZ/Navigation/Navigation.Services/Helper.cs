using System;
using System.Collections.Generic;
using System.Text;

namespace Navigation.Services
{
    public static class Helper
    {
        public static int? ConvertToNull(string v)
        {
            if (int.TryParse(v, out int i))
            {
                return i;
            }
            return null;
        }

        public static string AddDots(int level)
        {
            string dot = ".";
            if (level > 0)
            {
                string concat = ".";
                for (int i = 0; i < 3 * level; i++)
                {
                    concat = concat + dot;
                }
                return concat;
            }
            else return dot;
        }        
    }
}
