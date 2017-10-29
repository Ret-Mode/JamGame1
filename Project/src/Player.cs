using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1 {
	public class Player {
		public Texture2D PlayerTexture;
		public Movement PlayerPhysics;
		public bool TextureFaceLeft;
		private bool MoveFaceLeft;
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
			vpos.X = PlayerPhysics.Position.X - PlayerTexture.Width/2;
			vpos.Y = PlayerPhysics.Position.Y - PlayerTexture.Height;
			SpriteEffects Flip = ((MoveFaceLeft==TextureFaceLeft) ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
			spriteBatch.Draw(PlayerTexture, vpos, null, Color.White, 0.0f, Vector2.Zero, 1.0f, Flip, 0.0f);

		}
	};
}
