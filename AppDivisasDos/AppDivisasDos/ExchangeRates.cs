using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDivisasDos
{
   public class ExchangeRates
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }

        [JsonProperty("license")]
        public string License { get; set; }

        [JsonProperty("timestamp")]
        public string TimeStamp { get; set; }
            
        [JsonProperty("base")]
        public string Base { get; set;}

        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }

    public class Rates
    {
        public double BRL { get; set; }//Real Brazilero
        public double CAD { get; set; }//Dollar Canadiense
        public double CHF { get; set; }//Franco Suizo
        public double CLP { get; set; }//Pesos Chilenos
        public double COP { get; set; }//Pesos Colombianos
        public double DKK { get; set; }//Corona Danesa
        public double EUR { get; set; }//Euro
        public double GBP { get; set; }//Libra Esterlina
        public double INR { get; set; }//Rupia India
        public double JPY { get; set; }//Yen japonés
        public double MXN { get; set; }//Pesos Mexicanos
        public double USD { get; set; }//Dóllar Usa

    }
}
