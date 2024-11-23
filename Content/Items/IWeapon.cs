
using System.Collections.Generic;
using LCWeaponPack.Common.Systems;
using LCWeaponPack.Content.Items.Intefaces;
using Terraria.GameContent.UI.States;
using Terraria.ID;

namespace LCWeaponPack.Content.Items
{
    public interface IWeapon{

        Skill CurrentSkill { get; set; }

        int CurrentCoin { get; set; }

        Dictionary<Skill, List<short>> ProjectilesDict {get;}
        double AdditianialMultiplierDamage { get; set; }

    }
}

