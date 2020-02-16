using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.BusinessLogic.Interface;

namespace Thoughtworks.Model
{
    public class Transcation
    {
        public int TranscationId { get; set; } = 0;
        public double Deposit { get; set; } = 0;
        private volatile Object _withdrawl = 0;
        public double Withdrawl
        {
            get { lock (_withdrawl) { return Convert.ToDouble(_withdrawl); } }
            set { lock (_withdrawl) { _withdrawl = value; } }
        }
    }
}
