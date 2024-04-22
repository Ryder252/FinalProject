using Microsoft.AspNetCore.Mvc;

namespace Final_Project.Backend
{
    [ApiController]
    [Route("api/[controller]")]
    public class AudioController : ControllerBase
    {
        private readonly IAudioService _audioService;
        
        public AudioController(IAudioService audioService)
        {
            _audioService = audioService;
        }

        [HttpPost("AddSong")]
        public async Task<IActionResult> AddSong([FromForm] IFormFile file)
        {
            try
            {
                var songId = await _audioService.AddSongAsync(file);
                return Ok(new { SongId = songId });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetSongs")]
        public async Task<IActionResult> GetSongs()
        {
            try
            {
                var songs = await _audioService.GetSongsAsync();
                return Ok(songs);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetSongById/{id}")]
        public async Task<IActionResult> GetSongById(Guid id)
        {
            try
            {
                var song = await _audioService.GetSongByIdAsync(id);
                if(song == null)
                {
                    return NotFound();
                }
                return Ok(song);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("UpdateSong/{id}")]
        public async Task<IActionResult> UpdateSong(Guid id, [FromForm] IFormFile file)
        {
            try
            {
                await _audioService.UpdateSongAsync(id, file);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("DeleteSong/{id}")]
        public async Task<IActionResult> DeleteSong(Guid id)
        {
            try
            {
                await _audioService.DeleteSongAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}