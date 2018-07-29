using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGJamSummer2018.Core
{
    public class GraphicsManager
    {
        public string Title { get; private set; }
        public Vector2 Resolution { get; private set; }
        public Vector2 VirtualResolution { get; private set; }
        public bool IsFullScreen { get; private set; }
        public bool IsMouseVisible { get; private set; }
        public bool AllowVirtualResolution { get; private set; }

        public Game Game { get; private set; }
        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }
        public SpriteBatch SpriteBatch { get; private set; }

        public RenderTarget2D RenderVirtualResolution { get; private set; }

        public static GraphicsManager Instance { get { return instance ?? (instance = new GraphicsManager()); } }
        private static GraphicsManager instance;

        public void Init(Game1 game)
        {
            Game = game;
            GraphicsDeviceManager = new GraphicsDeviceManager(Game);
            GraphicsDeviceManager.ApplyChanges();
            SpriteBatch = new SpriteBatch(GraphicsDeviceManager.GraphicsDevice);
        }

        public void SetWindowTitle(string name)
        {
            Title = name;
            Game.Window.Title = Title;
        }

        public void SetResolution(Vector2 resolution, bool isFullScreen)
        {
            Resolution = resolution;
            IsFullScreen = isFullScreen;

            GraphicsDeviceManager.PreferredBackBufferWidth = (int)Resolution.X;
            GraphicsDeviceManager.PreferredBackBufferHeight = (int)Resolution.Y;
            GraphicsDeviceManager.IsFullScreen = IsFullScreen;
            GraphicsDeviceManager.ApplyChanges();
        }

        public void SetVirtualResolution(Vector2 virtualResolution)
        {
            VirtualResolution = virtualResolution;

            RenderVirtualResolution = new RenderTarget2D(GraphicsDeviceManager.GraphicsDevice,
                                                (int)VirtualResolution.X, (int)VirtualResolution.Y,
                                                false, GraphicsDeviceManager.PreferredBackBufferFormat,
                                                DepthFormat.Depth24);
            AllowVirtualResolution = true;
        }

        public void SetMouseVisible(bool isMouseVisible)
        {
            IsMouseVisible = isMouseVisible;
        }
    }
}