namespace CSharp_Ex2
{
    public struct PointIndex
    {
        private int m_Row;
        private int m_Column;

        public PointIndex(int i_Row, int i_Column)
        {
            m_Row = i_Row;
            m_Column = i_Column;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Column
        {
            get
            {
                return m_Column;
            }

            set
            {
                m_Column = value;
            }
        }

        // Check if the given point index is quitting point
        public bool IsQuitting()
        {
            return (m_Row == -1) && (m_Column == -1);
        }

        // Check if the point is inbounds of the board.
        public bool IsInBounds(int i_BoardSize)
        {
            return (m_Row < i_BoardSize) && (m_Column < i_BoardSize) && (m_Column >= 0) && (m_Row >= 0);
        }
    }
}
