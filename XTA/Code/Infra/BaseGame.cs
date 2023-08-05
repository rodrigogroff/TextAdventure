using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace XTA.Code.Infra
{
    public class BaseGame
    {
        public string currentDir = "";

        public Texture2D LoadTexture(GraphicsDevice device, string name)
        {
            if (currentDir == "")
                currentDir = Directory.GetCurrentDirectory() + "\\Content\\";

            using (FileStream stream = new FileStream(currentDir + name + ".png", FileMode.Open))
            {
                var r = Texture2D.FromStream(device, stream);
                stream.Close();
                return r;
            }
        }
    }
}
