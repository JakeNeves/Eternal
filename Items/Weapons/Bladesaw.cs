using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class Bladesaw : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cuts into enimies very heavily... \nHas a chance to confuse enimies as well.");
        }

        public override void SetDefaults()
        {
            item.width = 48;
            item.height = 48;
            item.damage = 75;
            item.useTime = 15;
            item.useAnimation = 15;
            item.knockBack = 25;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useStyle = 1;
            item.UseSound = SoundID.Item23;
            item.autoReuse = false;
            item.rare = 6;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Bleeding, 120);
            target.AddBuff(BuffID.Confused, 120);
        }
    }
}
