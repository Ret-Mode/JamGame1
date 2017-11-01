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
		//SpriteBatch spriteBatch;
		Player[] player;
		SetupReader sr;
		Compiler c;

		DrawingClass drawingClass;

		// NOTE (R-M):we have warning without volatile here
		Texture_Base[] tex = null;
		public Game1()

		{

			graphics = new GraphicsDeviceManager(this);

			Content.RootDirectory = "Content";
			sr = new SetupReader();
			// NOTE (R-M): test for CSharp compiler
			c = new Compiler();
			player = new Player[2] { new Player(), new Player() };
			player[0].Intelligence = new PlayerAI();
			player[1].Intelligence = new IdiotAI(IdiotAI.IdiotActions.Wait,10);

		}

		protected override void Initialize()
		{

			base.Initialize();

		}

		protected override void LoadContent()
		{
			//spriteBatch = new SpriteBatch(GraphicsDevice);

#if DEBUG
			drawingClass = new DrawDebug(GraphicsDevice);
#else
			drawingClass = new DrawRelease(GraphicsDevice);
#endif
			c.ReadScript("test");

			tex = sr.ReadTextures(tex, Content);

			sr.ReadActor(player[0], "Player", tex);
			sr.ReadActor(player[1], "Idiot", tex);

			player[0].Initialize(tex, new Vector2(200.0f, 150.0f));

        }
	
        protected override void UnloadContent()
        {
			sr.SaveActor(player[0], "Player");
			sr.SaveActor(player[1], "Idiot");
			sr.SaveTextures(tex);
        }
	
        protected override void Update(GameTime gameTime)
        {

			if (Keyboard.GetState().IsKeyDown(Keys.F12)) c.RunScript("test", "Message");
			if (Keyboard.GetState().IsKeyDown(Keys.F11)) sr.ReadActor(player[0], "Player", tex);

			foreach (Player p in player)
			{
				p.Intelligence.InventNextActions(player, p);
			}

			// NOTE (R-M): This should be moved from here

			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


			//Console.Write(gameTime.ToString);
		 	base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
			drawingClass.StartDraw(GraphicsDevice);
			drawingClass.Draw(player);
			drawingClass.EndDraw();
            base.Draw(gameTime);
        }
    }
}
