using System;

namespace CSharp_Ex2
{
    public class AiPlayer
    {
        private Player m_aiPlayer;

        public AiPlayer(ePlayers i_PlayerId, int i_Score, eCellType i_CellType)
        {
            m_aiPlayer = new Player(i_PlayerId, i_Score, i_CellType);
        }

        public ePlayers Id
        {
            get
            {
                return m_aiPlayer.PlayerId;
            }

            set
            {
                m_aiPlayer.PlayerId = value;
            }
        }

        public Player PlayerData
        {
            get
            {
                return m_aiPlayer;
            }

            set
            {
                m_aiPlayer = value;
            }
        }

        // Randomize the computer turn
        public PointIndex PlayTurn(Board i_GameBoard, int i_BoardSize)
        {
            PointIndex aiMove = new PointIndex();
            do
            {
                Random random = new Random();
                aiMove.Row = random.Next(0, i_BoardSize);
                aiMove.Column = random.Next(0, i_BoardSize);
            }
            while (!i_GameBoard.isCellEmpty(aiMove));

            return aiMove;
        }
    }
}
