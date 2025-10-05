using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Nekodisk : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 48;
            Item.height = 48;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Maroon>();
            Item.noMelee = true;
            Item.damage = 1000;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<NekodiskProjectile>();
            Item.shootSpeed = 16f;
            Item.noUseGraphic = true;
            Item.knockBack = 3f;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>())
                .AddIngredient(ModContent.ItemType<Nyanarang>())
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 12)
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 8)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
