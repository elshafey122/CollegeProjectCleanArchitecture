namespace SchoolProject.Core.Wrappings
{
    public class PaginatedResult<T>
    {
        public List<T> Data { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public object Meta { get; set; }
        public bool Succeed { get; set; }
        public List<string> Messages { get; set; }
        public PaginatedResult(bool succeed, List<T> data = default, List<string> messages = null, int count = 0, int pagenumber = 0, int pagesize = 10)
        {
            Succeed = succeed;
            Data = data;
            Messages = messages;
            TotalCount = count;
            CurrentPage = pagenumber;
            PageSize = pagesize;
            TotalPages = (int)Math.Ceiling(count / (double)pagesize);
        }
        public static PaginatedResult<T> Success(List<T> Data, int count, int pagenumber, int pagesize)
        {
            return new(true, Data, null, count, pagenumber, pagesize);
        }
        public bool HasPreviousPages()
        {
            return CurrentPage > 1;
        }
        public bool HasNextPages()
        {
            return CurrentPage < TotalPages;
        }
    }
}
