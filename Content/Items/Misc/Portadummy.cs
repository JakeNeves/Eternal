using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class Portadummy : ModItem
    {
        public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs("Creates a portable floating dummy at your cursor" +
                                                                            "\nDespawns when a boss is alive" +
                                                                            "\n<right> to despawn all portadummies");

        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 36;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useAnimation = 15;
            Item.useTime = 15;
            Item.value = Item.sellPrice(gold: 1);
            Item.rare = ItemRarityID.Gray;
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                var entitySource = player.GetSource_FromThis();

                if (player.whoAmI == Main.myPlayer)
                {
                    if (player.altFunctionUse == 2)
                    {
                        for (int i = 0; i < Main.maxNPCs; i++)
                        {
                            if (Main.npc[i].active && Main.npc[i].type == ModContent.NPCType<NPCs.Misc.Portadummy>())
                            {
                                NPC npc = Main.npc[i];
                                npc.life = 0;
                                npc.HitEffect();
                                // Main.npc[i].StrikeNPCNoInteraction(int.MaxValue, 0, 0, false, false, false);
                            }
                        }
                    }
                    else
                    {
                        if (NPC.CountNPCS(ModContent.NPCType<NPCs.Misc.Portadummy>()) < 25)
                        {
                            NPC.NewNPC(entitySource, (int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, ModContent.NPCType<NPCs.Misc.Portadummy>());
                        }
                    }
                }
            }

            return true;
        }

        public override void AddRecipes()
        {
            CreateRecipe()
                .AddIngredient(ItemID.TargetDummy)
                .Register();
        }
    }
}
