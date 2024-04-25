namespace Final_Project.Backend
{
    public class AudioService : IAudioService
    {
        private readonly string _filePath;
        private readonly List<Song> playList = new();

        public AudioService(IConfiguration config)
        {
            _filePath = config.GetValue<string>("FileStorage:DirectoryPath");
        }

        public async Task<Guid> AddSongAsync(IFormFile file)
        {
            ValidateFile(file);

            var songId = Guid.NewGuid();
            var fileName = await SaveFileAsync(file, songId);

            playList.Add(new Song 
            { 
              Id = songId, 
              Name = Path.GetFileNameWithoutExtension(fileName),
              FilePath = fileName
            });

            return songId;
        }

        public async Task<IEnumerable<Song>> GetSongsAsync()
        {
            return await Task.FromResult<IEnumerable<Song>>(playList);
        }

        public async Task<Song> GetSongByIdAsync(Guid id)
        {
            var song = playList.FirstOrDefault(s => s.Id == id);
            return await Task.FromResult(song);
        }

        public async Task UpdateSongAsync(Guid id, IFormFile file)
        {
            var song = await GetSongByIdAsync(id);

            if (song != null)
            {
                var filePath = Path.Combine(_filePath, $"{id}.mp3");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                using var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream);

                song.Name = Path.GetFileNameWithoutExtension(file.Name);
                song.FilePath = filePath;

                UpdatePlaylist(song);
            }
        }


        public async Task DeleteSongAsync(Guid id)
        {
            var song = await GetSongByIdAsync(id);
            if (song != null)
            {
                var filePath = Path.Combine(_filePath, $"{id}.mp3");
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                playList.Remove(song);
            }
        }

        private void ValidateFile(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                throw new ArgumentNullException(nameof(file));
            }
        }

        private async Task<string> SaveFileAsync(IFormFile file, Guid songId)
        {
            var fileName = $"{songId}.mp3";
            var filePath = Path.Combine(_filePath, fileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return file.FileName;
        }

        private void UpdatePlaylist(Song song)
        {
            var index = playList.FindIndex(song => song.Id == song.Id);

            if (index != -1)
                playList[index] = song;
        }
    }
}