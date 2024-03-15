namespace Blog.UI.Models
{
    public class PostCommentsView<T1, T2>
    {
        public T1 PostDetail { get; set; }
        public T2 Comments { get; set; }
    }
}
