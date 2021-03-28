using Eternal.Items.Weapons.Expert;
using Eternal.Items.Weapons.Melee;
using Eternal.Items.Weapons.Ranged;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.BossBags
{
    class CarmaniteScouterBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");
        }

        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
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
                player.QuickSpawnItem(ItemType<CarmaniteBane>());
            }

            if (Main.rand.Next(2) == 0)
            {
                player.QuickSpawnItem(ItemType<CarmanitePurgatory>());
            }

            if (Main.rand.Next(3) == 0)
            {
                player.QuickSpawnItem(ItemType<CarmaniteRipperClaws>());
            }

            if (Main.rand.Next(4) == 0)
            {
                player.QuickSpawnItem(ItemType<CarmaniteInfestor>());
            }

            if (Main.rand.Next(5) == 0)
            {
                player.QuickSpawnItem(ItemType<CarmaniteDeadshot>());
            }

            player.QuickSpawnItem(ItemType<Bloodtooth>());

            player.QuickSpawnItem(ItemType<Carmanite>(), Main.rand.Next(25, 75));
        }

        public override int BossBagNPC => NPCType<NPCs.Boss.CarmaniteScouter.CarmaniteScouter>();
    }
}
