using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MGJamSummer2018.Core;

namespace MGJamSummer2018.Scenes
{
    public class GameScene : Scene
    {
        private Grid world;
        private SpriteEntity test;

        public GameScene(string name) : base(name) { }

        public override void Initialize()
        {
            world = new Grid();
            world = new XmlManager<Grid>().Load("Maps/World_01.xml", world.GetType());
            
        }

        public override void LoadContent(ContentManager content)
        {
            world.LoadContent(content);
            test = new SpriteEntity("MainCharacter", "SOLDIER");
            test.ToggleAnimation();
            test.PlayAnimation("SNEAKING");
            test.LoopAnimation = true;
            test.LocalPosition = new Vector2(40, 40);
        }

        public override void Update(GameTime time)
        {
            test.Update(time);
        }

        public override void Draw(GameTime time, SpriteBatch batch)
        {
            world.Draw(batch);
            test.Draw(time);
        }
    }
}