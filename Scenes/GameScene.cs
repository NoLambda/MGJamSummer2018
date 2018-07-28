using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MGJamSummer2018.Core;
using MGJamSummer2018.Entities;

namespace MGJamSummer2018.Scenes
{
    public class GameScene : Scene
    {
        private Grid world;
        private Player player;

        public GameScene(string name) : base(name) { }

        public override void Initialize()
        {
            world = new Grid();
            world = new XmlManager<Grid>().Load("Maps/World_01.xml", world.GetType());

            player = new Player();
        }

        public override void LoadContent(ContentManager content)
        {
            world.LoadContent(content);
            player.LoadContent(content);
        }

        public override void Update(GameTime time)
        {
            world.Update(player);
            player.Update(time);
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            world.Draw(batch);
            player.Draw(batch);
        }
    }
}