using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using System.IO;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class Machinegunpocalypse : ModItem
    {
        private int bulletTypeIndex;

        private static readonly int[] possibleBullets =
        {
            ProjectileID.Bullet,
            ProjectileID.BulletHighVelocity,
            ProjectileID.ChlorophyteBullet,
            ProjectileID.CrystalBullet,
            ProjectileID.CursedBullet,
            ProjectileID.ExplosiveBullet,
            ProjectileID.GoldenBullet,
            ProjectileID.IchorBullet,
            ProjectileID.MoonlordBullet,
            ProjectileID.NanoBullet,
            ProjectileID.PartyBullet,
            ProjectileID.SilverBullet,
            ProjectileID.VenomBullet,
            ProjectileID.SniperBullet,
            ModContent.ProjectileType<BasaltShellProjectile>(),
            ModContent.ProjectileType<StarbulletProjectile>(),
            ModContent.ProjectileType<ArcaneShellProjectile>()
        };

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 78;
            Item.damage = 250;
            Item.knockBack = 2.6f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item36;
            Item.autoReuse = true;
            Item.shootSpeed = 9.5f;
            Item.shoot = AmmoID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<Aquamarine>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 4)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 6)
                .AddIngredient(ModContent.ItemType<EternalDeadEye>())
                .AddIngredient(ModContent.ItemType<TotalStarstorm>())
                .AddIngredient(ModContent.ItemType<Meganovae>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write((byte)bulletTypeIndex);
        }

        public override void NetReceive(BinaryReader reader)
        {
            bulletTypeIndex = reader.ReadByte();
        }

        public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            bulletTypeIndex = Main.rand.Next(possibleBullets.Length);

            int spread = 15;
            float spreadMult = 0.5f;

            for (int i = 0; i < 5; i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), possibleBullets[bulletTypeIndex], damage, knockback);
            }

            return false;
        }
    }
}
