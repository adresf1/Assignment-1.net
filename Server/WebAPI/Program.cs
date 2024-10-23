using FileRepositories;
using RepositoryContracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repository implementations for dependency injection
builder.Services.AddScoped<IPostRepository, PostFIleReposity>();
builder.Services.AddScoped<IUserRepository, UserFileReposity>();
builder.Services.AddScoped<ICommentRepostory, CommentFileRepository>();

// Add controllers support
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable routing and map the controllers
app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();