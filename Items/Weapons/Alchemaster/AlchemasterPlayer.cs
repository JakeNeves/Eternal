using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Alchemaster
{
    public class AlchemasterPlayer : ModPlayer
    {

        public static AlchemasterPlayer ModPlayer(Player player)
        {
            return player.GetModPlayer<AlchemasterPlayer>();
        }

        public float alchemicDamageAdd;
        public float alchemicDamageMult = 1f;
        public int alchemicCrit;


    }
}
