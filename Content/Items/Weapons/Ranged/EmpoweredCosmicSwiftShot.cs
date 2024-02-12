using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class EmpoweredCosmicSwiftShot : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("'How good can it be?'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 42;
            Item.height = 76;
            Item.damage = 300;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 18f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<Magenta>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmicSwiftShot>())
                .AddIngredient(ModContent.ItemType<Starfall>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 12)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-1, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 6 + Main.rand.Next(3);

            for (int i = 0; i < numberProjectiles; i++)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-200, 200), Main.rand.Next(-200, 200)), velocity, ModContent.ProjectileType<EmpoweredSwiftShotStarbuster>(), damage, knockback, player.whoAmI);
            }

            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }

            for (int i = 0; i < 3; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, ModContent.ProjectileType<EmpoweredSwiftShotStarbuster>(), damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            return false;
        }
    }
}
