using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;
using System.Reflection;
using Xamarin.Essentials;

namespace KalkulatorBMI
{
    public partial class MainPage : ContentPage
    {
        Archives data;
        public MainPage()
        {
            InitializeComponent();
            if (!File.Exists(App.DbPath))
            {
                string serializedResultList = JsonConvert.SerializeObject(new List<Archives>());
                File.WriteAllText(App.DbPath, serializedResultList);
            }
        }
        private void SumaryButton_Clicked(object sender, EventArgs e)
        {
            float height = float.Parse(EntHeight.Text);
            if (height < 3)
            {
                height *= 100;
            }

            float score = float.Parse(EntWeight.Text) / (height / 100 * height / 100);

            data = new Archives { Height = int.Parse(height.ToString()), Weight = int.Parse(EntWeight.Text), BMI = score };

            if (RadioButton_F.IsChecked)
            {
                data.Gender = false;
            }
            else
            {
                data.Gender = true;
            }

            label_result.Text = score.ToString();

            string result = "";
            if (score < 16)
            {
                result = "wygłodzenie";
            }
            if (score >= 16 && score < 19)
            {
                result = "niedowaga";
            }
            else if (score >= 19 && score < 24)
            {
                result = "waga prawidłowa";
            }
            else if (score >= 24 && score <= 30)
            {
                result = "nadwaga";
            }
            else if (score >= 30 && score <= 40)
            {
                result = "otyłość";
            }
            else if (score >= 40)
            {
                result = "skrajna otyłość";
            }
            label_score.Text = score.ToString("0.00") + " BMI";
            label_result.Text = "Wynik: " + result + ".";
            btn_saveResult.IsVisible = true;
            btn_saveResult.IsEnabled = true;

            label_score_invisible.Text = score.ToString("0.00");
            label_result_invisible.Text = result;
            label_gender_invisible.Text = data.Gender == false ? "Kobieta" : "Mężczyzna";
        }

        private async void SaveResult(object sender, EventArgs e)
        {
            int lastId = 0;
            string title = await DisplayPromptAsync("Tytuł", "Nadaj tytuł", "OK", "ANULUJ", "tytuł");
            if (string.IsNullOrWhiteSpace(title))
            {
                await DisplayAlert("Błąd", "Podaj tytuł zapisu.", "OK");
                return;
            }
            string path = App.DbPath;
            string file = File.ReadAllText(path);
            List<Archives> resultList = JsonConvert.DeserializeObject<List<Archives>>(file);

            if (resultList.Count > 0)
            {
                lastId = resultList[resultList.Count - 1].Id;
            }

            data.Id = lastId;
            data.Title = title;
            resultList.Add(data);

            string serializedResultList = JsonConvert.SerializeObject(resultList);
            File.WriteAllText(path, serializedResultList);

            btn_saveResult.IsVisible = false;
            btn_saveResult.IsEnabled = false;

            await DisplayAlert("Informacja", "Pomyślnie dodano nowy zapis.", "OK");
        }
        private void GoToList(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListViewPage());
        }
    }
}
