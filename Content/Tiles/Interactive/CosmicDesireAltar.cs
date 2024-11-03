using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Misc;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent.ObjectInteractions;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;

namespace Eternal.Content.Tiles.Interactive
{
    public class CosmicDesireAltar : ModTile
    {
        public override void SetStaticDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            DustType = DustID.DemonTorch;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Width = 6;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(5, 2);
            TileObjectData.newTile.CoordinateHeights = [ 16, 16, 16 ];
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.addTile(Type);
            LocalizedText name = CreateMapEntryName();
            AddMapEntry(new Color(100, 25, 75), name);
        }

        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings)
        {
            return false;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j];
            Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);
            if (Main.drawToScreen)
            {
                zero = Vector2.Zero;
            }
            int height = tile.TileFrameY == 36 ? 18 : 16;
            Main.spriteBatch.Draw(ModContent.Request<Texture2D>("Eternal/Content/Tiles/Interactive/CosmicDesireAltar_Glow").Value, new Vector2(i * 16 - (int)Main.screenPosition.X, j * 16 - (int)Main.screenPosition.Y) + zero, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, height), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        public override bool RightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];

            var entitySource = player.GetSource_FromThis();

            SoundEngine.PlaySound(SoundID.DD2_BookStaffCast, player.Center);

            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.CosmicTablet>()))
            {
                if(!DownedBossSystem.downedArkofImperious)
                {
                    Main.NewText("Proove that you can challenge me first...", 150, 36, 120);
                    player.KillMe(PlayerDeathReason.ByCustomReason(player.name + "'s heart disintegrated into stardust"), 10000, 1, false);
                }
                else
                {
                    Projectile.NewProjectile(entitySource, player.Center.X, player.Center.Y - 200, 0, 0, ModContent.ProjectileType<CosmicEmperorSummon>(), 0, 0, Main.myPlayer, 0f, 0f);
                }
            }
            else
            {
                Main.NewText("The shrine appears to desire an inscribed tablet...", 100, 24, 60);
            }
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.noThrow = 2;
            player.cursorItemIconEnabled = true;
            player.cursorItemIconID = ModContent.ItemType<Items.Summon.CosmicTablet>();
        }
    }
}
