using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class Phazon : ModItem
    {

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 96;
            Item.damage = 660;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.shootSpeed = 20f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<Magenta>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.PulseBow)
                .AddIngredient(ItemID.LunarBar, 16)
                .AddIngredient(ItemID.SpectreBar, 16)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 24)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 18)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 8;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-240, 240), Main.rand.Next(-240, 240)), velocity, ModContent.ProjectileType<PhazonProjectile>(), damage, knockback, player.whoAmI);
            }

            return false;
        }
    }
}
