using System.Collections.Generic;
using System.Linq;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Radiant
{
    public abstract class RadiantItem : ModItem
    {
        public override bool CloneNewInstances => true;
        public int etherealPowerCost = 0;

        public virtual void SafeSetDefaults() { }

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
            add += RadiantPlayer.ModPlayer(player).radiantDamageAdd;
            mult *= RadiantPlayer.ModPlayer(player).radiantDamageMultiply;
        }

        public override void GetWeaponKnockback(Player player, ref float knockback)
        {
            knockback += RadiantPlayer.ModPlayer(player).radiantKnockback;
        }

        public override void GetWeaponCrit(Player player, ref int crit)
        {
            crit += RadiantPlayer.ModPlayer(player).radiantCrit;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            TooltipLine tt = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
            if (tt != null)
            {
                string[] splitText = tt.text.Split(' ');
                string damageValue = splitText.First();
                string damageWord = splitText.Last();
                tt.text = damageValue + " radiant " + damageWord;
            }

            if (etherealPowerCost > 0)
            {
                tooltips.Add(new TooltipLine(mod, "Ethereal Power Cost", $"Uses {etherealPowerCost} ethereal power"));
            }
        }

        public override bool CanUseItem(Player player)
        {
            var radiantPlayer = player.GetModPlayer<RadiantPlayer>();

            if (radiantPlayer.etherealPowerCurrent >= etherealPowerCost)
            {
                radiantPlayer.etherealPowerCurrent -= etherealPowerCost;
                return true;
            }
            return false;
        }
    }
}
