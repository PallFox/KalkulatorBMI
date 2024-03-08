using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace KalkulatorBMI
{
    public partial class ListViewPage : ContentPage
    {
        public ListViewPage()
        {
            InitializeComponent();

            Load();
        }


        public void Load()
        {
            string path = App.DbPath;

            if(File.Exists(path))
            {
                string text = File.ReadAllText(path);


                List<Archives> results = JsonConvert.DeserializeObject<List<Archives>>(text);

                listViewBMI.ItemsSource = results;
            }
        }
    }
}