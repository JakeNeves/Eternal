using Eternal.Common.Players;
using Eternal.Content.Buffs;
using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class ReverseHolyCard : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 20;
            Item.useTime = 20;
            Item.UseSound = SoundID.Item8;
            Item.value = Item.sellPrice(gold: 40);
            Item.rare = ModContent.RarityType<HellMode>();
            Item.buffType = ModContent.BuffType<ReverseHolyCardCooldown>();
            Item.buffTime = 20900;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer && !player.GetModPlayer<BuffSystem>().reverseHolyCardCooldown)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    if (Main.npc[i].active && !Main.npc[i].boss)
                        player.ApplyDamageToNPC(Main.npc[i], 10000, 0f, 0);
                }
            }

            return true;
        }
    }
}
