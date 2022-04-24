using Eternal.Items.Materials;
using Eternal.Items.Tools;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.BossBags
{
    public class CosmicEmperorCapsule : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Capsule");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 36;
            item.height = 30;
            item.rare = -12;
            item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            if (Main.rand.Next(1) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<ExosiivaGladiusBlade>());
            }
            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<TheBigOne>());
            }
            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<Exelodon>());
            }
            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<FinalBossBlade>());
            }

            player.QuickSpawnItem(ModContent.ItemType<InterstellarMetal>(), Main.rand.Next(60, 90));
            player.QuickSpawnItem(ModContent.ItemType<CosmoniumFragment>(), Main.rand.Next(60, 90));
        }

        public override int BossBagNPC => ModContent.NPCType<NPCs.Boss.CosmicEmperor.CosmicEmperorP3>();
    }
}
