using System;
using System.Collections.Generic;
using System.Text;
using Thoughtworks.BusinessLogic.Interface;

namespace Thoughtworks.Model
{
    public class Account 
    {
        public int AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public List<Transcation> TranscationList { get; set; }
    }
}
