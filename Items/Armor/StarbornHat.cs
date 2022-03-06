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
    public class StarbornHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("17% increased magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 24;
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
            player.setBonus = "+60 mana, 25% decreased mana cost and 20% increased magic damage" +
                            "\nWeapon projectiles heal the player by 15% when below half health upon hitting any enemy" +
                            "\nStarborn weapons cost 0 mana" +
                            "\n15% increased damage when below half health";
            player.magicDamage += 0.20f;
            player.statManaMax2 += 60;
            player.manaCost -= 0.25f;

            EternalGlobalProjectile.starbornArmor = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, ModContent.DustType<Starmetal>(), 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.17f;
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
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
