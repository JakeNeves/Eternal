using Eternal.Content.Items.Materials;
using Eternal.Content.Rarities;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Weapons.Melee
{
    public class Omegabane : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 102;
            Item.height = 102;
            Item.DamageType = DamageClass.Melee;
            Item.rare = ModContent.RarityType<Turquoise>();
            Item.damage = 20000;
            Item.useAnimation = 16;
            Item.useTime = 16;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 2f;
            Item.autoReuse = true;
            Item.UseSound = SoundID.Item71;
        }
    }
}
