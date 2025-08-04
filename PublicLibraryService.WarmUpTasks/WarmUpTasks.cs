namespace PublicLibraryService.WarmUpTasks
{
    public static class WarmUpTasks
    {
        /// <summary>
        /// Check if a Book ID is a Power of Two
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public static bool IsPowerOfTwo(int bookId)
        {
            return int.IsPow2(bookId);
        }

        /// <summary>
        /// Reverse a Book Title
        /// </summary>
        /// <param name="bookTitle"></param>
        /// <returns></returns>
        public static string ReverseBookTitle(string bookTitle)
        {
            if (!string.IsNullOrEmpty(bookTitle))
            {
                return new string(bookTitle.Reverse().ToArray());
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// Generate Book Title Replicas
        /// </summary>
        /// <param name="title"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string RepeatBookTitle(string title, int count)
        {
            if (string.IsNullOrEmpty(title) || count <= 0)
                return string.Empty;

            var result = "";
            for (var i = 0; i < count; i++)
            {
                result += title;
            }

            return result;
        }
        /// <summary>
        /// List Odd-Numbered Book IDs
        /// </summary>
        /// <returns></returns>
        public static List<int> PrintOddBookIds()
        {
            var res = new List<int>();
            for (var i = 1; i < 100; i += 2)
            {
                res.Add(i);
            }
            return res;
        }

    }
}
