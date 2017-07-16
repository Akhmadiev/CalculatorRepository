namespace Calculator.Controllers
{
    using Enums;
    using Models;
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Sockets;
    using System.Web.Mvc;

    public class HomeController : Controller
    {
        ComputationContext db = new ComputationContext();

        public ActionResult Index(double? firstNumber, double? secondNumber, string expression)
        {
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
                            return this.View();
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
                        return this.View();
                }

                var host = Dns.GetHostEntry(Dns.GetHostName());
                var ip = host.AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork)?.ToString();

                var computation = new Computation
                {
                    Date = DateTime.Now,
                    Ip = ip,
                    Operation = type,
                    Result = result
                };

                db.Calculating.Add(computation);
                db.SaveChanges();


                this.ViewBag.firstNumber = firstNumber.ToString();
                this.ViewBag.secondNumber = secondNumber.ToString();
                this.ViewBag.expression = expression;
                this.ViewBag.Result = result;
            }

            return this.View();
        }
    }
}
