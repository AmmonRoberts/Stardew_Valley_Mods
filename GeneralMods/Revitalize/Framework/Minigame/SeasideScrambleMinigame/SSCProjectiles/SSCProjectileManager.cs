using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Revitalize.Framework.Minigame.SeasideScrambleMinigame.SSCProjectiles
{
    public class SSCProjectileManager
    {
        public List<SSCProjectile> projectiles;
        private List<SSCProjectile> garbageCollection;

        public SSCProjectileManager()
        {
            this.projectiles = new List<SSCProjectile>();
            this.garbageCollection = new List<SSCProjectile>();
        }

        public void addProjectile(SSCProjectile projectile)
        {
            this.projectiles.Add(projectile);
        }

        public void deleteProjectile(SSCProjectile projectile)
        {
            this.garbageCollection.Add(projectile);
            //this.projectiles.Remove(projectile);
        }

        public void update(GameTime Time)
        {
            foreach(SSCProjectile p in this.garbageCollection)
            {
                this.projectiles.Remove(p);
            }
            this.garbageCollection.Clear();

            foreach(SSCProjectile p in this.projectiles)
            {
                p.update(Time);

                //Do collision checking.
                foreach(SSCPlayer player in SeasideScramble.self.players.Values)
                {
                    if (p.collidesWith(player.hitBox))
                    {
                        p.onCollision(player);
                        player.onCollision(p);
                    }
                }
                foreach(SSCEnemies.SSCEnemy enemy in SeasideScramble.self.enemies.enemies)
                {
                    if (p.collidesWith(enemy.HitBox))
                    {
                        p.onCollision(enemy); //What happens to the projectile.
                        enemy.onCollision(p); //What happens to the entity.
                    }
                }
            }
        }

        public void draw(SpriteBatch b)
        {
            foreach (SSCProjectile p in this.projectiles)
            {             
                p.draw(b);
            }
        }

        //~~~~~~~~~~~~~~~~~~~~//
        //   Spawning Logic   //
        //~~~~~~~~~~~~~~~~~~~~//
        #region

        public void spawnDefaultProjectile(object Owner,Vector2 Position,Vector2 Direction,float Speed,Rectangle HitBox,Color Color,float Scale,int LifeSpan=300)
        {

            SSCProjectile basic = new SSCProjectile(Owner, new StardustCore.Animations.AnimatedSprite("DefaultProjectile", Position, new StardustCore.Animations.AnimationManager(SeasideScramble.self.textureUtils.getExtendedTexture("Projectiles", "Basic"), new StardustCore.Animations.Animation(0, 0, 4, 4)), Color), HitBox, Position, Direction, Speed, LifeSpan, Scale,1);
            this.addProjectile(basic);
        }
        public SSCProjectile getDefaultProjectile(object Owner, Vector2 Position, Vector2 Direction, float Speed, Rectangle HitBox, Color Color, float Scale, int LifeSpan = 300)
        {

            SSCProjectile basic = new SSCProjectile(Owner, new StardustCore.Animations.AnimatedSprite("DefaultProjectile", Position, new StardustCore.Animations.AnimationManager(SeasideScramble.self.textureUtils.getExtendedTexture("Projectiles", "Basic"), new StardustCore.Animations.Animation(0, 0, 4, 4)), Color), HitBox, Position, Direction, Speed, LifeSpan, Scale, 1);
            return basic;
        }

        #endregion
    }
}