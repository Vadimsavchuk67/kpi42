using PostService.Dtos;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PostService.App;

public partial class PostingList : Form
{
    private readonly HttpClient client = new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5050"),
        Timeout = TimeSpan.FromSeconds(10)
    };

    private readonly JsonSerializerOptions options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
    {
        Converters = { new JsonStringEnumConverter() }
    };

    public PostingList()
    {
        InitializeComponent();
    }

    private async Task LoadPostingsAsync()
    {
        btnRefresh.Enabled = false;
        btnDelete.Enabled = false;
        try
        {
            var postings = await client.GetFromJsonAsync<PostingGetDto[]>("postings", options);
            gridPostings.DataSource = postings;
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Не вдалося отримати дані. Перевірте, чи запущено сервер.");
        }
        catch (TaskCanceledException)
        {
            MessageBox.Show("Сервер не відповів вчасно.");
        }
        catch (JsonException)
        {
            MessageBox.Show("Сервер повернув дані в неправильному форматі.");
        }
        finally
        {
            btnRefresh.Enabled = true;
            btnDelete.Enabled = true;
        }
    }

    private async void PostingList_Load(object? sender, EventArgs e)
    {
        await LoadPostingsAsync();
    }

    private async void btnRefresh_Click(object? sender, EventArgs e)
    {
        await LoadPostingsAsync();
    }

    private async void btnDelete_Click(object? sender, EventArgs e)
    {
        if (gridPostings.CurrentRow?.DataBoundItem is not PostingGetDto posting)
        {
            MessageBox.Show("Виберіть відправлення, яке потрібно видалити.");
            return;
        }

        btnDelete.Enabled = false;
        btnRefresh.Enabled = false;
        try
        {
            using var response = await client.DeleteAsync($"postings/{posting.Id}");
            if (!response.IsSuccessStatusCode)
                MessageBox.Show($"Не вдалося видалити відправлення. Статус: {(int)response.StatusCode}.");
        }
        catch (HttpRequestException)
        {
            MessageBox.Show("Не вдалося з’єднатися із сервером.");
        }
        catch (TaskCanceledException)
        {
            MessageBox.Show("Сервер не відповів вчасно.");
        }
        finally
        {
            await LoadPostingsAsync();
        }
    }
}
