using Eternal.Projectiles.Weapons.Melee;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Hell
{
    public class SkeletronJawbone : ModItem
    {

        public override void SetStaticDefaults()
        {
	    DisplayName.SetDefault("Skeletron's Jawbone");
            Tooltip.SetDefault("Fires Bone Shards\n<right> to trigger Skeletron's Fury\n'the spirit of skeletron empowers you...'");
        }

        public override void SetDefaults()
        {
            item.width = 32;
            item.height = 28;
            item.damage = 30;
            item.melee = true;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.knockBack = 2.3f;
            item.rare = ItemRarityID.Red;
            item.value = Item.sellPrice(gold: 8);
            item.shoot = ModContent.ProjectileType<BoneShard>();
            item.shootSpeed = 8f;
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Hell;
                }
            }
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                item.useStyle = ItemUseStyleID.HoldingUp;
                item.UseSound = SoundID.DD2_MonkStaffGroundImpact;
                item.useTime = 75;
                item.useAnimation = 75;
                item.shoot = ProjectileID.None;
                item.shootSpeed = 0f;
                item.noMelee = true;
                item.autoReuse = false;
                item.buffType = ModContent.BuffType<Buffs.SkeletronFury>();
                item.buffTime = 2020;
            }
            else
            {
                item.useStyle = ItemUseStyleID.SwingThrow;
                item.UseSound = SoundID.Item1;
                item.shoot = ModContent.ProjectileType<BoneShard>();
                item.shootSpeed = 8f;
                item.useTime = 15;
                item.useAnimation = 15;
                item.noMelee = false;
                item.autoReuse = true;
                item.buffType = 0;
                item.buffTime = 0;
            }
            return true;

        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = Main.rand.Next(2, 6);
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<BoneShard>(), damage, knockBack, player.whoAmI);
            }
            return true;
        }

    }
}
