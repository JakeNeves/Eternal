using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ObjectData;
using Terraria.ModLoader;
using Eternal.Items.Placeable;
using Terraria.DataStructures;
using Eternal.NPCs.Boss.BionicBosses;
using Terraria.ID;
using Eternal.NPCs;
using Eternal.NPCs.Boss.BionicBosses.Omnicron;

namespace Eternal.Tiles.Interactive
{
    public class Beacon : ModTile
    {

        public override void SetDefaults()
        {
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = false;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style2xX);
            TileObjectData.newTile.Width = 4;
            TileObjectData.newTile.Height = 3;
            TileObjectData.newTile.Origin = new Point16(3, 2);
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 };
            TileObjectData.newTile.StyleWrapLimit = 2;
            TileObjectData.newTile.StyleMultiplier = 2;
            TileObjectData.addTile(Type);
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Exo Beacon");
            AddMapEntry(new Color(20, 20, 80), name);
            disableSmartCursor = true;
            dustType = -1;
        }

        public override bool NewRightClick(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.AtlasCore>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                Main.PlaySound(SoundID.Roar, player.position);
                Main.NewText("XR-2006 Atlas-X9 has awoken!", 175, 75, 255);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<Atlas>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.BorealisCore>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                Main.PlaySound(SoundID.Roar, player.position);
                Main.NewText("XR-2003 Borealis-X1 has awoken!", 175, 75, 255);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 900, ModContent.NPCType<Borealis>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.OrionCore>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                Main.PlaySound(SoundID.Roar, player.position);
                Main.NewText("XR-2008 Orion-X5 has awoken!", 175, 75, 255);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<Orion>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.OmnicronCore>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                Main.PlaySound(SoundID.Roar, player.position);
                Main.NewText("XM-2024 Omicron-X8 has awoken!", 175, 75, 255);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y + 900, ModContent.NPCType<Omnicron>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.TripletsCore>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                Main.PlaySound(SoundID.Roar, player.position);
                Main.NewText("XE-68 Photon has awoken!", 175, 75, 255);
                Main.NewText("XE-76 Proton has awoken!", 175, 75, 255);
                Main.NewText("XE-90 Quasar has awoken!", 175, 75, 255);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<Photon>());
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<Proton>());
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 900, ModContent.NPCType<Quasar>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.ExoBeaconBlackBox>()))
            {
                Main.PlaySound(SoundID.DD2_BetsySummon, player.position);
                NPC.NewNPC((int)player.Center.X, (int)player.Center.Y - 800, ModContent.NPCType<DrSebastion>());
            }
            else if (Main.LocalPlayer.HasItem(ModContent.ItemType<Items.Summon.ChromaCore>()))
            {
                NPC.SpawnOnPlayer(player.whoAmI, ModContent.NPCType<AtlasNeox>());
                CombatText.NewText(player.Hitbox, Main.DiscoColor, "UNIDENTIFIED TARGET FOUND, PREPARING UNITS FOR STRIKE!", dramatic: true);
            }
            else
            {
                CombatText.NewText(player.Hitbox, Color.Yellow, "I need some sort of mechanical thing to contact them...", dramatic: true);
            }
            return false;
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.player[Main.myPlayer];
            player.noThrow = 2;
            player.showItemIcon = true;
            player.showItemIcon2 = ModContent.ItemType<Items.Placeable.Beacon>();
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            Item.NewItem(i * 16, j * 16, 48, 48, ModContent.ItemType<Items.Placeable.Beacon>());
        }

    }
}
