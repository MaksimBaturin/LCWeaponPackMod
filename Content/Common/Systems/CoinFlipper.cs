using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LCWeaponPack.Content.Common.Systems
{
    internal class CoinFlipper
    {
        private SanitySystem sanitySystem = ModContent.GetInstance<SanitySystem>();

        private int headChance = 50;
        public bool MakeCoinFlip()
        {
            Random random = new Random();

            headChance = 50 + sanitySystem.CurrentSanity;

            if (random.Next(0, headChance+1) <= headChance)
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
