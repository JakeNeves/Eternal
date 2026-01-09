using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class MorbusLeggings : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 12;
            Item.value = Item.sellPrice(gold: 3);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 10;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BlightedFarrago>(), 16)
                .AddIngredient(ModContent.ItemType<NecroticTissue>(), 12)
                .AddIngredient(ModContent.ItemType<PolypChunk>(), 12)
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 6)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
