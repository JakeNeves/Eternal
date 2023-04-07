using Eternal.Content.Items.Materials;
using Eternal.Content.Projectiles.Weapons.Melee;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class ArkrystalBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            /* Tooltip.SetDefault("<right> for True Melee" +
                             "\n'Formally known as The Imperious Cohort...'"); */

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 68;
            Item.damage = 600;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 5;
            Item.useAnimation = 26;
            Item.useTime = 26;
            Item.shoot = ModContent.ProjectileType<ArkrystalBolt>();
            Item.shootSpeed = 18.2f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Magenta>();
            Item.autoReuse = true;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 0f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<ArkrystalBolt>();
                Item.shootSpeed = 18.2f;
            }
            return base.CanUseItem(player);
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 48)
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 30)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 4)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            Vector2 target = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
            float ceilingLimit = target.Y;
            if (ceilingLimit > player.Center.Y - 200f)
            {
                ceilingLimit = player.Center.Y - 200f;
            }

            for (int i = 0; i < 2; i++)
            {
                position = player.Center - new Vector2(Main.rand.NextFloat(401) * player.direction, 600f);
                position.Y -= 100 * i;
                Vector2 heading = target - position;

                if (heading.Y < 0f)
                {
                    heading.Y *= -1f;
                }

                if (heading.Y < 20f)
                {
                    heading.Y = 20f;
                }

                heading.Normalize();
                heading *= velocity.Length();
                heading.Y += Main.rand.Next(-40, 41) * 0.02f;
                Projectile.NewProjectile(source, position, heading, type, damage * 2, knockback, player.whoAmI, 0f, ceilingLimit);
            }

            for (int i = 0; i < 4; i++)
            {
                Projectile.NewProjectile(source, player.Center.X + Main.rand.NextFloat(-36, 36), player.Center.Y + Main.rand.NextFloat(-36, 36), velocity.X, velocity.Y, type, damage, knockback, player.whoAmI);
            }

            return false;
        }

    }
}
