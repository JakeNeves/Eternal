using Eternal.Projectiles.Weapons.Melee;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Melee
{
    class Wasteland : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("'Electrically electrifying!'");
        }

        public override void SetDefaults()
        {
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.width = 24;
            item.height = 20;
            item.useAnimation = 25;
            item.useTime = 25;
            item.shootSpeed = 30f;
            item.knockBack = 5.5f;
            item.damage = 60;
            item.rare = ItemRarityID.Orange;

            item.melee = true;
            item.channel = true;
            item.noMelee = true;
            item.noUseGraphic = true;

            item.UseSound = SoundID.Item1;
            item.value = Item.sellPrice(silver: 10);
            item.shoot = ProjectileType<WastelandProjectile>();
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

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 120);
        }
    }
}
