using MultiShop.DtoLayer.CommentDtos;

namespace MultiShop.Web.Services.CommentServices
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;

        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateCommentAsync(CreateCommentDto createCommentDto)
        {
            await _httpClient.PostAsJsonAsync("api/comments", createCommentDto);
        }

        public async Task DeleteCommentAsync(string id)
        {
            await _httpClient.DeleteAsync($"api/comments/{id}");
        }

        public async Task<UpdateCommentDto> GetByIdCommentAsync(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/comments/{id}");
                if (!responseMessage.IsSuccessStatusCode) return new UpdateCommentDto();
                return await responseMessage.Content.ReadFromJsonAsync<UpdateCommentDto>() ?? new UpdateCommentDto();
            }
            catch
            {
                return new UpdateCommentDto();
            }
        }

        public async Task<List<ResultCommentDto>> GetAllCommentAsync()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/comments");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultCommentDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>() ?? new List<ResultCommentDto>();
            }
            catch
            {
                return new List<ResultCommentDto>();
            }
        }

        public async Task UpdateCommentAsync(UpdateCommentDto updateCommentDto)
        {
            await _httpClient.PutAsJsonAsync($"api/comments/{updateCommentDto.UserCommentId}", updateCommentDto);
        }

        public async Task<List<ResultCommentDto>> CommentListByProductId(string id)
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync($"api/comments/CommentListByProductId/{id}");
                if (!responseMessage.IsSuccessStatusCode) return new List<ResultCommentDto>();
                return await responseMessage.Content.ReadFromJsonAsync<List<ResultCommentDto>>() ?? new List<ResultCommentDto>();
            }
            catch
            {
                return new List<ResultCommentDto>();
            }
        }

        public async Task<int> GetTotalCommentCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/comments/GetTotalCommentCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetActiveCommentCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/comments/GetActiveCommentCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }

        public async Task<int> GetPAssiveCommentCount()
        {
            try
            {
                var responseMessage = await _httpClient.GetAsync("api/comments/GetPassiveCommentCount");
                if (!responseMessage.IsSuccessStatusCode) return 0;
                return await responseMessage.Content.ReadFromJsonAsync<int>();
            }
            catch
            {
                return 0;
            }
        }
    }
}
