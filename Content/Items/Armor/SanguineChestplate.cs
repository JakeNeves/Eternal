using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class SanguineChestplate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 36;
            Item.height = 28;
            Item.value = Item.sellPrice(gold: 20);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 20;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MagmaticAlloy>(), 12)
                .AddIngredient(ModContent.ItemType<CoagulatedBlood>(), 20)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
