using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class MorbusRibPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 6);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<BlightedFarrago>(), 20)
                .AddIngredient(ModContent.ItemType<NecroticTissue>(), 16)
                .AddIngredient(ModContent.ItemType<PolypChunk>(), 16)
                .AddIngredient(ModContent.ItemType<SoulofBlight>(), 8)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
