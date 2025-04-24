using Eternal.Content.Items.Materials;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class MausoleumKey : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 10;
            Item.height = 20;
            Item.rare = ItemRarityID.Pink;
            Item.value = Item.sellPrice(gold: 10);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<MausoleumKeyFragment1>())
                .AddIngredient(ModContent.ItemType<MausoleumKeyFragment2>())
                .Register();
        }
    }
}
