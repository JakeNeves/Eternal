using Eternal.Content.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Items.Dyes
{
    public class ApparitionalBlood : ModItem
    {
        public override void SetStaticDefaults()
        {
            if (!Main.dedServ)
            {
                GameShaders.Armor.BindShader(
                    Item.type,
                    new ArmorShaderData(Mod.Assets.Request<Effect>("Assets/Effects/ApparitionalParticle"), "ApparitionalParticlePass")
                );
            }

            CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 3;
        }

        public override void SetDefaults()
        {
            int dye = Item.dye;

            Item.CloneDefaults(ItemID.BloodbathDye);
            Item.rare = ModContent.RarityType<Teal>();
            Item.dye = dye;
        }
    }
}