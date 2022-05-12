using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    public class OneHitObliterator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("One-Hit Obliterator");
            Tooltip.SetDefault("[c/FF0000:Cheat Item]" +
                "\nCan manipulate anything in a single swing" +
                "\nItems will not drop from enemies when killed" +
                "\nAutomatically drains your health to 1 HP");
        }

        public override void SetDefaults()
        {
            item.damage = 1;
            item.width = 32;
            item.height = 32;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 0;
            item.value = 0;
            item.rare = ItemRarityID.Expert;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            player.statLife = 1;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.life = 0;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(mod, "Damage", "Infinite Damage"));
        }
    }
}
