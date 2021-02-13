using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OppaTerminal.Models
{
    public class Payment
        {
            public int Id { get; set; }
            public Service Service { get; set; }
            public uint Number { get; set; }
            public string IDnumber { get; set; }
            public decimal Amount { get; set; }
            public decimal Commission { get; set; }
            public string AccountId { get; set; }//
        }

        public enum Service
        {
            Charity = 0,
            Taxes = 1,
            Finances = 2,//
            Mobile = 3//
        }

}
