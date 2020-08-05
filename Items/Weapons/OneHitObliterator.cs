using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Items.Weapons
{
    class OneHitObliterator : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("One-Hit Obliterator");
            Tooltip.SetDefault("[c/FF0000:Cheat Item]\nA single hit from this divine bulky-looking weapon sha'll perish no matter what...\nNo fellow terrarian sha'll weild this weapon in regards to it's power");
        }

        public override void SetDefaults()
        {
            item.damage = 9999999;
            item.melee = true;
            item.width = 48;
            item.height = 48;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 0;
            item.value = 0;
            item.rare = ItemRarityID.Expert;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
        }
    }
}
