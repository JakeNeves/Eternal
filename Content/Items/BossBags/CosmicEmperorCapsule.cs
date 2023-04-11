using Eternal.Common.ItemDropRules.Conditions;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Melee;
using Eternal.Content.Items.Weapons.Ranged;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.BossBags
{
    public class CosmicEmperorCapsule : ModItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Treasure Capsule (Cosmic Emperor)");
            // Tooltip.SetDefault("{$CommonItemTooltip.RightClickToOpen}");

            ItemID.Sets.BossBag[Type] = true;

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.width = 32;
            Item.height = 32;
            Item.rare = -12;
            Item.expert = true;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void ModifyItemLoot(ItemLoot itemLoot)
        {
            AbsoluteRNGDropCondition absoluteRNG = new AbsoluteRNGDropCondition();

            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ApparitionalMatter>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<InterstellarSingularity>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<StarmetalBar>(), minimumDropped: 30, maximumDropped: 60));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Vexation>(), 1));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<Starfall>(), 2));
            itemLoot.Add(ItemDropRule.Common(ModContent.ItemType<ApparitionalDisk>(), 3));

            ItemDropRule.ByCondition(absoluteRNG, ModContent.ItemType<ApparitionalViscara>(), 60);
        }

        public override void OnConsumeItem(Player player)
        {
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<ApparitionalViscara>()))
            {
                Main.NewText(player.name + " has obtained the Apparitional Viscara, a crazy rare drop from the Absolute RNG pool!", 247, 47, 154);
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/AbsoluteRNGDrop"), player.position);
            }
        }
    }
}
