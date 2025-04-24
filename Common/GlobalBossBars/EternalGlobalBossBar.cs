/*
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.BossBarStyles;
using Eternal.Content.NPCs.Boss.CarminiteAmalgamation;
using Eternal.Content.NPCs.Boss.DuneGolem;
using Eternal.Content.NPCs.Boss.Igneopede;
using Eternal.Content.NPCs.Boss.Duneworm;
using Eternal.Content.NPCs.Boss.Incinerius;
using Eternal.Content.NPCs.Boss.SubzeroElemental;
using Eternal.Content.NPCs.Boss.Niades;
using Eternal.Content.NPCs.Boss.TheGlare;
using Eternal.Content.NPCs.Boss.CosmicApparition;
using Eternal.Content.NPCs.Boss.AoI;
using Eternal.Content.NPCs.Boss.Trinity;

namespace ExampleMod.Common.GlobalBossBars
{
    public class EternalGlobalBossBar : GlobalBossBar
    {
        public override void PostDraw(SpriteBatch spriteBatch, NPC npc, BossBarDrawParams drawParams)
        {
            if (BossBarLoader.CurrentStyle == ModContent.GetInstance<EternalBossBarStyle>())
            {
                string text = "";
                string name = npc.GivenName;
                var font = FontAssets.MouseText.Value;

                // Vanilla Bosses
                switch (npc.type)
                {
                    case NPCID.KingSlime:
                        text = "Gelatinous Tyrant";
                        break;
                    case NPCID.EyeofCthulhu:
                        text = "Evil Presence of the Night Sky";
                        break;
                    case NPCID.EaterofWorldsHead:
                        text = "Centipede Sabotuer of The Corruption";
                        break;
                    case NPCID.BrainofCthulhu:
                        text = "Scourge of The Crimson";
                        break;
                    case NPCID.QueenBee:
                        text = "Matriarch of The Hive";
                        break;
                    case NPCID.SkeletronHead:
                        text = "Scourge of The Dungeon";
                        break;
                    case NPCID.Deerclops:
                        text = "Scourge of the Tundra";
                        break;
                    case NPCID.WallofFlesh:
                        text = "Scourge of The Underworld";
                        break;
                    case NPCID.SkeletronPrime:
                        text = "Mechanical Terror of The Cold Wind";
                        break;
                    case NPCID.Retinazer:
                        text = "Scourge of The Night Sky";
                        break;
                    case NPCID.Spazmatism:
                        text = "Scourge of The Night Sky";
                        break;
                    case NPCID.TheDestroyer:
                        text = "Rising from Deep Below";
                        break;
                    case NPCID.Plantera:
                        text = "Scourge of The Jungle";
                        break;
                    case NPCID.DukeFishron:
                        text = "Rising from The Ocean";
                        break;
                    case NPCID.EmpressButterfly:
                        text = "Guardian of The Hallowed";
                        break;
                    case NPCID.QueenSlimeBoss:
                        text = "Scourge of The Hallowed";
                        break;
                    case NPCID.Golem:
                        text = "Guardian of The Lihzard Temple";
                        break;
                    case NPCID.CultistBoss:
                        text = "Scourge of The Dungeon";
                        break;
                    case NPCID.MoonLordCore:
                        text = "Celestial Deity of The Moon";
                        break;
                    default:
                        text = "";
                        break;
                }

                // Eternal Mod Bosses
                // Pre-Hardmode
                if (npc.type == ModContent.NPCType<CarminiteAmalgamation>())
                    text = "Abominable Fleshbound Horror";
                if (npc.type == ModContent.NPCType<DuneGolem>())
                    text = "Possessed Desert Idol";
                // Hardmode
                if (npc.type == ModContent.NPCType<IgneopedeHead>())
                    text = "Giant Magmatic Burrower";
                if (npc.type == ModContent.NPCType<DunewormHead>())
                    text = "Ambusher of the Desert";
                if (npc.type == ModContent.NPCType<Incinerius>())
                    text = "The Underworld's Infernal Sabotuer";
                if (npc.type == ModContent.NPCType<SubzeroElemental>())
                    text = "Guardian of the Tundra";
                if (npc.type == ModContent.NPCType<Niades>())
                    text = "Scourge of the Dark Moon";
                if (npc.type == ModContent.NPCType<TheGlare>())
                    text = "Mask of Eternal Purgatory";
                // Post-Moon Lord
                if (npc.type == ModContent.NPCType<CosmicApparition>())
                    text = "Ambusher from the Fallen Comet";
                if (npc.type == ModContent.NPCType<ArkofImperious>())
                    text = "Guardian of The Shrine";
                if (npc.type == ModContent.NPCType<Thunderius>())
                    text = "Effigy of the Mind";
                if (npc.type == ModContent.NPCType<Infernito>())
                    text = "Effigy of the Body";
                if (npc.type == ModContent.NPCType<Cryota>())
                    text = "Effigy of the Soul";
                if (npc.type == ModContent.NPCType<TrinityCore>())
                    text = "Idol of Godhood";

                Vector2 nameSize = font.MeasureString(name);
                Vector2 textSize = font.MeasureString(text);
                spriteBatch.DrawString(font, name, drawParams.BarCenter - nameSize / 2 + new Vector2(0, -30), Color.White, 0f, new Vector2(drawParams.BarCenter.X, drawParams.BarCenter.Y - 30), 2f, SpriteEffects.None, 0f);
                spriteBatch.DrawString(font, text, drawParams.BarCenter - nameSize / 2 + new Vector2(0, -60), Color.White);
            }
        }
    }
}
*/