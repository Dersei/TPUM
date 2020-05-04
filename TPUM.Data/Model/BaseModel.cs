using System;

namespace TPUM.Data.Model
{
    public class BaseModel
    {
        public Guid ID { get; set; }

        public BaseModel()
        {
            ID = Guid.NewGuid();
        }
    }
}