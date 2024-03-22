namespace Blog.UI.Models
{
    public class PostDetailViewModel<T1, T2, T3>
    {
        public T1 PostDetail { get; set; }
        public T2 Comments { get; set; }
        public T3 PostTags { get; set; }
    }
}
