using System.Text.Json;
using Desktop.Core.Models;
using UM.Models.Files.View;

namespace Desktop.Core.Api;

public class ApiUpdater(List<String> Servers) : ApiBase(Servers)
{
	public async Task<List<VersionView>> GetAllVersions()
    {
        var client = await InitHttpClient();

        var res = await client.GetStringAsync("api/Update/GetVersions");

        var options = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };

        return JsonSerializer.Deserialize<List<VersionView>>(res, options)!;
    }

    public async Task GetUpdate(string build)
    {
        var client = await InitHttpClient();

        var filePath = "updates";

        var res = await client.GetAsync($"api/Update/DownloadUpdate?build={build}");

        if (res.IsSuccessStatusCode)
        {
            Stream fileStream = await res.Content.ReadAsStreamAsync();
            SaveStreamAsFile(filePath, fileStream, $"{build}.zip");
        }
        else
        {
            throw new Exception("Ошибка при получении файла");
        }
    }

    public async Task GetUpdateById(Guid id)
    {
        var client = await InitHttpClient();

        var filePath = "updates";

        try
        {
            var res = await client.GetAsync($"api/Update/DownloadUpdateById?id={id}");

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
            throw new Exception("Ошибка при загрузке обновления");
        }
    }

    public async Task<string> GetLastUpdate()
    {
        var client = await InitHttpClient();

        var res = await client.GetStringAsync("api/Update/GetLastUpdate");

        return res;
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