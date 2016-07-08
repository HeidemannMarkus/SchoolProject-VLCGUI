using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using NVMH.VlcGui.Interfaces;
using NVMH.PlayerManage;


namespace NVMH.VlcGui
{
    public class GuiManager
    {

        private IPlayer m_player;
        private IPlayerList m_playerList;
        private PlayerManager m_manager;




        public GuiManager()
        {
            m_player = null;
            m_playerList = new WindowConfig();
            m_manager = new PlayerManager();
        }

    }
}
