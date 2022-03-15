using Eternal.Projectiles.Weapons.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Magic
{
    public class CrystalBarrage : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires multiple colored projectiles" + 
                              "\n'Fire the rainbow, taste the rainbow'");
            Item.staff[item.type] = true;
        }

        public override void SetDefaults()
        {
            item.damage = 60;
            item.magic = true;
            item.mana = 6;
            item.width = 30;
            item.height = 30;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 1.5f;
            item.value = Item.sellPrice(gold: 6);
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.shootSpeed = 20f;
            item.shoot = ProjectileID.DiamondBolt;
            item.UseSound = SoundID.Item8;
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX + 4, speedY + 4, ProjectileID.RubyBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX + 8, speedY + 8, ProjectileID.AmberBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX + 12, speedY + 12, ProjectileID.TopazBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX - 12, speedY - 12, ProjectileID.EmeraldBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX - 8, speedY - 8, ProjectileID.SapphireBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX - 4, speedY - 4, ProjectileID.AmethystBolt, damage, knockBack, player.whoAmI);
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, type, damage, knockBack, player.whoAmI);

            return false;

        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.AddRecipeGroup("Eternal:GemStaff");
            recipe.SetResult(ItemID.HallowedBar, 6);
            recipe.AddRecipe();
        }

    }
}
