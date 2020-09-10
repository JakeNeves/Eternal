using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Projectiles.Minions;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons.Summon
{
    public class FrostyImmaterializer : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Creates cryonic energy from loose shards of ice\nEach energy takes up a quarter of a slot\nIt may also be a little op Pre-Moon Lord\n'Not a discount Cosmic Immaterializer I swear...'");
        }

        public override void SetDefaults()
        {
            item.width = 50;
            item.height = 44;
            item.damage = 190;
            item.mana = 30;
            item.knockBack = 9.5f;
            item.summon = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item44;
            item.useTime = 20;
            item.useAnimation = 20;
            item.autoReuse = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.value = Item.sellPrice(gold: 30);
            item.rare = ItemRarityID.Yellow;
            item.shoot = ProjectileType<CryonicEnergy>();
            item.shootSpeed = 5.75f;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, ProjectileType<CryonicEnergy>(), damage, knockBack, Main.myPlayer, 0f, 0f);
            return false;
        }

    }
}
