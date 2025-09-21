using Eternal.Common.Players;
using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class UltimusHeadgear : ModItem
    {
        public static readonly int MagicDamageBonus = 30;
        public static readonly int MagicDamageSetBonus = 42;
        public static readonly int ManaReductionBonus = 64;
        public static readonly int ManaBonus = 300;

        public static LocalizedText SetBonusText { get; private set; }

        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MagicDamageBonus);

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            SetBonusText = this.GetLocalization("SetBonus").WithFormatArgs(MagicDamageSetBonus, ManaReductionBonus, ManaBonus);
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 22;
            Item.value = Item.sellPrice(platinum: 6);
            Item.rare = ModContent.RarityType<Magenta>();
            Item.defense = 20;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<UltimusPlateMail>() && legs.type == ModContent.ItemType<UltimusLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = SetBonusText.Value;

            player.statManaMax2 = ManaBonus; ;

            player.manaCost -= ManaReductionBonus / 100f;

            player.GetDamage(DamageClass.Magic) += MagicDamageSetBonus / 100f;

            Lighting.AddLight(player.Center, 1.14f, 0.22f, 1.43f);

            ArmorSystem.ArkaniumArmor = true;
            ArmorSystem.StarbornArmor = true;
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
               .AddTile(ModContent.TileType<AncientFoundry>())
               .AddIngredient(ModContent.ItemType<ArkaniumCowl>())
               .AddIngredient(ModContent.ItemType<StarbornHood>())
               .AddIngredient(ModContent.ItemType<CoreofExodus>(), 8)
               .AddIngredient(ModContent.ItemType<AwakenedCometiteBar>(), 12)
               .Register();
        }
    }
}
