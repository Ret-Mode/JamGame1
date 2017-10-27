using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace Game1
{

    public class Game1 : Game
    {

		public struct Texture_Base
		{
			public Texture2D tex;
			public bool FaceLeft;
		};

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
		Player player;
		SetupReader sr;
		Compiler c;

		// NOTE (R-M):we have warning without volatile here
		Texture_Base[] tex = null;
        public Game1()

        {
			
            graphics = new GraphicsDeviceManager(this);

			Content.RootDirectory = "Content";
			sr = new SetupReader();
	    	// NOTE (R-M): test for CSharp compiler
	    	c = new Compiler();
			player = new Player();

			
        }

        protected override void Initialize()
        {

            base.Initialize();

        }
	
        protected override void LoadContent()
        {
			spriteBatch = new SpriteBatch(GraphicsDevice);


			c.ReadScript("test");

			tex = sr.ReadTextures(tex, Content);

			sr.ReadActor(player, "Player", tex);

			player.Initialize(tex, new Vector2(200.0f, 150.0f));

        }
	
        protected override void UnloadContent()
        {
			sr.SaveActor(player, "Player");
			sr.SaveTextures(tex);
        }
	
        protected override void Update(GameTime gameTime)
        {
			MovementDir Dir;
			if (Keyboard.GetState().IsKeyDown(Keys.F12)) c.RunScript("test", "Message");
			if (Keyboard.GetState().IsKeyDown(Keys.F11)) sr.ReadActor(player, "Player", tex);

			if(Keyboard.GetState().IsKeyDown(Keys.W)){
				if (Keyboard.GetState().IsKeyDown(Keys.A)){
					Dir = MovementDir.UpLeft;
				} else if (Keyboard.GetState().IsKeyDown(Keys.D)){
					Dir = MovementDir.UpRight;
				} else {
					Dir = MovementDir.Up;
				}
			} else if(Keyboard.GetState().IsKeyDown(Keys.S)){
				if (Keyboard.GetState().IsKeyDown(Keys.A)){
					Dir = MovementDir.DownLeft;
				} else if (Keyboard.GetState().IsKeyDown(Keys.D)){
					Dir = MovementDir.DownRight;
				} else {
					Dir = MovementDir.Down;
				}
			} else 	if (Keyboard.GetState().IsKeyDown(Keys.A)){
				Dir = MovementDir.Left;
			} else if (Keyboard.GetState().IsKeyDown(Keys.D)){
				Dir = MovementDir.Right;
			} else {
				Dir = MovementDir.Still;
			}

			player.PlayerPhysics.Move(Dir);

			// NOTE (R-M): This should be moved from here

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

		 	base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			GraphicsDevice.Clear(Color.Black);

			spriteBatch.Begin();
			player.Draw(spriteBatch);
			spriteBatch.End();
	    
	    
            base.Draw(gameTime);
        }
    }
}
