using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Legs)]
    public class SanguineGreaves : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 18;
            Item.value = Item.sellPrice(gold: 10);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 12;
        }

        public override void UpdateEquip(Player player)
        {
            player.moveSpeed += 0.05f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MagmaticAlloy>(), 8)
                .AddIngredient(ModContent.ItemType<CoagulatedBlood>(), 10)
                .AddTile(TileID.MythrilAnvil)
                .Register();
        }
    }
}
