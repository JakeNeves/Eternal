using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Eternal.Items.Materials;
using Eternal.Tiles;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Eternal.Dusts;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class StarbornHeadgear : ModItem
    {

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 22;
            item.value = Item.sellPrice(gold: 15);
            item.rare = ItemRarityID.Red;
            item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<StarbornScalePlate>() && legs.type == ModContent.ItemType<StarbornGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased ranged damage and shroomite armor effects";
            player.rangedDamage += 0.20f;
            player.rocketDamage += 0.20f;
            player.arrowDamage += 0.20f;
            player.bulletDamage += 0.20f;

            player.shroomiteStealth = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(position, (int)player.Center.X, (int)player.Center.Y, ModContent.DustType<Starmetal>(), 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;

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
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 4);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
