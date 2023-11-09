using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrastructure;
using ETicaretAPI.Infrastructure.Filters;
using ETicaretAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));//burdak� belirlenen adresden istek gelirse kabul et. di�erlerinden kabul etme demektir.

builder.Services.AddControllers(option=>option.Filters.Add<ValidationFilter>()/*Buraya Kendimize Ait Filter sistemini Eklemi� olduk*/)
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>() /*Burada Fluent Validatora config vas�tas� ile Application katman�ndan  (CreatProductValidator yerine) her hangi bir class ve ya interface i vermekle Application katman�n� buraya tan�tt���m�z i�in art�k 1000 tane de Validator yazarsak ta, kendisi tan�yacakt�r. Yani Bir kere tan�tmam�z yetiyor. Yani kodumuzu bu �ekilde yazmam�z, tan�tmam�z anlam�na geliyor. Bilmem anlata bildim mi?*/)
    .ConfigureApiBehaviorOptions(option=>option.SuppressModelStateInvalidFilter=true /* Burda da ��yle bi durum var. ASPNETCore un �zelii�i �u ki, validatora ters bi durum rastlarsa  API ye yans�tmadan direk Cliente geri g�nderiyor. Biz ise �imdi bu ayar ile onu bast�rd�k. yani demi� olduk ki, sen kar��ma  ters bi durumu ben y�netece�im (supppress demek �zerinden basmak.) */);

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
app.UseCors(); //CORS politikas�n� kullandim
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
