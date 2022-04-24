using Eternal.Dusts;
using Eternal.Items.Materials;
using Eternal.Tiles;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ArkaniumCowl : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased magic damage");
        }

        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 24;
            item.value = Item.sellPrice(platinum: 6);
            item.rare = ItemRarityID.Red;
            item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ArkaniumChestplate>() && legs.type == ModContent.ItemType<ArkaniumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "30% increased magic damage and 50% decreased mana cost" +
                            "\nYou release Swords when struck";
            player.magicDamage += 0.30f;
            player.manaCost -= 0.50f;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, ModContent.DustType<ArkEnergy>(), 0f, 0f, 0, new Color(255, 255, 255), 0.5f)];
            dust.fadeIn = 0.4f;
            dust.noGravity = true;

            EternalPlayer.ArkaniumArmor = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.magicDamage += 0.20f;
        }

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            foreach (TooltipLine line2 in tooltips)
            {
                if (line2.mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.overrideColor = EternalColor.Magenta;
                }
            }
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<AncientForge>());
            recipe.AddIngredient(ModContent.ItemType<BrokenShrineSword>(), 3);
            recipe.AddIngredient(ModContent.ItemType<ArkaniumCompound>(), 16);
            recipe.AddIngredient(ModContent.ItemType<WeatheredPlating>(), 2);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

    }
}
