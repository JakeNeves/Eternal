using Eternal.Common.Misc;
using Eternal.Content.Items.Accessories;
using Eternal.Content.Items.Materials;
using Eternal.Content.Items.Weapons.Magic;
using Eternal.Content.Projectiles.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.NPCs.Miniboss
{
    public class Shadeling : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];

            NPCID.Sets.NPCBestiaryDrawModifiers value = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Hide = true
            };
            NPCID.Sets.NPCBestiaryDrawOffset.Add(NPC.type, value);
        }

        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 24;
            NPC.aiStyle = 1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.BlueSlime;
            NPC.damage = 60;
            NPC.defense = 20;
            NPC.lifeMax = 1000;
            AnimationType = NPCID.BlueSlime;
            NPC.value = Item.sellPrice(platinum: 1);
            NPC.knockBackResist = 0f;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            NPC.buffImmune[BuffID.Suffocation] = true;
            NPC.buffImmune[BuffID.Poisoned] = true;
            NPC.buffImmune[BuffID.Venom] = true;
            NPC.buffImmune[BuffID.BetsysCurse] = true;
            NPC.buffImmune[BuffID.Daybreak] = true;
            NPC.buffImmune[BuffID.DryadsWardDebuff] = true;
            SpawnModBiomes = [ ModContent.GetInstance<Biomes.DarkMoon>().Type ];
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];

            NPC.TargetClosest();
        }

        public override void OnKill()
        {
            var entitySource = NPC.GetSource_Death();

            if (!Main.dedServ)
            {
                SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/ShademanTrap")
                {
                    Volume = 0.8f,
                    PitchVariance = Main.rand.NextFloat(0.2f, 0.9f),
                    MaxInstances = 0,
                });

                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 12f, 0f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, 12f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, -12f, 0f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
                Projectile.NewProjectile(entitySource, NPC.Center.X, NPC.Center.Y, 0f, -12f, ModContent.ProjectileType<ShadeBomb>(), NPC.damage, 0);
            }
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<ShadeMatter>(), 1, 2, 3));
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (Main.dedServ)
                return;

            var entitySource = NPC.GetSource_Death();

            for (int k = 0; k < 15.0; k++)
            {
                Dust.NewDust(NPC.Center, NPC.width, NPC.height, ModContent.DustType<Dusts.Shade>(), 0, 0, 0, default(Color), 1f);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
            => GlowMaskUtils.DrawNPCGlowMask(spriteBatch, NPC, ModContent.Request<Texture2D>("Eternal/Content/NPCs/Miniboss/Shadeling_Glow").Value);
    }
}
