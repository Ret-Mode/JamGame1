using System;
namespace Game1
{
	public class CollisionLine
	{
		enum SlopeDir { Down, DownLeft, DownRight, Left, Right, UpLeft, UpRight, Up};
		SlopeDir Slope;
		Vector2D Position;
		Vector2D Direction; // NOTE (R-M): Assuming that begin of a vector is (0,0), and vector begin at pod
		public CollisionLine(double PX, double PY, double DX, double DY)
		{
			Position.X = PX;
			Position.Y = PY;
			Direction.X = DX;
			Direction.Y = DY;

		}
	}
}
