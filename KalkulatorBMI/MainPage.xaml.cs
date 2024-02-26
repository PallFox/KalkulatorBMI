using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KalkulatorBMI
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        private void SumaryButton_Clicked(object sender, EventArgs e)
        {
            Archives data = new Archives { Height = int.Parse(EntHeight.Text), Weight = int.Parse(EntWeight.Text) };
            double BMI = data.Weight / (data.Height / 100) * (data.Height / 100);
            LblResult.Text = BMI.ToString();
        }
    }
}
