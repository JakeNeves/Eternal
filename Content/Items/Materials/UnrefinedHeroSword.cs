using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;

namespace Eternal.Content.Items.Materials
{
    public class UnrefinedHeroSword : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("[c/F72F9A:Guaranteed Rare Boss Drop]" + 
                             "\n'It looks rather rough, yet it's valuable, however something seems to be missing here'" +
                             "\n[c/008060:Ancient Artifact]" +
                             "\nWhatever this is, it remains unknown...");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 54;
            Item.height = 54;
            Item.rare = ItemRarityID.Gray;
            Item.value = Item.sellPrice(gold: 90);
        }
    }
}
