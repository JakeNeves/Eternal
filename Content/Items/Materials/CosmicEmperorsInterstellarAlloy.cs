using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Materials
{
    public class CosmicEmperorsInterstellarAlloy : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 11;
            Item.height = 28;
            Item.rare = ModContent.RarityType<Aquamarine>();
        }
    }
}
