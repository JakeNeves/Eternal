using Eternal.Content.Buffs.Weapons;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class MrFishbone : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

            ItemID.Sets.ItemsThatAllowRepeatedRightClick[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 38;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Lime;
            Item.damage = 60;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.UseSound = SoundID.Item1;
        }

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (Main.rand.NextBool(8) && player.statLifeMax >= player.statLifeMax2 / 2)
                player.AddBuff(ModContent.BuffType<MrFishbonesBoon>(), 960);

            SoundEngine.PlaySound(new SoundStyle($"{nameof(Eternal)}/Assets/Sounds/Custom/MrFishboneHit")
            {
                Volume = 0.8f,
                PitchVariance = Main.rand.NextFloat(1f, 1.25f),
                MaxInstances = 0,
            }, target.position);
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                Item.shoot = ModContent.ProjectileType<MrFishboneProjectile>();
                Item.shootSpeed = 4.5f;
                Item.noUseGraphic = true;
                Item.noMelee = true;
            }
            else
            {
                Item.shoot = ProjectileID.None;
                Item.shootSpeed = 0f;
                Item.noUseGraphic = false;
                Item.noMelee = false;
            }
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
