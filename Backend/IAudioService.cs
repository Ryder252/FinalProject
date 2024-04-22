namespace Final_Project.Backend
{
    public interface IAudioService
    {
        Task<Guid> AddSongAsync(IFormFile file);
        Task<IEnumerable<Song>> GetSongsAsync();
        Task<Song> GetSongByIdAsync(Guid id);
        Task UpdateSongAsync(Guid id, IFormFile file);
        Task DeleteSongAsync(Guid id);
    }

    public class Song
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}
