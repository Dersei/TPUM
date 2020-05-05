using System;

namespace TPUM.Data.Model
{
    public abstract class IdItem
    {
        public Guid ID { get; }

        protected IdItem()
        {
            ID = Guid.NewGuid();
        }
    }
}