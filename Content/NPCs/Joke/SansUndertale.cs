using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;

namespace Eternal.Content.NPCs.Joke
{
    public class SansUndertale : ModNPC
    {
        public override void SetStaticDefaults()
        {
            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 141;
            NPC.height = 131;
            NPC.lifeMax = 1;
            NPC.defense = 1;
            NPC.damage = 0;
            NPC.knockBackResist = -1f;
            NPC.aiStyle = 14;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit2;
            NPC.DeathSound = null;
        }

        public override void OnKill()
        {
            CombatText.NewText(NPC.Hitbox, Color.White, "undertale", dramatic: true);

            if (!Main.dedServ)
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/MegalovaniaMotifEarrape"));
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.zenithWorld)
                return SpawnCondition.Underworld.Chance * 1f;
            else
                return SpawnCondition.Underworld.Chance * 0f;
        }
    }
}
