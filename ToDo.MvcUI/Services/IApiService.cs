namespace ToDo.MvcUI.Services
{
    public interface IApiService
    {
        Task<T?> GetAsync<T>(string endpoint);
        Task<T?> PostAsync<T>(string endpoint, object data);
        Task<T?> PutAsync<T>(string endpoint, object data);
        Task<bool> DeleteAsync(string endpoint);
        Task<string?> LoginAsync(string endpoint, object data);
        void SetToken(string token);
        string? GetToken();
    }
}
