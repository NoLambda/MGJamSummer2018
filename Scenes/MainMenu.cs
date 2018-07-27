using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MGJamSummer2018.Core;

namespace MGJamSummer2018.Scenes
{
    public class MainMenu : Scene
    {
        public MainMenu(string name) : base(name) { }

        public override void LoadContent()
        {
<<<<<<< HEAD

=======
            texture = AssetManager.Instance.GetSprite("cube");
>>>>>>> 9bed17d7b65016b6b4faf02cc1d1e27726ace1aa
        }

        public override void Draw(SpriteBatch batch)
        {

        }

        public override void Update(GameTime time)
        {

        }
    }
}