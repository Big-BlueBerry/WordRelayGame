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

namespace WordRelayGame
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            wordLabel.Content = "개량";
        }

        private void Input_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = sender as TextBox;
            
            if (e.Key == Key.Enter)
            {
                var input = tb.Text;
                if (CheckWordLabel(input[0]))
                {
                    wordLabel.Content = input;
                }

                tb.Clear();
            }
        }

        private Boolean CheckWordLabel(char ch)
        {
            String label = wordLabel.Content.ToString().Trim();

            return (string.IsNullOrWhiteSpace(label)) || CompareChar(ch, label.Last());
        }

        private Boolean CompareChar(char userFirst, char labelLast)
        {
            Letter letter = Korean.ParseChar(labelLast);
            letter = Korean.TwoVoice(letter);
            labelLast = Korean.MergeLetter(letter);

            return userFirst == labelLast;
        }
    }
}
