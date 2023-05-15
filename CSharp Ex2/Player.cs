namespace CSharp_Ex2
{
    public enum ePlayers
    {
        PlayerOne,
        PlayerTwo
    }

    public class Player
    {
        private ePlayers m_PlayerId;
        private int m_Score;
        private eCellType m_CellType;

        public Player(ePlayers i_PlayerId, int i_Score, eCellType i_CellType)
        {
            m_PlayerId = i_PlayerId;
            m_Score = i_Score;
            m_CellType = i_CellType;
        }

        public int Score
        {
            get
            {
                return m_Score;
            }

            set
            {
                m_Score = value;
            }
        }

        public ePlayers PlayerId
        {
            get
            {
                return m_PlayerId;
            }

            set
            {
                m_PlayerId = value;
            }
        }

        public eCellType CellType
        {
            get
            {
                return m_CellType;
            }

            set
            {
                m_CellType = value;
            }
        }

        public override string ToString()
        {
            string playerString = string.Empty;
            switch (PlayerId)
            {
                case ePlayers.PlayerOne:
                    playerString = "Player One";
                    break;
                case ePlayers.PlayerTwo:
                    playerString = "Player Two";
                    break;
            }

            return playerString;
        }

        // 1) Player gets prompted to input a move
        // 2) Player's move is then validated to see if he didn;t input any illegal chars
        // 3) If move is legal the it returns it.
        // 4) If player pressed 'q' then the player move is returned as the point index (-1,-1)
        public PointIndex PlayTurn(Board i_GameBoard, Player i_FirstPlayer, Player i_SecondPlayer)
        {
            IO.PrintPlayerTurnPrompt(this);
            PointIndex playerMove = IO.GetPlayerIntendedCell(i_GameBoard, this, i_FirstPlayer, i_SecondPlayer);
            playerMove.Row--;
            playerMove.Column--;

            return playerMove;
        }
    }
}
