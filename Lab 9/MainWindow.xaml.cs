using DM;
using System.DirectoryServices;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Time time1;
        Time time2;

        public MainWindow()
        {
            InitializeComponent();

            FillInput();
        }

        /// <summary>
        ///  Метод для заполнения полей ввода, когда объекты не пусты
        /// </summary>
        private void FillInput()
        {
            if (time1 != null)
            {
                h1.Text = time1.ToString().Substring(0, 2);
                m1.Text = time1.ToString().Substring(3, 2);
            }

            if (time2 != null)
            {
                h2.Text = time2.ToString().Substring(0, 2);
                m2.Text = time2.ToString().Substring(3, 2);
            }
        }

        /// <summary>
        /// Метод для проверки корректности ввода
        /// </summary>
        /// <returns></returns>
        private short CheckInput()
        {
            short code;

            int hrs, mns;
            
            hrs = InputDataWithCheck.InputIntegerWithValidation(h1.Text, 0, 23);
            mns = InputDataWithCheck.InputIntegerWithValidation(m1.Text, 0, 59);

            if (hrs != -1 && mns != -1)
            {
                time1 = new Time((byte)hrs, (byte)mns);
                code = 0;
            }
            else
            {
                code = 1;
            }

            hrs = InputDataWithCheck.InputIntegerWithValidation(h2.Text, 0, 23);
            mns = InputDataWithCheck.InputIntegerWithValidation(m2.Text, 0, 59);

            if (hrs != -1 && mns != -1)
            {
                time2 = new Time((byte)hrs, (byte)mns);

                if (code == 0)
                {
                    code = 0;
                }
                else
                {
                    code = 1;
                }
            }
            else
            {
                if (code == 1)
                {
                    code = 3;
                }
                else
                { 
                    code = 2;
                }
            }

            return code;
        }

        /// <summary>
        /// При нажатии кнопки вычисляет разность между первым и вторым временем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneDif_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0)
                MessageBox.Show($"Результат операции: {time1.Difference(time2)}", "Sucсsess", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (CheckInput() == 1)
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (CheckInput() == 2)
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Недопустимое значение в поле для ввода обоих времён", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки вычисляет разность между вторым и первым временем
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoDif_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0)
                MessageBox.Show($"Результат операции: {time2.Difference(time1)}", "Sucсsess", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (CheckInput() == 1)
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (CheckInput() == 2)
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Недопустимое значение в поле для ввода обоих времён", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки выводит результат сравнения двух времен
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comp_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0)
                MessageBox.Show($"time1 > time2: {time1 > time2}\n" + $"time1 < time2: {time1 < time2}", "Sucsess", MessageBoxButton.OK, MessageBoxImage.Information);
            else if (CheckInput() == 1)
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (CheckInput() == 2)
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else
                MessageBox.Show("Недопустимое значение в поле для ввода обоих времён", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки увеличивает первое время на 1 минуту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onePlus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 2)
            {
                MessageBox.Show($"Увеличили первое время на минуту: {time1++}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки увеличивает второе время на 1 минуту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoPlus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 1)
            {
                MessageBox.Show($"Увеличили второе время на минуту: {time2++}\n", "Sucсsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки уменьшает первое время на 1 минуту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneMinus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 2)
            {
                MessageBox.Show($"Уменьшили первое время на минуту: {time1--}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки уменьшает второе время на 1 минуту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoMinus_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 1)
            {
                MessageBox.Show($"Уменьшили второе время на минуту: {time2--}\n", "Sucсsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки сравнивает первое время с 00:00
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneNull_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 2)
            {
                MessageBox.Show($"Первое время отлично от 00:00: {(bool)time1}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии копки сравнивает второе время с 00:00
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoNull_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 1)
            {
                MessageBox.Show($"Второе время отлично от 00:00: {(bool)time2}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        /// При нажатии кнопки переводит первое время в минуты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oneMin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 2)
            {
                MessageBox.Show($"Первое время в миунтах: {(int)time1}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода первого времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

        /// <summary>
        ///  При нажатии кнопки переводит второе время в минуты
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void twoMin_Click(object sender, RoutedEventArgs e)
        {
            if (CheckInput() == 0 || CheckInput() == 1)
            {
                MessageBox.Show($"Второе время в миунтах: {(int)time2}\n", "Suсcsess", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Недопустимое значение в поле для ввода второго времени", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            FillInput();
        }

    }
}