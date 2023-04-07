using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Phantasma : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("Can phase through tiles");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.width = 24;
            Item.height = 20;
            Item.useAnimation = 25;
            Item.useTime = 25;
            Item.shootSpeed = 30f;
            Item.knockBack = 9f;
            Item.damage = 300;
            Item.rare = ModContent.RarityType<Teal>();

            Item.DamageType = DamageClass.Melee;
            Item.channel = true;
            Item.noMelee = true;
            Item.noUseGraphic = true;

            Item.UseSound = SoundID.Item1;
            Item.value = Item.sellPrice(gold: 10);
            Item.shoot = ModContent.ProjectileType<PhantasmaProjectile>();
        }

        private static readonly int[] unwantedPrefixes = new int[] { PrefixID.Terrible, PrefixID.Dull, PrefixID.Annoying, PrefixID.Broken, PrefixID.Damaged, PrefixID.Shoddy };
    }
}
