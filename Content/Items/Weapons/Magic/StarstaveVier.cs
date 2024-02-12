using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class StarstaveVier : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starstave Vier");
            // Tooltip.SetDefault("Fires a small barage of starstave bolts that indulges in mitosis");
            Item.staff[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 44;
            Item.height = 44;
            Item.damage = 800;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 14;
            Item.knockBack = 4f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<StarstaveProjectileVier>();
            Item.shootSpeed = 12f;
            Item.UseSound = SoundID.Item8;
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarstaveDrei>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 24)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 32)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 40)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 40)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-96, 96), Main.rand.Next(-96, 96)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
