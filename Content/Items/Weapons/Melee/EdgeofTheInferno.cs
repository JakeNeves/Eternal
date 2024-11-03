using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class EdgeofTheInferno : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Edge of the Inferno");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 70;
            Item.height = 70;
            Item.damage = 440;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<EdgeofTheInfernoProjectile>();
            Item.shootSpeed = 30f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<EmpoweredMalachiteEdge>())
                .AddIngredient(ModContent.ItemType<IgniumBar>(), 45)
                .AddIngredient(ItemID.HellstoneBar, 20)
                .AddTile(ModContent.TileType<AncientFoundry>())
                .Register();
        }
    }
}
