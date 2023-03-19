using Confluent.Kafka;
using CQRS.Core.Consumers;
using CQRS.Core.Infraestructure;
using Microsoft.EntityFrameworkCore;
using Post.Cmd.Domain.Entities;
using Post.Cmd.Domain.Repositories;
using Post.Cmd.Infrastructure.DataAccess;
using Post.Query.Api.Queries;
using Post.Query.Infrastructure.Consumers;
using Post.Query.Infrastructure.Dispatchers;
using Post.Query.Infrastructure.Handlers;
using Post.Query.Infrastructure.Repositories;
using EventHandler = Post.Query.Infrastructure.Handlers.EventHandler;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Action<DbContextOptionsBuilder> configureDbContext = options =>
{
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
};
builder.Services.AddDbContext<DatabaseContext>(configureDbContext);
builder.Services.AddSingleton<DatabaseContextFactory>(new DatabaseContextFactory(configureDbContext));

var dataContext = builder.Services.BuildServiceProvider().GetRequiredService<DatabaseContext>();
dataContext.Database.EnsureCreated();

builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IEventHandler, EventHandler>();
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddHostedService<ConsumerHostedService>();
builder.Services.AddScoped<IQueryHandler, QueryHandler>();

var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService<IQueryHandler>();
var dispatcher = new QueryDispatcher();
dispatcher.RegisterHandler<FindAllPostsQuery>(queryHandler.HandlerASync);
dispatcher.RegisterHandler<FindPostByIdQuery>(queryHandler.HandlerASync);
dispatcher.RegisterHandler<FindPostByAuthorQuery>(queryHandler.HandlerASync);
dispatcher.RegisterHandler<FindPostWithCommentsQuery>(queryHandler.HandlerASync);
dispatcher.RegisterHandler<FindPostWithLikesQuery>(queryHandler.HandlerASync);

builder.Services.AddSingleton<IQueryDispatcher<PostEntity>>(_ => dispatcher);

builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
