using ETicaretAPI.Application.ViewModels.Products;
using FluentValidation;

namespace ETicaretAPI.Application.Validators.Products
{
    public class CreateProductValidator : AbstractValidator<VM_Product_Create>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Lütfen Ürün Adını Boş Geçmeyiniz")
                .MaximumLength(150)
                .MinimumLength(3)
                    .WithMessage("Ürün Adını 150 ile 3 karakter arasında giriniz");
            RuleFor(p => p.Stock)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Stok bilgisini Boş Geçmeyiniz")
                .Must(s => s >= 0)
                    .WithMessage("Stok bilgisi Negatif olamaz");

            RuleFor(p => p.Price)
                .NotEmpty()
                .NotNull()
                    .WithMessage("Fiyat bilgisini Boş Geçmeyiniz")
                .Must(s => s >= 0)
                    .WithMessage(" Fiyat bilgisi Negatif olamaz");
        }
    }
}
