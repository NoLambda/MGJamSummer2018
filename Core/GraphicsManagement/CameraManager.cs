using MGJamSummer2018.Entities;
using Microsoft.Xna.Framework;

namespace MGJamSummer2018.Core
{
    public class CameraManager
    {
        public Vector2 CameraPosition { get; set; }
        public Vector2 PlayerPosition { get; set; }
        public Matrix Transform { get; set; }

        public static CameraManager Instance { get { return instance ?? (instance = new CameraManager()); } }
        private static CameraManager instance;

        public void Follow(Player entity, float smoothTime)
        {
            PlayerPosition = new Vector2(-entity.Position.X - (entity.CollisionBox.Width / 2) + GraphicsManager.Instance.VirtualResolution.X / 2, -entity.Position.Y - (entity.CollisionBox.Height / 2) + GraphicsManager.Instance.VirtualResolution.Y / 2);
            CameraPosition = Vector2.Lerp(CameraPosition, PlayerPosition, smoothTime);
            Transform = Matrix.CreateTranslation(CameraPosition.X, CameraPosition.Y, 0);
        }
    }
}