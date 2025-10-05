using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Eternal.Content.Projectiles.Misc;

namespace Eternal.Content.Items.Misc
{
    public class JakeTEMnomicon : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 40;
            Item.rare = ModContent.RarityType<PulsatingPurple>();
            Item.value = 0;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useAnimation = 10;
            Item.useTime = 10;
            Item.consumable = true;
            Item.maxStack = 9999;
        }

        public override bool? UseItem(Player player)
        {
            switch (Main.rand.Next(4))
            {
                case 0:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "...", dramatic: true);
                    break;
                case 1:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "This is stupid!", dramatic: true);
                    break;
                case 2:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Jake, seriously... What the hell is this?", dramatic: true);
                    break;
                case 3:
                    CombatText.NewText(player.Hitbox, Color.Yellow, "Oh come on now, really?", dramatic: true);
                    break;
            }

            if (player.whoAmI == Main.myPlayer)
            {
                var entitySource = player.GetSource_FromThis();

                Projectile.NewProjectile(entitySource, new Vector2(player.Center.X, player.Center.Y - 300), new Vector2(0f, 0f), ModContent.ProjectileType<JakeTEMnomiconProjectile>(), 0, 0f);
            }

            return true;
        }
    }
}
