using ParkIT.Models;
using ParkIT.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParkIT
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SlotsPage : ContentPage
    {
        private ParkingSlotViewModel _parkingViewmodel;
        ObservableCollection<ParkingSlot> parkingslots;
        public SlotsPage()
        {
            InitializeComponent();
            parkingslots = new ObservableCollection<ParkingSlot>();
            _parkingViewmodel = new ParkingSlotViewModel();
            slotsview();
        }

        public void slotsview()
        {
            this.Title = "Book A Slot";
            parkingslots = _parkingViewmodel.LoadData();
            StackLayout stackHori1 = null;
            StackLayout stackHori2 = null;
            int count = 0;
            if (parkingslots != null)
            {
                foreach (var item in parkingslots)
                {
                    if (count == 0)
                    {
                        stackHori1 = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Fill };
                    }
                    if (count == 3)
                    {
                        stackHori2 = new StackLayout { Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Fill };
                    }

                    var frame = new Frame { BackgroundColor = Color.White, HeightRequest = 100, WidthRequest = 10, HasShadow = true, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Fill };
                    Color backgroundcolor;
                    if (item.Status == "Booked")
                    {
                        backgroundcolor = Color.Orange;
                    }
                    else if (item.Status == "Alloted")
                    {
                        backgroundcolor = Color.Red;
                    }
                    else
                    {
                        backgroundcolor = Color.White;
                    }
                    var button = new Button { Text = item.ID.ToString(), BackgroundColor = backgroundcolor, HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.Fill };
                    button.Clicked += Button_Clicked;
                    frame.Content = button;

                    if (count < 3)
                    {
                        stackHori1.Children.Add(frame);
                    }
                    else
                    {
                        stackHori2.Children.Add(frame);
                    }

                    count++;
                }
                if (stackHori1 != null)
                {
                    stackLayoutOuter.Children.Add(stackHori1);
                }
                if (stackHori2 != null)
                {
                    stackLayoutOuter.Children.Add(stackHori2);
                }
            }

        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            var view = (Frame)button.Parent;

            Color c = button.BackgroundColor;
            if (c == Color.White)//Check with green
            {
                bool response = await DisplayAlert("Title", "Are you sure you want to book a slot", "ok", "Cancel");

                if (response)
                {
                    button.BackgroundColor = Color.Orange;
                }
                else
                {
                    button.BackgroundColor = Color.White;
                }
            }
        }
        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}