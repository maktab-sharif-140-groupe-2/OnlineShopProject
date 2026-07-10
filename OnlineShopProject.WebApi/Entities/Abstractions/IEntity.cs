namespace OnlineShopProject.WebApi.Entities.Abstractions;

public interface IEntity
{
    /// <summary>
    /// زمان ساخت موجودیت در سیستم 
    /// </summary>
    public DateTime CreatedAt { get; }

    /// <summary>
    /// زمان تغییر موجودیت در سیستم 
    /// </summary>
    public DateTime? ModifiedAt { get; }

    /// <summary>
    /// زمان حذف نرم موجودیت از سیستم 
    /// </summary>
    public DateTime? DeletedAt { get; }

    /// <summary>
    /// ایا موجودیت حذف نرم شده یا نه
    /// </summary>
    public bool IsDeleted { get; }

    /// <summary>
    /// متد تغییر پراپرتی های مربوط به حذف نرم موجودیت
    /// </summary>
    public void SoftDelete();

    /// <summary>
    /// متد تغییر پراپرتی مربوط به اپدیت موجودیت
    /// </summary>
    public void Update();
}