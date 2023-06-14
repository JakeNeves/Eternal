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
    public class NaquadahHelmet : ModItem
    {
        public override void SetStaticDefaults()
        {
            // Tooltip.SetDefault("20% increased minion damage");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(platinum: 15);
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.defense = 40;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<NaquadahChestplate>() && legs.type == ModContent.ItemType<NaquadahGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "+24 minion slots and 30% increased minion damage" +
                            "\nSome weapons receive special abilities" +
                            "\nStarborn, Arkanium and Ultimus Armor Effects" +
                            "\n[c/FCA5033:Rift Bonus]" +
                            "\nImmunity to Rift Withering" +
                            "\nProtection against the Rod of Distortion's unstability";
            player.GetDamage(DamageClass.Summon) += 0.30f;
            player.maxMinions += 8;
            ArmorSystem.StarbornArmor = true;
            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.UltimusArmor = true;
            ArmorSystem.NaquadahArmor = true;

            Lighting.AddLight(player.Center, 0.75f, 0f, 0.75f);

            Dust dust;
            Vector2 position = Main.LocalPlayer.Center;
            dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.Wraith, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
            dust.fadeIn = 0.3f;
            dust.noGravity = true;
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<NaquadahBar>(), 5)
                .AddIngredient(ModContent.ItemType<CrystalizedOminite>())
                .AddIngredient(ModContent.ItemType<StarbornHelmet>())
                .AddIngredient(ModContent.ItemType<UltimusHelmet>())
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
