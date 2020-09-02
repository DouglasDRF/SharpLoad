using System;

namespace SharpLoad.Core.Models
{
    public abstract class BaseModel
    { 
        public int Id { get; private set; }
       
        protected BaseModel(int id)
        {
            Id = id;
        }
        protected BaseModel()
        {

        }
    }
}
