using System.Text.Json;
using UM.Models.Files.View;

namespace Desktop.Core.Api;

public class ApiUpdater(List<String> servers) : ApiBase(servers)
{
	public async Task<List<VersionView>> GetAllVersions()
    {
        var client = await InitHttpClient();

        var res = await client.GetStringAsync("api/Updates/GetVersions");

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<VersionView>>(res, options)!;
    }

    public async Task DownloadUpdate(Guid id)
    {
        var client = await InitHttpClient();

        var filePath = "updates";

        try
        {
            var res = await client.GetAsync($"api/Updates/DownloadUpdate?id={id}");

            if (res.IsSuccessStatusCode)
            {
                Stream fileStream = await res.Content.ReadAsStreamAsync();
                SaveStreamAsFile(filePath, fileStream, $"{id}.zip");
            }
            else
            {
                throw new Exception("Ошибка при загрузке обновления");
            }
        }
        catch (Exception e)
        {
            // throw new Exception("Ошибка при загрузке обновления");
            throw new Exception(e.Message);
        }
    }

    private void SaveStreamAsFile(string filePath, Stream inputStream, string fileName)
    {
        DirectoryInfo info = new DirectoryInfo(filePath);

        if (!info.Exists)
        {
            info.Create();
        }

        string path = Path.Combine(filePath, fileName);
        using FileStream outputFileStream = new FileStream(path, FileMode.Create);
        inputStream.CopyTo(outputFileStream);
    }
}