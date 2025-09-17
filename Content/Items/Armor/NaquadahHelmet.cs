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
    public class NaquadahHelmet : ModItem
    {
        public static readonly int MagicDamageBonus = 20;
        public static readonly int MagicDamageSetBonus = 30;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(MagicDamageSetBonus);
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
            player.setBonus = SetBonusText.Value;

            player.GetDamage(DamageClass.Magic) += MagicDamageSetBonus / 100f;
            
            ArmorSystem.NaquadahArmor = true;
            ArmorSystem.NaquadahArmorMagicBonus = true;

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
            player.armorEffectDrawOutlinesForbidden = true;
            player.armorEffectDrawOutlines = true;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += MagicDamageBonus / 100f;
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
