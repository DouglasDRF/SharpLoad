namespace SharpLoad.Core.Models
{
    public abstract class BaseModel
    { 
        public int Id { get; }
       
        protected BaseModel(int id)
        {
            Id = id;
        }
        protected BaseModel() { }
    }
}
