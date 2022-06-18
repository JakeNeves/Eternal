using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Projectiles.Weapons.Melee.Shortsword;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class EmpoweredSerpentKiller : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 34;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.damage = 440;
            Item.useAnimation = 6;
            Item.useTime = 6;
            Item.useStyle = ItemUseStyleID.Rapier;
            Item.knockBack = 3f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<EmpoweredSerpentKillerProjectile>();
            Item.shootSpeed = 2.1f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<SerpentKiller>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 12)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 8 + Main.rand.Next(4); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-128, 128), Main.rand.Next(-128, 128)), velocity + new Vector2(Main.rand.Next(-8, 8), Main.rand.Next(-8, 8)), ModContent.ProjectileType<EmpoweredSerpentKillerShoot>(), damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return true;
        }
    }
}
