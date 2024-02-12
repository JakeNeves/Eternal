using Eternal.Content.Buffs;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Potions
{
    public class InstabilityWardPotion : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 30;
        }

        public override void SetDefaults()
        {
            Item.width = 16;
            Item.height = 26;
            Item.useStyle = ItemUseStyleID.DrinkLiquid;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.rare = ModContent.RarityType<Teal>();
            Item.value = Item.sellPrice(gold: 16);
            Item.buffType = ModContent.BuffType<InstabilityWard>();
            Item.buffTime = 51800;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MoteofOminite>())
                .AddIngredient(ModContent.ItemType<Astragel>(), 2)
                .AddIngredient(ItemID.BottledWater)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
