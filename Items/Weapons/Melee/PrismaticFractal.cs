using Microsoft.Xna.Framework;
using Eternal.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    class PrismaticFractal : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Not to be confused with the Terraprisma'\nPierce the rainbow, Taste the rainbow!\nFires a Prisim Sword\n[c/FC036B:Dedicated Content]\nDedicated to [c/038CFC:JakeTEM]");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 38;
            item.shootSpeed = 12f;
            item.damage = 120;
            item.knockBack = 10.5f;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.useAnimation = 30;
            item.useTime = 30;
            item.UseSound = SoundID.Item1;
            item.melee = true;
            item.rare = ItemRarityID.LightRed;
            item.autoReuse = true;
            item.value = Item.sellPrice(gold: 1, silver: 30);
            item.shoot = mod.ProjectileType("PrismaticFractalProjectile");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddIngredient(ItemID.CrystalShard, 30);
            recipe.AddIngredient(ItemID.HallowedBar, 5);
            recipe.AddIngredient(ItemID.WoodenSword);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
