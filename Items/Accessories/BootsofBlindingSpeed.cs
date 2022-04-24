using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Accessories
{
    public class BootsofBlindingSpeed : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Boots of Blinding Speed");
            Tooltip.SetDefault("The wearer can run fast at the cost of obstructed view" +
                "\n'I can't see!'");
        }

        public override void SetDefaults()
        {
            item.width = 22;
            item.height = 30;
            item.value = Item.sellPrice(gold: 3, silver: 30);
            item.rare = ItemRarityID.Green;
            item.accessory = true;
            item.defense = 1;
        }

        public override bool CanEquipAccessory(Player player, int slot)
        {
            if (slot < 10)
            {
                int maxAccessoryIndex = 5 + player.extraAccessorySlots;
                for (int i = 3; i < +maxAccessoryIndex; i++)
                {
                    if (slot != i && (player.armor[i].type == ModContent.ItemType<BlackLantern>()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += 0.8f;
            player.accRunSpeed = 8.3f;

            player.AddBuff(BuffID.Obstructed, 1);
        }
    }
}
