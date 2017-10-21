using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Game1
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		Compiler c;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Contents";
	    
	    	// NOTE (R-M): test for CSharp compiler
	    	c = new Compiler();
			c.ReadScript("test");
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
	    
            base.Initialize();
        }
	
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
	    
            // TODO: use this.Content to load your game content here
        }
	
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
	
        protected override void Update(GameTime gameTime)
        {
			if (Keyboard.GetState().IsKeyDown(Keys.F12)) c.RunScript("test", "Message");
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

			// TODO: Add your update logic here

		 	base.Update(gameTime);
        }
	
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

	    
            // TODO: Add your drawing code here
	    
            base.Draw(gameTime);
        }
    }
}
