using System;

namespace Game1
{

	public class Vector2D
	{
		public double X;
		public double Y;
		public double Dot(Vector2D Other) { return X * Other.X + Y * Other.Y; }
		public double ThisPerp(Vector2D Other) { return X * Other.Y - Y * Other.X; }
		public double OtherPerp(Vector2D Other) { return Y * Other.X - X * Other.Y; }
		public double LengthSqr() { return X * X + Y * Y; }
		public double Length() { return Math.Sqrt(this.X * this.X + this.Y * this.Y); } 
		public Vector2D Add(Vector2D Other){ Vector2D Tmp = new Vector2D(); Tmp.X = X + Other.X; Tmp.Y = Y + Other.Y; return Tmp; }
		public Vector2D Diff(Vector2D Other) { Vector2D Tmp = new Vector2D(); Tmp.X = X - Other.X; Tmp.Y = Y - Other.Y; return Tmp; }
		public Vector2D Mult(double Val) { Vector2D Tmp = new Vector2D(); Tmp.X = X * Val; Tmp.Y = Y * Val; return Tmp; }
		public Vector2D Div(double Val) { Vector2D Tmp = new Vector2D(); Tmp.X = X / Val; Tmp.Y = Y / Val; return Tmp; }
	};

}