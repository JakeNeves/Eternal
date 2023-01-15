using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Projectiles.Weapons.Magic;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Magic
{
    public class StarstaveZwei : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Starstave Zwei");
            Tooltip.SetDefault("Fires multiple Starstave Bolts");
            Item.staff[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.damage = 300;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 12;
            Item.knockBack = 4f;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.shoot = ModContent.ProjectileType<StarstaveProjectile>();
            Item.shootSpeed = 8f;
            Item.UseSound = SoundID.Item8;
            Item.rare = ModContent.RarityType<Teal>();
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarstaveEin>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 6)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 8)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 10)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 10)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 2 + Main.rand.Next(4); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-64, 64), Main.rand.Next(-64, 64)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
