using System.ComponentModel.DataAnnotations;

namespace GameStore
{
    public record GameDtoV1(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        DateTime ReleaseDate,
        string imageUri
    );

    public record GameDtoV2(
        int Id,
        string Name,
        string Genre,
        decimal Price,
        decimal RetailPrice,
        DateTime ReleaseDate,
        string imageUri
    );

    public record CreateGameDto(
       [Required][StringLength(50)] string Name,
       [Required][StringLength(20)] string Genre,
       [Range(1, 100)] decimal Price,
        DateTime ReleaseDate,
       [Url][StringLength(100)] string ImageUri);

    public record UpdateGameDto(
       [Required][StringLength(50)] string Name,
       [Required][StringLength(20)] string Genre,
       [Range(1, 100)] decimal Price,
        DateTime ReleaseDate,
       [Url][StringLength(100)] string ImageUri);

}
