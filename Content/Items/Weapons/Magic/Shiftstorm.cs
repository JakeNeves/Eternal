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
    public class Shiftstorm : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 300;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.mana = 36;
            Item.knockBack = 8f;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.shoot = ModContent.ProjectileType<ShiftstormPortalProjectile>();
            Item.shootSpeed = 0f;
            Item.UseSound = SoundID.Item84;
            Item.rare = ModContent.RarityType<Teal>();
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 4;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 6)
                .AddIngredient(ModContent.ItemType<MoteofOminite>(), 12)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>(), 6)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Projectile.NewProjectile(source, Main.MouseWorld, velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);

            return false;
        }
    }
}
