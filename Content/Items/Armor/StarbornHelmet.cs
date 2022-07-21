using Eternal.Common.Players;
using Eternal.Content.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class StarbornHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("17% increased minion damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 20;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 16;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<StarbornScalePlate>() && legs.type == ModContent.ItemType<StarbornGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+8 minion slots and 20% increased minion damage" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15% when below half healt upon hitting any enemy" +
                            "\n15% increased damage when below half health";
            player.GetDamage(DamageClass.Summon) += 0.20f;
            player.maxMinions += 8;
            ArmorSystem.StarbornArmor = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.17f;
        }

        /*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddTile(ModContent.TileType<Starforge>());
            recipe.AddIngredient(ModContent.ItemType<StarmetalBar>(), 5);
            recipe.AddIngredient(ModContent.ItemType<CometiteBar>(), 16);
            recipe.AddIngredient(ModContent.ItemType<GalaxianPlating>(), 4);
            recipe.AddIngredient(ModContent.ItemType<CometiteCrystal>(), 6);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/

    }
}
