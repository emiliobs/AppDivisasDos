using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace AppDivisasDos
{
    public partial class DivisasPage : ContentPage
    {
        private ExchangeRates exchangeRates;

        public DivisasPage()
        {
            InitializeComponent();

            sourcePicker.Items.Add("Coronas Danesas");
            sourcePicker.Items.Add("Dólares Canadienses");
            sourcePicker.Items.Add("Dólares Estadounidenses");
            sourcePicker.Items.Add("Euros");
            sourcePicker.Items.Add("Francos Suizos");
            sourcePicker.Items.Add("Libras Esterlinas");
            sourcePicker.Items.Add("Pesos Chilenos");
            sourcePicker.Items.Add("Pesos Colombianos");
            sourcePicker.Items.Add("Pesos Mexicanos");
            sourcePicker.Items.Add("Reales Brasileros");
            sourcePicker.Items.Add("Rupias Indias");
            sourcePicker.Items.Add("Yenes Japoneses");

            targetPicker.Items.Add("Coronas Danesas");
            targetPicker.Items.Add("Dólares Canadienses");
            targetPicker.Items.Add("Dólares Estadounidenses");
            targetPicker.Items.Add("Euros");
            targetPicker.Items.Add("Francos Suizos");
            targetPicker.Items.Add("Libras Esterlinas");
            targetPicker.Items.Add("Pesos Chilenos");
            targetPicker.Items.Add("Pesos Colombianos");
            targetPicker.Items.Add("Pesos Mexicanos");
            targetPicker.Items.Add("Reales Brasileros");
            targetPicker.Items.Add("Rupias Indias");
            targetPicker.Items.Add("Yenes Japoneses");

            this.LoadRates();

            convertButton.Clicked += ConvertButton_Clicked;
        }

        private async void ConvertButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(amountEntry.Text))
            {
                await DisplayAlert("ERROR:", "Debe ingresar un valor a convertir", "Aceptar");
                amountEntry.Focus();
                return;
            }

            if (sourcePicker.SelectedIndex == -1)
            {
                await DisplayAlert("ERROR:", "Debe seleccionar una moneda origen","Aceptar");
                sourcePicker.Focus();
                return;
            }

            if (targetPicker.SelectedIndex == -1)
            {
                await DisplayAlert("ERROR:", "Debe seleccionar una moneda destino", "Aceptar");
                targetPicker.Focus();
                return;
            }

            var amount = decimal.Parse(amountEntry.Text);
            var amountConverted = this.Convert(amount, sourcePicker.SelectedIndex, targetPicker.SelectedIndex);
            convertedLaber.Text = string.Format("{0:C2} {1} = {2:C2} {3} ", 
                amount, 
                sourcePicker.Items[sourcePicker.SelectedIndex],
                amountConverted,
                targetPicker.Items[targetPicker.SelectedIndex]);
        }

        private decimal Convert(decimal amount, int source, int target)
        {
            var sourceRate = this.GetRate(source);
            var targetRate = this.GetRate(target);

            return amount / (decimal)sourceRate * (decimal)targetRate;
        }

        private double GetRate(int index)
        {
            switch (index)
            {
                case 0: return exchangeRates.Rates.DKK;
                case 1: return exchangeRates.Rates.CAD;
                case 2: return exchangeRates.Rates.USD;
                case 3: return exchangeRates.Rates.EUR;
                case 4: return exchangeRates.Rates.CHF;
                case 5: return exchangeRates.Rates.GBP;
                case 6: return exchangeRates.Rates.CLP;
                case 7: return exchangeRates.Rates.COP;
                case 8: return exchangeRates.Rates.MXN;
                case 9: return exchangeRates.Rates.BRL;
                case 10: return exchangeRates.Rates.INR;
                case 11: return exchangeRates.Rates.JPY;

                default: return -1;
            }

        }

        private async void LoadRates()
        {
            try
            {
                waitActivityIndicator.IsRunning = true;

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://openexchangerates.org");
                var url = "/api/latest.json?app_id=18f5e77fadd348949e8adb5c0351a53a";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                this.exchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(result);
                waitActivityIndicator.IsRunning = false;
            }
            catch (Exception ex)
            {

                await DisplayAlert("ERROR: " , ex.Message ,"Aceptar");
                waitActivityIndicator.IsRunning = false;
            }
        }
    }
}
