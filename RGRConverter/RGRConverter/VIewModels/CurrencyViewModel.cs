﻿﻿using RGRConverter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
  using RGRConverter.Models;
  using Xamarin.Forms;

namespace RGRConverter.VIewModels
{
    class CurrencyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string leftCurrency;
        private string rightCurrency;
        private double leftNumber;
        private double rightNumber;
        private DateTime selectedDate;
        
        public  ObservableCollection<string> Currencies{get;set; }
        
        public CurrencyViewModel()
        {
            
            Currencies=new ObservableCollection<string>();
            
            
            Initialize();
            var test = 0;
        }


        public DateTime SelectedDate
        {
            
            get { return selectedDate;}
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    OnPropertyChanged("SelectedDate");
                }
            }
        }

        public double LeftNumber
        {
            get { return leftNumber;}
            set
            {
                if (leftNumber != value)
                {
                    leftNumber = value;
                    OnPropertyChanged("LeftNumber");
                }
            }
        }
        
        public double RightNumber
        {
            get { return rightNumber;}
            set
            {
                if (rightNumber != value)
                {
                    rightNumber = value;
                    OnPropertyChanged("RightNumber");
                }
            }
        }

        public string LeftCurrency
        {
            get { return leftCurrency; }
            set
            {
                if (leftCurrency != value)
                {
                    leftCurrency = value;
                    OnPropertyChanged("LeftCurrency");
                }
            }
        }

        public string RightCurrency
        {
            get { return rightCurrency; }
            set
            {
                if (rightCurrency != value)
                {
                    rightCurrency = value;
                    OnPropertyChanged("RightCurrency");
                }
            }
        }

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        private async void Initialize()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri("https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var deserializedJson = JsonConvert.DeserializeObject<List<Currency>>(responseContent.ReadAsStringAsync().Result);
                new List<string>(deserializedJson.Select(i => i.cc)).ForEach(Currencies.Add);
                Currencies.Add("UAH");
                LeftCurrency = Currencies.FirstOrDefault(i=>i=="USD");
                RightCurrency = Currencies.FirstOrDefault(i=>i=="UAH");
            }

            SelectedDate = DateTime.Now;
        }

        async Task<double> MakeCurrencyApiRequest(string cc)
        {
            HttpClient client=new HttpClient();
            HttpRequestMessage request=new HttpRequestMessage();
            request.RequestUri=new Uri($"https://bank.gov.ua/NBUStatService/v1/statdirectory/exchange?json&valcode={cc}&date={SelectedDate.ToString("yyyyMMdd")}");
            request.Method = HttpMethod.Get;
            request.Headers.Add("Accept", "application/json");
            HttpResponseMessage response = await client.SendAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                HttpContent responseContent = response.Content;
                var test = responseContent.ReadAsStringAsync().Result;
                
                var deserializedJson = JsonConvert.DeserializeObject<List<Currency>>(responseContent.ReadAsStringAsync().Result).FirstOrDefault();
                if (deserializedJson != null) return deserializedJson.rate;
                return -1;
            }

            return 0;
        }
        
        public async void CalculateLeft()
        {
            if (LeftCurrency != null)
            {
                double leftrate = await MakeCurrencyApiRequest(LeftCurrency);
                double rightrate = await MakeCurrencyApiRequest(RightCurrency);
                double test;
                if (leftrate == 0)
                {
                    RightNumber = LeftNumber=0;
                }
                
                
                else
                {
                    RightNumber = (LeftNumber * (leftrate<0?1:leftrate)) / (rightrate<0?1:rightrate);
                }
            }
        }
    }
}

