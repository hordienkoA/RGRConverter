using RGRConverter.VIewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RGRConverter.VIewModels;

using Xamarin.Forms;
//using Xamarin.Essentials;
namespace RGRConverter
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        CurrencyViewModel bindingContext;
        public MainPage()
        {
            
            InitializeComponent();
            //var current = Connectivity.ConnectionProfiles;
            //if (current.Contains(ConnectionProfile.WiFi) || current.Contains(ConnectionProfile.Cellular))
            //{
                bindingContext=new CurrencyViewModel(); 
                BindingContext = bindingContext;  
            //}
            //else
            //{
                //DisplayAlert("Internet alert", "There is no internet coonection", "Ok");
                //if(Device.OS==TargetPlatform.Android)
                  //  DependencyService.Get<IMethods>().Close_App();
            //}

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            //var current = Connectivity.ConnectionProfiles;
            //if (//current.Contains(ConnectionProfile.WiFi) || current.Contains(ConnectionProfile.Cellular))
            //{
                ControlsHelper.InvalidateMeasure((Picker) sender);
                bindingContext.CalculateLeft();
            //}
            //else
            //{
                //DisplayAlert("Internet alert", "There is no internet coonection", "Ok");
            //}
        }

        private void LeftEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            bindingContext.CalculateLeft();
        }

        

        
    }
}