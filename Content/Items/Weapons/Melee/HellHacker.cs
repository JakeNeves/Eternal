using Eternal.Common.Configurations;
using Eternal.Common.Systems;
using Eternal.Content.Projectiles.Weapons.Melee;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class HellHacker : ModItem
    {
        public override void SetStaticDefaults()
        {
            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 72;
            Item.height = 72;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ItemRarityID.Cyan;
            Item.damage = 120;
            Item.useAnimation = 12;
            Item.useTime = 12;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2.5f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item1;
            Item.shoot = ModContent.ProjectileType<HellHackerProjectile>();
            Item.shootSpeed = 8f;
            Item.noUseGraphic = true;
            Item.noMelee = true;
        }

        public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
        {
            if (DownedBossSystem.downedIncinerius)
                damage += 0.25f;
            if (DownedBossSystem.downedChimera)
                damage += 0.5f;
            if (DownedBossSystem.downedCosmicApparition)
                damage += 0.75f;
        }

        public override bool CanUseItem(Player player)
        {
            return player.ownedProjectileCounts[Item.shoot] < 1;
        }
    }
}
