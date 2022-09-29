namespace RStore.Api.Data;

public class Author
{
    public int Id { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Bio { get; set; }
    public virtual IList<Book> Books { get; set; }
}
