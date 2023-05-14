namespace CSharp_Ex2
{
    public enum ePlayers
    {
        PlayerOne,
        PlayerTwo
    }

    public class Player
    {
        private ePlayers m_playerId;
        private int m_score;
        private eCellType m_cellType;


        public Player(ePlayers playerId, int score, eCellType cellType)
        {
            m_playerId = playerId;
            m_score = score;
            m_cellType = cellType;
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
            PointIndex playerMove;
            IO.PrintPlayerTurnPrompt(this);
            playerMove = IO.GetHumanPointIndex(i_GameBoard, this, i_FirstPlayer, i_SecondPlayer);
            playerMove.Row--;
            playerMove.Column--;

            return playerMove;
        }


    }
}
