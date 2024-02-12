using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class ThunderblightJade : ModItem
    {
        public override void SetStaticDefaults()
        {
            ItemID.Sets.ItemNoGravity[Item.type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.buyPrice(platinum: 1, gold: 30);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.maxStack = 9999;
        }

        public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
        {
            Lighting.AddLight(Item.Center, 2.15f, 2.45f, 1.05f);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .AddIngredient(ModContent.ItemType<ThunderblightShard>(), 4)
                .AddIngredient(ModContent.ItemType<ElectriteBar>(), 12)
                .Register();
        }
    }
}
