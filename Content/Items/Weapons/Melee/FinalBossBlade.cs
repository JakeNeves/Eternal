using Eternal.Content.Projectiles.Weapons.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;
using Eternal.Content.Rarities;
using Eternal.Content.Tiles.CraftingStations;
using Eternal.Content.Items.Materials;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class FinalBossBlade : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Final Boss Blade");
            Tooltip.SetDefault("<right> to change attack modes" +
                             "\nMode 1 [c/35bce6:Starspear] - Fires a projectile that leaves a trail of piercing spike rings behind" +
                             "\nMode 2 [c/35bce6:Charging Buster] - Allows the player to ram into enemies" +
                             "\nMode 3 [c/35bce6:Holdable Control Sword] - Fires a sword controlled by the cursor" +
                             "\n'How good can it be?'");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 68;
            Item.height = 68;
            Item.damage = 20000;
            Item.DamageType = DamageClass.Melee;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 4f;
            Item.useAnimation = 24;
            Item.useTime = 24;
            Item.shoot = ModContent.ProjectileType<FinalBossBladeProjectile1Shoot>();
            Item.shootSpeed = 30f;
            Item.UseSound = SoundID.Item1;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.autoReuse = true;
        }
    }
}
