using Calculator.Enums;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Calculator.Models
{
    public class Computation
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Ip адрес
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Тип операции
        /// </summary>
        public OperationType Operation { get; set; }

        /// <summary>
        /// Результат
        /// </summary>
        public double Result { get; set; }
    }

    public class CalculatorContext : DbContext
    {
        public DbSet<Computation> Computations { get; set; }
    }
}