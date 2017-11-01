using System;
using Microsoft.Xna.Framework;
namespace Game1
{
	public class CollisionLine
	{
		enum SlopeDir { Down, DownLeft, DownRight, Left, Right, UpLeft, UpRight, Up};
		SlopeDir Slope;
		Vector2 Position;
		Vector2 Direction; // NOTE (R-M): Assuming that begin of a vector is (0,0), and vector begin at pod
		public CollisionLine(float PX, float PY, float DX, float DY)
		{
			Position.X = PX;
			Position.Y = PY;
			Direction.X = DX;
			Direction.Y = DY;

		}
	}
}
