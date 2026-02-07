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
    public class Bowpocalypse : ModItem
    {
        private int arrowTypeIndex;

        private static readonly int[] possibleArrows =
        {
            ProjectileID.WoodenArrowFriendly,
            ProjectileID.BeeArrow,
            ProjectileID.BloodArrow,
            ProjectileID.BoneArrow,
            ProjectileID.ChlorophyteArrow,
            ProjectileID.CursedArrow,
            ProjectileID.DD2BetsyArrow,
            ProjectileID.FireArrow,
            ProjectileID.FrostArrow,
            ProjectileID.FrostburnArrow,
            ProjectileID.HellfireArrow,
            ProjectileID.HolyArrow,
            ProjectileID.IchorArrow,
            ProjectileID.JestersArrow,
            ProjectileID.MoonlordArrow,
            ProjectileID.PhantasmArrow,
            ProjectileID.ShadowFlameArrow,
            ProjectileID.ShimmerArrow,
            ProjectileID.UnholyArrow,
            ProjectileID.VenomArrow,
            ModContent.ProjectileType<EmberArrowProjectile>(),
            ModContent.ProjectileType<StarbornArrowProjectile>(),
            ModContent.ProjectileType<ArkiumShard>(),
            ModContent.ProjectileType<UnstabowProjectile>()
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
            Item.useTime = 18;
            Item.useAnimation = 18;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = SoundID.Item5;
            Item.autoReuse = true;
            Item.shootSpeed = 9.5f;
            Item.shoot = AmmoID.Arrow;
            Item.useAmmo = AmmoID.Arrow;
            Item.rare = ModContent.RarityType<Aquamarine>();
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 4)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 6)
                .AddIngredient(ModContent.ItemType<ArkiumManbow>())
                .AddIngredient(ModContent.ItemType<CosmicSwiftShot>())
                .AddIngredient(ModContent.ItemType<Unstabow>())
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
        }

        public override void NetSend(BinaryWriter writer)
        {
            writer.Write((byte)arrowTypeIndex);
        }

        public override void NetReceive(BinaryReader reader)
        {
            arrowTypeIndex = reader.ReadByte();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            arrowTypeIndex = Main.rand.Next(possibleArrows.Length);

            int spread = 15;
            float spreadMult = 0.5f;

            for (int i = 0; i < 5; i++)
            {
                float vX = velocity.X + Main.rand.Next(-spread, spread + 1) * spreadMult;
                float vY = velocity.Y + Main.rand.Next(-spread, spread + 1) * spreadMult;

                Projectile.NewProjectile(source, position, new Vector2(vX, vY), possibleArrows[arrowTypeIndex], damage, knockback);
            }

            return false;
        }
    }
}
