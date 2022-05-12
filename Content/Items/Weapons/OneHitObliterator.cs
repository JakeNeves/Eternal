using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons
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
            Item.damage = 1;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            player.statLife = 1;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.life = 0;
        }

        /*public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(mod, "Damage", "Infinite Damage"));
        }*/
    }
}
