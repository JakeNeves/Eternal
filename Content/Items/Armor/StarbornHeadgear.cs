using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class StarbornHeadgear : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 22;
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
            player.setBonus = "20% increased ranged damage" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15 HP when below half healt upon hitting any enemy" +
                            "\n15% increased damage when below half health" +
                            "\n[c/FCA5033:Starborn Headgear Bonus]" +
                            "\nGrants the stealth effect of the Shroomite Armor";
            player.GetDamage(DamageClass.Ranged) += 0.20f;
            player.arrowDamage += 0.20f;
            player.bulletDamage += 0.20f;
            ArmorSystem.StarbornArmor = true;

            player.shroomiteStealth = true;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.PurpleTorch, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.17f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 5)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 16)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 4)
                .AddIngredient(ModContent.ItemType<StarpowerCrystal>(), 6)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
