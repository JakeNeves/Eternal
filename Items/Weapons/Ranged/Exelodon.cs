using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class Exelodon : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires fourteen bullets at once\nIt's a little op for an endgame weapon but it does the job...\n'The Megalodon's long lost equivilent...'");
        }

        public override void SetDefaults()
        {
            item.width = 72;
            item.height = 40;
            item.damage = 20000;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 1;
            item.useAnimation = 18;
            item.reuseDelay = 14;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shootSpeed = 9.75f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
        }

        public override bool ConsumeAmmo(Player player)
        {
            return !(player.itemAnimation < item.useAnimation - 2);
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(30, Main.DiscoG, Main.DiscoB);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ModContent.ItemType<Astragel>(), 30);
            recipe.AddIngredient(ModContent.ItemType<SignalumBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 200);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ModContent.ItemType<NovaNexus>());
            recipe.AddIngredient(ModContent.ItemType<CosmicSwiftShot>());
            recipe.AddIngredient(ModContent.ItemType<StormBeholder>());
            recipe.AddIngredient(ModContent.ItemType<CoreofEternal>(), 3);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float numberProjectiles = 2 + Main.rand.Next(6);

            float rotation = MathHelper.ToRadians(15);

            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 70f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }

            for (int i = 0; i < numberProjectiles; i++)
            {
                //Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1)));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
