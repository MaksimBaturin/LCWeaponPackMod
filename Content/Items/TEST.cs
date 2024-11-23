using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using LCWeaponPack.Common.Systems;
using LCWeaponPack.Content.Items.Intefaces;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.DataStructures;


namespace LCWeaponPack.Content.Items
{ 
	public class TEST : ModItem, IWeapon
	{

		private AttackSystem attackSystem = ModContent.GetInstance<AttackSystem>();

		private Skill currentSkill;

		public Skill CurrentSkill 
		{
			get { return currentSkill; }
			set	{ currentSkill = value;}
		}

		private int currentCoin;
		public int CurrentCoin {get { return currentCoin;} set {currentCoin = value;} }

		private Dictionary<Skill, List<short>> projectilesDict = new Dictionary<Skill, List<short>>()
        {
            { Skill.Common, new List<short>() { ProjectileID.NightBeam, ProjectileID.LightBeam, ProjectileID.TerraBeam } },
			{ Skill.Rare, new List<short>() { ProjectileID.RocketI , ProjectileID.RocketII} },
			{ Skill.Ultimate, new List<short>() { ProjectileID.GrenadeI} },
        };

		public Dictionary<Skill, List<short>> ProjectilesDict { get { return projectilesDict; } }

        public double AdditianialMultiplierDamage { get; set; }

        public override bool CanShoot(Player player)
        {
			attackSystem.MakeCoinFlip(this.Item);
            Item.shoot = projectilesDict[CurrentSkill][CurrentCoin];
            return base.CanShoot(player);
        }
        public override void HoldItem(Player player)
        {
			Dictionary<Item, Dictionary<Skill, List<int>>> NewSession = new Dictionary<Item, Dictionary<Skill, List<int>>>
			{
				{this.Item, new Dictionary<Skill, List<int>>
					{
					{Skill.Common, [projectilesDict[Skill.Common].Count, 0]},
					{Skill.Rare, [projectilesDict[Skill.Rare].Count, 0]},
                    {Skill.Ultimate, [projectilesDict[Skill.Ultimate].Count, 0]}
                    }
				} 
			};

			attackSystem.RegisterItem(NewSession);
        }
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			int buffedDamage = damage + (int)(AdditianialMultiplierDamage * damage);
            Projectile.NewProjectile(source, player.MountedCenter, new Vector2(player.direction * 15f, 0f), type, buffedDamage, 
				knockback, player.whoAmI, player.direction * player.gravDir, player.itemAnimationMax);
            return false;
		}

		public override void SetDefaults()
		{
			Item.SetNameOverride("TEST");
			Item.damage = 50;
			Item.DamageType = DamageClass.Melee;
			Item.width = 40;
			Item.height = 40;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 6;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
			Item.shoot = ProjectileID.NightBeam;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
