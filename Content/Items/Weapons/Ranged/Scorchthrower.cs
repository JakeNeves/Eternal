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
    public class Scorchthrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a long superheated flame bar" +
                             "\n'KILL IT WITH FIRE!'");
        }

        public override void SetDefaults()
        {
            Item.damage = 660;
            Item.DamageType = DamageClass.Ranged;
            Item.width = 64;
            Item.height = 22;
            Item.useTime = 8;
            Item.useAnimation = 16;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.noMelee = true;
            Item.knockBack = 4;
            Item.value = Item.sellPrice(platinum: 2, gold: 36);
            Item.rare = ModContent.RarityType<Teal>();
            Item.UseSound = SoundID.Item34;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<ScorchthrowerFlame>();
            Item.shootSpeed = 10f;
            Item.useAmmo = AmmoID.Gel;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.Flamethrower)
                .AddIngredient(ItemID.LunarBar, 16)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 12)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }

        public override bool CanUseItem(Player player)
        {
            return !player.wet;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }
    }
}
