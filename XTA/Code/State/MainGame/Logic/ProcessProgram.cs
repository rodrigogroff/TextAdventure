using System;
using System.Linq;

namespace XTA.Code.State
{
    public partial class GameState_ShowMainGame : GameState
    {
        public void ProcessProgram(string program)
        {
            if (program.StartsWith("/goto"))
            {
                ProcessRoom(program.Split(' ')[1]);
            }
        }
    }
}
