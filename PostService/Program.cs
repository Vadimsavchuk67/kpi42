using PostService.DataAccess;
using PostService.Dtos;
using PostService.Mappings;
using PostService.BusinessLogic;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IPostingService, PostingService>();
builder.Services.AddScoped<IPostingRepository, PostingRepository>();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var repository = (PostingRepository)scope.ServiceProvider.GetRequiredService<IPostingRepository>();
    repository.CreateDb();
}

app.MapGet("/", () => "Це API поштового клієнта.");

app.MapGet("/postings", (IPostingService service) =>
{
    var postings = service.GetAll();
    return Results.Ok(postings.Select(PostingMapper.ToPostingGetDto).ToList());
});

app.MapGet("/postings/{id:int}", (int id, IPostingService service) =>
{
    var posting = service.Find(id);
    if (posting == null)
        return Results.NotFound();

    return Results.Ok(PostingMapper.ToPostingGetDto(posting));
});

app.MapPost("/postings", (PostingPostDto dto, IPostingService service) =>
{
    var posting = PostingMapper.ToPosting(dto);
    var saved = service.Create(posting);
    var result = PostingMapper.ToPostingGetDto(saved);
    return Results.Created($"/postings/{result.Id}", result);
});

app.MapPut("/postings/{id:int}", (int id, PostingPutDto dto, IPostingService service) =>
{
    if (id != dto.Id)
        return Results.BadRequest();

    var posting = PostingMapper.ToPosting(dto);
    var updated = service.Update(posting);
    if (updated == null)
        return Results.NotFound();

    return Results.Ok(PostingMapper.ToPostingGetDto(updated));
});

app.MapDelete("/postings/{id:int}", (int id, IPostingService service) =>
{
    var result = service.Delete(id);
    if (result == 0)
        return Results.NotFound();

    return Results.NoContent();
});

app.Run();
