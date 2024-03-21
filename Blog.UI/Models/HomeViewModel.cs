namespace Blog.UI.Models
{
    public class HomeViewModel<T1, T2>
    {
        public T1 Posts { get; set; }
        public T2 Pagination { get; set; }
    }
}
