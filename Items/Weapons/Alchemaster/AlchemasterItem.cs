using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Alchemaster
{
    public abstract class AlchemasterItem : ModItem
    {

        public int alchemicResourceCost = 0;

        public virtual void SafeSetDefaults()
        {
        }

        public sealed override void SetDefaults()
        {
            SafeSetDefaults();
            item.melee = false;
            item.ranged = false;
            item.magic = false;
            item.summon = false;
            item.thrown = false;
        }

        public override void ModifyWeaponDamage(Player player, ref float add, ref float mult, ref float flat)
        {
            add += AlchemasterPlayer.ModPlayer(player).alchemicDamageAdd;
            mult *= AlchemasterPlayer.ModPlayer(player).alchemicDamageMult;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += AlchemasterPlayer.ModPlayer(player).alchemicCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageType = splitText.Last();
                tt.text = damageValue + " alchemic " + damageType;
            }
        }

    }
}
