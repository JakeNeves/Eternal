using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternal.Content.Projectiles.Misc
{
    public abstract class RottensandBallProjectile : ModProjectile
    {
        public override string Texture => "Eternal/Content/Projectiles/Misc/RottensandBallProjectile";

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.FallingBlockDoesNotFallThroughPlatforms[Type] = true;
            ProjectileID.Sets.ForcePlateDetection[Type] = true;
        }
    }

    public class RottensandBallFallingProjectile : RottensandBallProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tiles.RottensandBlock>(), ModContent.ItemType<Items.Placeable.RottensandBlock>());
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EbonsandBallFalling);
        }
    }

    public class RottensandBallGunProjectile : RottensandBallProjectile
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            ProjectileID.Sets.FallingBlockTileItem[Type] = new(ModContent.TileType<Tiles.RottensandBlock>());
        }

        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.EbonsandBallGun);
            AIType = ProjectileID.EbonsandBallGun;
        }
    }
}
