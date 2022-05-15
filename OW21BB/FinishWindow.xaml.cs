using Newtonsoft.Json;
using OW21BB.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace OW21BB
{
    /// <summary>
    /// Interaction logic for FinishWindow.xaml
    /// </summary>
    public partial class FinishWindow : Window
    {
        
        public FinishWindow(List<Part> parts)
        {
            InitializeComponent();
            string jsonData = JsonConvert.SerializeObject(parts);
            File.AppendAllText("config.json", "\n"+jsonData);

            NameStack.Children.Add(new Label()
            {
                Content = $"NAME",
                FontWeight = FontWeights.Bold
            });
            TypeStack.Children.Add(new Label()
            {
                Content = $"TYPE",
                FontWeight = FontWeights.Bold
            });
            PriceStack.Children.Add(new Label()
            {
                Content = $"PRICE",
                FontWeight = FontWeights.Bold
            });

            foreach (var item in parts)
            {
                NameStack.Children.Add(new Label()
                {
                    Content = item.Name
                });
                TypeStack.Children.Add(new Label()
                {
                    Content = item.Type
                });
                PriceStack.Children.Add(new Label()
                {
                    Content = item.Price +"$"
                });

            }
        }
    }
}
