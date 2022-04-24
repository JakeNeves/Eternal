using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons.Expert
{
    class PrimordialBolt : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Shocks Enemies");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.damage = 25;
            item.knockBack = 2;
            item.value = Item.buyPrice(silver: 3);
            item.useTime = 10;
            item.useAnimation = 10;
            item.UseSound = SoundID.Item1;
            item.useStyle = ItemUseStyleID.SwingThrow;
            item.rare = ItemRarityID.Expert;
            item.expert = true;
            item.autoReuse = true;
            item.melee = true;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            target.AddBuff(BuffID.Electrified, 120);
        }
    }
}
