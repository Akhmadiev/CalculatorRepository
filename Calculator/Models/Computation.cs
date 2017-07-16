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

    public class ComputationContext : DbContext
    {
        public DbSet<Computation> Calculating { get; set; }
    }
}