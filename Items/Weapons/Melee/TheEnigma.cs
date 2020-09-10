using Eternal.Items;
using Eternal.Projectiles;
using Eternal.Tiles;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Eternal.Items.Weapons.Melee
{
    class TheEnigma : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Someone Thought It looked like an Elemental Unleashed Weapon\n'Purium Yoyo Purium Yoyo'");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 20;
            item.height = 16;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 30f;
            item.knockBack = 5.5f;
            item.damage = 4000;
            item.rare = ItemRarityID.Red;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(platinum: 9);
            item.shoot = ProjectileType<TheEnigmaProjectile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(5, 35, 215);
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };

        public override bool AllowPrefix(int pre)
        {
            if (Array.IndexOf(unwantedPrefixes, pre) > -1)
            {
                return false;
            }
            return true;
        }
    }
}
