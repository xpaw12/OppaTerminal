using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OppaTerminal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OppaTerminal.Controllers
{
    public class HomeController : Controller
    {
        private readonly OppaTerminalDbContext _db;

        public HomeController(OppaTerminalDbContext db)
        {
            this._db = db;
        }
        public IActionResult Credits()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Mobile()
        {
            Payment model = new Payment();
            return View(model);
        }

        [HttpPost]
        public IActionResult Mobile(Payment incomingData)
        {
            if (incomingData.Number < 500000000 || incomingData.Number > 599999999)
                return View("Error");
            if (incomingData.Amount < 1 || incomingData.Amount > 100)
                return View("Error");
            if (incomingData.Amount < 50)
                incomingData.Commission = 0.5m;
            else
                incomingData.Commission = incomingData.Amount * 0.01m;
            
            incomingData.Amount -= incomingData.Commission;
            incomingData.Service = (Service)3;

            SavePaymentsToDb(incomingData);
            return View("Success");
        }

        public IActionResult CharityOrTaxes()
        {
            Payment model = new Payment();
            return View(model);
        }
        [HttpPost]
        public IActionResult CharityOrTaxes(Payment incomingData)
        {
            if (incomingData.Number < 500000000 || incomingData.Number > 599999999)
                return View("Error");
            if (incomingData.Amount < 1)
                return View("Error");
            if (incomingData.IDnumber.Length != 11)
                return View("Error");
            if (!IsDigitsOnly(incomingData.IDnumber))
                return View("Error");
            if (incomingData.Amount < 50)
                incomingData.Commission = 0.5m;
            else
                incomingData.Commission = incomingData.Amount * 0.01m;

            incomingData.Amount -= incomingData.Commission;

            SavePaymentsToDb(incomingData);
            return View("Success");
        }


        public IActionResult Finances()
        {
            Payment model = new Payment();
            model.AccountId = "GE";
            return View(model);
        }
        [HttpPost]
        public IActionResult Finances(Payment incomingData)
        {
            if (incomingData.Number < 500000000 || incomingData.Number > 599999999)
                return View("Error");
            if (incomingData.Amount < 1)
                return View("Error");
            if (incomingData.IDnumber.Length != 11)
                return View("Error");
            if (!IsDigitsOnly(incomingData.IDnumber))
                return View("Error");
            if (!IsAccountIdLegit(incomingData.AccountId))
                return View("Error");
            if (incomingData.Amount < 50)
                incomingData.Commission = 0.5m;
            else
                incomingData.Commission = incomingData.Amount * 0.01m;

            incomingData.Amount -= incomingData.Commission;
            incomingData.Service = (Service)2;

            SavePaymentsToDb(incomingData);
            return View("Success");

        }

        private void SavePaymentsToDb(Payment data)
        {
            _db.Payments.Add(data);
            _db.SaveChanges();
        }

        static bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        static bool IsAccountIdLegit(string str) //GE00XX0000000000000000
        {
            if (str.Length != 22)
                return false;
            if (str[0] != 'G' || str[1] != 'E' || (str[4] > '0' && str[4] < '9') || (str[5] > '0' && str[5] < '9'))
                return false;
            for(int i=2;i<=3;i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }
            for (int i = 6; i <str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                    return false;
            }

            return true;
        }
    }
}
