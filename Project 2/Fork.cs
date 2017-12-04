using System;
namespace MultiThreading2
{
    public class Fork : ICloneable
    {
        public bool IsInUse { get; set; }

        public Fork()
        {
            IsInUse = false;
        }

        public object Clone() 
        {
            Fork res = new Fork();
            res.IsInUse = IsInUse;
            return res;
        }
    }
}
