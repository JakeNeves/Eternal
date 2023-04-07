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
    public class StarbornMask : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("17% increased melee damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = Item.sellPrice(gold: 15);
            Item.rare = ModContent.RarityType<Teal>();
            Item.defense = 38;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<StarbornScalePlate>() && legs.type == ModContent.ItemType<StarbornGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "20% increased melee damage and 17% increased melee speed" +
                            "\nSome weapons receive special abilities" +
                            "\nWeapon projectiles heal the player by 15 HP when below half healt upon hitting any enemy" +
                            "\n15% increased damage when below half health" +
                            "\n[c/FCA5033:Starborn Mask Bonus]" +
                            "\nSome melee weapons recieve special modifiers";
            player.GetDamage(DamageClass.Melee) += 0.20f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.17f;
            ArmorSystem.StarbornArmor = true;

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
            player.GetDamage(DamageClass.Melee) += 0.17f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<StarmetalBar>(), 5)
                .AddIngredient(ModContent.ItemType<CometiteBar>(), 16)
                .AddIngredient(ModContent.ItemType<GalaxianPlating>(), 4)
                .AddIngredient(ModContent.ItemType<CometiteCrystal>(), 6)
                .AddTile(ModContent.TileType<Starforge>())
                .Register();
        }
    }
}
