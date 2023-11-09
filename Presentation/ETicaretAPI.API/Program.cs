using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));//burdaký belirlenen adresden istek gelirse kabul et. diðerlerinden kabul etme demektir.

builder.Services.AddControllers(option=>option.Filters.Add<ValidationFilter>()/*Buraya Kendimize Ait Filter sistemini Eklemiþ olduk*/)
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>() /*Burada Fluent Validatora config vasýtasý ile Application katmanýndan  (CreatProductValidator yerine) her hangi bir class ve ya interface i vermekle Application katmanýný buraya tanýttýðýmýz için artýk 1000 tane de Validator yazarsak ta, kendisi tanýyacaktýr. Yani Bir kere tanýtmamýz yetiyor. Yani kodumuzu bu þekilde yazmamýz, tanýtmamýz anlamýna geliyor. Bilmem anlata bildim mi?*/)
    .ConfigureApiBehaviorOptions(option=>option.SuppressModelStateInvalidFilter=true /* Burda da þöyle bi durum var. ASPNETCore un özeliiði þu ki, validatora ters bi durum rastlarsa  API ye yansýtmadan direk Cliente geri gönderiyor. Biz ise þimdi bu ayar ile onu bastýrdýk. yani demiþ olduk ki, sen karýþma  ters bi durumu ben yöneteceðim (supppress demek üzerinden basmak.) */);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors(); //CORS politikasýný kullandim
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
