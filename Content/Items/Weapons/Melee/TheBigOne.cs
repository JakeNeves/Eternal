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
    public class TheBigOne : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("'The mighty forest felling axe of eternity!'" +
                             "\n[c/FC036B:Dedicated Content]" +
                             "\nDedicated to [c/038CFC:Jaer Hend]"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 164;
            Item.height = 162;
            Item.damage = 1000;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.shoot = ModContent.ProjectileType<TheBigOneProjectile>();
            Item.shootSpeed = 6f;
            Item.UseSound = SoundID.Item71;
            Item.rare = ModContent.RarityType<Maroon>();
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<CosmoniumFragment>(), 6)
                .AddIngredient(ModContent.ItemType<InterstellarMetal>(), 12)
                .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 24)
                .AddIngredient(ModContent.ItemType<ApparitionalMatter>(), 16)
                .AddTile(ModContent.TileType<Nanoforge>())
                .Register();
        }
    }
}
