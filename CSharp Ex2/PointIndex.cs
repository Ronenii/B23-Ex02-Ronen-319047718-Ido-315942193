namespace CSharp_Ex2
{
    public struct PointIndex
    {
        private int m_row;
        private int m_column;

        public PointIndex(int i_row, int i_column)
        {
            m_row = i_row;
            m_column = i_column;
        }

        public int Row
        {
            get
            {
                return m_row;
            }
            set
            {
                m_row = value;
            }
        }
        public int Column
        {
            get
            {
                return m_column;
            }
            set
            {
                m_column = value;
            }
        }
    }
}
