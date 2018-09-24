namespace ToDoApi.Models
{

    public interface IIdentifiable<T>
    {
        T Id { get; set; }
    }
}
