using LCWeaponPack.Content.Common.Systems;
using LCWeaponPack.Content.Items;
using LCWeaponPack.Content.Items.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.ModLoader;
using Terraria;
namespace LCWeaponPack.Common.Systems{
    public class AttackSystem : ModSystem
    {
        private CoinFlipper coinFlipper = new CoinFlipper();
        // weapon : (skill : (TotalCoins, CurrentCoin))
        private Dictionary<Item, Dictionary<Skill, List<int>>> WeaponSkillCoinMap = new Dictionary<Item, Dictionary<Skill, List<int>>>();
        private Dictionary<Item, Skill> CurrentWeaponSkills = new Dictionary<Item, Skill>();

        public void RegisterItem(Dictionary<Item, Dictionary<Skill, List<int>>> item)
        {
            if (!WeaponSkillCoinMap.ContainsKey(item.First().Key))
            {
                WeaponSkillCoinMap.Add(item.First().Key, item.First().Value);
                CurrentWeaponSkills[item.First().Key] = Skill.Common;
            }
        }

        public void MakeCoinFlip(Item weapon)
        {
            if (HasCoinsLeft(weapon))
            {
                bool result = coinFlipper.MakeCoinFlip();
                if (result && weapon.ModItem is IWeapon myItem)
                {
                    myItem.AdditianialMultiplierDamage += 0.05
                }

                DecreaseCoinCount(weapon);
                UpdateWeaponState(weapon);
                DisplaySkillInfo(weapon);
            }
            else
            {
                ChangeSkill(weapon);
            }
        }

        private bool HasCoinsLeft(Item weapon)
        {
            return WeaponSkillCoinMap[weapon][CurrentWeaponSkills[weapon]][1] > 0;
        }

        private void DecreaseCoinCount(Item weapon)
        {
            WeaponSkillCoinMap[weapon][CurrentWeaponSkills[weapon]][1]--;
            if (weapon.ModItem is IWeapon myItem)
            {
                myItem.CurrentCoin = WeaponSkillCoinMap[weapon][CurrentWeaponSkills[weapon]][1];
            }
        }

        private void UpdateWeaponState(Item weapon)
        {
            if (WeaponSkillCoinMap[weapon][CurrentWeaponSkills[weapon]][1] == 0)
            {
                ChangeSkill(weapon);
            }
        }
        //Это временно, потом будет UI
        private void DisplaySkillInfo(Item weapon)
        {
            Main.NewText(CurrentWeaponSkills[weapon].ToString() + " " + WeaponSkillCoinMap[weapon][CurrentWeaponSkills[weapon]][1].ToString(), 150, 250, 150);
        }

        public void ChangeSkill(Item weapon)
        {
            Skill newSkill = GetRandomSkill();
            CurrentWeaponSkills[weapon] = newSkill;
            ResetCoinCount(weapon, newSkill);
            UpdateWeaponSkill(weapon);
            Main.NewText("State changed!", 150, 250, 150);
        }

        private Skill GetRandomSkill()
        {
            Random rnd = new Random();
            int randomValue = rnd.Next(0, 6);
            return randomValue switch
            {
                5 => Skill.Ultimate,
                >= 3 and < 5 => Skill.Rare,
                _ => Skill.Common,
            };
        }

        private void ResetCoinCount(Item weapon, Skill skill)
        {
            WeaponSkillCoinMap[weapon][skill][1] = WeaponSkillCoinMap[weapon][skill][0];
        }

        private void UpdateWeaponSkill(Item weapon)
        {
            if (weapon.ModItem is IWeapon myItem)
            {
                myItem.CurrentSkill = CurrentWeaponSkills[weapon];
                myItem.AdditianialMultiplierDamage = 0;
            }
        }
    }
}