using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class NaquadahChestplate : ModItem
    {

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 26;
            Item.value = Item.sellPrice(platinum: 30);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.defense = 50;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 200;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 16)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 32)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
