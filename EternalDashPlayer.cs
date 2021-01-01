using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Accessories;
using static Terraria.ModLoader.ModContent;
using Eternal.Items.Accessories.Expert;

namespace Eternal
{
    public class EternalDashPlayer : ModPlayer
    {
        public static readonly int DashDown = 0;
        public static readonly int DashUp = 1;
        public static readonly int DashRight = 2;
        public static readonly int DashLeft = 3;

        public int DashDir = -1;

        public bool DashActive = false;
        public int DashDelay = MAX_DASH_DELAY;
        public int DashTimer = MAX_DASH_TIMER;

        public readonly float DashVelocity = 15f;

        public static readonly int MAX_DASH_DELAY = 50;
        public static readonly int MAX_DASH_TIMER = 35;

        public override void ResetEffects()
        {
            bool dashAccessoryEquipped = false;

            for (int i = 3; i < 8 + player.extraAccessorySlots; i++)
            {
                Item item = player.armor[i];

                if (item.type == ItemType<CosmicStarstryderTreads>() || item.type == ItemType<FrostKingsCore>())
                    dashAccessoryEquipped = true;
                else if (item.type == ItemID.EoCShield || item.type == ItemID.MasterNinjaGear || item.type == ItemID.Tabi)
                    return;
            }

            if (!dashAccessoryEquipped || player.setSolar || player.mount.Active || DashActive)
                return;

            if (player.controlDown && player.releaseDown && player.doubleTapCardinalTimer[DashDown] < 15)
                DashDir = DashDown;
            else if (player.controlUp && player.releaseUp && player.doubleTapCardinalTimer[DashUp] < 15)
                DashDir = DashUp;
            else if (player.controlRight && player.releaseRight && player.doubleTapCardinalTimer[DashRight] < 15)
                DashDir = DashRight;
            else if (player.controlLeft && player.releaseLeft && player.doubleTapCardinalTimer[DashLeft] < 15)
                DashDir = DashLeft;
            else
                return;

            DashActive = true;

        }
    }
}
