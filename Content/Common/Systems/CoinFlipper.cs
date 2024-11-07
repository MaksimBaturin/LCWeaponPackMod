using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LCWeaponPack.Content.Common.Systems
{
    internal static class CoinFlipper
    {
        public static bool MakeCoinFlip()
        {
            Random random = new Random();
            if (random.Next(0,1) == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
