using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp_Ex2
{
    public enum ePlayers
    {
        PlayerOne,
        PlayerTwo
    }
    public enum eMode
    {
        Human,
        Computer,
        Exit
    }
    public class Player
    {
        private ePlayers m_playerId;
        private int m_score;
        private eMode m_mode;
        private eCellType m_cellType;


        public Player(ePlayers playerId, int score, eMode mode, eCellType cellType)
        {
            m_playerId = playerId;
            m_score = score;
            m_mode = mode;
            m_cellType = cellType;
        }

        public eMode Mode
        {
            get
            {
                return m_mode;
            }
            set
            {
                m_mode = value;
            }
        }
        public int Score
        {
            get
            {
                return m_score;
            }
            set
            {
                m_score = value;
            }
        }

        public ePlayers PlayerId
        {
            get
            {
                return m_playerId;
            }
            set
            {
                m_playerId = value;
            }
        }
        public eCellType CellType
        {
            get
            {
                return m_cellType;
            }
            set 
            {
                m_cellType = value;
            }
        }

        public override string ToString()
        {
            string playerString = string.Empty;
            switch(PlayerId)
            {
                case ePlayers.PlayerOne:
                    playerString = "Player 1";
                    break;
                case ePlayers.PlayerTwo:
                    playerString = "Player 2";
                    break;
            }

            return playerString;
        }
    }
}
