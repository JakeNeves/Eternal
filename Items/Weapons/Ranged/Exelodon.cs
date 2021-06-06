using Eternal.Projectiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Eternal.Tiles;
using Eternal.Items.Materials;

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
            item.damage = 10000;
            item.noMelee = true;
            item.ranged = true;
            item.useTime = 7;
            item.useAnimation = 7;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.UseSound = SoundID.NPCHit41; //mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/Custom/NovaNexus");
            item.autoReuse = true;
            item.shootSpeed = 9.75f;
            item.shoot = AmmoID.Bullet;
            item.useAmmo = AmmoID.Bullet;
            item.rare = ItemRarityID.Red;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            tooltips[0].overrideColor = new Color(30, Main.DiscoG, Main.DiscoB);
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(TileType<AncientForge>());
            recipe.AddIngredient(ItemType<StellarAlloy>(), 5);
            recipe.AddIngredient(ItemType<Astragel>(), 30);
            recipe.AddIngredient(ItemType<SignalumBar>(), 10);
            recipe.AddIngredient(ItemType<StarmetalBar>(), 200);
            recipe.AddIngredient(ItemType<CometiteBar>(), 15);
            recipe.AddIngredient(ItemType<CosmoniumFragment>());
            recipe.AddIngredient(ItemType<NovaNexus>());
            recipe.AddIngredient(ItemType<CosmicSwiftShot>());
            recipe.AddIngredient(ItemType<StormBeholder>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 14;
            for (int j = 0; j < numberProjectiles; j++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(15));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            return false;
        }

    }
}
