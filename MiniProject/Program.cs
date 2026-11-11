using ClubhouseAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================
// 1. 注册服务 (Dependency Injection)
// ==========================================

// 注册数据库上下文 (EF Core)
builder.Services.AddDbContext<ClubhouseDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 注册控制器
builder.Services.AddControllers();

// 注册 Swagger/OpenAPI 服务 (这是让 Swagger 页面能出来的关键)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// ==========================================
// 2. 配置 HTTP 请求管道 (Middleware)
// ==========================================

// 如果是开发环境，启用 Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();       // 生成 swagger.json 数据
    app.UseSwaggerUI();     // 渲染 Swagger 网页界面
}

// 强制 HTTPS 重定向
app.UseHttpsRedirection();

// 授权中间件 (即使现在没用到，保留着以后加权限用)
app.UseAuthorization();

app.UseDefaultFiles();
app.UseStaticFiles();

// 将请求路由到对应的 Controller
app.MapControllers();

// 启动应用
app.Run();