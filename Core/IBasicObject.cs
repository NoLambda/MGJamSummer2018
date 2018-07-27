using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    interface IBasicObject
    {
        void Draw(GameTime gTime);
        void Update(GameTime gTime);
        void Reset();
    }
}
