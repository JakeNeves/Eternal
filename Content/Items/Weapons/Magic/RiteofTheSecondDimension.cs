using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class RiteofTheSecondDimension : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 800;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 40;
            Item.knockBack = 6f;
            Item.useAnimation = 8;
            Item.useTime = 8;
            Item.shoot = ModContent.ProjectileType<RoTSDProjectile>();
            Item.shootSpeed = 24f;
            Item.UseSound = new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/RoTSDShoot")
            {
                Volume = 0.25f,
                PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                MaxInstances = 0,
            };
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.autoReuse = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>(), 20)
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 10)
                .AddIngredient(ModContent.ItemType<OminaquaditeBar>(), 10)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3 + Main.rand.Next(5); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-100, 100), Main.rand.Next(-100, 100)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
