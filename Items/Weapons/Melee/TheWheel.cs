using Eternal.Projectiles.Weapons.Melee;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;

namespace Eternal.Items.Weapons.Melee
{
    public class TheWheel : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Grinding it wheely good with this yo-yo interpitation of XR-2006'");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 24;
            item.height = 20;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 15f;
            item.knockBack = 2.6f;
            item.damage = 230;
            item.rare = ItemRarityID.Red;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(gold: 30);
            item.shoot = ModContent.ProjectileType<TheWheelProjectile>();
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = EternalColor.Magenta;
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