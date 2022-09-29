using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APITestingTemplate.Tests.BookCategory;

namespace APITestingTemplate.Helpers
{
    public static class Resources
    {
        // Book
        public static string GetBookById = "Book";

        public static string AddBook = "Book/Add";

        public static string DeleteBook = "Book/";

        public static string GetAllBooks = "Book/GetAll";

        public static string UpdateBook = "Book/Update";

        //BookCategory

        public static string GetAllBookCategories = "BookCategory/GetAll";

        public static string AddABookCategory = "/BookCategory/Add";

        public static string EditABookCategory = "/BookCategory/Update";

        public static string DeleteCategory = "BookCategory/";

        private const string GetAllBooksOfSpecificCategoryFormat = "BookCategory/{0}/Books";

        public static string GetAllBooksOfSpecificCategory(string categoryId) => string.Format(
            GetAllBooksOfSpecificCategoryFormat, categoryId);
    };

    
}
