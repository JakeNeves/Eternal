using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class HellWheel : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hell Wheel");
            Tooltip.SetDefault("\nAllows you to travel at blazing speeds." +
                             "\n'It's said the halo that surrounds the reader blesses them with such speed...'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 40;
            Item.rare = ModContent.RarityType<HellMode>();
            Item.value = Item.sellPrice(gold: 60);
        }

        public override void HoldItem(Player player)
        {
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                int maxdusts = 8;
                for (int i = 0; i < maxdusts; i++)
                {
                    float dustDistance = 25;
                    float dustSpeed = 12;
                    Vector2 offset = Vector2.UnitX.RotateRandom(MathHelper.Pi) * dustDistance;
                    Vector2 velocity = -offset.SafeNormalize(-Vector2.UnitY) * dustSpeed;
                    Dust vortex = Dust.NewDustPerfect(new Vector2(player.Center.X, player.Center.Y) + offset, DustID.BlueTorch, velocity, 0, default(Color), 1.5f);
                    vortex.noGravity = true;
                }
            }

            for (int i = 0; i < 10; i++)
            {
                player.velocity.X *= 1.01f;
                player.moveSpeed *= 2f;
                player.maxRunSpeed *= 2f;
            }
        }
    }
}
