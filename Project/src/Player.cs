using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1 {
	public class Player {
		public Texture2D PlayerTexture;
		public Movement PlayerPhysics;
		public IAI Intelligence;
		public bool TextureFaceLeft;
		private bool MoveFaceLeft;
        public static int FrameCounter = 0; //Actual frame of animation
        public static int SkipFrameCounter = 3; //How many frames skipped before next frame of animation
		public String ID;
		public String TextureName;
		public Player() {
			PlayerPhysics = new Movement();
		}

		public void UpdateTexture(Game1.Texture_Base[] texture)
		{
			for (int i = 0; i<texture.Length; ++i)
			{
				if (texture[i].tex.Name == TextureName)
				{
					PlayerTexture = texture[i].tex;
					TextureFaceLeft = texture[i].FaceLeft;
					break;
				}
			}
		}

		public void Initialize(Game1.Texture_Base[] texture, Vector2 position){
			
			PlayerPhysics.Position.X = position.X;
			PlayerPhysics.Position.Y = position.Y;
			UpdateTexture(texture);

		}

		public void Update(){

		}

		// NOTE (R-M): this shoulb be divided into 2 functions in the future
		public void SetPosition(int X, int Y, bool ResetPhysics){
			PlayerPhysics.Position.X = (float)X;
			PlayerPhysics.Position.Y = (float)Y;
			if (ResetPhysics)
			{
				PlayerPhysics.Speed = PlayerPhysics.Acceleration = Vector2.Zero;
			}
		}

		public void Draw(SpriteBatch spriteBatch){
			if (PlayerPhysics.Speed.X > 0.0f)
			{
				MoveFaceLeft = false;
			}
			else if (PlayerPhysics.Speed.X < 0.0f)
			{
				MoveFaceLeft = true;
			}
			Vector2 vpos = new Vector2();

            int size_of_frame = 32;
            float scale = 3; //Scale of our sprite
            Rectangle ActualFrame = new Rectangle(FrameCounter * size_of_frame, 0, size_of_frame, size_of_frame); //Cut out one frame
            vpos.X = PlayerPhysics.Position.X - scale * size_of_frame / 2;
            vpos.Y = PlayerPhysics.Position.Y - scale * size_of_frame / 2; 
            SpriteEffects Flip = ((MoveFaceLeft==TextureFaceLeft) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
			spriteBatch.Draw(PlayerTexture, vpos, ActualFrame, Color.White, 0.0f, Vector2.Zero, scale, Flip, 0.0f);
		}
	};
}
