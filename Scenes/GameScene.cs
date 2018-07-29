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
            player = new Player(world);
            rootEntity.AddChild(player);
        }

        public override void LoadContent(ContentManager content)
        {
            world.LoadContent(content);
            base.LoadContent(content);
        }

        public override void Update(GameTime gTime)
        {
            world.Update(player);
            base.Update(gTime);
        }

        public override void Draw(GameTime gTime)
        {
            world.Draw(GraphicsManager.Instance.SpriteBatch);
            base.Draw(gTime);
        }
    }
}