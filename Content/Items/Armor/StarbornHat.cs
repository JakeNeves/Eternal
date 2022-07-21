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
    public class StarbornHat : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("17% increased magic damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
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
            player.setBonus = "+60 mana, 25% decreased mana cost and 20% increased magic damage" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15% when below half health upon hitting any enemy" +
                            "\nStarborn weapons cost 0 mana" +
                            "\n15% increased damage when below half health";
            player.GetDamage(DamageClass.Magic) += 0.20f;
            player.statManaMax2 += 60;
            player.manaCost -= 0.25f;
            ArmorSystem.StarbornArmor = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += 0.17f;
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
