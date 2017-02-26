using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfLogicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public int[] boardArray = new int[42];

        public int[] movesLeft = new int[4] { 0, 1, 7, 8 }; // 1 7 8
        public int[] movesRight = new int[4] { 0, -1, 7, 6 }; 
        public int[] movesMiddle = new int[4] { -8,-1, 7, 1 };

        public int setCounts;
        public int SetCounts
        {
            get
            {
                return setCounts;
            }
            set
            {
                setCounts = value;
            }
        }


        public string[] GetAllWinning
        {
            get
            {
                return getWinningCombinations();
            }
           
        }


        private string[] getWinningCombinations()
        {
            // Horizontal
            string[] HorisontalArr = GetWinningsHorizontal(41);
            // Vertical
            string[] VerticalArr = GetWinningsUp(41);

            // diagonal from up down rght
            string[] DiagonalArrDown = GetWinningsDigonalDown(41);
            // diagonal from down up rght
            string[] DiagonalArrUp = GetWinningsDigonalUp(6);

            // copy Horizontal and Vertical
            string[] bothHVArray = MyCopyArray(HorisontalArr, VerticalArr);
            // copy diagonal
            string[] bothDGUDArray = MyCopyArray(DiagonalArrUp, DiagonalArrDown);
            // final result
            string[] resultArray = MyCopyArray(bothHVArray, bothDGUDArray);

            return resultArray;
        }


        public MainWindow()
        {
            InitializeComponent();
          //  CreateGridDynamicly();

            // var rows = GetDataGridRows(nameofyordatagrid);
            //foreach (DataGridRowD r in mainGrid.RowDefinitions)
            //{

            //    DataRowView rv = (DataRowView)r.Item;
            //    foreach (DataGridColumn column in mainGrid.Columns)
            //    {
            //        if (column.GetCellContent(r) is TextBlock)
            //        {
            //            TextBlock cellContent = column.GetCellContent(r) as TextBlock;
            //            MessageBox.Show(cellContent.Text);
            //        }
            //    }
            //}

            int iRows = 0;
            int iColumns = 0;
            int iMatrix = 41;


            foreach (RowDefinition rows in secondGrid.RowDefinitions)
            {
                
                
                foreach (ColumnDefinition cells in secondGrid.ColumnDefinitions)
                {
                  
                    Button myButton = new Button();
                    myButton.Width = 103;
                    myButton.Height = 74;
                    myButton.VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    myButton.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    myButton.Content = iMatrix.ToString() + "," + iRows.ToString() + "," + iColumns.ToString();
                    myButton.Tag = iMatrix.ToString() + "," + iRows.ToString() + "," + iColumns.ToString(); 
                    myButton.Name = "H" + iMatrix.ToString();
                    myButton.Click += new RoutedEventHandler(this.myButton_Click);

                    //myButton.PreviewMouseMove += new MouseButtonEventHandler(this.myButton_PreviewMouseMove);

                    Grid.SetRow(myButton, iRows);
                    Grid.SetColumn(myButton, iColumns);
                    secondGrid.Children.Add(myButton);
                    // secondGrid.SetValu
                    // secondGrid.RowDefinitions.Count
                    
                    iColumns++;
                    iMatrix--;
                    //cells[0]
                }

                iRows++;

                iColumns = 0;
            }


           //  <Button Content="Button" Grid.Column="1" HorizontalAlignment="Left" Grid.Row="1" VerticalAlignment="Top" Width="103" Height="74"/>
        }

        private string[] GetWinningsHorizontal(int StartPosition)
        {
            string[] mianArr = new string[24];
            int count = 0;

            for (int p = 0; p <= 5; p++)
            {
                for (int m = 0; m <= 3; m++)
                {
                    mianArr[count] = string.Concat((StartPosition).ToString(), ",", 
                        (StartPosition - 1).ToString(), ",",
                        (StartPosition - 2).ToString(), ",",
                        (StartPosition - 3).ToString());

                    StartPosition = StartPosition - 1;
                    count++;
                }

                StartPosition = StartPosition - 3;
            }

            return mianArr;
        }


        private string[] GetWinningsUp(int StartPosition)
        {
            string[] mianArr = new string[21];
            int count = 0;

            for (int p = 0; p <= 6; p++)
            {
                for (int m = 0; m <= 2; m++)
                {
                    mianArr[count] = string.Concat((StartPosition).ToString(), ",",
                       (StartPosition - 7).ToString(), ",",
                        (StartPosition - 14).ToString(), ",",
                       (StartPosition - 21).ToString());

                    StartPosition = StartPosition - 1;
                    count++;
                }

               // StartPosition = StartPosition - 2;
            }

            return mianArr;
        }


        private string[] GetWinningsDigonalDown(int StartPosition)
        {
            string[] mianArr = new string[12];
            int count = 0;

            for (int p = 0; p <= 2; p++)
            {
                for (int m = 0; m <= 3; m++)
                {
                    mianArr[count] = string.Concat((StartPosition).ToString(), ",",
                      (StartPosition - 8).ToString(), ",",
                      (StartPosition - 16).ToString(), ",",
                      (StartPosition - 24).ToString());

                    StartPosition = StartPosition - 1;
                    count++;
                }

                StartPosition = StartPosition - 3;
            }

            return mianArr;
        }

        private string[] GetWinningsDigonalUp(int StartPosition)
        {
            string[] mianArr = new string[12];
            int count = 0;

            for (int p = 0; p <= 2; p++)
            {
                for (int m = 0; m <= 3; m++)
                {
                    mianArr[count] = string.Concat((StartPosition).ToString(), ",",
                     (StartPosition + 6).ToString(), ",",
                     (StartPosition + 12).ToString(), ",",
                     (StartPosition + 18).ToString());

                    StartPosition = StartPosition - 1;
                    count++;
                }

                StartPosition = StartPosition + 11;
            }

            return mianArr;
        }




        private void myButton_PreviewMouseMove(object sender, MouseButtonEventArgs e)
        {

           // throw new NotImplementedException();
        } // end main function

        public void CreateGridDynamicly()
        {
            Grid DynamicGrid = new Grid();
            DynamicGrid.Width = 400;
            DynamicGrid.HorizontalAlignment = HorizontalAlignment.Left;
            DynamicGrid.VerticalAlignment = VerticalAlignment.Top;
            DynamicGrid.ShowGridLines = true;
            DynamicGrid.Background = new SolidColorBrush(Colors.LightSteelBlue);

            // Create Columns
            ColumnDefinition gridCol1 = new ColumnDefinition();
            ColumnDefinition gridCol2 = new ColumnDefinition();
            ColumnDefinition gridCol3 = new ColumnDefinition();
            DynamicGrid.ColumnDefinitions.Add(gridCol1);
            DynamicGrid.ColumnDefinitions.Add(gridCol2);
            DynamicGrid.ColumnDefinitions.Add(gridCol3);

            // Create Rows
            RowDefinition gridRow1 = new RowDefinition();
            gridRow1.Height = new GridLength(45);
            RowDefinition gridRow2 = new RowDefinition();
            gridRow2.Height = new GridLength(45);
            RowDefinition gridRow3 = new RowDefinition();
            gridRow3.Height = new GridLength(45);
            DynamicGrid.RowDefinitions.Add(gridRow1);
            DynamicGrid.RowDefinitions.Add(gridRow2);
            DynamicGrid.RowDefinitions.Add(gridRow3);

            //RootWind
        }

        private bool GetInvalidMove(int locBoard)
        {
            int selectedRow = 0;
            int previousRow = locBoard - 7;

            // check for invalid move
            if (boardArray[locBoard] == 0)
            {
                // devided by seven
                selectedRow = locBoard / 7;
                if (selectedRow > 0)
                {
                    // find the previous row
                    if (boardArray[previousRow] == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }


        // combinig two arrays
        private string[] MyCopyArray(string[] source, string[] target)
        {
            
            string[] resultArray = new string[source.Length + target.Length];
            
            Array.Copy(source, resultArray, source.Length);
            Array.Copy(target, 0, resultArray, source.Length, target.Length);

            return resultArray;
        }

        private void ManipulatingWinnings(string[] resultArray, int[] selectedValues)
        {
            string[] fourArray = null;
            int combinationValue = 0;
            int sumValues = 0;

            int countCombination = 0;

            foreach (string resultValue in resultArray)
            {
                fourArray = resultValue.Split(Char.Parse(","));
                for (int i = 0; i <= 3; i++)
                {
                    combinationValue = int.Parse(fourArray[i]);

                    sumValues = selectedValues[combinationValue] + sumValues;
                    // sumValues++;

                    if (sumValues == 4)
                    {
                        MessageBox.Show(countCombination.ToString() + " - " + resultValue, "Player Winner is here");
                        break;
                    }

                    if (sumValues == -4) // 12
                    {
                        MessageBox.Show(countCombination.ToString() + " - " + resultValue, "Computer Winner is here");
                        break;
                    }
                }

                sumValues = 0;
                countCombination++;
            }
            

        }

        private void myButton_Click(object sender, RoutedEventArgs e)
        {
            // testing

            // building combinations 
            // Horizontal
            string[] HorisontalArr = GetWinningsHorizontal(41);
            // Vertical
            string[] VerticalArr = GetWinningsUp(41);

            // diagonal from up down rght
            string[] DiagonalArrDown = GetWinningsDigonalDown(41);
            // diagonal from down up rght
            string[] DiagonalArrUp = GetWinningsDigonalUp(6);

            // copy arrays
            string[] bothHVArray = MyCopyArray(HorisontalArr, VerticalArr);
            string[] bothDGUDArray = MyCopyArray(DiagonalArrUp, DiagonalArrDown);
            string[] resultArray = MyCopyArray(bothHVArray, bothDGUDArray);
            // -----------------

            // loop through winnings

            int[] testArray = new int[42];

            testArray[32] = -1;
            testArray[24] = -1;
            testArray[16] = -1;
            testArray[8] = -1;

            testArray[40] = 1;
            testArray[39] = 1;
            testArray[38] = 1;
            testArray[37] = 1;

            ManipulatingWinnings(resultArray, testArray);

            //string[] ArrNew = new string[HorisontalArr.Length + VerticalArr.Length];

           // Array.Copy(HorisontalArr, ArrNew, HorisontalArr.Length);
           // Array.Copy(VerticalArr, 0, ArrNew, HorisontalArr.Length, VerticalArr.Length);


            Button selectedButton = (Button)sender;
            
            string arrResult = selectedButton.Tag.ToString();
            string[] myArray = arrResult.Split(Char.Parse(","));

            int iPlayerSelect = int.Parse(myArray[0]);
            int iRow = int.Parse(myArray[1]);
            int iColumn = int.Parse(myArray[2]);

            // checked for InvalidateArrange move
            if (GetInvalidMove(iPlayerSelect) == true)
            {
                MessageBox.Show(iPlayerSelect.ToString(), "Invalid Move");
                return;
            }

            if (boardArray[iPlayerSelect] == 0)
            {
                boardArray[iPlayerSelect] = 1;
                selectedButton.IsEnabled = false;
                selectedButton.Background = Brushes.Blue;
                selectedButton.Foreground = Brushes.White;
                selectedButton.Content = "Clicked";
            }

            int iComputerMove = 0;
     
            for (int p = 0; p <= 3; p++)
            {

                if (iColumn == 6)
                {
                    iComputerMove = iPlayerSelect + movesLeft[p];

                    // check value
                    if (boardArray[iComputerMove] > 0)
                        continue;
                    else
                    {
                        boardArray[iComputerMove] = 2;
                        break;
                    }
                 }
                else if (iColumn == 0)
                {
                    iComputerMove = iPlayerSelect + movesRight[p];

                    // check value
                    if (boardArray[iComputerMove] > 0)
                        continue;
                    else
                    {
                        boardArray[iComputerMove] = 2;
                        break;
                    }
                }
                else
                {
                    iComputerMove = iPlayerSelect + movesMiddle[p];

                    if (iComputerMove < 0 || iComputerMove > 41)
                        continue;

                    // check value
                    if (boardArray[iComputerMove] > 0)
                        continue;
                    else
                    {
                        boardArray[iComputerMove] = 2;
                        break;
                    }
                }

            }

            // computer button
            foreach (var child in secondGrid.Children)
            {
                Button btn = (Button)child;
                if (btn.Name.CompareTo("H" + iComputerMove) == 0)
                {
                    btn.IsEnabled = false;
                    btn.Background = Brushes.Red;
                    btn.Foreground = Brushes.White;
                    btn.Content = "Computer";
                    break;
                }
            }

            /*
            // checked for InvalidateArrange move
            if (GetInvalidMove(iResult) == true)
            {
                MessageBox.Show(iResult.ToString(), "Invalid Move");
                return;
            }
            // check for invalid move
            // ===========================================================
           // int selectedRow = 0;
          //  int previousRow = iResult - 7;
            
            // check for invalid move
         //   if (boardArray[iResult] == 0)
         //   {
                // devided by seven
           //     selectedRow = iResult / 7;
          //      if (selectedRow > 0)
            //    {
             //       // find the previous row
               //     if (boardArray[previousRow] == 0)
               //     {
              //          MessageBox.Show(iResult.ToString(), "invalid Move");
                //        //SetCounts++;
                 //       return;
                //    }
              //  }
           // }
            // ==========================================================

            // assign value 
            boardArray[iResult] = 1;

            // increase the counts
            SetCounts++;

           // string selected = selectedButton.Name;

           // Button myButtonNext = new Button();
         //   myButtonNext.Name = 
            int ComputerMove = 0;
            // computer move
            foreach (var child in secondGrid.Children)
            {
                Button btn = (Button)child;

                ComputerMove = iResult + 1;

                if (btn.Name.CompareTo("H" + ComputerMove) == 0)
                {
                    if (boardArray[ComputerMove] > 0)
                    {
                        if (GetInvalidMove(ComputerMove) == true)
                        {
                            MessageBox.Show(ComputerMove.ToString(), "Invalid Move Comuter");
                            return;
                        }
                    }

                    boardArray[ComputerMove] = 2;

                    btn.IsEnabled = false;
                    btn.Background = Brushes.Red;
                    btn.Foreground = Brushes.White;
                    btn.Content = "Computer";
                    break;
                }
                
                
           /*     
                
                if (boardArray[ComputerMove] > 0)
                {
                    if (GetInvalidMove(ComputerMove) == true)
                    {
                        MessageBox.Show(ComputerMove.ToString(), "Invalid Move Comuter");
                        return;
                    }
                }
                else
                {
                    //if (GetInvalidMove(iResult) == true)
                    //{
                    //    MessageBox.Show(iResult.ToString(), "Invalid Move");
                    //    return;
                    //}
                    boardArray[ComputerMove] = 2;

                    if (btn.Name.CompareTo("H" + ComputerMove) == 0)
                    {
                        btn.IsEnabled = false;
                        btn.Background = Brushes.Red;
                        btn.Foreground = Brushes.White;
                        btn.Content = "Computer";
                        break;
                    }
                }
            * */

          //  }

           // object itemFind = secondGrid.FindName("H40");
          //  if (itemFind is Button)
          //  {
           //     Button newButton = (Button)itemFind;
          //      newButton.IsEnabled = false;
          //  }
        /*
            selectedButton.IsEnabled = false;
            selectedButton.Background = Brushes.Blue;
            selectedButton.Foreground = Brushes.White;
            selectedButton.Content = "Clicked";


           // ButtonAutomationPeer peer = new ButtonAutomationPeer();
            
            
              
            if (SetCounts == 42)
            {
                MessageBox.Show("All Moves are executed", "Game Over");
            }
            //MessageBox.Show(SetCounts.ToString(), "Total");

            */
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        

        public class Node
        {
            private bool terminalNode;

            public Node()
            {
            }
 
            public List<Node> Children(bool Player)
            {
                // Create your subtree here and return the results
                List<Node> children = new List<Node>();
                return children;
            }
 
            public bool IsTerminal(bool Player)
            {
                // Game over?
                terminalNode = false;
                return terminalNode;
            }
 
            public int GetTotalScore(bool Player)
            {
                int totalScore = 0;
 
                // This method is a heuristic evaluation function to evaluate
                // the current situation of the player
                // It depends on the game. For example chess, tic-tac-to or other games suitable
                // for the minimax algorithm can have different evaluation functions.
 
                return totalScore;
            }
 
        } // node

        public class AlphaBeta
        {
            bool MaxPlayer = true;

            public AlphaBeta()
            {
            }

            public int Iterate(Node node, int depth, int alpha, int beta, bool Player)
            {
                if (depth == 0 || node.IsTerminal(Player))
                {
                    return node.GetTotalScore(Player);
                }

                if (Player == MaxPlayer)
                {
                    foreach (Node child in node.Children(Player))
                    {
                        alpha = Math.Max(alpha, Iterate(child, depth - 1, alpha, beta, !Player));
                        if (beta < alpha)
                        {
                            break;
                        }

                    }

                    return alpha;
                }
                else
                {
                    foreach (Node child in node.Children(Player))
                    {
                        beta = Math.Min(beta, Iterate(child, depth - 1, alpha, beta, !Player));

                        if (beta < alpha)
                        {
                            break;
                        }
                    }

                    return beta;
                }
            }
        }
    }
}
