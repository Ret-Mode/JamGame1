using System;
using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace Game1
{
	abstract public class DrawingClass
	{
		abstract public void Draw(Player[] players);
		abstract public void StartDraw(GraphicsDevice device);
		abstract public void EndDraw();
	};

	public class DrawDebug : DrawingClass
	{
		SpriteBatch sBatch;
		Texture2D tex;
		Color[] color;
		Color still = Color.Green;
		Color move = Color.Blue;
		Color attack = Color.Red;

		public DrawDebug(GraphicsDevice device)
		{
			sBatch = new SpriteBatch(device);
			tex = new Texture2D(device, 1, 1);
			color = new Color[1];
			still.A = 127;
			move.A = 127;
			attack.A = 127;
		}

		~DrawDebug()
		{
			tex.Dispose();
			sBatch.Dispose();
		}

		public override void StartDraw(GraphicsDevice device)
		{
			device.Clear(Color.Black);
			sBatch.Begin();
		}

		public override void EndDraw()
		{
			sBatch.End();
		}

		public override void Draw(Player[] players)
		{
			
			Rectangle rect = new Rectangle();

			foreach (Player p in players)
			{
				color[0] = still;
				tex.SetData(color);
				rect.X = (int)p.PlayerPhysics.Position.X - (int)p.PlayerPhysics.Dimension.X;
				rect.Y = (int)p.PlayerPhysics.Position.Y - (int)p.PlayerPhysics.Dimension.Y;
				rect.Width = (int)p.PlayerPhysics.Dimension.X * 2;
				rect.Height = (int)p.PlayerPhysics.Dimension.Y * 2;
				sBatch.Draw(tex, rect, Color.White);
			}

			foreach (Player p in players)
			{				
				p.Draw(sBatch);
			}
		}
	};

	public class DrawRelease : DrawingClass
	{
		SpriteBatch sBatch;

		public DrawRelease(GraphicsDevice device)
		{
			sBatch = new SpriteBatch(device);
		}

		~DrawRelease()
		{
			sBatch.Dispose();
		}

		public override void StartDraw(GraphicsDevice device)
		{
			device.Clear(Color.Black);
			sBatch.Begin();
		}

		public override void EndDraw()
		{
			sBatch.End();
		}

		public override void Draw(Player[] players)
		{
			foreach (Player p in players)
			{
				p.Draw(sBatch);
			}
		}
	};


}
