using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;
using Terraria;
using LCWeaponPack.Content.Common.LCModPlayerClass;

namespace LCWeaponPack.Content.Common.Systems
{
    internal class SanitySystem : ModSystem
    {
        private Player player;
        public int CurrentSanity { get; set; } = 0;

        public override void OnWorldLoad()
        {
            player = Main.LocalPlayer;
        }
        public void displayName()
        {
            Main.NewText(player.name, 150, 250, 150);
        }

        public void DecreaseSanity(int hurtDamage)
        {
            Main.NewText("HurtDamage: "+ hurtDamage.ToString(), 50, 250, 150);

            int newSanity = CurrentSanity - 5 ;
            if (newSanity < -45) CurrentSanity = -45;
            else CurrentSanity = newSanity;


            Main.NewText("Sanity: " + CurrentSanity.ToString(), 50, 250, 150);
        }

        public void IncreaseSanity(int doneDamage)
        {
            Main.NewText("DoneDamage: " + doneDamage.ToString(), 50, 250, 150);

            int newSanity = CurrentSanity + 5;
            if (newSanity > 45) CurrentSanity = 45;
            else CurrentSanity = newSanity;

            Main.NewText("Sanity: " + CurrentSanity.ToString(), 50, 250, 150);
        }
    }
}
