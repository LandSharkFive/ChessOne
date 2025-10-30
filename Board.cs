using System.Text;

namespace ChessOne
{
    public class Board
    {
        const int GridSize = 8;

        public int[,] piece = new int[GridSize, GridSize];

        public enum PieceType
        {
            Empty = 0, 
            WP = 1, 
            WR = 2,
            WN = 3,
            WB = 4,
            WQ = 5,
            WK = 6,
            BP = 7,
            BR = 8,
            BN = 9,
            BB = 10,
            BQ = 11,
            BK = 12
        }

        public void Clear()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    piece[x, y] = 0;
                }
            }
        }

        public void Reset()
        {
            Clear();

            // black
            piece[0, 0] = 8;
            piece[1, 0] = 9;
            piece[2, 0] = 10;
            piece[3, 0] = 11;
            piece[4, 0] = 12;
            piece[5, 0] = 10;
            piece[6, 0] = 9;
            piece[7, 0] = 8;

            // white
            piece[0, 7] = 2;
            piece[1, 7] = 3;
            piece[2, 7] = 4;
            piece[3, 7] = 5;
            piece[4, 7] = 6;
            piece[5, 7] = 4;
            piece[6, 7] = 3;
            piece[7, 7] = 2;

            // pawns
            for (int x = 0; x < 8; x++)
            {
                piece[x, 1] = 7;
                piece[x, 6] = 1;
            }
        }

        public void Set(int x, int y, int a)
        {
            piece[x, y] = a;
        }

        public void Remove(int x, int y)
        {
            piece[x, y] = 0;
        }

        public int GetPiece(int x, int y)
        {
            return piece[x, y];
        }

        public void Move(int a, int b, int c, int d)
        {
            piece[c, d] = piece[a, b];
            piece[a, b] = 0;
        }

        public void ShortCastle(int color)
        {
            if (color == 0)
            {
                // black
                piece[4, 0] = 0;
                piece[5, 0] = 8;
                piece[6, 0] = 12;
                piece[7, 0 ] = 0;   
            }
            else
            {
                // white
                piece[4, 7] = 0;
                piece[5, 7] = 2;
                piece[6, 7] = 6;
                piece[7, 7] = 0;
            }
        }

        public void LongCastle(int color)
        {
            if (color == 0)
            {
                // black
                piece[0, 0] = 0;
                piece[1, 0] = 0;
                piece[2, 0] = 12;
                piece[3, 0] = 8;
                piece[4, 0] = 0;
            }
            else
            {
                // white
                piece[0, 7] = 0;
                piece[1, 7] = 0;
                piece[2, 7] = 6;
                piece[3, 7] = 2;
                piece[4, 7] = 0;
            }
        }

        public static int GetFile(char ch)
        {
            int result = -1;
            char c = char.ToLower(ch);
            switch (c)
            {
                case 'a': result = 0; break;
                case 'b': result = 1; break;
                case 'c': result = 2; break;
                case 'd': result = 3; break;
                case 'e': result = 4; break;
                case 'f': result = 5; break;
                case 'g': result = 6; break;
                case 'h': result = 7; break;
            }
            return result;
        }

        public static int GetRank(char ch)
        {
            char c = char.ToLower(ch);
            int result = -1;
            switch (c)
            {
                case '1': result = 7; break;
                case '2': result = 6; break;
                case '3': result = 5; break;
                case '4': result = 4; break;
                case '5': result = 3; break;
                case '6': result = 2; break;
                case '7': result = 1; break;
                case '8': result = 0; break;
            }
            return result;
        }

        public static int GetPieceByStr(string str)
        {
            string a = str.ToUpper();
            int result = -1;
            switch (a)
            {
                case "WP": result = 1; break;
                case "WR": result = 2; break;
                case "WN": result = 3; break;
                case "WB": result = 4; break;
                case "WQ": result = 5; break;
                case "WK": result = 6; break;
                case "BP": result = 7; break;
                case "BR": result = 8; break;
                case "BN": result = 9; break;
                case "BB": result = 10; break;
                case "BQ": result = 11; break;
                case "BK": result = 12; break;
            }
            return result;
        }

        public static bool IsSquare(string a)
        {
            if (a.Length < 2)
                return false;
            if (GetFile(a[0]) == -1)
                return false;
            if (GetRank(a[1]) == -1)
                return false;
            return true;
        }

        public void Move(string from, string to)
        {
            if (IsSquare(from) && IsSquare(to))
            {
                int a = GetFile(from[0]);
                int b = GetRank(from[1]);
                int c = GetFile(to[0]);
                int d = GetRank(to[1]);
                Move(a, b, c, d);
            }
        }

    }
}
