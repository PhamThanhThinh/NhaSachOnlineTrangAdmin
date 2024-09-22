using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NhaSachOnlineTrangAdmin.Models.Domain
{
  public class Book
  {
    public int Id { get; set; }
    public string Title { get; set; }
    
    // mỗi cuốn sách sẽ có mã xuất bản riêng
    [Required]
    public string Isbn { get; set; }
    [Required]
    public int TotalPages { get; set; }
    [Required]
    public int AuthorID { get; set; }
    [Required]
    public int PublisherId {  get; set; }
    [Required]
    public int GenreId { get; set; }

    [NotMapped]
    public string? AuthorName { get; set; }

    [NotMapped]
    public string? PublisherName { get; set; }

    [NotMapped]
    public string? GenreName { get; set; }


    // SelectListItem tương ứng với dropdown list trong winform
    [NotMapped]
    public List<SelectListItem>? AuthorList { get; set; }

    [NotMapped]
    public List<SelectListItem>? PublisherList { get; set; }

    [NotMapped]
    public List<SelectListItem>? GenreList { get; set; }
  }
}
