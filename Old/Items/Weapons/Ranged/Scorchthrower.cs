using Eternal.Items.Materials;
using Eternal.Items.Materials.Elementalblights;
using Eternal.Projectiles.Weapons.Ranged;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Ranged
{
    public class Scorchthrower : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Fires a long superheated flame bar" +
                             "\n'KILL IT WITH FIRE!'");
        }

        public override void SetDefaults()
        {
            item.damage = 850;
            item.ranged = true;
            item.width = 64;
            item.height = 22;
            item.useTime = 8;
            item.useAnimation = 16;
            item.useStyle = ItemUseStyleID.HoldingOut;
            item.noMelee = true;
            item.knockBack = 4;
            item.value = Item.sellPrice(platinum: 2, gold: 36);
            item.rare = ItemRarityID.Red;
            item.UseSound = SoundID.Item34;
            item.autoReuse = true;
            item.shoot = ModContent.ProjectileType<ScorchthrowerFlame>();
            item.shootSpeed = 10f;
            item.useAmmo = AmmoID.Gel;
        }

        public override bool CanUseItem(Player player)
        {
            return !player.wet;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Teal;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Flamethrower);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 12);
            recipe.AddIngredient(ItemID.LunarBar, 6);
            recipe.AddIngredient(ModContent.ItemType<InfernoblightCrystal>(), 12);
            recipe.AddIngredient(ModContent.ItemType<ScoriumBar>(), 26);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 54f;
            if (Collision.CanHit(position, 6, 6, position + muzzleOffset, 6, 6))
            {
                position += muzzleOffset;
            }
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, -2);
        }
    }
}
