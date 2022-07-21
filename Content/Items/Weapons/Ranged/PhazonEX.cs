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
    public class PhazonEX : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Phazon EX");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 64;
            Item.height = 96;
            Item.damage = 1000;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 8;
            Item.useAnimation = 8;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item91;
            Item.autoReuse = true;
            Item.shootSpeed = 20f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<Turquoise>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Phazon>())
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 26)
                .AddIngredient(ModContent.ItemType<CoreofEternal>(), 30)
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>())
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, 0);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 6 + Main.rand.Next(4);
            float rotation = MathHelper.ToRadians(15);

            position += Vector2.Normalize(velocity) * 15f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                if (Main.rand.NextBool(4))
                {
                    Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                    Projectile.NewProjectile(source, position, perturbedSpeed, ModContent.ProjectileType<PhazonEXProjectile>(), damage, knockback, player.whoAmI);
                }
                else
                {
                    Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-360, 360), Main.rand.Next(-360, 360)), velocity, ModContent.ProjectileType<PhazonEXProjectile>(), damage, knockback, player.whoAmI);
                }
            }

            return false;
        }
    }
}
