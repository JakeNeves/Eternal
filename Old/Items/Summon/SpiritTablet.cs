using Eternal.NPCs;
using Eternal.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Summon
{
    public class SpiritTablet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Calls upon an unknown entity\n'something watches you from deep below'");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 28;
            item.maxStack = 99;
            item.rare = ItemRarityID.Gray;
            item.useAnimation = 45;
            item.useTime = 45;
            item.UseSound = SoundID.Item44;
            item.useStyle = ItemUseStyleID.HoldingUp;
            item.shoot = ProjectileType<ShadowSpawn>();
            item.consumable = true;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ZoneRockLayerHeight && !NPC.AnyNPCs(NPCType<ShadowMonster>());
        }

        public override bool UseItem(Player player)
        {
            if (!player.ZoneRockLayerHeight)
            {
                Main.NewText("The call is too weak, a voice says to use it in the caverns deep below", 50, 255, 130);
            }
            return true;
        }
    }
}
