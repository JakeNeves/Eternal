using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class EmpoweredApparitionalDisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 78;
            Item.height = 78;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.noMelee = true;
            Item.damage = 440;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<EmpoweredApparitionalDiskProjectile>();
            Item.shootSpeed = 9f;
            Item.noUseGraphic = true;
            Item.knockBack = 3f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ApparitionalDisk>())
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 12)
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 16)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 20)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 3; ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-128, 128), Main.rand.Next(-128, 128)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return true;
        }
    }
}
