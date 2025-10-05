using Eternal.Content.Items.Materials;
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
    public class DX7060 : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("DX-7060");
            // Tooltip.SetDefault("'You have no ideas what it does!'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 74;
            Item.height = 50;
            Item.damage = 1000;
            Item.knockBack = 4f;
            Item.noMelee = true;
            Item.DamageType = DamageClass.Ranged;
            Item.useTime = 5;
            Item.useAnimation = 5;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/DX7060Fire");
            Item.autoReuse = true;
            Item.shootSpeed = 24f;
            Item.shoot = AmmoID.Bullet;
            Item.useAmmo = AmmoID.Bullet;
            Item.rare = ModContent.RarityType<Aquamarine>();
        }

        public override Vector2? HoldoutOffset()
        {
            return new Vector2(1, 0);
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
            float numberProjectiles = 2;
            float rotation = MathHelper.ToRadians(Main.rand.NextFloat(15f, 45f));

            position += Vector2.Normalize(velocity) * 15f;

            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f;
                Projectile.NewProjectile(source, position, perturbedSpeed, type, damage, knockback, player.whoAmI);
            }

            for (int i = 0; i < numberProjectiles + Main.rand.Next(4); i++)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-120, 120), Main.rand.Next(-120, 120)), velocity, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EternalDeadEye>())
                .AddIngredient(ModContent.ItemType<SebastionsCyberChassis>())
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 6)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 12)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 20)
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 20)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
