﻿namespace RStore.Api.Dto.Book;

public class BookReadOnlyDto : BaseDto
{
    public string Title { get; set; }
    public string Image { get; set; }
    public double Price { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
}
