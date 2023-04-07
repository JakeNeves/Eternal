using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Vanity
{
    public class CursorofTheCosmos : ModItem
    {
        public override LocalizedText DisplayName => base.DisplayName.WithFormatArgs("Cursor of the Cosmos");
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Causes your mouse cursor to shift from various shades of purple");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.accessory = true;
            Item.vanity = true;
        }

        public override void UpdateVanity(Player player)
        {
            AccessorySystem.hasCursorofTheCosmos = true;
        }
    }
}
