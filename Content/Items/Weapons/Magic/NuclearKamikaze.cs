using Eternal.Content.Items.Materials;
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
    public class NuclearKamikaze : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nuclear Kamikaze");
            /* Tooltip.SetDefault("Casts fast moving razorblades, similar to the Razorblade Typhoon" +
                             "\n'Not to be confused with the Nuclear Fury...'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 28;
            Item.damage = 300;
            Item.DamageType = DamageClass.Magic;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.mana = 30;
            Item.knockBack = 8f;
            Item.useAnimation = 4;
            Item.useTime = 4;
            Item.shoot = ModContent.ProjectileType<NuclearKamikazeProjectile>();
            Item.shootSpeed = 24f;
            Item.UseSound = SoundID.Item84;
            Item.rare = ModContent.RarityType<Teal>();
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<Starforge>())
                .AddIngredient(ItemID.RazorbladeTyphoon)
                .AddIngredient(ItemID.LunarBar, 12)
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 20)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 6)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 6)
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 10)
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            for (int i = 0; i < 6 + Main.rand.Next(6); ++i)
            {
                Projectile.NewProjectile(source, player.position + new Vector2(Main.rand.Next(-80, 80), Main.rand.Next(-80, 80)), velocity, type, damage, knockback, Main.myPlayer, 0f, 0f);
            }

            return false;
        }
    }
}
