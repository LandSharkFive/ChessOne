using System.Reflection;

namespace ChessOne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitBoard();
            this.Paint += new PaintEventHandler(Form1_Paint);
        }

        private const int TileSize = 80;
        private const int GridSize = 8;
        private Color Clr1 = Color.White;
        private Color Clr2 = Color.DarkGray;

        private PictureBox[,] chessBox;

        private Board board = new Board();

        private TextBox TextBox1 = new TextBox();

        Dictionary<string, Image> Images = new Dictionary<string, Image>();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Width = 800;
            this.Height = 800;
            this.Text = "chess";

            DrawLabels();
            DrawTextBox1();
            LoadImages();
            board.Reset();
        }

        private void InitBoard()
        {
            // initialize the "chess board"
            chessBox = new PictureBox[GridSize, GridSize];

            // rows and columns
            for (var n = 0; n < 8; n++)
            {
                for (var m = 0; m < 8; m++)
                {
                    // create new Panel control which will be one 
                    // chess board tile
                    PictureBox newBox = new PictureBox
                    {
                        Size = new Size(TileSize, TileSize),
                        Location = new Point(TileSize * n, TileSize * m),
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };

                    // add to Form's Controls so that they show up
                    Controls.Add(newBox);

                    // add to our 2d array of panels for future use
                    chessBox[n, m] = newBox;

                    // color the backgrounds
                    if ((n + m) % 2 == 0)
                    {
                        newBox.BackColor = Clr1;
                    }
                    else
                    {
                        newBox.BackColor = Clr2;
                    }
                }
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawAllPieces(board);
            Refresh();
        }

        private void DrawLabels()
        {
            // labels
            for (int i = 0; i < 8; i++)
            {
                int k = 8 - i;
                Label newLabel = new Label();
                newLabel.Text = k.ToString();
                newLabel.Location = new Point(650, 30 + TileSize * i);
                Controls.Add(newLabel);
            }

            // labels
            string[] rank = { "A", "B", "C", "D", "E", "F", "G", "H" };
            for (int j = 0; j < 8; j++)
            {
                Label newLabelB = new Label();
                newLabelB.Text = rank[j];
                newLabelB.Width = 20;
                newLabelB.Location = new Point(35 + TileSize * j, 650);
                Controls.Add(newLabelB);
            }
        }

        private void DrawTextBox1()
        {
            TextBox1.Size = new Size(600, 100);
            TextBox1.Location = new Point(0, 700);
            Controls.Add(TextBox1);
            TextBox1.KeyDown += new KeyEventHandler(TextBox1_KeyDown);
        }


        private void LoadImages()
        {
            // Read the resources of the currently executing assembly
            var assembly = Assembly.GetExecutingAssembly();
            // Get all resource names with "Build action = Embedded Resource"
            var names = assembly.GetManifestResourceNames();

            foreach (var name in names)
            {
                // Check if it's a PNG file
                if (name.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase))
                {
                    // Create a sanitized name by removing the assembly and file extension parts
                    var prefix = $"{assembly.GetName().Name}.Image.".Length;
                    var sanitizedName = name.Substring(prefix, name.IndexOf(".png") - prefix);

                    // Load the image from the manifest resource stream
                    var image = Image.FromStream(assembly.GetManifestResourceStream(name));

                    // Add the image to the dictionary
                    Images.Add(sanitizedName, image);
                }
            }

        }

        private void DrawAllPieces(Board board)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    int a = board.piece[x, y];
                    DrawPiece(a, x, y);
                }
            }
        }

        private void DrawPiece(int piece, int x, int y)
        {
            PictureBox pb = chessBox[x, y];
            switch (piece)
            {
                case 0:
                    pb.Image = null;
                    break;

                case 1:
                    pb.Image = Images["white-pawn"];
                    break;

                case 2:
                    pb.Image = Images["white-rook"];
                    break;

                case 3:
                    pb.Image = Images["white-knight"];
                    break;

                case 4:
                    pb.Image = Images["white-bishop"];
                    break;

                case 5:
                    pb.Image = Images["white-queen"];
                    break;

                case 6:
                    pb.Image = Images["white-king"];
                    break;

                case 7:
                    pb.Image = Images["black-pawn"];
                    break;

                case 8:
                    pb.Image = Images["black-rook"];
                    break;

                case 9:
                    pb.Image = Images["black-knight"];
                    break;

                case 10:
                    pb.Image = Images["black-bishop"];
                    break;

                case 11:
                    pb.Image = Images["black-queen"];
                    break;

                case 12:
                    pb.Image = Images["black-king"];
                    break;
            }
        }


        private void MoveByStr(Board board, string pos1, string pos2)
        {
            if (pos1.Length < 2 || pos2.Length < 2)
            {
                return;
            }
            int a = Board.GetFile(pos1[0]);
            int b = Board.GetRank(pos1[1]);
            int c = Board.GetFile(pos2[0]);
            int d = Board.GetRank(pos2[1]);
            if (a == -1 || b == -1 || c == -1 || d == -1)
            {
                return;
            }
            MoveByInt(board, a, b, c, d);
        }

        private void MoveByInt(Board board, int a, int b, int c, int d)
        {
            board.Move(a, b, c, d);
            DrawPiece(0, a, b);
            int g = board.GetPiece(c, d);
            DrawPiece(g, c, d);
        }

        private void SetByStr(Board board, string pos, string type)
        {
            if (pos.Length < 2)
            {
                return;
            }
            int a = Board.GetFile(pos[0]);
            int b = Board.GetRank(pos[1]);
            if (a == -1 || b == -1)
            {
                return;
            }
            int p = Board.GetPieceByStr(type);
            if (p == -1)
            {
                return;
            }
            SetByInt(board, a, b, p);
        }

        private void SetByInt(Board board, int x, int y, int a)
        {
            board.Set(x, y, a);
            DrawPiece(a, x, y);
        }

        private void RemoveByStr(Board board, string pos)
        {
            if (pos.Length < 2)
            {
                return;
            }
            int a = Board.GetFile(pos[0]);
            int b = Board.GetRank(pos[1]);
            if (a == -1 || b == -1)
            {
                return;
            }
            RemoveByInt(board, a, b);
        }


        private void RemoveByInt(Board board, int x, int y)
        {
            board.Remove(x, y);
            DrawPiece(0, x, y);
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoCommand(TextBox1.Text);
                TextBox1.Text = "";
            }
        }

        private void DoCommand(string str)
        {
            str = str.ToLower();
            string[] arg = str.Split(' ');
            string cmd = arg[0];
            if (cmd.StartsWith("c"))
            {
                board.Clear();
            }
            else if (cmd.StartsWith("d"))
            {
                if (arg.Length > 1)
                {
                    RemoveByStr(board, arg[1]);
                }
            }
            else if (cmd.StartsWith("k"))
            {
                if (arg.Length > 2)
                {
                    int color = 0;
                    if (arg[1].StartsWith("w"))
                    {
                        color = 1;
                    }
                    if (arg[2].Length == 2)
                    {
                        board.ShortCastle(color);
                    }
                    else if (arg[2].Length == 3)
                    {
                        board.LongCastle(color);
                    }
                }
            }
            else if (cmd.StartsWith("m"))
            {
                if (arg.Length > 2)
                {
                    MoveByStr(board, arg[1], arg[2]);
                }
            }
            else if (cmd.StartsWith("r"))
            {
                board.Reset();
            }
            else if (cmd.StartsWith("s"))
            {
                if (arg.Length > 2)
                {
                    SetByStr(board, arg[1], arg[2]);
                }
            }
        }


    }
}
