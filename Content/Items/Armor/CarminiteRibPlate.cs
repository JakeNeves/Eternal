using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class CarminiteRibPlate : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 20;
            Item.value = Item.sellPrice(silver: 60);
            Item.rare = ItemRarityID.Green;
            Item.defense = 8;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<Carminite>(), 12)
                .AddIngredient(ItemID.Bone, 24)
                .AddTile(TileID.Anvils)
                .Register();
        }
    }
}
