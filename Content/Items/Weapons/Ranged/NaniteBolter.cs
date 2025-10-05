using Eternal.Content.Items.Ammo;
using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Ranged;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Ranged
{
    public class NaniteBolter : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 58;
            Item.height = 22;
            Item.damage = 600;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/NaniteBolter");
            Item.autoReuse = true;
            Item.shootSpeed = 5f;
            Item.shoot = ModContent.ProjectileType<OminiteNaniteProjectile>();
            Item.useAmmo = ModContent.ItemType<OminiteNanites>();
            Item.rare = ModContent.RarityType<Maroon>();
            Item.knockBack = 2f;
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            float numberProjectiles = 2 + Main.rand.Next(6);
            float rotation = MathHelper.ToRadians(45);

            Vector2 muzzleOffset = Vector2.Normalize(velocity) * 25f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            position += Vector2.Normalize(velocity) * Main.rand.NextFloat(15f, 45f);

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            return true;
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(2, -1);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 30)
                .AddIngredient(ModContent.ItemType<ShiftblightAmethyst>(), 6)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
