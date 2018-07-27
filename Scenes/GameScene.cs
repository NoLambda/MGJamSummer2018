using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MGJamSummer2018.Core;

namespace MGJamSummer2018.Scenes
{
    public class GameScene : Scene
    {
        private Grid world;

        public GameScene(string name) : base(name) { }

        public override void Initialize()
        {
            world = new Grid();
            world = new XmlManager<Grid>().Load("Maps/World_01.xml", world.GetType());
        }

        public override void LoadContent()
        {
<<<<<<< HEAD
            world.LoadContent(content);
        }

        public override void Update(GameTime time)
        {

=======
            texture = AssetManager.Instance.GetSprite("cube");
>>>>>>> 9bed17d7b65016b6b4faf02cc1d1e27726ace1aa
        }

        public override void Draw(SpriteBatch batch)
        {
            world.Draw(batch);
        }
    }
}