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

    
}
