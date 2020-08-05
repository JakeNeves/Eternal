using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ThunderduneHeadgear : ModItem
    {
        public override void SetStaticDefaults() {
            Tooltip.SetDefault("Grants Immunity to Electrocution... \nThis thunderdune made helmet dates back to the ancient kahogaus, a cult of thunderdune...");
        }

        public override void SetDefaults() {
            item.width = 20;
            item.height = 24;
            item.value = Item.buyPrice(gold: 75, platinum: 5);
            item.rare = ItemRarityID.Orange;
            item.defense = 20;
        }

        public override void UpdateEquip(Player player) {
            player.buffImmune[BuffID.Electrified] = true;
        }
    }
}