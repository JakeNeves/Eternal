using Eternal.Common.Players;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Buffs
{
    public class HolyMantle : ModBuff
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Mantle");
            Description.SetDefault("The Holy Mantle is protecting you, this will last until you get hit");
            Main.buffNoSave[Type] = true;
            Main.buffNoTimeDisplay[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<BuffSystem>().holyMantle = true;

            if (player.GetModPlayer<BuffSystem>().holyMantle)
            {
                player.buffTime[buffIndex] = 18000;
            }
            else
            {
                player.DelBuff(buffIndex);
                buffIndex--;
            }
        }
    }
}
