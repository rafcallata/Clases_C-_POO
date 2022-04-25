using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConceptsPOO
{
    public class CommissionEmploye : Employee
    {
        public float CommisionPercentaje { get; set; }
        public decimal Sales { get; set; }
        public override decimal GetValueToPay()
        {
            return Sales * (decimal)CommisionPercentaje;
        }
        public override string ToString()
        {
            return $"{base.ToString()}" +
                $"\n\tCommission percentaje: {$"{CommisionPercentaje:P2}",18}" +
                $"\n\tSales................: {$"{Sales:C2}" ,18}" +
                $"\n\tValue to pay:........: {$"{GetValueToPay():C2}",18}";
        }
    }
}