namespace sentirsebien_backend.API.Dtos
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
    }
}
