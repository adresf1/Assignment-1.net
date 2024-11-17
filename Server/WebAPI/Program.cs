using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);


// Tilføjer services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register Repository
builder.Services.AddScoped<IPostRepository, PostFIleReposity>();
builder.Services.AddScoped<IUserRepository, UserFileReposity>();
builder.Services.AddScoped<ICommentRepostory, CommentFileRepository>();

// tilføjer controllersne 
builder.Services.AddControllers();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

