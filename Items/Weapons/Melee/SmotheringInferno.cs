using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Melee
{
    class SmotheringInferno : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Ignites Enemies\n'Hot'");
        }

        public override void SetDefaults()
        {
            item.width = 34;
            item.height = 38;
            item.damage = 100;
            item.knockBack = 5;
            item.value = Item.buyPrice(silver: 3);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Pink;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.OnFire, 60);
        }
    }
}
