using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRRODriver.Entities
{
    public class TransactionsRegistrarItem
    {
        public long NumFiscal { get; set; } // Фіскальний номер ПРРО
        public long NumLocal { get; set; } // Локальний номер ПРРО
        public string Name { get; set; } // Найменування ПРРО
        public bool Closed { get; set; } // Ознака скасованої реєстрації ПРРО, на якому наразі не закрито зміну

        public TransactionsRegistrarItem()
        {
            NumFiscal = Constants.NumberNotSet;
            NumLocal = Constants.NumberNotSet;
            Name = string.Empty;
            Closed = false;
        }
    }
}
