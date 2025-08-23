using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Misc
{
    public class CapriciousNote : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 24;
            Item.height = 28;
            Item.rare = ItemRarityID.Gray;
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 10;
            Item.useTime = 10;
        }

        public override bool? UseItem(Player player)
        {
            switch (Main.rand.Next(12))
            {
                case 0:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "This is hardly readable...", dramatic: true);
                    break;
                case 1:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Eternal is Dead!", dramatic: true);
                    break;
                case 2:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Go Outside...", dramatic: true);
                    break;
                case 3:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "The Final Boss is still in Development. :)", dramatic: true);
                    break;
                case 4:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Shoutouts to Calamity!", dramatic: true);
                    break;
                case 5:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "What did you even expect?", dramatic: true);
                    break;
                case 6:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "If you can't read this, you're an Undertale fan...", dramatic: true);
                    break;
                case 7:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Ocram Lives!", dramatic: true);
                    break;
                case 8:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Your princess is in alother castle...", dramatic: true);
                    break;
                case 9:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "We've been trying to reach you about your car's extended warranty...", dramatic: true);
                    break;
                case 10:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Isaac, and his Isaac, lived alone in a small Isaac, on top of an Isaac...", dramatic: true);
                    break;
                case 11:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "???", dramatic: true);
                    break;
            }

            return true;
        }
    }
}
