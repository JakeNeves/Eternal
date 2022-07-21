using Eternal.Content.Rarities;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Vexation : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Heals you upon striking an enemy");

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 44;
            Item.damage = 440;
            Item.knockBack = 3.5f;
            Item.value = Item.buyPrice(platinum: 1, gold: 3);
            Item.useTime = 16;
            Item.useAnimation = 16;
            Item.UseSound = SoundID.Item1;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.rare = ModContent.RarityType<Teal>();
            Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee;
        }

        public override void OnHitNPC(Player player, NPC target, int damage, float knockBack, bool crit)
        {
            player.HealEffect(Main.rand.Next(3, 6), false);
        }
    }
}
