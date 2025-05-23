﻿using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Misc
{
    public class OneHitObliterator : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("One-Hit Obliterator");
            /* Tooltip.SetDefault("[c/FF0000:Cheat Item]" +
                "\nCan manipulate anything in a single swing" +
                "\nAutomatically drains your health to 1 HP"); */
        }

        public override void SetDefaults()
        {
            Item.damage = 1;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = 0;
            Item.rare = ItemRarityID.Expert;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
        }

        public override void HoldItem(Player player)
        {
            // if (!player.active || player.dead)
            //     player.statLife = 1;

            player.statLife = 1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            // target.life = 1;
            // player.ApplyDamageToNPC(target, 9999, 0f, 0, false);

            if (Main.netMode != NetmodeID.MultiplayerClient)
                target.StrikeInstantKill();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            int dmg = tooltips.FindIndex(x => x.Name == "Damage");
            tooltips.RemoveAt(dmg);
            tooltips.Insert(dmg, new TooltipLine(Mod, "Damage", "Infinite damage"));
        }
    }
}
