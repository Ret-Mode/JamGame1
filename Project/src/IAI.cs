using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
namespace Game1
{
	//NOTE (R-M): base class for intelligence; don't rely on this api, it will
	//change a lot
	abstract public class IAI
	{
		abstract public void InventNextActions(Player[] players, Player Me);
	};

	public class PlayerAI : IAI
	{
		public override void InventNextActions(Player[] players, Player Me)
		{
			MovementDir Dir;
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
			Me.PlayerPhysics.Move(Dir);
		}
	};

	public class IdiotAI : IAI
	{
		public enum IdiotActions
		{
			FollowPlayer=0, WanderLikeAnIdiot=1, Wait=2
		};

		private struct IdiotActionState
		{
			public uint FramesToEndAction;
			public IdiotActions Action;
			public MovementDir Dir;
		}

		private IdiotActionState State;

		public IdiotAI(IdiotActions StartAction, uint FrameToStartCounting)
		{
			State.Action = StartAction;
			State.FramesToEndAction = FrameToStartCounting;
		}

		public override void InventNextActions(Player[] players, Player Me)
		{

			if (State.FramesToEndAction == 0)
			{
				State.Action = (IdiotActions)(((int)State.Action + 1) % 3);
				if (IdiotActions.WanderLikeAnIdiot == State.Action){
					State.Dir = (MovementDir)((9 - (int)State.Dir)%10);
					State.FramesToEndAction = 20;
				} else {
					State.FramesToEndAction = 50;
				}

			}
			else
			{
				--State.FramesToEndAction;
			}

			switch (State.Action)
			{
				case(IdiotActions.FollowPlayer):
					float DX = players[0].PlayerPhysics.Position.X - Me.PlayerPhysics.Position.X;
					float DY = players[0].PlayerPhysics.Position.Y - Me.PlayerPhysics.Position.Y;
					if (DX != 0.0f)
					{
						float Slope = Math.Abs(DY / DX);
						if (Slope > 2.0f)
						{
							if (DY > 0.0f)
							{
								Me.PlayerPhysics.Move(MovementDir.Down);
							}
							else if (DY < 0.0f)
							{
								Me.PlayerPhysics.Move(MovementDir.Up);
							}
							else
							{
								Me.PlayerPhysics.Move(MovementDir.Still);
							}
						}
						else if (Slope < 0.5f)
						{
							if (DX > 0.0f)
							{
								Me.PlayerPhysics.Move(MovementDir.Right);
							}
							else if (DX < 0.0f)
							{
								Me.PlayerPhysics.Move(MovementDir.Left);
							}
							else
							{
								Me.PlayerPhysics.Move(MovementDir.Still);
							}
						}
						else
						{
							if (DY > 0.0f)
							{
								if (DX > 0.0f)
								{
									Me.PlayerPhysics.Move(MovementDir.DownRight);
								}
								else if (DX < 0.0f)
								{
									Me.PlayerPhysics.Move(MovementDir.DownLeft);
								}	
							}
							else if (DY < 0.0f)
							{
								if (DX > 0.0f)
								{
									Me.PlayerPhysics.Move(MovementDir.UpRight);
								}
								else if (DX < 0.0f)
								{
									Me.PlayerPhysics.Move(MovementDir.UpLeft);
								}
							}
						}
					}
					else if (DY > 0.0f)
					{
						Me.PlayerPhysics.Move(MovementDir.Down);
					}
					else if (DY < 0.0f)
					{
						Me.PlayerPhysics.Move(MovementDir.Up);
					}
					else
					{
						Me.PlayerPhysics.Move(MovementDir.Still);
					}
					break;

				case(IdiotActions.WanderLikeAnIdiot):
					Me.PlayerPhysics.Move(State.Dir);
					break;
				case(IdiotActions.Wait):
					Me.PlayerPhysics.Move(MovementDir.Still);
					break;
				default:
					
					break;

			}

		}
	};
}
