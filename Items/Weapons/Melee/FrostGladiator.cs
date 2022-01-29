using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    public class FrostGladiator : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Freezes enemies on hit!");
        }

        public override void SetDefaults()
        {
            item.width = 42;
            item.height = 48;
            item.damage = 95;
            item.knockBack = 18;
            item.value = Item.buyPrice(platinum: 1);
            item.useTime = 18;
            item.useAnimation = 18;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Yellow;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
            if (Main.rand.NextBool(3))
            {
                Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), hitbox.Width, hitbox.Height, 92);
            }
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Frostburn, 2400);
        }

    }
}
