namespace Calculator.Controllers
{
    using Enums;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        public ActionResult Index(double? firstNumber, double? secondNumber, string expression)
        {
            var computations = new List<Computation>();
            using (var db = new CalculatorContext())
            {
                computations.AddRange(db.Computations);
            }
            if (firstNumber.HasValue && secondNumber.HasValue)
            {
                OperationType type;
                double result;
                switch (expression)
                {
                    case "*":
                        result = firstNumber.Value * secondNumber.Value;
                        type = OperationType.Multiplication;
                        break;
                    case "/":
                        if (secondNumber.Value == 0)
                        {
                            this.ViewBag.Result = "Error";
                            return this.View(computations);
                        }

                        type = OperationType.Divide;
                        result = firstNumber.Value / secondNumber.Value;
                        break;
                    case "+":
                        type = OperationType.Addition;
                        result = firstNumber.Value + secondNumber.Value;
                        break;
                    case "-":
                        type = OperationType.Divison;
                        result = firstNumber.Value - secondNumber.Value;
                        break;
                    default:
                        this.ViewBag.Result = "Error";
                        return this.View(computations);
                }

                var host = Dns.GetHostEntry(Dns.GetHostName());
                var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();

                using (var db = new CalculatorContext())
                {
                    var computation = new Computation
                    {
                        Date = DateTime.Now,
                        Ip = ip,
                        Operation = type,
                        Result = result
                    };

                    db.Computations.Add(computation);
                    db.SaveChanges();
                }

                return RedirectToAction("Index");
            }

            return this.View(computations);
        }
    }
}
