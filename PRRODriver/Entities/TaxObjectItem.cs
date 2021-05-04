using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRRODriver.Entities
{
    public class TaxObjectItem
    {

        //Try to make comments at the english language. You'll become more familare with english and any other programer will undestand commetns easily
        public long Entity { get; set; } // Ідентифікатор запису ГО
        public bool SingleTax { get; set; } // Ознака ФОП – платника єдиного податку
        public string Name { get; set; } // Найменування ГО
        public string Address { get; set; } // Адреса ГО
        public string Tin { get; set; } // Код ЄДРПОУ/ДРФО платника податків
        public string Ipn { get; set; } // Податковий номер платника ПДВ
        public string OrgName { get; set; } // Найменування суб’єкта господарювання

        public TransactionsRegistrarItem[] TransactionsRegistrars { get; set; } // Перелік ПРРО, зареєстрованих для ГО

        //Better to make parameterless constructor with basic initializations
        public TaxObjectItem()
        {
            Entity = Constants.NumberNotSet;
            SingleTax = false;
            Name = string.Empty;
            Address = string.Empty;
            Tin = string.Empty;
            Ipn = string.Empty;
            OrgName = string.Empty;
        }
    }
}
