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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FourTitleInputArrange
{
    /// <summary>
    /// Interaction logic for FourTitleInputArrangeWindow.xaml
    /// </summary>
    public partial class FourTitleInputArrangeWindow : Window
    {
        public FourTitleInputArrangeWindow()
        {
            InitializeComponent();

#if DEBUG
            T1.Text = "qw";
            T2.Text = "ert";
            T3.Text = "tyui";
            T4.Text = "asdfg";
#endif
        }

        private void Arrange_OnClick(object sender, RoutedEventArgs e)
        {

            int[] numList = new[]
            {
                T1.Text.Count(),
                T2.Text.Count(),
                T3.Text.Count(),
                T4.Text.Count(),
            };

            char[][] StrArr = new[]
            {
                T1.Text.ToArray(),
                T2.Text.ToArray(),
                T3.Text.ToArray(),
                T4.Text.ToArray(),
            };



            TO.Text =
                "Output:\n\n" +

                ArrangeArray(StrArr, numList);

        }

        public string ArrangeArray(char[][] InArr, int[] N)
        {
            string output = "";




            for (int i0=0,i1 = 0, i2 = 0, i3 = 0;  i0 < N[0]; )
            {
                output += InArr[0][i0].ToString() + 
                    InArr[1][i1].ToString() + 
                    InArr[2][i2].ToString() + 
                    InArr[3][i3].ToString() + "\n";


                if (i3 == N[3] - 1 && i2 == N[2] - 1 && i1 == N[1]-1)
                {
                    i0++;
                    i1 = 0;
                    i2 = 0;
                    i3 = 0;
                }
                else if (i3 == N[3] - 1 && i2 == N[2] - 1)
                {
                    i2 = 0;
                    i3 = 0;
                    i1++;
                }
                else if (i3 == N[3]-1)
                {
                    i3 = 0;
                    i2++;
                }
                else if (i3 < N[3])
                {
                    i3++;
                }
            }


            return output;


        }

    }
}
