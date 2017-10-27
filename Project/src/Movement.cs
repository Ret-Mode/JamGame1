using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace Game1
{


	public enum MovementDir {Down, DownLeft, DownRight, Left, Right, UpLeft, UpRight, Up, Still};

	public class Movement
	{
		public Vector2 Position;
		public Vector2 Dimension;
		public Vector2 Speed;
		public Vector2 Acceleration;
		public Vector2 MoveAcceleration;
		public Vector2 MaxSpeed;
		public Vector2 SpeedFalloff;
		public bool BoxCollision(Movement Other) {
			if(Math.Abs(this.Position.X - Other.Position.X) > (this.Dimension.X + Other.Dimension.X)) return false;
			else if (Math.Abs(this.Position.Y - Other.Position.Y) > (this.Dimension.Y + Other.Dimension.Y)) return false;
			return true;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MoveAcc(MovementDir Dir)
		{
			switch (Dir){
				case(MovementDir.Down):
					Acceleration.Y = MoveAcceleration.Y;
					break;
				case(MovementDir.DownLeft):
					Acceleration.Y = MoveAcceleration.Y * 0.7f;
					Acceleration.X = - MoveAcceleration.X * 0.7f;
					break;
				case(MovementDir.DownRight):
					Acceleration.Y = MoveAcceleration.Y * 0.7f;
					Acceleration.X = MoveAcceleration.X * 0.7f;
					break;
				case(MovementDir.Left):
					Acceleration.X = - MoveAcceleration.X;
					break;
				case(MovementDir.Right):
					Acceleration.X = MoveAcceleration.X;
					break;
				case(MovementDir.UpLeft):
					Acceleration.Y = - MoveAcceleration.Y * 0.7f;
					Acceleration.X = - MoveAcceleration.X * 0.7f;
					break;
				case(MovementDir.UpRight):
					Acceleration.Y = - MoveAcceleration.Y * 0.7f;
					Acceleration.X = MoveAcceleration.X * 0.7f;
					break;
				case(MovementDir.Up):
					Acceleration.Y = - MoveAcceleration.Y;
					break;
				case(MovementDir.Still):
					break;
				default:
					break;
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void MoveSpd(MovementDir Dir){
			Speed.X *= SpeedFalloff.X;
			Speed.X += Acceleration.X;
			Speed.Y *= SpeedFalloff.Y;
			Speed.Y += Acceleration.Y;
			//double TmpSpeed;
			float TmpMaxSpeed;
			switch (Dir){
				case(MovementDir.Down):
					Speed.Y = ((Speed.Y < MaxSpeed.Y) ? (Speed.Y) : (MaxSpeed.Y));
					break;
				case(MovementDir.DownLeft):
					TmpMaxSpeed = MaxSpeed.Y * 0.7f;
					Speed.Y = ((Speed.Y < TmpMaxSpeed) ? (Speed.Y) : (TmpMaxSpeed));
					TmpMaxSpeed = - MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X > TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.DownRight):
					TmpMaxSpeed = MaxSpeed.Y * 0.7f;
					Speed.Y = ((Speed.Y < TmpMaxSpeed) ? (Speed.Y) : (TmpMaxSpeed));
					TmpMaxSpeed = MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X < TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.Left):
					TmpMaxSpeed = - MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X > TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.Right):
					TmpMaxSpeed = MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X < TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.UpLeft):
					TmpMaxSpeed = -MaxSpeed.Y;
					Speed.Y = ((Speed.Y > TmpMaxSpeed) ? (Speed.Y) : (TmpMaxSpeed));
					TmpMaxSpeed = - MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X > TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.UpRight):
					TmpMaxSpeed = -MaxSpeed.Y;
					Speed.Y = ((Speed.Y > TmpMaxSpeed) ? (Speed.Y) : (TmpMaxSpeed));
					TmpMaxSpeed = MaxSpeed.X * 0.7f;
					Speed.X = ((Speed.X < TmpMaxSpeed) ? (Speed.X) : (TmpMaxSpeed));
					break;
				case(MovementDir.Up):
					TmpMaxSpeed = -MaxSpeed.Y;
					Speed.Y = ((Speed.Y > TmpMaxSpeed) ? (Speed.Y) : (TmpMaxSpeed));
					break;
				case(MovementDir.Still):
					break;
				default:
					break;
			}
			Acceleration.Y = Acceleration.X = 0.0f;
			Position.X += Speed.X;
			Position.Y += Speed.Y;

		}

		public void Move(MovementDir Dir){
			MoveAcc(Dir);
			MoveSpd(Dir);
			// TODO (R-M): check collisions;
		}

	};
}
