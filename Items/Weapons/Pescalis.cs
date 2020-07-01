using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Eternal.Items.Weapons
{
    class Pescalis : ModItem
    {
        public override void SetStaticDefaults()
        {
            //This is a post-Moon Lord Weapon
            DisplayName.SetDefault("Pescalis [WIP]");
            Tooltip.SetDefault("Fires multiple things (Using Vanilla Projectiles for now...) \n[c/008060:Ancient Artifact] \nThis blade was once weilded by a cosmic champion, until he met his fate with Astrum Deus, a powerful god who killed the champion in one attack! \nHowever, the campion's soul quickly reincarnate with it's brittle body and the champion eventually found a way to cheat death, but the stategy remains unknown...");
        }

        public override void SetDefaults()
        {
            //Things here may change...
            item.width = 32;
            item.height = 32;
            item.damage = 1024;
            item.knockBack = 92;
            item.value = Item.buyPrice(platinum: 1, gold: 3);
            item.useTime = 15;
            item.useAnimation = 15;
            item.UseSound = SoundID.Item44;
            item.useStyle = 1;
            item.rare = 10;
            item.autoReuse = true;
            item.melee = true;
            item.shoot = ProjectileID.StarWrath;
            item.shoot = ProjectileID.Starfury;
            item.shoot = ProjectileID.LightDisc;
        }
    }
}
