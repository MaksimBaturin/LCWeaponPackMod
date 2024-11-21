using LCWeaponPack.Content.Common.Systems;
using LCWeaponPack.Content.Items;
using Terraria;
using Terraria.ModLoader;

namespace LCWeaponPack.Content.Common.LCModPlayerClass
{
    internal class LCModPlayer : ModPlayer
    {
        private SanitySystem sanitySystem = ModContent.GetInstance<SanitySystem>();
        public override void OnHurt(Player.HurtInfo info)
        {
            sanitySystem.DecreaseSanity(info.Damage);
            base.OnHurt(info);
        }
        public override void OnRespawn()
        {
            sanitySystem.CurrentSanity = 0;
            base.OnRespawn();
        }

        public override void OnHitNPCWithItem(Item item, NPC target, NPC.HitInfo hit, int damageDone)
        {
            sanitySystem.IncreaseSanity(damageDone);
            base.OnHitNPCWithItem(item, target, hit, damageDone);
        }
    }
}
