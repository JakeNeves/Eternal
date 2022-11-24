using Eternal.Content.Rarities;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Materials
{
    public class CosmoniumFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("One is required for every known powerful item" +
                             "\n'A shard of the emperor's promise'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 100;

            ItemID.Sets.ItemNoGravity[Item.type] = true;

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(8, 6));
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 20;
            Item.value = Item.buyPrice(platinum: 1, gold: 25, silver: 50);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.maxStack = 999;
        }
    }
}
