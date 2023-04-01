using AutoMapper;

namespace ICTTaxApi.Tools
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            //CreateMap<AuthorCreationDTO, Author>();
            //CreateMap<Author, AuthorDTO>();
            //CreateMap<Author, AuthorWithBooksDTO >()
            //    .ForMember(authorDTO => authorDTO.Books, options => options.MapFrom(MapAuthorDTOBooks));
            //CreateMap<BookCreationDTO, Book>()
            //    .ForMember(book => book.BooksAuthors, options => options.MapFrom(MapBooksAuthors));
            //CreateMap<BookPatchDTO, Book>().ReverseMap();
            //CreateMap<Book, BookDTO>().ReverseMap();
            //CreateMap<Book, BookWithAuthorsDTO>()
            //    .ForMember(bookDTO => bookDTO.Authors, options => options.MapFrom(MapBookDTOAuthors));
            //CreateMap<CommentCreationDTO, Comment>();
            //CreateMap<Comment, CommentDTO>();
        }

        


        //private List<BookAuthor> MapBooksAuthors(BookCreationDTO bookDTO, Book book)
        //{
        //    var result = new List<BookAuthor>();

        //    if(bookDTO.AutoresIds == null) { return result; }

        //    foreach(var authorId in bookDTO.AutoresIds)
        //    {
        //        result.Add(new BookAuthor() { AuthorId = authorId });
        //    }
        //    return result;
        //}

        //private List<AuthorDTO> MapBookDTOAuthors(Book book, BookDTO bookDTO)
        //{
        //    var result = new List<AuthorDTO>();

        //    if (book.BooksAuthors == null) { return result; }

        //    foreach (var authorBook in book.BooksAuthors)
        //    {
        //        result.Add(new AuthorDTO() {
        //            Id = authorBook.AuthorId,
        //            Name = authorBook.Author.Name
        //        });
        //    }
        //    return result;
        //}

        //private List<BookDTO> MapAuthorDTOBooks(Author author, AuthorDTO authorDTO)
        //{
        //    var result = new List<BookDTO>();

        //    if (author.BooksAuthors == null) { return result; }

        //    foreach (var authorBook in author.BooksAuthors)
        //    {
        //        result.Add(new BookDTO()
        //        {
        //            Id = authorBook.BookId,
        //            Title = authorBook.Book.Title
        //        });
        //    }
        //    return result;
        //}

    }
}
