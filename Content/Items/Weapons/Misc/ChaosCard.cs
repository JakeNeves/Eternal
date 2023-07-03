using Eternal.Content.Projectiles.Weapons.Misc;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Misc
{
    public class ChaosCard : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.width = 30;
            Item.height = 30;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.noUseGraphic = true;
            Item.shootSpeed = 24f;
            Item.shoot = ModContent.ProjectileType<ChaosCardProjectile>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(Mod, "Damage", "Infinite damage"));
        }
    }
}
