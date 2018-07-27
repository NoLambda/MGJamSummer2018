using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    public class CameraManager
    {
        public Vector2 CameraPosition { get; }
        public Vector2 PlayerPosition { get; }
        public Matrix Transform { get; }

        public static CameraManager Instance { get { return instance ?? (instance = new CameraManager()); } }
        private static CameraManager instance;

        //public void Follow(/*ENTITY*/ entity, float smoothTime)
        //{
        //    PlayerPosition = new Vector2(/*ENTITY*/.Position.X - (/*ENTITY*/.Texture.Bounds.Width / 2) + GraphicsManager.Instance.VirtualResolution.X / 2, -/*ENTITY*/.Position.Y - (/*ENTITY*/.Texture.Bounds.Height / 2) + GraphicsManager.Instance.VirtualResolution.Y / 2);
        //    CameraPosition = Vector2.Lerp(CameraPosition, PlayerPosition, smoothTime);
        //    Transform = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0);
        //}
    }
}