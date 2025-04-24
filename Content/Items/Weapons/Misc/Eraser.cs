using Eternal.Content.Projectiles.Weapons.Misc;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Misc
{
    public class Eraser : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.width = 26;
            Item.height = 20;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 12f;
            Item.shoot = ModContent.ProjectileType<EraserProjectile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(Mod, "Damage", "Infinite damage"));
        }
    }
}
