using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class NaquadahMask : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("20% increased melee damage");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 22;
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
            player.setBonus = "30% increased melee damage and 25% increased melee speed" +
                            "\nSome weapons receive special abilities" +
                            "\nYou release spike bombs upon getting hit" +
                            "\n[c/FCA5033:Rift Bonus]" +
                            "\nImmunity to Rift Withering" +
                            "\nProtection against the Rod of Distortion's unstability";

            player.GetDamage(DamageClass.Melee) += 0.30f;
            player.GetAttackSpeed(DamageClass.Melee) += 0.25f;

            ArmorSystem.NaquadahArmor = true;
            ArmorSystem.NaquadahArmorMeleeBonus = true;

            Lighting.AddLight(player.Center, 0.75f, 0f, 0.75f);

            if (Main.rand.NextBool(2))
            {
                Dust dust;
                Vector2 position = Main.LocalPlayer.Center;
                dust = Main.dust[Dust.NewDust(player.position, (int)player.width, (int)player.height, DustID.Wraith, 0f, 0f, 0, new Color(255, 255, 255), 1f)];
                dust.fadeIn = 0.3f;
                dust.noGravity = true;
            }
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawShadow = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += 0.20f;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ModContent.ItemType<WeaponsGradeNaquadahAlloy>(), 5)
                .AddIngredient(ModContent.ItemType<CrystallizedOminite>())
                .AddIngredient(ModContent.ItemType<WeatheredPlating>(), 6)
                .AddTile(ModContent.TileType<RotaryHearthForge>())
                .Register();
        }
    }
}
