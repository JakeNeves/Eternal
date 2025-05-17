using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Accessories.Vanity
{
    public class JakesCursor : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 60);
            Item.rare = ModContent.RarityType<FlamingBlue>();
            Item.accessory = true;
            Item.vanity = true;
        }

        public override void UpdateVanity(Player player)
        {
            AccessorySystem.hasJakesCursor = true;
        }
    }
}
