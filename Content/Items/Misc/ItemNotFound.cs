using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class ItemNotFound : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 28;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.rare = ItemRarityID.Gray;
            Item.buffType = ModContent.BuffType<Buffs.Error>();
            Item.buffTime = 4500;
        }

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI == Main.myPlayer)
            {
                for (int i = 0; i < Main.maxNPCs; i++)
                {
                    Main.npc[i].AddBuff(ModContent.BuffType<Buffs.Error>(), 4500);
                }
            }

            return true;
        }
    }
}
