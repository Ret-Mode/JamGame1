
using System;
using System.Xml;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
// NOTE (R-M): agressive checks for xml config file?


namespace Game1
{
	public class SetupReader
	{
		private const String Base = "Config";
		private const String OptsName = "Content\\config.xml";
		private const String Version = "V003";


		// NOTE (R-M): check if xml exist and 
		private void CheckAndCreateConfig()
		{

			XmlDocument Options = new XmlDocument();
			if (!System.IO.File.Exists(OptsName))
			{
				XmlNode N = Options["Config"];

				if (N == null)
				{
					N = Options.CreateElement("Config");
					Options.AppendChild(N);
					Options.Save(OptsName);
				}
			}
			else
			{ // NOTE (R-M): PARANOID
				Options.Load(OptsName);
				XmlNode N = Options["Config"];

				if (N == null)
				{
					N = Options.CreateElement("Config");
					Options.AppendChild(N);
					Options.Save(OptsName);
				}
			}

		}


		private void RegisterNode(XmlDocument Doc, String Node)
		{
			XmlNode N = Doc[Node];
			if (N == null)
			{
				N = Doc.CreateElement(Node);
				Doc.AppendChild(N);

			}
		}

		private void RegisterNode(XmlDocument Doc, XmlNode Parent, String Node)
		{
			XmlElement N = Parent[Node];
			if (N == null)
			{
				N = Doc.CreateElement(Node);
				Parent.AppendChild(N);
			}
		}

		public SetupReader()
		{
			CheckAndCreateConfig();
		}

		public void SaveTextures(Game1.Texture_Base[] textures)
		{
			XmlDocument Options = new XmlDocument();
			Options.Load(OptsName);
            RegisterNode(Options, Base);
			XmlElement Textures = Options[Base]["Textures"];

			if (Textures != null)
			{
				Textures.RemoveAll();
			}
            RegisterNode(Options, Options[Base], "Textures");
			Textures = Options[Base]["Textures"];
			foreach (Game1.Texture_Base T in textures)
			{
				XmlElement N = Options.CreateElement(T.tex.Name);
				N.SetAttribute("FaceLeft", T.FaceLeft.ToString());
				Textures.AppendChild(N);

			}
			Options.Save(OptsName);
						
		}

		public Game1.Texture_Base[] ReadTextures(Game1.Texture_Base[] textures, Microsoft.Xna.Framework.Content.ContentManager cmanager)
		{
			XmlDocument Options = new XmlDocument();
			Options.Load(OptsName);
			RegisterNode(Options, Base);
			XmlNodeList E = Options[Base]["Textures"].ChildNodes;
			if (E != null)
			{
				textures = new Game1.Texture_Base[E.Count];

				for (int i = 0; i < E.Count; ++i)
				{
					String Name = E[i].Name;
					String Face = ((XmlElement)E[i]).GetAttribute("FaceLeft");

					textures[i].tex = cmanager.Load<Texture2D>(Name);
					textures[i].FaceLeft = bool.Parse(Face);
				}
			}
			return textures;

		}

		public void SaveActor(Player Object, String ActorNode)
		{
			XmlDocument Options = new XmlDocument();
			Options.Load(OptsName);

			RegisterNode(Options,Base);
			XmlElement Ver_ = Options[Base]["Version"];
			if (Ver_ != null)
			{
				Options[Base].RemoveChild(Ver_);
			}
            RegisterNode(Options, Options[Base], "Version");
			Options[Base]["Version"].InnerText = Version;

			XmlElement Player_ = Options[Base]["Actor"][ActorNode];
			//NOTE (R-M): We'll overwrite only this node in file
			if (Player_ != null)
			{
				Options[Base]["Actor"].RemoveChild(Player_);
			}
            
			RegisterNode(Options, Options[Base], "Actor");
			RegisterNode(Options, Options[Base]["Actor"], ActorNode);
			Player_ = Options[Base]["Actor"][ActorNode];
			Player_.SetAttribute("DimensionX", Object.PlayerPhysics.Dimension.X.ToString());
			Player_.SetAttribute("DimensionY", Object.PlayerPhysics.Dimension.Y.ToString());
			Player_.SetAttribute("MoveAccelerationX", Object.PlayerPhysics.MoveAcceleration.X.ToString());
			Player_.SetAttribute("MoveAccelerationY", Object.PlayerPhysics.MoveAcceleration.Y.ToString());
			Player_.SetAttribute("MaxSpeedX", Object.PlayerPhysics.MaxSpeed.X.ToString());
			Player_.SetAttribute("MaxSpeedY", Object.PlayerPhysics.MaxSpeed.Y.ToString());
			Player_.SetAttribute("SpeedFalloffX", Object.PlayerPhysics.SpeedFalloff.X.ToString());
			Player_.SetAttribute("SpeedFalloffY", Object.PlayerPhysics.SpeedFalloff.Y.ToString());
			Player_.SetAttribute("Texture", Object.PlayerTexture.Name);
			Options.Save(OptsName);
		}

		public void ReadActor(Player Object, String ActorNode, Game1.Texture_Base[] textures)
		{

			XmlDocument Options = new XmlDocument();
			Options.Load(OptsName);
			String V = Options[Base]["Version"].InnerText;
			XmlElement N = Options[Base]["Actor"][ActorNode];
			if (N != null)
			{
				if (V.Equals("V003")){
					Object.PlayerPhysics.Dimension.X = float.Parse(N.GetAttribute("DimensionX"));
					Object.PlayerPhysics.Dimension.Y = float.Parse(N.GetAttribute("DimensionY"));
					Object.PlayerPhysics.MoveAcceleration.X = float.Parse(N.GetAttribute("MoveAccelerationX"));
					Object.PlayerPhysics.MoveAcceleration.Y = float.Parse(N.GetAttribute("MoveAccelerationY"));
					Object.PlayerPhysics.MaxSpeed.X = float.Parse(N.GetAttribute("MaxSpeedX"));
					Object.PlayerPhysics.MaxSpeed.Y = float.Parse(N.GetAttribute("MaxSpeedY"));
					Object.PlayerPhysics.SpeedFalloff.X = float.Parse(N.GetAttribute("SpeedFalloffX"));
					Object.PlayerPhysics.SpeedFalloff.Y = float.Parse(N.GetAttribute("SpeedFalloffY"));
					Object.TextureName = N.GetAttribute("Texture");
					Object.UpdateTexture(textures);


				}
			}
		}
	}
}
