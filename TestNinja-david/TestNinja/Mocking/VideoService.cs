using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        private readonly IFileReader _reader;
        private IVideoRepository _repository;

        public VideoService(IFileReader reader, IVideoRepository repository)
        {
            _reader = reader;
            _repository = repository;
        }
        public string ReadVideoTitle(string path)
        {
            var str = _reader.FileRead(path);
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }



        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = _repository.Videos().Where(v => v.IsProcessed == false);

            foreach (var v in videos)
                videoIds.Add(v.Id);

            return String.Join(",", videoIds);
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }

    public interface IVideoRepository
    {
        IEnumerable<Video> Videos();
    }
    public class DbVideoContext : IVideoRepository
    {
        public IEnumerable<Video> Videos()
        {
            using (var context = new VideoContext())
            {
                return context.Videos;
            };
        }
    }
}