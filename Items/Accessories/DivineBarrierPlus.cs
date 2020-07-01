using Eternal.Tiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Accessories
{
    [AutoloadEquip(EquipType.Shield)]
    class DivineBarrierPlus : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Divine Barrier+");

            Tooltip.SetDefault("Provides a barrier both in front and behind you. \nEnhanced with a Spiritual Gem which allows you to dash.");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = Item.buyPrice(gold: 15, silver: 75);
            item.rare = 8;
            item.accessory = true;
            item.defense = 50;
            item.lifeRegen = 20;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            EternalDashPlayer mp = player.GetModPlayer<EternalDashPlayer>();

            if(!mp.DashActive)
                return;

            player.eocDash = mp.DashTimer;
            player.armorEffectDrawShadowEOCShield = true;

            if(mp.DashTimer == EternalDashPlayer.MAX_DASH_TIMER)
            {
                Vector2 newVelocity = player.velocity;

                if ((mp.DashDir == EternalDashPlayer.DashUp && player.velocity.Y > -mp.DashVelocity) || (mp.DashDir == EternalDashPlayer.DashDown && player.velocity.Y < mp.DashVelocity))
                {
                    float dashDirection = mp.DashDir == EternalDashPlayer.DashDown ? 1 : -1.3f;
                    newVelocity.Y = dashDirection * mp.DashVelocity;
                }
                else if ((mp.DashDir == EternalDashPlayer.DashLeft && player.velocity.X > -mp.DashVelocity) || (mp.DashDir == EternalDashPlayer.DashRight && player.velocity.X < mp.DashVelocity))
                {
                    int dashDirection = mp.DashDir == EternalDashPlayer.DashRight ? 1 : -1;
                    newVelocity.X = dashDirection * mp.DashVelocity;
                }

                player.velocity = newVelocity;
            }

            mp.DashTimer--;
            mp.DashDelay--;

            if(mp.DashDelay == 0)
            {
                mp.DashDelay = EternalDashPlayer.MAX_DASH_DELAY;
                mp.DashTimer = EternalDashPlayer.MAX_DASH_TIMER;
                mp.DashActive = false;
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<SpiritualBeacon>());
            recipe.AddIngredient(ItemType<DivineBarrier>());
            recipe.AddIngredient(ItemType<SpiritualGem>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }

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

                if (item.type == ItemType<DivineBarrierPlus>())
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
