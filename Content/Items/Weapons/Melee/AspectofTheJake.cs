using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class AspectofTheJake : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Aspect of the Jake");

            /* Tooltip.SetDefault("Inflicts confusion upon striking an enemy" +
                             "\n'The power of Jake is within you!'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 46;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Blue;
            Item.damage = 10;
            Item.useAnimation = 18;
            Item.useTime = 18;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1.75f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.crit = 75;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            target.AddBuff(BuffID.Confused, 1500);

            for (int k = 0; k < 8; k++)
            {
                int dust = Dust.NewDust(target.position, target.width, target.height, DustID.RedTorch, target.oldVelocity.X * 1f, target.oldVelocity.Y * 1f);
            }
        }
    }
}
