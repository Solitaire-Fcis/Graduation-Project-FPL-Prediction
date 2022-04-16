using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Windows.Shell;
// Python Runtime Environment Reference
using Python.Runtime;

// Directories may vary on different machines
// For Setup pythonnet solution must be installed and pointing to python home
// Change Directories to match your machine's configurations
namespace GP_Fantasy_Pred
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        #region All Declarations
        public PyObject ScriptReturn, Predicted_Points, Team, Substitutions, Captain, Cost;
        public int Budget, Gameweek, NumofDef, NumofMid, NumofFwd, fwdStart = -350,
            midStart = -450,defStart = -450, subStart = -175, total_points = 0;
        public List<List<string>> forwards, midfielders, defenders, subs;
        public List<string> goalkeeperStart, goalkeeperSub;
        public TextBlock Name = new TextBlock(), Points = new TextBlock();
        public Image FWD = new Image(), MID = new Image(), DEF = new Image(), GK = new Image(), SUB, GKSUB;
        #endregion

        public Window1(PyObject ScriptReturn, PyObject Predicted_Points, PyObject Team,
            PyObject Substitutions, PyObject Captain, PyObject Cost, int Budget)
        {
            this.Budget = Budget;
            this.ScriptReturn = ScriptReturn;
            this.Predicted_Points = Predicted_Points;
            this.Team = Team;
            this.Substitutions = Substitutions;
            this.Captain = Captain;
            this.Cost = Cost;
            InitializeComponent();
            Construct_Team();
            Draw_Players();
            PythonEngine.Shutdown();
        }
        
        // Drawing Players Fit
        public void Draw_Players()
        {
            #region Starting Players
            // Drawing Forwards
            if (this.forwards.Count == 2)
                fwdStart = -175;
            if(this.midfielders.Count == 4)
                midStart = -350;
            if(this.midfielders.Count == 3)
                midStart = -337;
            if(this.defenders.Count == 4)
                defStart = -350;
            if(this.defenders.Count == 3)
                defStart = -337;

            for (int i = 0;i < this.forwards.Count; i++)
            {
                string playerName = "";
                int posIndex = 0, points = 0;
                for (int j = 0; j < forwards[i].Count; j++)
                {
                    if (int.TryParse(forwards[i][j], out _))
                    {
                        posIndex = j;
                        points = int.Parse(forwards[i][j + 1]);
                        total_points += points;
                        break;
                    }
                }
                for (int j = 0; j < 2; j++)
                    playerName += forwards[i][j] + ' ';
                if (this.forwards.Count == 3)
                {
                    FWD = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(fwdStart + 15, 425, 0, 0);
                    Points.Margin = new Thickness(fwdStart + 15, 447, 0, 0);
                    if (forwards[i][posIndex - 1] == "(captain)")
                    {
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    FWD.Margin = new Thickness(fwdStart+15, 350, 0, 0);
                    FWD.Height = 65;
                    FWD.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(FWD);
                    fwdStart += 350;
                }
                else if(this.forwards.Count == 2)
                {
                    FWD = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(fwdStart + 15, 425, 0, 0);
                    Points.Margin = new Thickness(fwdStart + 15, 447, 0, 0);
                    if (forwards[i][posIndex - 1] == "(captain)")
                    {
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    FWD.Margin = new Thickness(fwdStart + 15, 350, 0, 0);
                    FWD.Height = 65;
                    FWD.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(FWD);
                    fwdStart += 350;
                }
                else if (this.forwards.Count == 1)
                {
                    FWD = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(0 + 15, 425, 0, 0);
                    Points.Margin = new Thickness(0 + 15, 447, 0, 0);
                    if (forwards[i][posIndex - 1] == "(captain)")
                    {
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                    }
                    else
                        FWD.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    FWD.Margin = new Thickness(0 + 15, 350, 0, 0);
                    FWD.Height = 65;
                    FWD.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(FWD);
                    break;
                }
            }
            // Drawing Midfielders
            for (int i = 0; i < this.midfielders.Count; i++)
            {
                string playerName = "";
                int posIndex = 0, points = 0;
                for (int j = 0; j < midfielders[i].Count; j++)
                {
                    if (int.TryParse(midfielders[i][j], out _))
                    {
                        posIndex = j;
                        points = int.Parse(midfielders[i][j + 1]);
                        total_points += points;
                        break;
                    }
                }
                for (int j = 0; j < 2; j++)
                    playerName += midfielders[i][j] + ' ';
                if (this.midfielders.Count == 3)
                {
                    MID = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(midStart + 15, 200, 0, 0);
                    Points.Margin = new Thickness(midStart + 15, 222, 0, 0);
                    if (midfielders[i][posIndex - 1] == "(captain)")
                    {
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    MID.Margin = new Thickness(midStart + 15, 125, 0, 0);
                    MID.Height = 65;
                    MID.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(MID);
                    midStart += 337;
                }
                else if (this.midfielders.Count == 4)
                {
                    MID = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(midStart + 15, 200, 0, 0);
                    Points.Margin = new Thickness(midStart + 15, 222, 0, 0);
                    if (midfielders[i][posIndex - 1] == "(captain)")
                    {
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    MID.Margin = new Thickness(midStart + 15, 125, 0, 0);
                    MID.Height = 65;
                    MID.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(MID);
                    midStart += 237;
                }
                else if (this.midfielders.Count == 5)
                {
                    MID = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(midStart + 15, 200, 0, 0);
                    Points.Margin = new Thickness(midStart + 15, 222, 0, 0);
                    if (midfielders[i][posIndex - 1] == "(captain)")
                    {
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        MID.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    MID.Margin = new Thickness(midStart + 15, 125, 0, 0);
                    MID.Height = 65;
                    MID.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(MID);
                    midStart += 225;
                }
            }
            // Drawing Defenders
            for (int i = 0; i < this.defenders.Count; i++)
            {
                string playerName = "";
                int posIndex = 0, points = 0;
                for(int j = 0;j < defenders[i].Count;j++)
                {
                    if(int.TryParse(defenders[i][j], out _))
                    {
                        posIndex = j;
                        points = int.Parse(defenders[i][j + 1]);
                        total_points += points;
                        break;
                    }
                }
                for(int j = 0;j < 2;j++)
                    playerName += defenders[i][j] + ' ';
                if (this.defenders.Count == 3)
                {
                    DEF = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(defStart + 15, 25, 0, 0);
                    Points.Margin = new Thickness(defStart + 15, 47, 0, 0);
                    if (defenders[i][posIndex - 1] == "(captain)")
                    {
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    DEF.Margin = new Thickness(defStart + 15, -50, 0, 0);
                    DEF.Height = 65;
                    DEF.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(DEF);
                    defStart += 337;
                }
                else if (this.defenders.Count == 4)
                {
                    DEF = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(defStart + 15, 25, 0, 0);
                    Points.Margin = new Thickness(defStart + 15, 47, 0, 0);
                    if (defenders[i][posIndex - 1] == "(captain)")
                    {
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    DEF.Margin = new Thickness(defStart + 15, -50, 0, 0);
                    DEF.Height = 65;
                    DEF.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(DEF);
                    defStart += 237;
                }
                else if (this.defenders.Count == 5)
                {
                    DEF = new Image();
                    Name = new TextBlock();
                    Points = new TextBlock();
                    Name.Background = Brushes.DarkGreen;
                    Points.Background = Brushes.Green;
                    Name.Foreground = Brushes.White;
                    Points.Foreground = Brushes.White;
                    Name.FontSize = 10;
                    Points.FontSize = 10;
                    Name.TextAlignment = TextAlignment.Center;
                    Points.TextAlignment = TextAlignment.Center;
                    Name.VerticalAlignment = VerticalAlignment.Center;
                    Points.VerticalAlignment = VerticalAlignment.Center;
                    Name.FontFamily = new FontFamily("Calibri");
                    Points.FontFamily = new FontFamily("Calibri");
                    Name.Text = playerName;
                    Points.Text = points.ToString();
                    Name.Width = 100;
                    Points.Width = 100;
                    Name.Height = 12;
                    Points.Height = 12;
                    Name.Margin = new Thickness(defStart + 15, 25, 0, 0);
                    Points.Margin = new Thickness(defStart + 15, 47, 0, 0);
                    if (defenders[i][posIndex - 1] == "(captain)")
                    {
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player-Captain.png", UriKind.Relative));
                        Points.Text = (points * 2).ToString();
                        total_points += points;
                    }
                    else
                        DEF.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                    DEF.Margin = new Thickness(defStart + 15, -50, 0, 0);
                    DEF.Height = 65;
                    DEF.Width = 65;
                    Sub1Grid.Children.Add(Name);
                    Sub1Grid.Children.Add(Points);
                    Sub1Grid.Children.Add(DEF);
                    defStart += 225;
                }
            }

            // Drawing Goalkeeper
            string GKplayerName = "";
            int GKposIndex = 0, GKpoints = 0;
            for (int j = 0; j < goalkeeperStart.Count; j++)
            {
                if (int.TryParse(goalkeeperStart[j], out _))
                {
                    GKposIndex = j;
                    GKpoints = int.Parse(goalkeeperStart[j + 1]);
                    total_points += GKpoints;
                    break;
                }
            }
            for (int j = 0; j < 2; j++)
                GKplayerName += goalkeeperStart[j] + ' ';
            GK = new Image();
            Name = new TextBlock();
            Points = new TextBlock();
            Name.Background = Brushes.DarkGreen;
            Points.Background = Brushes.Green;
            Name.Foreground = Brushes.White;
            Points.Foreground = Brushes.White;
            Name.FontSize = 10;
            Points.FontSize = 10;
            Name.TextAlignment = TextAlignment.Center;
            Points.TextAlignment = TextAlignment.Center;
            Name.VerticalAlignment = VerticalAlignment.Center;
            Points.VerticalAlignment = VerticalAlignment.Center;
            Name.FontFamily = new FontFamily("Calibri");
            Points.FontFamily = new FontFamily("Calibri");
            Name.Text = GKplayerName;
            Points.Text = GKpoints.ToString();
            Name.Width = 100;
            Points.Width = 100;
            Name.Height = 12;
            Points.Height = 12;
            Name.Margin = new Thickness(15, -150, 0, 0);
            Points.Margin = new Thickness(15, -128, 0, 0);
            if (goalkeeperStart[GKposIndex - 1] == "(captain)")
            {
                GK.Source = new BitmapImage(new Uri("/Background-Images/GK-Captain.png", UriKind.Relative));
                Points.Text = (GKpoints * 2).ToString();
                total_points += GKpoints;
            }
            else
                GK.Source = new BitmapImage(new Uri("/Background-Images/GK.png", UriKind.Relative));
            GK.Margin = new Thickness(15, -225, 0, 0);
            GK.Height = 80;
            GK.Width = 80;
            Sub1Grid.Children.Add(Name);
            Sub1Grid.Children.Add(Points);
            Sub1Grid.Children.Add(GK);
            #endregion

            #region Substitutions
            for (int i = 0; i < this.subs.Count; i++)
            {
                string playerName = "";
                int posIndex = 0, points = 0;
                for (int j = 0; j < subs[i].Count; j++)
                {
                    if (int.TryParse(subs[i][j], out _))
                    {
                        posIndex = j;
                        points = int.Parse(subs[i][j + 1]);
                        break;
                    }
                }
                for (int j = 0; j < 2; j++)
                    playerName += subs[i][j] + ' ';
                SUB = new Image();
                Name = new TextBlock();
                Points = new TextBlock();
                Name.Background = Brushes.Green;
                Points.Background = Brushes.LightGreen;
                Name.Foreground = Brushes.White;
                Points.Foreground = Brushes.Black;
                Name.FontSize = 10;
                Points.FontSize = 10;
                Name.TextAlignment = TextAlignment.Center;
                Points.TextAlignment = TextAlignment.Center;
                Name.VerticalAlignment = VerticalAlignment.Center;
                Points.VerticalAlignment = VerticalAlignment.Center;
                Name.FontFamily = new FontFamily("Calibri");
                Points.FontFamily = new FontFamily("Calibri");
                Name.Text = playerName;
                Points.Text = points.ToString();
                Name.Width = 100;
                Points.Width = 100;
                Name.Height = 12;
                Points.Height = 12;
                Name.Margin = new Thickness(subStart, 625, 0, 0);
                Points.Margin = new Thickness(subStart, 647, 0, 0);
                SUB.Source = new BitmapImage(new Uri("/Background-Images/Player.png", UriKind.Relative));
                SUB.Margin = new Thickness(subStart, 550, 0, 0);
                SUB.Height = 65;
                SUB.Width = 65;
                Sub1Grid.Children.Add(Name);
                Sub1Grid.Children.Add(Points);
                Sub1Grid.Children.Add(SUB);
                subStart += 350;
            }
            string GKSubplayerName = "";
            int GKSubposIndex = 0, GKSubpoints = 0;
            for (int j = 0; j < goalkeeperSub.Count; j++)
            {
                if (int.TryParse(goalkeeperSub[j], out _))
                {
                    GKSubposIndex = j;
                    GKSubpoints = int.Parse(goalkeeperSub[j + 1]);
                    break;
                }
            }
            for (int j = 0; j < 2; j++)
                GKSubplayerName += goalkeeperSub[j] + ' ';
            SUB = new Image();
            Name = new TextBlock();
            Points = new TextBlock();
            Name.Background = Brushes.Green;
            Points.Background = Brushes.LightGreen;
            Name.Foreground = Brushes.White;
            Points.Foreground = Brushes.Black;
            Name.FontSize = 10;
            Points.FontSize = 10;
            Name.TextAlignment = TextAlignment.Center;
            Points.TextAlignment = TextAlignment.Center;
            Name.VerticalAlignment = VerticalAlignment.Center;
            Points.VerticalAlignment = VerticalAlignment.Center;
            Name.FontFamily = new FontFamily("Calibri");
            Points.FontFamily = new FontFamily("Calibri");
            Name.Text = GKSubplayerName;
            Points.Text = GKSubpoints.ToString();
            Name.Width = 100;
            Points.Width = 100;
            Name.Height = 12;
            Points.Height = 12;
            Name.Margin = new Thickness(-500, 625, 0, 0);
            Points.Margin = new Thickness(-500, 647, 0, 0);
            SUB.Source = new BitmapImage(new Uri("/Background-Images/GK.png", UriKind.Relative));
            SUB.Margin = new Thickness(-500, 550, 0, 0);
            SUB.Height = 80;
            SUB.Width = 80;
            Sub1Grid.Children.Add(Name);
            Sub1Grid.Children.Add(Points);
            Sub1Grid.Children.Add(SUB);
            #endregion

            #region Extras
            TextBlock TotpointsBlock = new TextBlock();
            TotpointsBlock.Text = total_points.ToString();
            TotpointsBlock.FontFamily = new FontFamily("Calibri");
            TotpointsBlock.Margin = new Thickness(-950, 627, 0, 0);
            TotpointsBlock.TextAlignment = TextAlignment.Center;
            TotpointsBlock.VerticalAlignment = VerticalAlignment.Center;
            TotpointsBlock.Foreground = Brushes.White;
            TotpointsBlock.FontSize = 20;
            TotpointsBlock.FontWeight = FontWeights.Bold;
            Sub1Grid.Children.Add(TotpointsBlock);
            #endregion
        }

        // Protoype of a Button Click's Effect
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        // Constructing Team and Gathering Information On Players Positions and Counts in Each Position
        public void Construct_Team()
        {
            forwards = new List<List<string>>();
            midfielders = new List<List<string>>();
            defenders = new List<List<string>>();
            subs = new List<List<string>>();
            for (int i = 0;i < 11;i++)
            {
                var player = Team[i];
                List<string> playerinfo = player.ToString().Split().ToList();
                int posIndex = 0;
                for (int j = 0; j < playerinfo.Count; j++)
                {
                    if (int.TryParse(playerinfo[j], out _))
                    {
                        posIndex = j;
                        break;
                    }
                }
                if(playerinfo[posIndex] == "4")
                    forwards.Add(playerinfo);
                if (playerinfo[posIndex] == "3")
                    midfielders.Add(playerinfo);
                if (playerinfo[posIndex] == "2")
                    defenders.Add(playerinfo);
                if (playerinfo[posIndex] == "1")
                    goalkeeperStart = playerinfo;
            }
            for (int i = 0; i < 4; i++)
            {
                var player = Substitutions[i];
                List<string> playerinfo = player.ToString().Split().ToList();
                int posIndex = 0;
                for (int j = 0; j < playerinfo.Count; j++)
                {
                    if (int.TryParse(playerinfo[j], out _))
                    {
                        posIndex = j;
                        break;
                    }
                }
                if (playerinfo[posIndex] == "1")
                    goalkeeperSub = playerinfo;
                else
                    subs.Add(playerinfo);
            }
        }
        // Any Utility Functions
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
