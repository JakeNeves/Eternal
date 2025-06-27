using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class EternalDeadEye : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 60;
            Item.height = 32;
            Item.damage = 310;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 12;
            Item.useAnimation = 12;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/MeganovaeFire");
            Item.autoReuse = true;
            Item.shootSpeed = 12f;
            Item.shoot = AmmoID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.knockBack = 2f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 4;

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            if (Main.rand.NextBool(75))
            {
                for (int i = 0; i < numberProjectiles; i++)
                {
                    Projectile.NewProjectile(source, player.position.X - 130, player.position.Y + Main.rand.Next(-10, 10), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                    Projectile.NewProjectile(source, player.position.X + 130, player.position.Y + Main.rand.Next(-10, 10), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Meganovae>())
                .AddIngredient(ModContent.ItemType<TotalStarstorm>())
                .AddIngredient(ModContent.ItemType<ProviditeBar>(), 12)
                .AddIngredient(ModContent.ItemType<CoreofExodus>(), 6)
                .AddIngredient(ModContent.ItemType<MindCrystalCluster>())
                .AddIngredient(ModContent.ItemType<BodyCrystalCluster>())
                .AddIngredient(ModContent.ItemType<SoulCrystalCluster>())
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, -1);
        }
    }
}
