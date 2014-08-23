using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LD30
{
    public enum AspectMode
    {
        /// <summary>
        /// This will do nothing. Your RenderTarget will be drawn as is.
        /// </summary>
        None,
        /// <summary>
        /// Default mode. Fills the screen while maintaining aspect ratio. Will Letterbox or Pillarbox accordingly.
        /// </summary>
        Letterbox,
        /// <summary>
        /// Fills the entire screen, stretching the image.
        /// </summary>
        Stretch,
        /// <summary>
        /// Zooms in (centered) on the image to fill the screen. Will not show stuff on corners (like formatted for TV movies)
        /// CURRENTLY NOT IMPLEMENTED
        /// </summary>
        Zoom
    }

    /// <summary>
    /// How to use: create a new Resolution object with desired virtual width, height and a pointer to your Game
    /// In your Draw Method, first call Setup and pass it a RenderTarget2D
    /// Then do all your drawing as needed using a spritebatch
    /// Finally call our Resolution.Draw with the spritebatch
    /// TODO: this seems weird... There has to be a better flow.
    /// 
    /// </summary>
    class Resolution
    {
        /// <summary>
        /// Game instance
        /// </summary>
        Game game;

        /// <summary>
        /// Aspect Mode to use. Defaults to Letterbox
        /// </summary>
        AspectMode Mode = AspectMode.Letterbox;

        /// <summary>
        /// Virtual Width
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Virtual Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Actual Screen Width
        /// </summary>
        public int ScreenWidth { get; set; }

        /// <summary>
        /// Actual Screen Height
        /// </summary>
        public int ScreenHeight { get; set; }

        RenderTarget2D target;
        Vector2 scale;
        Viewport v;

        /// <summary>
        /// Color used to clear the back buffer (set to Color.Black for black bars)
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// Creates a new instance of resolution manager
        /// </summary>
        /// <param name="width">Virtual width</param>
        /// <param name="height">Virtual height</param>
        /// <param name="game">Game instance</param>
        public Resolution(int width, int height, Game game)
        {
            this.game = game;

            target = new RenderTarget2D(game.GraphicsDevice, width, height);

            BackgroundColor = Color.Black;

            game.Window.ClientSizeChanged += Window_ClientSizeChanged;

            Width = width;
            Height = height;

            Initialize();
        }

        /// <summary>
        /// Handles resizing everything when window size changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            Initialize();
        }


        /// <summary>
        /// Change Aspect Mode
        /// </summary>
        /// <param name="mode">Mode to be changed</param>
        public void ChangeMode(AspectMode mode)
        {
            Mode = mode;
            Initialize();
        }

        /// <summary>
        /// Initialize values based on provided virtual width and height
        /// </summary>
        public void Initialize()
        {
            // First we set the actual window width and height
            ScreenWidth = game.GraphicsDevice.PresentationParameters.BackBufferWidth;
            ScreenHeight = game.GraphicsDevice.PresentationParameters.BackBufferHeight;

            // Some helper variables
            float targetRatio;
            float scaleX, scaleY;
            int w, h;
            Vector2 position;

            // Depending on which mode we want we do different things
            switch (Mode)
            {
                case AspectMode.None:
                    // Don't do anything. Scale to 1 and set viewport to default
                    scale = Vector2.One;
                    v = game.GraphicsDevice.Viewport;

                    break;
                case AspectMode.Letterbox:
                    // First we determine the ratio of our virtual resolution
                    targetRatio = Width / (float)Height;

                    // Set scale for X and Y
                    scaleX = ScreenWidth / (float)Width;
                    scaleY = ScreenHeight / (float)Height;

                    // Set our new width to screen width
                    w = ScreenWidth;

                    // Set our new height according to our new width
                    h = (int)(w / targetRatio + .5f);

                    // Our scale will be based on width
                    scale = new Vector2(scaleX);

                    // This will help center our viewport on the Y axis
                    position = new Vector2(0, (ScreenHeight - h) / 2);

                    // Check if height overflows window size. If it does, then we are in Pillarbox (vertical bars)
                    if (h > ScreenHeight)
                    {
                        // Set our new height to fill the window
                        h = ScreenHeight;

                        // Our width depends on our height ratio
                        w = (int)(h * targetRatio + .5f);

                        // Scale based on height
                        scale = new Vector2(scaleY);

                        // This will center our viewport on the X axis
                        position = new Vector2((ScreenWidth - w) / 2, 0);
                    }

                    // Set our drawing viewport using adjusted starting position and our virtual width and height multiplied by scale
                    v = new Viewport(
                        (int)position.X,
                        (int)position.Y,
                        (int)(Width * scale.X),
                        (int)(Height * scale.Y));

                    break;
                case AspectMode.Stretch:
                    // On stretch mode all we care about is filling the screen. This will distort our image.

                    // First we get the scale modifier
                    scaleX = ScreenWidth / (float)Width;
                    scaleY = ScreenHeight / (float)Height;

                    scale = new Vector2(scaleX, scaleY);

                    // And we create our viewport, from 0,0 stretched to fill the window
                    v = new Viewport(
                        0,
                        0,
                        (int)(Width * scale.X),
                        (int)(Height * scale.Y));

                    break;
                case AspectMode.Zoom:
                    // Currently does nothing
                    scale = Vector2.One;
                    v = new Viewport(
                        0,
                        0,
                        ScreenWidth,
                        ScreenHeight);
                    break;
                default:
                    // Should this even be accesible?
                    scale = Vector2.One;
                    v = new Viewport(
                        0,
                        0,
                        ScreenWidth,
                        ScreenHeight);
                    break;
            }

        }

        /// <summary>
        /// Converts window coordinates to our virtual resolution coordinates
        /// </summary>
        /// <param name="x">Value of X</param>
        /// <param name="y">Value of Y</param>
        /// <returns>New Vector2</returns>
        public Vector2 TransformCoordinates(int x, int y)
        {
            // Subtract position values if there are any then divide by scale
            int x1 = (int)((x - v.X) / scale.X);
            int y1 = (int)((y - v.Y) / scale.Y);

            return new Vector2(x1, y1);
        }

        public Vector2 TransformCoordinates(Vector2 position)
        {
            // Subtract position values if there are any then divide by scale
            int x1 = (int)((position.X - v.X) / scale.X);
            int y1 = (int)((position.Y - v.Y) / scale.Y);

            return new Vector2(x1, y1);
        }

        /// <summary>
        /// Call this in your Draw method before anything else
        /// TODO: Need to refactor this... I don't like having to call two methods in Draw
        /// </summary>
        /// <param name="target">The RenderTarget</param>
        public void Setup()
        {
            // We set the RenderTarget so the user can draw normally to it, we'll take care of drawing it to screen
            game.GraphicsDevice.SetRenderTarget(target);

            // Clear it to the standard Color.CornflowerBlue
            // TODO: Let user change this?
            game.GraphicsDevice.Clear(Color.CornflowerBlue);
        }

        /// <summary>
        /// Draw our render target to the screen
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // Set RenderTarget to the back buffer
            game.GraphicsDevice.SetRenderTarget(null);

            // Change the viewport to render only where we want
            game.GraphicsDevice.Viewport = v;

            // Clear the back buffer to our background color. This will give us bars (vertical or horizontal) on Letterbox mode
            game.GraphicsDevice.Clear(BackgroundColor);

            // Begin drawing
            // TODO: Let the user modify SpriteBatch settings
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);

            // Draw the RenderTarget. We use our scale.
            spriteBatch.Draw(target, Vector2.Zero, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            spriteBatch.End();
        }
    }
}
