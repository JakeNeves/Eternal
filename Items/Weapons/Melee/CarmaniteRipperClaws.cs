﻿using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    public class CarmaniteRipperClaws : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 20;
            item.melee = true;
            item.rare = ItemRarityID.Green;
            item.damage = 20;
            item.useAnimation = 8;
            item.useTime = 8;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.4f;
            item.autoReuse = true;
            item.UseSound = SoundID.Item1;
        }
    }
}
