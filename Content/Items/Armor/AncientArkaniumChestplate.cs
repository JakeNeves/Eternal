using Eternal.Content.Rarities;
using Eternal.Content.Items.Materials;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Body)]
    public class AncientArkaniumChestplate : ModItem
    {

        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("+160 max life");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 22;
            Item.value = Item.sellPrice(platinum: 2);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 24;
        }

        public override void UpdateEquip(Player player)
        {
            player.statLifeMax2 += 80;
        }
    }
}
