using Eternal.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class Ataxelgrim : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Throws an Ataxelgrim Knife\n'Not To Be Confised With \"Terragrim\"'");
        }

        public override void SetDefaults()
        {

            item.width = 18;
            item.height = 46;
            item.damage = 25;
            item.ranged = true;
            item.knockBack = 9.75f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 15;
            item.useTime = 15;
            item.rare = ItemRarityID.Green;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(gold: 3);
            item.shoot = ProjectileType<AtaxelgrimProjectile>();

        }

    }
}
