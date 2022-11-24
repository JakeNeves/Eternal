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
    public class ArkaniumHelmet : ModItem
    {

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("20% increased minion damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
            Item.value = Item.sellPrice(platinum: 6);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ArkaniumChestplate>() && legs.type == ModContent.ItemType<ArkaniumGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "30% increased minion damage, 25% decreased mana cost and +16 minion slots" +
                            "\nSwords rain down on you upon getting hit";

            player.GetDamage(DamageClass.Summon) += 1.30f;
            player.manaCost -= 1.25f;
            player.maxMinions += 16;

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.JungleTorch, 0f, 0f, 0, new Color(255, 255, 255), 0.5f)];
            dust.fadeIn = 0.4f;
            dust.noGravity = true;

            ArmorSystem.ArkaniumArmor = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 1.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<ArkaniumCompoundSheets>(), 20)
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 12)
                .AddIngredient(ModContent.ItemType<RefinedArkrystalSheets>(), 30)
                .AddTile(ModContent.TileType<AncientForge>())
                .Register();
        }
    }
}
